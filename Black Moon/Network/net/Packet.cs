using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.net
{
    public class Packet
    {
        private byte[] payload;

        public Packet(byte[] payload)
        {
            this.payload = payload;
        }

        public byte getcID()
        {
            return payload[0];
        }

        public byte getpID()
        {
            return payload[1];
        }

        public byte getSlot()
        {
            return payload[2];
        }

        public byte getCon()
        {
            return payload[3];
        }

        public int getLength()
        {
            return Convert.ToInt32(payload[4]);
        }

        //data after identifiers
        public byte[] getData()
        {
            byte[] temp = new byte[getLength() - 5];
            Array.Copy(payload, 5, temp, 0, getLength() - 5);
            return temp;
        }

        //Whole packet
        public byte[] getPayload()
        {
            return payload;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[len=" + getLength() + ",data='" + System.Text.Encoding.ASCII.GetString(payload) + "' : ");
            for (int x = 0; x < getLength(); x++)
            {
                sb.Append(byteToHex(payload[x], true) + " ");
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string byteToHex(byte b, bool forceLeadingZero)
        {
            StringBuilder sb = new StringBuilder();
            if (b / 16 > 0 || forceLeadingZero)
                sb.Append(hex[b / 16]);
            sb.Append(hex[b % 16]);
            return sb.ToString();
        }

        private static char[] hex = "0123456789ABCDEF".ToCharArray();
    }
}
