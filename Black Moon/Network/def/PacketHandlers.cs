using BlackMoon.Network.net;
using BlackMoon.Network.packethandler;
using BlackMoon.Network.net.packets;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.def
{
    public class PacketHandlers
    {
        private static Dictionary<string, PacketHandler> handlers = new Dictionary<string, PacketHandler>();

        public PacketHandlers()
        {
            PacketHandler temp = new GenericHandler();
            handlers.Add("0.0", temp);

            Console.WriteLine("Binded packetHandlers");
        }

        public static void handlePacket(PlayerSession player, Packet p)
        {
            PacketHandler handler = null;

            //Console.WriteLine("cID:'{0}' pID:'{1}'", p.getcID(), p.getpID());

            if (handlers.TryGetValue(p.getcID() + "." + p.getpID(), out handler))
            {
                handler.handlePacket(player, p);
            }
            else
            {
                Console.WriteLine("Unhandled packet: " + p + ".");
            }
        }
    }
}
