using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackMoon.Player;
using System.Threading.Tasks;
using BlackMoon.Network.def;
using System.Net.Sockets;

namespace BlackMoon.Network.net
{
    public class Connection
    {
        public byte[] buffer;
        private object chuckedPackets_lock = new object(); //for thread safety
        public List<Byte> chuckedRawPackets;
        public System.Net.Sockets.Socket socket;
        private Queue<Packet> queuedPackets;
        public byte loginStage = 0;
        private int pingCount = 0;
        public PlayerSession playerSession;

        public Connection()
        {
            chuckedRawPackets = new List<Byte>();
            queuedPackets = new Queue<Packet>();
        }

        public void setPlayer(PlayerSession playerSession)
        {
            this.playerSession = playerSession;
        }

        public PlayerSession getPlayerSession()
        {
            return playerSession;
        }

        public void setPinged()
        {
            pingCount++;
        }

        public int getPingCount()
        {
            return pingCount;
        }

        public void resetPingCount()
        {
            pingCount = 0;
        }

        public void appendChuckedPackets(int amountOfBytes)
        {
            lock (chuckedPackets_lock)
            {
                //TODO: Fix later, not very efficent why clone the data if it exists in buffer >.>
                byte[] chuckedPacket = new byte[amountOfBytes];
                Buffer.BlockCopy(buffer, 0, chuckedPacket, 0, amountOfBytes);
                chuckedRawPackets.AddRange(chuckedPacket);
            }
        }

        public void processQueuedPackets(Object threadContext)
        {
            lock (queuedPackets)
            {
                if (playerSession == null) //no player why even bother?.
                    return;
                if (queuedPackets.Count == 0)
                    return;
                try //Exception occurs when it hits zero elements.
                {
                    Packet p = null;
                    while (queuedPackets.Count > 0)
                    {
                        p = queuedPackets.Dequeue();
                        if (p != null && playerSession != null)
                            PacketHandlers.handlePacket(playerSession, p);
                    }
                }
                catch (InvalidOperationException) { }
            }
        }

        public void addPacketToQueue(Packet p)
        {
            lock (queuedPackets)
            {
                queuedPackets.Enqueue(p);
            }
        }

        public void packetDecoder()
        {
            if (chuckedRawPackets.Count >= 5 && playerSession != null)
            {
                //get opcode
                byte cID = chuckedRawPackets[0];
                byte pID = chuckedRawPackets[1];
                byte slot = chuckedRawPackets[2];
                byte con = chuckedRawPackets[3];
                int packetSize = Convert.ToInt32(chuckedRawPackets[4]);

                if (packetSize > chuckedRawPackets.Count || packetSize == 0) //5 for our 4 identifiers, and 1 size 
                {
                    //Packet not fully arrived
                    return;
                }

                Packet p;

                lock (chuckedPackets_lock) //Multiple PacketDecoders.. RemoeRange and GetRange same time not good, better lock.
                {
                    byte[] payload = chuckedRawPackets.GetRange(0, packetSize).ToArray();
                    //Console.WriteLine("Packet decoder found complete packet: " + System.Text.Encoding.ASCII.GetString(payload));
                    p = new Packet(payload);
                }

                lock (chuckedPackets_lock)
                {
                    chuckedRawPackets.RemoveRange(0, packetSize);
                }

                addPacketToQueue(p);
                packetDecoder(); //Loop until all packets retrieved
            }
        }

        public void SendPacket(Packet packet)
        {
            if (packet == null) return;
            try
            {
                if (socket.Connected)
                {
                    lock (socket)
                    {
                        //we use a blocking mode send, no async on the outgoing
                        //since this is primarily a multithreaded application, shouldn't cause problems to send in blocking mode
                        socket.Send(packet.getPayload(), SocketFlags.None);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
            }
        }
    }
}
