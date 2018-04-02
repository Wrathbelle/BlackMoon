using BlackMoon.Network.client;
using BlackMoon.Network.net;
using BlackMoon.Network.net.packets;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMoon.Core
{
    public static class SessionManager
    {
        private static Client client;
        private static string sessionID = "testSessionID";

        public static void Start()
        {
            client = new Client();
            if(Environment.GetCommandLineArgs().Length > 2)
            {
                sessionID = Environment.GetCommandLineArgs()[1];
            }
        }

        public static PlayerSession getSession()
        {
            return client.getConnection().playerSession;
        }

        public static void Stop()
        {
            client = null;
            sessionID = null;
        }

        public static string getSessionId()
        {
            return sessionID;
        }
    }
}
