using Server.DataStorage;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public class DoctorHandler : CommunicationHandler {
        public Doctor connectedDoctor { get; set; }

        public DoctorHandler(FileStorage fileStorage, NetworkStream networkStream) : base(fileStorage, networkStream) {
            communicationType = CommunicationType.DOCTOR;
        }

        public override void Update(CommunicationType communicationOrigin, Session session) {
            if (communicationOrigin == CommunicationType.PATIENT) {
                Console.WriteLine(session.getLatestMessage(communicationOrigin));
                MessageCommunication.SendMessage(networkStream, session.getLatestMessage(communicationOrigin));
            }
        }
    }
}


