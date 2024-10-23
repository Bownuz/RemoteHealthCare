using Server.DataStorage;
using System;
using System.Net.Sockets;

namespace Server.ThreadHandlers
{
    public class PatientHandler : CommunicationHandler
	{
		public Patient connectedPatient {  get; set; }

		public PatientHandler(FileStorage fileStorage, TcpClient tcpClient) : base(fileStorage, tcpClient) 
        {
		    communicationType = CommunicationType.PATIENT;
		}

        public override void Update(CommunicationType communicationOrigin, Session session)
        {
            if (communicationOrigin == CommunicationType.DOCTOR)
            {
                MessageCommunication.SendMessage(tcpClient, session.getLatestMessage(communicationType));
            }
        }
    }

}

