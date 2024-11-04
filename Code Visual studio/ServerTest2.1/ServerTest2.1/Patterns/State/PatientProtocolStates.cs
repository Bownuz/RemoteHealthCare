using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;


namespace Server.Patterns.State.PatientStates {
    public class P_Welcome(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            protocol.ChangeState(new P_Initialise(protocol, patientHandler));
            MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.p_welcome);
        }
    }

    public class P_Initialise(DataProtocol protocol, PatientHandler patientHandler) : PatientState(protocol, patientHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.a_goodbye);
                patientHandler.networkStream.Close();
                return;
            }


            if (!isJson(input)) {
                return;
            }

            PatientInitialisationMessage dataFromMessage = JsonSerializer.Deserialize<PatientInitialisationMessage>(input);
            Patient connectedPerson;
            FileStorage storageFromThread = patientHandler.fileStorage;
            Session currentSession;

            if (!storageFromThread.PatientExists(dataFromMessage.ClientName)) {
                storageFromThread.AddPatient(dataFromMessage.ClientName);
                storageFromThread.SaveToFile();
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
            MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.p_readyToRecieve);
        }

    }

    public class P_RecievingData(DataProtocol protocol, PatientHandler patientHandler, Patient patient) : PatientState(protocol, patientHandler) {
        Patient patient = patient;
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.a_goodbye);
                patientHandler.networkStream.Close();
                return;
            }

            if (!isJson(input)) {
                goto readyMessage;
            }

            if (patient.currentSession == null) {
                MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.p_noSessionActive);
                goto readyMessage;
            }

            if (patient.currentSession != null && !patient.currentSession.observers.Contains(patientHandler)) {
                patient.currentSession.AddObserver(patientHandler);
            }

            if (patient.currentSession != null && !patient.currentSession.observers.Contains(patientHandler.fileStorage)) {
                patient.currentSession.AddObserver(patientHandler.fileStorage);
            }

            String patientName = patientHandler.connectedPatient.Name;
            PatientRecieveData patientData = JsonSerializer.Deserialize<PatientRecieveData>(input);
            patientData.patientName = patientName;
            String messageWithName = JsonSerializer.Serialize(patientData);

            patient.currentSession.addMessage(messageWithName, CommunicationType.PATIENT);
            readyMessage: MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.p_readyToRecieve);
        }
    }
}
