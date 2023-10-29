using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiUSBmeadow20231024
{
    class ErrorChecking

    {
        private int _expectedPacketLength;
        private int _calCHX;
        private int _recPacketCount = 0;
        private int _lostPacketCount = 0;
        private int _packetNumberRollovers = 0;
        private int _chkSumErrors = 0;
        private int _lengthErrors = 0;
        private int _headerErrors = 0;

        public int ExpectedPacketLength { get => _expectedPacketLength; set => _expectedPacketLength = value; }
        public int CalCHX { get => _calCHX; set => _calCHX = value; }
        public int RecPacketCount { get => _recPacketCount; set => _recPacketCount = value; }
        public int LostPacketCount { get => _lostPacketCount; set => _lostPacketCount = value; }
        public int PacketNumberRollovers { get => _packetNumberRollovers; set => _packetNumberRollovers = value; }
        public int ChkSumErrors { get => _chkSumErrors; set => _chkSumErrors = value; }
        public int LengthErrors { get => _lengthErrors; set => _lengthErrors = value; }
        public int HeaderErrors { get => _headerErrors; set => _headerErrors = value; }

        public ErrorChecking(int expectedPacketLength)
        {
            _expectedPacketLength = expectedPacketLength;
        }
        public int CalculateCHX(string message)
        {
            int chx = 0;
            for (int i = 0; i < message.Length, i++)
            {
                chx += (byte)message[i];
            }
            return chx;
        }
    }
}
