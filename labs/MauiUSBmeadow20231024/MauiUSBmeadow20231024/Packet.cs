using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace MauiUSBmeadow20231024
{
    internal class Packet
    {
        private string  _contents;
        private int     _packetNumber;
        private int     _rolloverCount;
        private string  _header;

        private readonly int _packetLength;
        private readonly int _headerLength;
        private readonly int _rolloverModulus;
        private readonly int _checksum;


        /// <summary>
        /// The _contents of the packet
        /// </summary>
        public string Contents
        {
            get => _contents;
            set
            {
                int oldPacketNum = _packetNumber;
                _contents = value;
                _header = value[.._headerLength];
                _packetNumber = Convert.ToInt16(value.Substring(_headerLength, 3));
                if(_packetNumber - oldPacketNum < 0)
                {
                    _rolloverCount++;
                }
            }
        }

        /// <summary>
        /// The packet number
        /// </summary>
        public int PacketNumber { get => _packetNumber; }
        
        /// <summary>
        /// The length of the header
        /// </summary>
        public int HeaderLength { get => _headerLength; }

        /// <summary>
        /// The recieved checksum
        /// </summary>
        public int Checksum { get => _checksum; }

        /// <summary>
        /// The packet number resulting in a rollover
        /// </summary>
        public int RolloverModulus { get => _rolloverModulus; }

        /// <summary>
        /// The number of rollovers that have occured
        /// </summary>
        public int RolloverCount { get => _rolloverCount; }

        /// <summary>
        /// The packet header
        /// </summary>
        public string Header { get => _header; }

        /// <summary>
        /// The length of the packet
        /// </summary>
        public int PacketLength { get => _packetLength; }

        // ----- Constructors ----- //
        /// <summary>
        /// A packet object
        /// </summary>
        /// <param name="headerLength">The length of the _header (ex. "###" would be 3)</param>
        public Packet(int headerLength, int rolloverModulus, int packetLength)
        {
            _headerLength = headerLength;
            _rolloverModulus = rolloverModulus;
            _packetLength = packetLength;
        }

        /// <summary>
        /// A packet object
        /// </summary>
        /// <param name="headerLength">The length of the _header</param>
        /// <param name="contents">The _contents of the packet</param>
        public Packet(int headerLength, string contents)
        {
            _headerLength = headerLength;
            Contents = contents;
        }
    }
}
