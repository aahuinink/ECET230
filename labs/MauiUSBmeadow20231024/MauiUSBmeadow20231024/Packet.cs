using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSolar
{
    enum PacketError 
    { 
        LengthError,
        HeaderError,
        NumberError,
        ChecksumError,
    }

    internal class Packet
    {
        private int _length;
        private string _header;
        private int _number;
        private string _message;
        private int _checksum;

        public int Length { get => _length; set => _length = value; }
        public string Header { get => _header; set => _header = value; }
        public int Number { get => _number; set => _number = value; }
        public string Message { get => _message; set => _message = value; }
        public int Checksum { get => _checksum; set => _checksum = value; }

        public Packet() { }

        /// <summary>
        /// Parses a string into a packet object and handles error checking
        /// </summary>
        /// <param name="recieved">The recieved string to be parsed into the object</param>
        /// <returns>Packet</returns>
        public List<PacketError> TryRXParse(string recieved)
        {
            List<PacketError> errors = new List<PacketError>(); // create a new list of errors to hold any errors thrown
            recieved = recieved.TrimEnd('\r');
            Length = recieved.Length;
            // check length
            if (Length != 37)
            {
                errors.Add(PacketError.LengthError);
            }

            // pop and check header
            Header = recieved.Substring(0, 3);
            recieved = recieved.Substring(3);

            if (Header!="###")
            { 
                errors.Add(PacketError.HeaderError);
            }

            // calculate checksum
            int chx = 0;
            for (int i = 0; i < Length - 6; i++)
            {
                chx += (byte)recieved[i];
            }
            chx %= 1000; // take last three digits

            // pop packet number
            if (!int.TryParse(recieved.Substring(0, 3), out _number))
            {
                errors.Add(PacketError.NumberError);
            }

            recieved = recieved.Substring(3);

            // pop message
            Message = recieved.Substring(0, 28);
            recieved = recieved.Substring(recieved.Length - 4);

            // check recieved checksum
            int recChx;
            //check that it can be parsed and that it matches the calculated checksum
            if(!int.TryParse(recieved, out recChx) | recChx != chx)
            {
                errors.Add(PacketError.ChecksumError);
            }

            Checksum = chx;

            return errors;
        }


        public void Send(string payload, SerialPort serialPort)
        {
            int chx = 0;                    // checksum variable
            
            // convert payload to bytes to calculate checksum
            byte[] payloadByte = Encoding.UTF8.GetBytes(payload);
            // calculate checksum
            for (int i = 0; i < 4; i++)
            {
                chx += payloadByte[i];
            }

            // create packet string
            string packetString = "###" + payload + chx.ToString() + "\r\n";

            // put packetString into txBuffer
            byte[] txBuffer = Encoding.UTF8.GetBytes(packetString);
            // send txBuffer
            serialPort.Write(txBuffer, 0, txBuffer.Length);
            return;
        }   
    }
}