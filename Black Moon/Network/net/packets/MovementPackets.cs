using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.net.packets
{
    public class MovementPackets : PacketsBase
    {
        public void sendMovement(Connection connection, int X, int Y)
        {
            //Console.WriteLine("{0},{1}", X, Y);
            connection.SendPacket(new PacketBuilder(new PacketBuilder.PacketInfo(0, 0)).addShort(X).addShort(Y).toPacket());
        }
    }
}
