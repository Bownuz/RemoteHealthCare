using System.Net.Sockets;
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
            if (!Person.currentSession.Observers.Contains(fileStorage))
            {
                Person.currentSession.AddObserver(fileStorage);
            }

            while (true)
            {
                if (tcpClient.Connected)
                {
                    string recievedMessage;
                    if ((recievedMessage = MessageCommunication.ReciveMessage(tcpClient)) != null)
                    {
                        protocol.processMessage(recievedMessage);
                    }
                }
            }
        }
    }
}
