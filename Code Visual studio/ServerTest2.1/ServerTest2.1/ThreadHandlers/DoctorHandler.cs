using Server.DataStorage;
using System.Net.Sockets;

namespace Server.ThreadHandlers {
    public class DoctorHandler : CommunicationHandler {
        public Doctor connectedDoctor { get; set; }

        public DoctorHandler(FileStorage fileStorage, NetworkStream networkStream) : base(fileStorage, networkStream) {
            communicationType = CommunicationType.DOCTOR;
        }

        public override void Update(CommunicationType communicationOrigin, Session session) {
            if(communicationOrigin == CommunicationType.PATIENT && session != null) {
                string latestMessage = session.getLatestMessage(communicationOrigin);
                if(latestMessage != null) {
                    Console.WriteLine(latestMessage);
                    MessageCommunication.SendMessage(networkStream, latestMessage);
                }
            }
        }

        private void StartTraining(string patientName) {
            var patient = fileStorage.GetPatient(patientName);
            if(patient?.currentSession == null) {
                MessageCommunication.SendMessage(networkStream, $"Error: Patient {patientName} or session not found.");
                return;
            }

            patient.currentSession.addMessage($"Training started for {patientName}", CommunicationType.DOCTOR);
        }

        private void StopTraining(string patientName) {
            var patient = fileStorage.GetPatient(patientName);
            if(patient?.currentSession == null) {
                MessageCommunication.SendMessage(networkStream, $"Error: Patient {patientName} or session not found.");
                return;
            }

            patient.currentSession.addMessage($"Training stopped for {patientName}", CommunicationType.DOCTOR);
        }

        private void EmergencyStop(string patientName, int resistance) {
            var patient = fileStorage.GetPatient(patientName);
            if(patient?.currentSession == null) {
                MessageCommunication.SendMessage(networkStream, $"Error: Patient {patientName} or session not found.");
                return;
            }

            patient.currentSession.addMessage($"{patientName} ; message: NOODSTOP ; newResistance: {resistance}", CommunicationType.DOCTOR);
            MessageCommunication.SendMessage(networkStream, $"{patientName} ; message: NOODSTOP ; newResistance: {resistance}");
        }
    }
}
