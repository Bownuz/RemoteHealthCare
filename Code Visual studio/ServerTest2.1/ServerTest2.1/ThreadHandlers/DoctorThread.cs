using System.Net.Http;
using System.Net.Sockets;
using Server.DataProtocol;
using Server.DataStorage;
using Server.Patterns.Observer;


namespace Server.ThreadHandlers
{
    internal class DoctorThread : CommunicationThread
    {
        public DoctorThread(FileStorage fileStorage, TcpClient client) : base(fileStorage, client)
        {
            clientType = ClientType.DOCTOR;
        }

        public override void HandleThread()
        {
            protocol = new DataProtocol.DataProtocol(clientType, this);
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
