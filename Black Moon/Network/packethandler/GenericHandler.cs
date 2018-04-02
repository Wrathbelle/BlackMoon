using BlackMoon.Network.net;
using BlackMoon.Network.packethandler;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.packethandler
{
    public class GenericHandler : PacketHandler
    {
        public void handlePacket(PlayerSession player, Packet p)
        {
            /*player.getPackets().sendMessage(System.Text.Encoding.ASCII.GetString(p.getData()));*/
            //Console.WriteLine("Got back {0}", System.Text.Encoding.ASCII.GetString(p.getData()));
        }
    }
}
