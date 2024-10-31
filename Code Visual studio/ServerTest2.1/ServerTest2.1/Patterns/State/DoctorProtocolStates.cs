using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;

namespace Server.Patterns.State.DoctorStates {
    public class D_Welcome : DoctorState {
        public D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            protocol.ChangeState(new D_Login(protocol, doctorHandler));
            MessageCommunication.SendMessage(doctorHandler.networkStream, "Enter Login Data");
        }
    }

    public class D_Login : DoctorState {
        public D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }

            if (!jsonRegex.IsMatch(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "This message is not a JsonString");
                return;
            }

            DoctorInitialiseMessage messageContent = JsonSerializer.Deserialize<DoctorInitialiseMessage>(input);
            Doctor doctor = new Doctor(messageContent.DoctorID, messageContent.DoctorName, messageContent.DoctorPassword);

            if (!(doctorHandler.fileStorage.DoctorExists(doctor.DoctorID) &&
                doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorPassword.Equals(doctor.DoctorPassword) &&
                doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorName.Equals(doctor.DoctorName)
            )) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Incorrect Login");
                return;
            }

            doctorHandler.connectedDoctor = doctorHandler.fileStorage.getDoctor(doctor.DoctorID);
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            MessageCommunication.SendMessage(doctorHandler.networkStream, "This Login State Should Still be Added");
        }
    }


    public class D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }

            switch (input) {
                case "Retrieve Data":
                    protocol.ChangeState(new D_FetchingData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Which patient and date should data be retrieved from?");
                    break;
                case "Subscribe":
                    protocol.ChangeState(new D_Subscribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList("Which patient should be subscribed to?"));
                    break;
                case "Unsubscribe":
                    protocol.ChangeState(new D_Unsubsribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList("Which patient should be unsubscribed from?"));
                    break;
                case "Send Data":
                    protocol.ChangeState(new D_SendData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "What data should be sent?");
                    break;
                case "Start Session":
                    protocol.ChangeState(new D_StartingSession(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList("Which Patient should a session start?"));
                    break;
                case "End Session":
                    protocol.ChangeState(new D_StartingSession(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList("Which Patient should a session End?"));
                    break;
                default:
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "This command is not valid.");
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                    break;
            }
        }

        public String MessageWithPatientsList(String message) {
            DoctorMessageWithList MessageJson = new DoctorMessageWithList(message, doctorHandler.fileStorage.PatientNamesToArray());
            return JsonSerializer.Serialize(MessageJson);
        }
    }

    public class D_FetchingData : DoctorState {
        public D_FetchingData(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!jsonRegex.IsMatch(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Add failed message");
                return;
            }

            try {
                var dataToFetch = JsonSerializer.Deserialize<DoctorFetchData>(input);
                var sessionToSend = doctorHandler.fileStorage.GetPatient(dataToFetch.PatientName).GetSession(dataToFetch.SessionDate);
                MessageCommunication.SendMessage(doctorHandler.networkStream, JsonSerializer.Serialize(sessionToSend));
            } catch (Exception ex) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ex.Message);
            }

            MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
        }
    }

    public class D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (doctorHandler.fileStorage.PatientExists(input) && !doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.AddObserver(doctorHandler);                
            } else {  
                MessageCommunication.SendMessage(doctorHandler.networkStream, $"Failed to add: {input} - This patient does not exist or is already subscribed.");
            }

            MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
        }
    }

    public class D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (doctorHandler.fileStorage.PatientExists(input) && doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.RemoveObserver(doctorHandler);
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
            } else {
                MessageCommunication.SendMessage(doctorHandler.networkStream, $"Failed to remove: {input} - This patient does not exist or is not subscribed.");
            }
        }
    }

    public class D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!jsonRegex.IsMatch(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Add failed message");
                return;
            }

            var message = JsonSerializer.Deserialize<DoctorDataMessage>(input);
            if (doctorHandler.fileStorage.PatientExists(message.PatientName)) {
                doctorHandler.fileStorage.GetPatient(message.PatientName).currentSession.addMessage(input, CommunicationType.DOCTOR);
            } else {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "This patient does not exist.");
            }

            MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
        }
    }

    public class D_StartingSession(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "This patient does not exist");
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                return;
            }
            Patient patient = doctorHandler.fileStorage.GetPatient(input);


            patient.currentSession = new Session(DateTime.Now, patient.lastConnectedErgometer, patient.lastConnectedHeartrateMonitor);
            patient.sessions.Add(patient.currentSession);
            patient.currentSession.AddObserver(doctorHandler.fileStorage);
            patient.currentSession.AddObserver(doctorHandler);

            MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
        }
    }

    public class D_EndingSession(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "This patient does not exist");
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                return;
            }

            Patient patient = doctorHandler.fileStorage.GetPatient(input);

            patient.currentSession.sessionEnd = DateTime.Now;

            patient.currentSession = null;

            MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
        }
    }

}
