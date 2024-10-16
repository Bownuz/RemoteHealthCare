using System.Net.Sockets;
using Server.DataStorage;
using Server.DataProtocol;
using ServerTest2._1.Patterns.Observer;
using System.Text;

namespace Server.ThreadHandlers
{
    internal class ClientThread : Subject, Observer
    {
        FileStorage fileStorage;
        Person Person;
        TcpClient client;

        public ClientThread(FileStorage fileStorage, TcpClient client)
        {
            this.fileStorage = fileStorage;
            this.client = client;
        }

        public void HandleThread()
        {
            Protocol protocol = new Protocol();
            while (true)
            {
                if (client.Connected)
                {
                    string recievedMessage;
                    if ((recievedMessage = MessageCommunication.ReciveMessage(client)) != null)
                    {
                        protocol.processMessage(recievedMessage);
                        NotifyAll();
                    }
                }
            }
        }

        public void Update()
        {
            MessageCommunication.SendMessage(client, Person.currentSession.getLatestMessage(ClientType.CLIENT));
        }
    }
}
