using Server.DataStorage;
using System.Net.Sockets;

namespace Server.ThreadHandlers
{
    public class DoctorHandler : CommunicationHandler
	{
		public Doctor connectedDoctor {  get; set; }

        public DoctorHandler(FileStorage fileStorage, TcpClient tcpClient) : base(fileStorage, tcpClient)
        {
            communicationType = CommunicationType.DOCTOR;
        }

        public override void Update(CommunicationType communicationOrigin, Session session)
        {
            if (communicationOrigin == CommunicationType.PATIENT)
            {
                MessageCommunication.SendMessage(tcpClient, session.getLatestMessage(communicationOrigin));
            }
        }
    }

}

