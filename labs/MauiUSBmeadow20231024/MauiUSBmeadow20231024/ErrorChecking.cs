using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSolar
{
    class ErrorChecking

    {
        private int _lostPacketCount = 0;
        private int _chkSumErrors = 0;
        private int _lengthErrors = 0;
        private int _headerErrors = 0;
        private int _numberErrors = 0;

        /// <summary>
        /// The number of lost packets
        /// </summary>
        public int LostPacketCount { get => _lostPacketCount; set => _lostPacketCount = value; }
        /// <summary>
        /// The recieved and calculated checksums did not match
        /// </summary>
        public int ChkSumErrors { get => _chkSumErrors; set => _chkSumErrors = value; }
        /// <summary>
        /// The length of the recieved packet was incorrect
        /// </summary>
        public int LengthErrors { get => _lengthErrors; set => _lengthErrors = value; }
        /// <summary>
        /// Incorrect packet header.
        /// </summary>
        public int HeaderErrors { get => _headerErrors; set => _headerErrors = value; }
        /// <summary>
        /// Failures to parse the packet number
        /// </summary>
        public int NumberErrors { get => _numberErrors; set => _numberErrors = value; }

        /// <summary>
        /// An object that handles errors and consolidates error data.
        /// </summary>
        public ErrorChecking() { }

        /// <summary>
        /// Handles errors by incrementing the corresponding error count.
        /// </summary>
        /// <param name="error">The packet error thrown during parsing.</param>
        internal void Handle(PacketError error)
        {
            switch (error)
            {
                case PacketError.LengthError: _lengthErrors++; break;
                case PacketError.HeaderError: _headerErrors++; break;
                case PacketError.NumberError: NumberErrors++; break;
                case PacketError.ChecksumError: _chkSumErrors++; break;
            }
        }
    }
}
