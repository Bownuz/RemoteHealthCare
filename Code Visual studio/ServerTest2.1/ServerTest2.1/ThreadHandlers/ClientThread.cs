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
            protocol = new DataProtocol.DataProtocol(clientType, this);
            AddObserver(fileStorage);

            while (tcpClient.Connected)
            {
                string recievedMessage;
                String response;
                if ((recievedMessage = MessageCommunication.ReciveMessage(tcpClient)) != null)
                {
                    response = protocol.processMessage(recievedMessage);
                    MessageCommunication.SendMessage(tcpClient, response);
                    UpdateAll();

                    if (response.Equals("Goodbye")) { 
                        tcpClient.Close();
                    }
                }
                
            }
        }
    }
}
