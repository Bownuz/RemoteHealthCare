using Server.DataStorage;
using Server.Patterns.Observer;
using System.Net.Sockets;

namespace Server.ThreadHandlers
{
    internal abstract class CommunicationThread(FileStorage fileStorage, TcpClient tcpClient) : Subject, Observer
    {
        
        protected FileStorage fileStorage = fileStorage;
        protected Person Person;
        protected TcpClient tcpClient = tcpClient;
        protected DataProtocol.DataProtocol protocol;
        protected ClientType clientType;

        public abstract void HandleThread();

        public void Update()
        {
            MessageCommunication.SendMessage(tcpClient, Person.currentSession.getLatestMessage(clientType));
        }

    }
}
