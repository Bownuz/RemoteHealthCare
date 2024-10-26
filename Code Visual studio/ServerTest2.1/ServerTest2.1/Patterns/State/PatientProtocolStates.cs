using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.Patterns.State.PatientStates {
    public class P_Welcome(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            protocol.ChangeState(new P_Initialise(protocol, patientHandler));
            MessageCommunication.SendMessage(patientHandler.sslStream, "Welcome Client");
        }
    }

    public class P_Initialise(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(patientHandler.sslStream, "Goodbye");
                patientHandler.sslStream.Close();
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
                MessageCommunication.SendMessage(patientHandler.sslStream, "Ready to recieve data");
            }
            MessageCommunication.SendMessage(patientHandler.sslStream, "add failed message");
        }

    }

    public class P_RecievingData(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(patientHandler.sslStream, "Goodbye");
                patientHandler.sslStream.Close();
            }

            if (jsonRegex.IsMatch(input)) {
                patientHandler.connectedPatient.currentSession.addMessage(input, CommunicationType.PATIENT);
                MessageCommunication.SendMessage(patientHandler.sslStream, "Ready to recieve data");
            }
            MessageCommunication.SendMessage(patientHandler.sslStream, "add failed message");
        }
    }
}
