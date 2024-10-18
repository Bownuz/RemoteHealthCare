using Server.DataStorage;
using Server.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
