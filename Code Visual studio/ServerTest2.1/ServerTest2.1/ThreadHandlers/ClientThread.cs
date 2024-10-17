using System.Net.Sockets;
using Server.DataStorage;
using Server.DataProtocol;
using Server.Patterns.Observer;

namespace Server.ThreadHandlers
{
    internal class ClientThread(FileStorage fileStorage, TcpClient client) : Subject, Observer
    {
        FileStorage fileStorage = fileStorage;
        Person Person;
        TcpClient client = client;
        DataProtocol.DataProtocol protocol;

        public void HandleThread()
        {
            protocol = new DataProtocol.DataProtocol(ClientType.CLIENT);
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
