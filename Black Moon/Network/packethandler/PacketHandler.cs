using BlackMoon.Network.net;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.packethandler
{
    interface PacketHandler
    {
        void handlePacket(PlayerSession player, Packet p);
    }
}
