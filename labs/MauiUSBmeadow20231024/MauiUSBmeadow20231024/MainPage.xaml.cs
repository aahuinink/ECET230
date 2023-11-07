using Microsoft.Maui.Controls.Compatibility;
using System.IO.Ports;
using System.Runtime.ExceptionServices;
using System.Text;

namespace MauiSolar;

public partial class MainPage : ContentPage
{
    // global class variables
    private bool bPortOpen=false;           // is a serial port open?
    private bool firstPack = false;         // is this the first packet recieved since opening the port?
    private string recString;               // the string recieved from the meadow board
    private int currentPackNumber = 0;      // the current packet number
    private int packetCount = 0;            // the number of packets recieved
    private int rolloverCount = 0;          // the number of times the packet count has rolled over that the application sees
    private int totalPackets = 0;
    SerialPort serialPort = new SerialPort();   // serial port for connecting to the meadow board

    ErrorChecking errorChecking = new ErrorChecking();

    public MainPage()
	{
		InitializeComponent();
        string[] ports = SerialPort.GetPortNames();
        pkrComPort.ItemsSource = ports;
        pkrComPort.SelectedIndex = ports.Length;
        
        Loaded += MainPage_Loaded;
	}

    /// <summary>
    /// sets up serial port parameters upon page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainPage_Loaded(object sender, EventArgs e)
    {
        serialPort.BaudRate = 115200;
        serialPort.ReceivedBytesThreshold = 1;
        serialPort.DataReceived += SerialPort_DataReceived;
    }

    /// <summary>
    /// Event handler for when data is recieved from the meadow board.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        recString = serialPort.ReadLine();                      // reads received string from meadow board
       
        MainThread.BeginInvokeOnMainThread(MyMainThreadCode);   // invokes main code on main thread
    }

    /// <summary>
    /// Main code to be invoked by main thread
    /// </summary>
    private void MyMainThreadCode()
    {
        //local variables
        string parsedData;
        Packet packet = new Packet();
        List<PacketError> errors = new List<PacketError>();

        // display recieved packet string
        // also toggle recieved packet history
        if (checkboxHistory.IsChecked)
        {
            labelRXdata.Text = recString + labelRXdata.Text;
        }
        else
        {
            labelRXdata.Text = recString;
        }

        // try parsing the recieved string into the packet object
        errors = packet.TryRXParse(recString);

        // handle errors
        if (errors.Count > 0)       // if there are errors
        {
            foreach (PacketError error in errors)
            {
                errorChecking.Handle(error);
            }
            return; // exit since packet had errors
        }

        // if the packet passes error checking

        // if it's the first packet since the port has been opened
        if(firstPack)
        {
            firstPack = false;
            currentPackNumber = packet.Number - 1; // to reconcile the packets recieved and the lost packet numbers
        }
        // parse the data into the parsedData string
        parsedData = $"" +
            $"{packet.Length,-16}" +
            $"{packet.Header,-16}" +
            $"{packet.Number,-16}";

        for (int i = 0; i < 7; i++)
        {
            parsedData += $"{packet.Message.Substring(i*4,4),-16}";
        }
        parsedData += $"{packet.Checksum}\r\n";

        // Toggle parsed history
        if (checkboxParsedHistory.IsChecked)
        {
            labelParsedData.Text = parsedData + labelParsedData.Text;
        }
        else
        {
            labelParsedData.Text = parsedData;
        }

        // check for lost packets and calculate total packets sent
        int diff = packet.Number - (currentPackNumber%1000);

        // lost packets, no rollover

        if (diff > 1)
        {
            errorChecking.LostPacketCount += diff;
            currentPackNumber = packet.Number;
        } 
        // lost packets and/or rollover
        else if (diff < 1)
        {
            rolloverCount++;
            errorChecking.LostPacketCount += (1000 - currentPackNumber) + packet.Number;
            packetCount += (1000 - currentPackNumber) + packet.Number;
            currentPackNumber = packet.Number;
        }
        // no lost packets or rollover
        else
        {
            currentPackNumber = packet.Number;
            packetCount++;
        }

        //update UI with error/packet info
        ecRecieved.Text = (packetCount + 1000*rolloverCount).ToString();
        ecLost.Text = errorChecking.LostPacketCount.ToString();
        ecChecksum.Text = errorChecking.ChkSumErrors.ToString();
        ecHeader.Text = errorChecking.HeaderErrors.ToString();
        ecLength.Text = errorChecking.LengthErrors.ToString();
        ecNumber.Text = errorChecking.NumberErrors.ToString();
    }

    private void btnOpenClose_Clicked(object sender, EventArgs e)
    {
        if (!bPortOpen)
        {
            serialPort.PortName = pkrComPort.SelectedItem.ToString();
            serialPort.Open();
            btnOpenClose.Text = "Close";
            bPortOpen = true;
            firstPack = true;
            return;
        }
        serialPort.Close();
        btnOpenClose.Text = "Open";
        bPortOpen= false;
        return;
    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {
        string[] ports = SerialPort.GetPortNames();
        pkrComPort.ItemsSource = ports;
        pkrComPort.SelectedIndex = ports.Length;
    }

    private void btnSend_Clicked(object sender, EventArgs e)
    {
        try
        {
            Packet outPacket = new Packet();            // create a packet object to send the user data in
            string messageOut = entrySend.Text;         // get user input        
            outPacket.Send(messageOut, serialPort);     // send the user data out of the serial port
        }
        catch(Exception ex)
        {
            DisplayAlert("Alert!", ex.ToString(), "OK");
        }
    }
}

