using Server.DataStorage;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public class PatientHandler : CommunicationHandler {
        public Patient connectedPatient { get; set; }

        public PatientHandler(FileStorage fileStorage, NetworkStream networkStream) : base(fileStorage, networkStream) {
            communicationType = CommunicationType.PATIENT;
        }

        public override void Update(CommunicationType communicationOrigin, Session session) {
            if (communicationOrigin == CommunicationType.DOCTOR) {
                Console.WriteLine(session.getLatestMessage(communicationOrigin));
                MessageCommunication.SendMessage(networkStream, session.getLatestMessage(communicationOrigin));
            }
        }
    }

}

