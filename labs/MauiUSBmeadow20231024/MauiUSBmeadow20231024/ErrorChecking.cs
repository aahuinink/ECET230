using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiUSBmeadow20231024
{
    internal class ErrorChecking
    {
        private int recCHX;
        private int calCHX;
        private int packetNumber;
        private int recPacketCount = 0;
        private int lostPacketCount = 0;
        private int packetCountRollovers = 0;
        private int chkSumErrors = 0;
        private int lengthErrors = 0;
        private int headerErrors = 0;

        // --- Encapsulations --- //

        /// <summary>
        /// The checksum from the latest recieved packet
        /// </summary>
        public int RecCHX { get => recCHX; set => recCHX = value; }

        /// <summary>
        /// The calculated checksum from the latest recieved packet
        /// </summary>
        public int CalCHX { get => calCHX; set => calCHX = value; }

        /// <summary>
        /// The number of recieved packets count
        /// </summary>
        public int RecPacketCount { get => recPacketCount; set => recPacketCount = value; }

        /// <summary>
        /// The number of lost packets
        /// </summary>
        public int LostPacketCount { get => lostPacketCount; set => lostPacketCount = value; }

        /// <summary>
        /// The number of times the packet count has rolled over
        /// </summary>
        public int PacketCountRollovers { get => packetCountRollovers; set => packetCountRollovers = value; }

        /// <summary>
        /// The number of packet checksum errors
        /// </summary>
        public int ChkSumErrors { get => chkSumErrors; set => chkSumErrors = value; }

        /// <summary>
        /// The number of packet length errors
        /// </summary>
        public int LengthErrors { get => lengthErrors; set => lengthErrors = value; }
        /// <summary>
        /// The packet number of the latest recieved packet
        /// </summary>
        public int PacketNumber { get => packetNumber; set => packetNumber = value; }

        /// <summary>
        /// The number of header errors
        /// </summary>
        public int HeaderErrors { get => headerErrors; set => headerErrors = value; }

        /// <summary>
        /// Base constructor
        /// </summary>
        public ErrorChecking() { }

        /// <summary>
        /// Overload in case packet number needs to be updated
        /// </summary>
        /// <param name="packetNumber">The packet number of the most recently recieved packet</param>
        public ErrorChecking(int packetNumber)
        {
            PacketNumber = packetNumber;
        }

        /// <summary>
        /// Calculates the checksum from the packet data
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public void CalculateCHX(string values)
        {
            int chx = 0;

            // Calculate checksum
            for (int i = 0; i < values.Length; i++)
            {
                chx += (byte)values[i];
            }
            chx %= 1000; // take the last three digits

            // set calCHX to the value
            CalCHX = chx;
        }
    }
}
