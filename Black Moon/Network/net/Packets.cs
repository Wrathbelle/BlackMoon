using BlackMoon.Network.net.packets;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Network.net
{
    public class Packets
    {
        private PlayerSession session;

        public Packets(PlayerSession session)
        {
            this.session = session;
        }
    }
}
