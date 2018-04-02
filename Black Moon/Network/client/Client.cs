using BlackMoon.Network.def;
using BlackMoon.Network.net;
using BlackMoon.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlackMoon.Network.client
{
    public class Client
    {
        Socket clientListenerSocket;
        Connection connection;
        public PacketHandlers packetHandlers;

        public Client()
        {
            Console.WriteLine("STARTED CLIENT");
            packetHandlers = new PacketHandlers();
            new Thread(delegate () {
                try
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    IPEndPoint ipe = new IPEndPoint(ipAddress, Properties.Settings.Default.DefaultPort);
                    Console.WriteLine("Connecting to {0} on port {1}.", ipAddress.ToString(), Properties.Settings.Default.DefaultPort);
                    clientListenerSocket = new Socket(ipe.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    clientListenerSocket.BeginConnect(ipe, new AsyncCallback(connectCallback), clientListenerSocket);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                }
            }).Start();
        }

        public Connection getConnection()
        {
            return connection;
        }

        private void connectCallback(IAsyncResult result)
        {
            Socket s = (Socket)result.AsyncState;
            try
            {
                s.EndConnect(result);

                Console.WriteLine("Socket connected to {0}",
                s.RemoteEndPoint.ToString());

                connection = new Connection();
                connection.setPlayer(new PlayerSession(connection));
                connection.socket = s;
                connection.buffer = new byte[Properties.Settings.Default.DefaultBuffer];

                s.BeginReceive(connection.buffer, 0, connection.buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void receiveCallback(IAsyncResult result)
        {
            connection = (Connection)result.AsyncState;
            try
            {
                if (connection == null || connection.socket == null) return;

                int bytesRead = connection.socket.EndReceive(result);

                if (bytesRead > 0)
                {
                    connection.appendChuckedPackets(bytesRead); //appends current buffer data end of raw packets.
                    connection.packetDecoder();

                    connection.socket.BeginReceive(connection.buffer, 0, connection.buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), connection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + ex.StackTrace);
            }
            connection.processQueuedPackets(null);
        }      
    }
}
