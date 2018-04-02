using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.net.packets
{
    public class AuthenticationPackets : PacketsBase
    {
        public void sendSessionId(Connection connection, string sessionID)
        {
            connection.SendPacket(new PacketBuilder(new PacketBuilder.PacketInfo(0, 0)).addByte(0).addByte(0).addString(sessionID).toPacket());
        }
    }
}
