using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;

namespace Server.Patterns.State.DoctorStates {
    //TODO: fix all return strings
    public class D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            protocol.ChangeState(new D_Login(protocol, doctorHandler));
            return "Enter Login Data";
        }
    }

    public class D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            if (jsonRegex.IsMatch(input)) {
                Doctor doctor = JsonSerializer.Deserialize<Doctor>(input);

                if (doctorHandler.fileStorage.DoctorExists(doctor.DoctorID) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorPassword.Equals(doctor.DoctorPassword) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorName.Equals(doctor.DoctorName)
                    ) {
                    doctorHandler.connectedDoctor = doctorHandler.fileStorage.getDoctor(doctor.DoctorID);
                    protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                    return "This Login State Should Still be Added";
                }

            }
            return "Incorrect Login";
        }
    }

    public class D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }


            switch (input) {
                case "Retrieve Data":
                    protocol.ChangeState(new D_FetchingData(protocol, doctorHandler));
                    return "Which patient and date should data be retrieved from?";

                case "Subscribe":
                    AddRemoveObserverMessage addMessageData = new AddRemoveObserverMessage("Which patient should be subscribed to?", doctorHandler.fileStorage.PatientNamesToArray());
                    String addMessage = JsonSerializer.Serialize(addMessageData);

                    protocol.ChangeState(new D_Subscribing(protocol, doctorHandler));
                    return addMessage;

                case "Unsubscribe":
                    AddRemoveObserverMessage removeMessageData = new AddRemoveObserverMessage("Which patient should be unsubscribed form?", doctorHandler.fileStorage.PatientNamesToArray());
                    String removeMessage = JsonSerializer.Serialize(removeMessageData);

                    protocol.ChangeState(new D_Unsubsribing(protocol, doctorHandler));
                    return removeMessage;

                case "Send Data":
                    protocol.ChangeState(new D_SendData(protocol, doctorHandler));
                    return "What data should be sent?";

                default:
                    MessageCommunication.SendMessage(doctorHandler.sslStream, "this Command is not Valid.");
                    return $"ready to recieve Command";
            }
        }
    }


    public class D_FetchingData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            if (!jsonRegex.IsMatch(input)) {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "add failed message";
            }

            try {
                DoctorFetchData dataToFetch = JsonSerializer.Deserialize<DoctorFetchData>(input);
                Session sessionToSend = doctorHandler.fileStorage.GetPatient(dataToFetch.PatientName).GetSession(dataToFetch.SessionDate);

                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.sslStream, JsonSerializer.Serialize<Session>(sessionToSend));
                return "Ready to recieve Command";
            } catch (Exception ex) {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return ex.Message;
            }
        }
    }
    public class D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            if (doctorHandler.fileStorage.PatientExists(input) && !doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.AddObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "Ready to recieve Command";
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            MessageCommunication.SendMessage(doctorHandler.sslStream, $"Failed to add : {input} : this patient does not exist or this Client was Subscribed to this patient");
            return $"Ready to recieve Command";
        }
    }

    public class D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            if (doctorHandler.fileStorage.PatientExists(input) && doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.RemoveObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "Ready to recieve Command";
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            MessageCommunication.SendMessage(doctorHandler.sslStream, $"Failed to Remove : {input} : this patient does not exist or This Client was not subscribed to this patient");e
            return $"Ready to recieve Command";

        }
    }

    public class D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override string CheckInput(string input) {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }


            if (jsonRegex.IsMatch(input)) {
                DoctorDataMessage message = JsonSerializer.Deserialize<DoctorDataMessage>(input);

                if (doctorHandler.fileStorage.PatientExists(message.PatientName)) {
                    doctorHandler.fileStorage.GetPatient(message.PatientName).currentSession.addMessage(input, CommunicationType.DOCTOR);
                    protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                    return "Ready to recieve Command";
                }
                MessageCommunication.SendMessage(doctorHandler.sslStream, "this patient does not exist");
                return "Ready to recieve Command";
            }
            return "add failed message";
        }
    }


}
