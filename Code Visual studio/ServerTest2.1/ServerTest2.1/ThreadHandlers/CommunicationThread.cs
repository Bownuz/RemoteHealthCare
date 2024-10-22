using Server.DataStorage;
using Server.Patterns.Observer;
using System.Net.Sockets;

namespace Server.ThreadHandlers
{
    internal abstract class CommunicationThread(FileStorage fileStorage, TcpClient tcpClient) : Observer
    {
        
        public FileStorage fileStorage = fileStorage;
        public Person Person { get; set; }
        protected TcpClient tcpClient = tcpClient;
        protected DataProtocol.DataProtocol protocol;
        protected ClientType clientType;

        public abstract void HandleThread();
        public abstract void Update(ClientType messageType);
    }
}
