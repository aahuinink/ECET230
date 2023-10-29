using System.IO.Ports;
using System.Text;

namespace MauiUSBmeadow20231024;

public partial class MainPage : ContentPage
{
    // global class variables
    private bool bPortOpen=false;
    private string newPacket = "";
    private int oldPacketNumber = -1;
    private int newPacketNumber = 0;
    private int recPacketCount = 0;
    private int lostPacketCount = 0;
    private int packetCountRollovers = 0;
    private int chkSumErrors = 0;
    private int lengthErrors = 0;

    SerialPort serialPort = new SerialPort();

	public MainPage()
	{
		InitializeComponent();
        string[] ports = SerialPort.GetPortNames();
        pkrComPort.ItemsSource = ports;
        pkrComPort.SelectedIndex = ports.Length;
        Loaded += MainPage_Loaded;
	}

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        serialPort.BaudRate = 115200;
        serialPort.ReceivedBytesThreshold = 1;
        serialPort.DataReceived += SerialPort_DataReceived;
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        newPacket = serialPort.ReadLine();
        MainThread.BeginInvokeOnMainThread(MyMainThreadCode);

    }

    /// <summary>
    /// Main code to be invoked by main thread
    /// </summary>
    private void MyMainThreadCode()
    {
        //local variables
        int calChkSum = 0;
        string parsedData;

        // Toogle Packet history
        if (checkboxHistory.IsChecked)
        {
            labelRXdata.Text = newPacket + labelRXdata.Text;
        } else
        {
            labelRXdata.Text = newPacket;
        }

        // if the packet length is correct
        if(newPacket.Length == 38)
        {
            // if the packet header is correct
            if (newPacket.Substring(0, 3) == "###")
            {
                // parse the data into the parsedData string
                parsedData = $"" +
                    $"{newPacket.Length,-16}" +
                    $"{newPacket.Substring(0,3),-16}" +
                    $"{newPacket.Substring(3,3),-16}";
                for (int i = 6; i < 34; i+=4)
                {
                    parsedData += $"{newPacket.Substring(i,4),-16}";
                }
                parsedData += $"{newPacket.Substring(34, 3)}\r\n";

                // Toggle parsed history
                if (checkboxParsedHistory.IsChecked)
                {
                    labelParsedData.Text = parsedData + labelParsedData.Text;
                }
                else
                {
                    labelParsedData.Text = parsedData;
                }

                

            } else
            {
                corruptPacketCount++;
            }
        }
        // if there is data missing from/added to the packetcle
        else
        {
            corruptPacketCount++;
        }

        // update the packet error data
        ecCorrupted.Text = corruptPacketCount.ToString();
        ecLost.Text = lostPacketCount.ToString();
    }

    private void btnOpenClose_Clicked(object sender, EventArgs e)
    {
        if (!bPortOpen)
        {
            serialPort.PortName = pkrComPort.SelectedItem.ToString();
            serialPort.Open();
            btnOpenClose.Text = "Close";
            bPortOpen = true;
            return;
        }
        serialPort.Close();
        btnOpenClose.Text = "Open";
        bPortOpen= false;
        return;
    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {

    }

    private void btnSend_Clicked(object sender, EventArgs e)
    {
        try
        {
            string messageOut = entrySend.Text;
            messageOut += "\r\n";
            byte[] messageBytes = Encoding.UTF8.GetBytes(messageOut);
            serialPort.Write(messageBytes, 0, messageBytes.Length);
        }
        catch(Exception ex)
        {
            DisplayAlert("Alert!", ex.ToString(), "OK");
        }
        
    }
}

