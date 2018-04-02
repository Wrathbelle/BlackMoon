using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.net
{
    public class PacketBuilder
    {
        public class PacketInfo
        {
            public byte cID;
            public byte pID;

            public PacketInfo(byte cID, byte pID)
            {
                this.cID = cID;
                this.pID = pID;
            }
        }
        private PacketInfo packetInfo;
        private byte[] data;
        public int dataLength
        {
            get
            {
                return data.Length;
            }
        }
        
        private int size //overall length of data + packetinfo
        {
            get
            {
                return (5 + dataLength);
            }
        }


        public PacketBuilder(PacketInfo packetInfo, byte[] array = null)
        {
            this.packetInfo = packetInfo;
            data = array ?? new byte[0];
        }

        private void addCapacity(int bytesToIncrease)
        {
            byte[] newData = new byte[bytesToIncrease + data.Length];
            Array.Copy(data, 0, newData, 0, data.Length);
            data = newData;
        }

        public PacketBuilder addByte(byte val)
        {
            addCapacity(1);
            data[dataLength - 1] = (byte)val;
            return this;
        }

        public PacketBuilder addShort(int val)
        {
            addCapacity(2);
            addByte((byte)(val >> 8));
            addByte((byte)val);
            return this;
        }

        public PacketBuilder addThrice(int val)
        {
            addCapacity( 3);
            addByte((byte)(val >> 16));
            addByte((byte)(val >> 8));
            addByte((byte)val);
            return this;
        }

        public PacketBuilder addInt(int val)
        {
            addByte((byte)(val >> 24));
            addByte((byte)(val >> 16));
            addByte((byte)(val >> 8));
            addByte((byte)val);
            return this;
        }



        public PacketBuilder addString(string s)
        {
            if (s == null || s == "") return this;
            int lastIndex = dataLength;
            addCapacity(s.Length + 1);
            System.Text.Encoding.ASCII.GetBytes(s).CopyTo(data, lastIndex);
            return this;
        }

        public PacketBuilder addBytes(byte[] data)
        {
            return addBytes(data, 0, data.Length);
        }

        public PacketBuilder addBytes(byte[] data, int offset, int len)
        {
            addCapacity(len);
            Array.Copy(data, offset, this.data, dataLength, len);
            return this;
        }

        public byte[] getData()
        {
            return data;
        }

        public byte[] getPayload()
        {
            //todo fully transition to only 2 bytes sending
            byte[] payload = new byte[size + 2];
            payload[0] = packetInfo.cID;
            payload[1] = packetInfo.pID;
            payload[2] = 0;
            payload[3] = 0;
            payload[4] = (byte)(size + 2);
            if (data.Length > 1)
            {
                Array.Copy(data, 0, payload, 5, data.Length);
                //Console.WriteLine("data length {0}", data.Length);
            }
            return payload;
        }

        public Packet toPacket()
        {
            return new Packet(getPayload());
        }
    }
}
