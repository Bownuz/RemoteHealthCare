using System.Net.Sockets;
using Server.DataStorage;
using Server.Patterns.Observer;

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

            MessageCommunication.SendMessage(tcpClient, protocol.processMessage(""));
            while (tcpClient.Connected)
            {
                string recievedMessage;
                String response;
                if ((recievedMessage = MessageCommunication.ReciveMessage(tcpClient)) != null)
                {
                    response = protocol.processMessage(recievedMessage);
                    MessageCommunication.SendMessage(tcpClient, response);

                    if (response.Equals("Goodbye")) { 
                        tcpClient.Close();
                    }
                }
                
            }  

        }

        public override void Update(ClientType messageType)
        {
            if (clientType == ClientType.DOCTOR)
            {
                Console.WriteLine("{0}", Person.currentSession.getLatestMessage(messageType));
            }
        }
    }
}
