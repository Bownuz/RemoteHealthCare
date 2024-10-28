﻿using Server.DataStorage;
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
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            if(jsonRegex.IsMatch(input)) {
                Doctor doctor = JsonSerializer.Deserialize<Doctor>(input);

                if(doctorHandler.fileStorage.DoctorExists(doctor.DoctorID) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorPassword.Equals(doctor.DoctorPassword) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorName.Equals(doctor.DoctorName)
                ) {
                    doctorHandler.connectedDoctor = doctorHandler.fileStorage.getDoctor(doctor.DoctorID);
                    protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "This Login State Should Still be Added");
                }
                else {
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Incorrect Login");
                }
            }
        }
    }

    public class D_RecievingCommand : DoctorState {
        public D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            switch(input) {
                case "Retrieve Data":
                    protocol.ChangeState(new D_FetchingData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Which patient and date should data be retrieved from?");
                    break;
                case "Subscribe":
                    var addMessageData = new AddRemoveObserverMessage("Which patient should be subscribed to?", doctorHandler.fileStorage.PatientNamesToArray());
                    string addMessage = JsonSerializer.Serialize(addMessageData);

                    protocol.ChangeState(new D_Subscribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, addMessage);
                    break;
                case "Unsubscribe":
                    var removeMessageData = new AddRemoveObserverMessage("Which patient should be unsubscribed from?", doctorHandler.fileStorage.PatientNamesToArray());
                    string removeMessage = JsonSerializer.Serialize(removeMessageData);

                    protocol.ChangeState(new D_Unsubsribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, removeMessage);
                    break;
                case "Send Data":
                    protocol.ChangeState(new D_SendData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "What data should be sent?");
                    break;
                default:
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "This command is not valid.");
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                    break;
            }
        }
    }

    public class D_FetchingData : DoctorState {
        public D_FetchingData(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            if(!jsonRegex.IsMatch(input)) {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Add failed message");
                return;
            }

            try {
                var dataToFetch = JsonSerializer.Deserialize<DoctorFetchData>(input);
                var sessionToSend = doctorHandler.fileStorage.GetPatient(dataToFetch.PatientName).GetSession(dataToFetch.SessionDate);

                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, JsonSerializer.Serialize(sessionToSend));
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
            }
            catch(Exception ex) {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, ex.Message);
            }
        }
    }

    public class D_Subscribing : DoctorState {
        public D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            if(doctorHandler.fileStorage.PatientExists(input) && !doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.AddObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
            }
            else {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, $"Failed to add: {input} - This patient does not exist or is already subscribed.");
            }
        }
    }

    public class D_Unsubsribing : DoctorState {
        public D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            if(doctorHandler.fileStorage.PatientExists(input) && doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                doctorHandler.fileStorage.GetPatient(input).currentSession.RemoveObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
            }
            else {
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, $"Failed to remove: {input} - This patient does not exist or is not subscribed.");
            }
        }
    }

    public class D_SendData : DoctorState {
        public D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if(input.Equals("Quit Communication")) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Goodbye");
                doctorHandler.networkStream.Close();
            }

            if(jsonRegex.IsMatch(input)) {
                var message = JsonSerializer.Deserialize<DoctorDataMessage>(input);

                if(doctorHandler.fileStorage.PatientExists(message.PatientName)) {
                    doctorHandler.fileStorage.GetPatient(message.PatientName).currentSession.addMessage(input, CommunicationType.DOCTOR);
                    protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                }
                else {
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "This patient does not exist.");
                    MessageCommunication.SendMessage(doctorHandler.networkStream, "Ready to receive command");
                }
            }
            else {
                MessageCommunication.SendMessage(doctorHandler.networkStream, "Add failed message");
            }
        }
    }
}
