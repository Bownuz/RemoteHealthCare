using System.Net.Sockets;
using Server.DataStorage;
using Server.DataProtocol;
using ServerTest2._1.Patterns.Observer;
using System.Text;

namespace Server.ThreadHandlers
{
    internal class ClientThread(FileStorage fileStorage, TcpClient client) : Subject, Observer
    {
        FileStorage fileStorage = fileStorage;
        Person Person;
        TcpClient client = client;

        public void HandleThread()
        {
            Protocol protocol = new Protocol();
            AddObserver(fileStorage);

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
