using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.Patterns.State.PatientStates {
    public class P_Welcome(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            protocol.ChangeState(new P_Initialise(protocol, patientHandler));
            MessageCommunication.SendMessage(patientHandler.networkStream, "Welcome Client");
        }
    }

    public class P_Initialise(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(patientHandler.networkStream, "Goodbye");
                patientHandler.networkStream.Close();
                return;
            }


            if (jsonRegex.IsMatch(input)) {
                PatientInitialisationMessage dataFromMessage = JsonSerializer.Deserialize<PatientInitialisationMessage>(input);
                Patient connectedPerson;
                FileStorage storageFromThread = patientHandler.fileStorage;
                Session currentSession;

                if (!storageFromThread.PatientExists(dataFromMessage.ClientName)) {
                    storageFromThread.AddPatient(dataFromMessage.ClientName);
                }
                patientHandler.connectedPatient = storageFromThread.GetPatient(dataFromMessage.ClientName);
                connectedPerson = patientHandler.connectedPatient;
                currentSession = connectedPerson.currentSession;

                connectedPerson.lastConnectedErgometer = dataFromMessage.ConnectedErgometer;
                connectedPerson.lastConnectedHeartrateMonitor = dataFromMessage.ConnectedHeartRateMonitor;

                if (currentSession != null) {
                    currentSession.ergometerName = dataFromMessage.ConnectedErgometer;
                    currentSession.heartRateMonitorName = dataFromMessage.ConnectedHeartRateMonitor;
                    currentSession.AddObserver(patientHandler);
                }

                protocol.ChangeState(new P_RecievingData(protocol, patientHandler, connectedPerson));
                MessageCommunication.SendMessage(patientHandler.networkStream, "Ready to recieve data");
                return;
            }
            MessageCommunication.SendMessage(patientHandler.networkStream, "This message was not a Json String");
        }

    }

    public class P_RecievingData(DataProtocol protocol, PatientHandler patientHandler, Patient patient) : PatientState(protocol, patientHandler) {
        Patient patient = patient;
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(patientHandler.networkStream, "Goodbye");
                patientHandler.networkStream.Close();
                return;
            }

            if (patient.currentSession == null) {
                MessageCommunication.SendMessage(patientHandler.networkStream, "Ready to recieve data");
                return;
            }

            if (patient.currentSession != null && !patient.currentSession.observers.Contains(patientHandler)) {
                patient.currentSession.AddObserver(patientHandler);
            }

            if (jsonRegex.IsMatch(input)) {
                patientHandler.connectedPatient.currentSession.addMessage(input, CommunicationType.PATIENT);
                MessageCommunication.SendMessage(patientHandler.networkStream, "This message was not a Json String");
                return;
            }
    
            MessageCommunication.SendMessage(patientHandler.networkStream, "Ready to recieve data");
        }
    }
}
