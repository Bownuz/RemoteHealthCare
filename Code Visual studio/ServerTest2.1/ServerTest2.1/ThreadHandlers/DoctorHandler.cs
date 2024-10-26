using Server.DataStorage;
using System.Net.Security;

namespace Server.ThreadHandlers {
    public class DoctorHandler : CommunicationHandler {
        public Doctor connectedDoctor { get; set; }

        public DoctorHandler(FileStorage fileStorage, SslStream sslStream) : base(fileStorage, sslStream) {
            communicationType = CommunicationType.DOCTOR;
        }

        public override void Update(CommunicationType communicationOrigin, Session session) {
            if (communicationOrigin == CommunicationType.PATIENT) {
                Console.WriteLine(session.getLatestMessage(communicationOrigin));
                MessageCommunication.SendMessage(sslStream, session.getLatestMessage(communicationOrigin));
            }
        }
    }
}


