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
        private int     _expectedPacketLength;
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
            _expectedPacketLength = expectedPacketLength;
        }

        // ----- Encapsulation ----- //

        /// <summary>
        /// The contents of the packet
        /// </summary>
        public string Contents
        {
            get => _contents;
            set
            {
                switch (_direction) {
                    case Direction.Recieve:
                        _packetLength = value.Length;
                        // pop out header
                        _header = value[.._headerLength];          
                        value = value.Substring(_headerLength);     
                        // pop packet number
                        _packetNumber = Convert.ToInt16(value.Substring(0, 3));
                        value = value.Substring(3);
                        // pop recieved checksum
                        _recChecksum = Convert.ToInt32(value.Substring(value.Length-3));
                        value = value.Substring(0, value.Length);

                        // calculate checksum
                        for (int i = 3; i < value.Length; i++)
                        {
                            _calChecksum += (byte)value[i];
                        }

                        // set contents field
                        _contents = value;

                        break;

                    default:
                        _contents = value;
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
        /// The expected length of the contents of the packet
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

        /// <summary>
        /// The expected length of the packet
        /// </summary>
        public int ExpectedPacketLength { get => _expectedPacketLength; }
    }
}
