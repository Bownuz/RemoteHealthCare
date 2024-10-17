using System.Net.Sockets;
using Server.DataStorage;

namespace Server.ThreadHandlers
{
    internal class ClientThread : CommunicationThread
    {
        public ClientThread(FileStorage fileStorage, TcpClient client) : base(fileStorage, client)
        {
            clientType = ClientType.CLIENT;
        }

        public override void HandleThread()
        {
            protocol = new DataProtocol.DataProtocol(ClientType.DOCTOR, this);
            AddObserver(fileStorage);

            while (true)
            {
                if (tcpClient.Connected)
                {
                    string recievedMessage;
                    if ((recievedMessage = MessageCommunication.ReciveMessage(tcpClient)) != null)
                    {
                        protocol.processMessage(recievedMessage);
                        NotifyAll();
                    }
                }
            }
        }
    }
}
