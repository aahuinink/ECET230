using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace MauiUSBmeadow20231024
{

    enum Direction
    {
        Send,
        Recieve,
    }

    internal class Packet
    {
        

        private Direction _direction;
        private string  _contents;
        private string  _header;
        private int     _packetNumber;
        private int     _packetLength;
        private int     _recChecksum;
        private int     _calChecksum;

        private readonly int _headerLength;

        // ----- Constructors ----- //
        /// <summary>
        /// A packet object
        /// </summary>
        /// <param name="headerLength">The length of the _header (ex. "###" would be 3)</param>
        public Packet(int headerLength, int expectedPacketLength)
        {
            _headerLength = headerLength;
            _packetLength = expectedPacketLength;
            _direction = Direction.Recieve;
        }

        /// <summary>
        /// Constructs a packet object
        /// </summary>
        /// <param name="headerLength">The length of the _header</param>
        /// <param name="contents">The _contents of the packet</param>
        public Packet(int headerLength, string contents, int expectedPacketLength)
        {
            _headerLength = headerLength;
            Contents = contents;
            _direction = Direction.Send;
            _packetLength = expectedPacketLength;
        }

        // ----- Encapsulation ----- //

        /// <summary>
        /// The _contents of the packet
        /// </summary>
        public string Contents
        {
            get => _contents;
            set
            {
                switch (_direction) {
                    case Direction.Recieve:
                        _contents = value;
                        _header = value[.._headerLength];
                        _packetNumber = Convert.ToInt16(value.Substring(_headerLength, 3));
                        _recChecksum = Convert.ToInt32(value.Substring(34, 3));
                        for (int i = 3; i < 34; i++)
                        {
                            _calChecksum += (byte)value[i];
                        }
                        break;
                    default:
                        break;
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
        /// The calculated checksum
        /// </summary>
        public int CalChecksum { get => _calChecksum; }

        /// <summary>
        /// The packet header
        /// </summary>
        public string Header { get => _header; }

        /// <summary>
        /// The length of the packet
        /// </summary>
        public int PacketLength { get => _packetLength; }

        /// <summary>
        /// The direction of the packet
        /// </summary>
        public Direction Direction { get => _direction; }

        /// <summary>
        /// The recieved checksum
        /// </summary>
        public int RecChecksum { get => _recChecksum; }
    }
}
