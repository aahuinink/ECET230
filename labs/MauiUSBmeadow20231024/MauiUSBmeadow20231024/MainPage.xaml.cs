using System.IO.Ports;
using System.Text;

namespace MauiUSBmeadow20231024;

public partial class MainPage : ContentPage
{
    // global class variables
    private bool bPortOpen=false;

    SerialPort serialPort = new SerialPort();
    Packet recPacket = new Packet(headerLength: 3, expectedPacketLength: 38);
    ErrorChecking errorChecking = new ErrorChecking(expectedPacketLength: 38);

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
        recPacket.Contents = serialPort.ReadLine();
        MainThread.BeginInvokeOnMainThread(MyMainThreadCode);

    }

    /// <summary>
    /// Main code to be invoked by main thread
    /// </summary>
    private void MyMainThreadCode()
    {
        //local variables
        string parsedData;

        // Do Error Checking:
        // if the packet length is correct
        if(recPacket.PacketLength != recPacket.ExpectedPacketLength)
        {
            errorChecking.LengthErrors++;
            return;
        }

        // incorrect header
        if (recPacket.Header != "###")
        {
            errorChecking.HeaderErrors++;
            return;
        }

        // checksum errors
        if (recPacket.CalChecksum != recPacket.RecChecksum)
        {
            errorChecking.ChkSumErrors++;
            return;
        }

        // if the packet passes error checking
        // parse the data into the parsedData string
        parsedData = $"" +
            $"{recPacket.PacketLength,-16}" +
            $"{recPacket.Header,-16}" +
            $"{recPacket.PacketNumber,-16}";
        for (int i = 0; i < recPacket.Contents.Length; i+=4)
        {
            parsedData += $"{recPacket.Contents.Substring(i,4),-16}";
        }
        parsedData += $"{recPacket.RecChecksum}\r\n";

        // Toggle parsed history
        if (checkboxParsedHistory.IsChecked)
        {
            labelParsedData.Text = parsedData + labelParsedData.Text;
        }
        else
        {
            labelParsedData.Text = parsedData;
        }

        // Toggle Packet history
        if (checkboxHistory.IsChecked)
        {
            labelRXdata.Text = recPacket.Contents + labelRXdata.Text;
        }
        else
        {
            labelRXdata.Text = recPacket.Contents;
        }

        // update the packet error data UI

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

