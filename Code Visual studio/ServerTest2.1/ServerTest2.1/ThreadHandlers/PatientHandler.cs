using Server.DataStorage;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public class PatientHandler : CommunicationHandler {
        public Patient connectedPatient { get; set; }

        public PatientHandler(FileStorage fileStorage, SslStream sslStream) : base(fileStorage, sslStream) {
            communicationType = CommunicationType.PATIENT;
        }

        public override void Update(CommunicationType communicationOrigin, Session session) {
            if (communicationOrigin == CommunicationType.DOCTOR) {
                Console.WriteLine(session.getLatestMessage(communicationOrigin));
                MessageCommunication.SendMessage(sslStream, session.getLatestMessage(communicationOrigin));
            }
        }
    }

}

