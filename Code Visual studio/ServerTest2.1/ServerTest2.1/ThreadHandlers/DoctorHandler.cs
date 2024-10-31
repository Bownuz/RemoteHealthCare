using Server.DataStorage;
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

        private void StartTraining(string patientName) {
            var session = fileStorage.GetPatient(patientName)?.currentSession;
            session?.addMessage($"Training started for {patientName}", CommunicationType.DOCTOR);
        }

        private void StopTraining(string patientName) {
            var session = fileStorage.GetPatient(patientName)?.currentSession;
            session?.addMessage($"Training stopped for {patientName}", CommunicationType.DOCTOR);
        }

        private void EmergencyStop(string patientName, int resistance) {
            var session = fileStorage.GetPatient(patientName)?.currentSession;
            session?.addMessage($"{patientName} ; message: NOODSTOP ; newResistance: {resistance}", CommunicationType.DOCTOR);
            MessageCommunication.SendMessage(networkStream, $"{patientName} ; message: NOODSTOP ; newResistance: {resistance}");
        }
    }
}
