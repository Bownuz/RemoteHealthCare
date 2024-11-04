using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;

namespace Server.Patterns.State.DoctorStates {
    public class D_Welcome : DoctorState {
        public D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            protocol.ChangeState(new D_Login(protocol, doctorHandler));
            MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_enterLogin);
        }
    }

    public class D_Login : DoctorState {
        public D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler) { }

        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            if (isJson(input)) {
                DoctorInitialiseMessage messageContent = JsonSerializer.Deserialize<DoctorInitialiseMessage>(input);
                Doctor doctor = new Doctor(messageContent.DoctorID, messageContent.DoctorName, messageContent.DoctorPassword);

                if (!(doctorHandler.fileStorage.DoctorExists(doctor.DoctorID) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorPassword.Equals(doctor.DoctorPassword) &&
                    doctorHandler.fileStorage.getDoctor(doctor.DoctorID).DoctorName.Equals(doctor.DoctorName)
                )) {
                    MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_incorrectLogin);
                    return;
                }

                doctorHandler.connectedDoctor = doctorHandler.fileStorage.getDoctor(doctor.DoctorID);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_correctLogin);
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
            }
        }
    }


    public class D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(String input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            switch (input) {
                case ValidMessages.d_retrieveData:
                    protocol.ChangeState(new D_FetchingData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_retrieveDataResponse);
                    break;
                case ValidMessages.d_subscribe:
                    protocol.ChangeState(new D_Subscribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList(ValidMessages.d_subscribeResponse));
                    break;
                case ValidMessages.d_unsubscribe:
                    protocol.ChangeState(new D_Unsubsribing(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList(ValidMessages.d_unsubscribeResponse));
                    break;
                case ValidMessages.d_sendData:
                    protocol.ChangeState(new D_SendData(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_sendDataResponse);
                    break;
                case ValidMessages.d_startSession:
                    protocol.ChangeState(new D_StartingSession(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList(ValidMessages.d_startSessionResponse));
                    break;
                case ValidMessages.d_endSession:
                    protocol.ChangeState(new D_EndingSession(protocol, doctorHandler));
                    MessageCommunication.SendMessage(doctorHandler.networkStream, MessageWithPatientsList(ValidMessages.d_endSessionResponse));
                    break;
                default:
                    MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_commandInvalid);
                    MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
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
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!isJson(input)) {
                goto readyMessage;
            }

            try {
                var dataToFetch = JsonSerializer.Deserialize<DoctorFetchData>(input);
                var sessionToSend = doctorHandler.fileStorage.GetPatient(dataToFetch.PatientName).GetSession(dataToFetch.SessionDate);
                MessageCommunication.SendMessage(doctorHandler.networkStream, JsonSerializer.Serialize(sessionToSend));
            } catch (Exception ex) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ex.Message);
            }

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

    public class D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_patientNotExist);
                goto readyMessage;
            }

            if (doctorHandler.fileStorage.GetPatient(input).currentSession == null) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.p_noSessionActive);
                goto readyMessage;
            }

            if (doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_personAlreadySubscribed);
                goto readyMessage;
            }

            doctorHandler.fileStorage.GetPatient(input).currentSession.AddObserver(doctorHandler);

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

    public class D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_patientNotExist);
                goto readyMessage;
            }

            if (doctorHandler.fileStorage.GetPatient(input).currentSession == null) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.p_noSessionActive);
                goto readyMessage;
            }

            if (!doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_personNotSubscribed);
                goto readyMessage;
            }

            doctorHandler.fileStorage.GetPatient(input).currentSession.RemoveObserver(doctorHandler);

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

    public class D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!isJson(input)) {
                goto readyMessage;
            }

            DoctorDataMessage message = JsonSerializer.Deserialize<DoctorDataMessage>(input);
            if (!doctorHandler.fileStorage.PatientExists(message.PatientName)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_patientNotExist);
                goto readyMessage;
            }

            doctorHandler.fileStorage.GetPatient(message.PatientName).currentSession.addMessage(input, CommunicationType.DOCTOR);

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

    public class D_StartingSession(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_patientNotExist);
                goto readyMessage;
            }

            Patient patient = doctorHandler.fileStorage.GetPatient(input);


            patient.currentSession = new Session(DateTime.Now, patient.lastConnectedErgometer, patient.lastConnectedHeartrateMonitor);
            patient.sessions.Add(patient.currentSession);
            patient.currentSession.AddObserver(doctorHandler.fileStorage);
            patient.currentSession.AddObserver(doctorHandler);
            doctorHandler.fileStorage.SaveToFile();

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

    public class D_EndingSession(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler) {
        public override void CheckInput(string input) {
            if (input.Equals(ValidMessages.a_quit)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_goodbye);
                doctorHandler.networkStream.Close();
                return;
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            Patient patient = doctorHandler.fileStorage.GetPatient(input);


            if (patient.currentSession == null) {
                goto readyMessage;
            }

            if (!doctorHandler.fileStorage.PatientExists(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_patientNotExist);
                goto readyMessage;
            }


            patient.currentSession.sessionEnd = DateTime.Now;
            patient.currentSession = null;

            doctorHandler.fileStorage.SaveToFile();

            readyMessage: MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.d_readyToRecieve);
        }
    }

}
