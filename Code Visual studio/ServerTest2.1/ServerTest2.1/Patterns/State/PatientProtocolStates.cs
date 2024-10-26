using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.Patterns.State.PatientStates {
    public class P_Welcome(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override string CheckInput(string input) {
            protocol.ChangeState(new P_Initialise(protocol, patientHandler));
            return "Welcome Client";
        }
    }

    public class P_Initialise(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }


            if (jsonRegex.IsMatch(input)) {
                PatientInitialisationMessage dataFromMessage = JsonSerializer.Deserialize<PatientInitialisationMessage>(input);
                Patient connectedPerson;
                FileStorage storageFromThread = patientHandler.fileStorage;

                if (!storageFromThread.PatientExists(dataFromMessage.ClientName)) {
                    storageFromThread.AddPatient(dataFromMessage.ClientName);
                }
                patientHandler.connectedPatient = storageFromThread.GetPatient(dataFromMessage.ClientName);
                connectedPerson = patientHandler.connectedPatient;

                connectedPerson.currentSession =
                    new Session(
                        dataFromMessage.dateTime,
                        dataFromMessage.ConnectedErgometer,
                        dataFromMessage.ConnectedHeartRateMonitor
                        );
                connectedPerson.addSession(connectedPerson.currentSession);
                connectedPerson.currentSession.AddObserver(patientHandler);
                if (!connectedPerson.currentSession.observers.Contains(storageFromThread)) {
                    connectedPerson.currentSession.AddObserver(storageFromThread);
                }


                protocol.ChangeState(new P_RecievingData(protocol, patientHandler));
                return "Ready to recieve data";
            }
            return "add failed message";
        }

    }

    public class P_RecievingData(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            if (jsonRegex.IsMatch(input)) {
                patientHandler.connectedPatient.currentSession.addMessage(input, CommunicationType.PATIENT);
                return "Ready to recieve data";
            }
            return "add failed message";
        }
    }
}
