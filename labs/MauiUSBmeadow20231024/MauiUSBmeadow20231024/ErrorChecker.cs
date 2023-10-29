using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiUSBmeadow20231024
{
    internal class ErrorChecker

    {
        private int expectedPacketLength;
        private int calCHX;
        private int recPacketCount = 0;
        private int lostPacketCount = 0;
        private int packetNumberRollovers = 0;
        private int chkSumErrors = 0;
        private int lengthErrors = 0;
        private int headerErrors = 0;

        // --- Encapsulations --- //

        /// <summary>
        /// The calculated _checksum from the latest recieved packet
        /// </summary>
        public int CalCHX { get => calCHX; set => calCHX = value; }

        /// <summary>
        /// The number of recieved packets count
        /// </summary>
        public int RecPacketCount { 
            get => recPacketCount; 
            set => recPacketCount = PacketNumber + ; 
        }

        /// <summary>
        /// The number of lost packets
        /// </summary>
        public int LostPacketCount { get => lostPacketCount; set => lostPacketCount = value; }

        /// <summary>
        /// The number of times the packet count has rolled over
        /// </summary>
        public int PacketCountRollovers { get => packetNumberRollovers; set => packetNumberRollovers = value; }

        /// <summary>
        /// The number of packet _checksum errors
        /// </summary>
        public int ChkSumErrors { get => chkSumErrors; set => chkSumErrors = value; }

        /// <summary>
        /// The number of packet length errors
        /// </summary>
        public int LengthErrors { get => lengthErrors; set => lengthErrors = value; }
        /// <summary>
        /// The packet number of the latest recieved packet
        /// </summary>
        public int PacketNumber { get => PacketNumber; set => PacketNumber = value; }

        /// <summary>
        /// The number of _header errors
        /// </summary>
        public int HeaderErrors { get => headerErrors; set => headerErrors = value; }
        /// <summary>
        /// The expected packet length
        /// </summary>
        public int ExpectedPacketLength { get => expectedPacketLength; set => expectedPacketLength = value; }

        public Packet Packet { get => packet; set => packet = value; }

        /// <summary>
        /// Calculates the _checksum from the packet data
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public void CalculateCHX(string values)
        {
            int chx = 0;

            // Calculate _checksum
            for (int i = 0; i < values.Length; i++)
            {
                chx += (byte)values[i];
            }
            chx %= 1000; // take the last three digits

            // set calCHX to the value
            CalCHX = chx;
        }

        /// <summary>
        /// Verifies that the recieved _checksum matches the calculated _checksum.
        /// If not, it increases the _checksum error count.
        /// </summary>
        /// <returns>Boolean if the recieved chx equals the calculated chx</returns>
        public bool VerifyCHX()
        {
            // if not equal
            if (Checksum != calCHX)
            {
                chkSumErrors++;  // increase error count
            }
            return (Checksum == calCHX);
        }

    }
}
