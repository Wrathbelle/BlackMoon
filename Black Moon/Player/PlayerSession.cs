using BlackMoon.Network.net;
using BlackMoon.Network.net.packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Player
{
    public class PlayerSession
    {
        private Connection connection;
        private bool disconnected;
        private Packets packets;

        public PlayerSession(Connection connection)
        {
            this.connection = connection; //without this, new Packets(this); wouldn't function.
            packets = new Packets(this);
            if (connection != null)
            {
                //start filling out Player info
            }
        }

        public void sendPacket(PacketsBase packet)
        {

        }

        public void setConnection(Connection connection)
        {
            this.connection = connection;
        }

        public Connection getConnection()
        {
            return connection;
        }

        public bool isDisconnected()
        {
            return disconnected;
        }

        public void setDisconnected(bool disconnected)
        {
            this.disconnected = disconnected;
        }
    }
}
