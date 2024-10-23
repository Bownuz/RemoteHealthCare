using Server.ThreadHandlers;
using System;
using System.Text.Json;

namespace Server.Patterns.State.DoctorStates
{
    public class D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            protocol.ChangeState(new D_Login(protocol, doctorHandler));
            return "Enter Login Data";
        }
    }

    public class D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }

            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            return "This Login State Should Still be Added";
        }
    }

    public class D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
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
                    return "this Command is not Valid \n" +
                        "Ready to recieve Command";
            }
        }
    }


    public class D_FetchingData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }
            throw new System.NotImplementedException();
        }
    }
    public class D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }

            if (doctorHandler.fileStorage.PatientExists(input) && !doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler)) { 
                doctorHandler.fileStorage.GetPatient(input).currentSession.AddObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "Ready to recieve Command";
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            return $"Failed to add : {input} : this patient does not exist or this Client was Subscribed to this patient\n" +
                $"Ready to recieve Command";
        }
    }

    public class D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }

            if (doctorHandler.fileStorage.PatientExists(input) && doctorHandler.fileStorage.GetPatient(input).currentSession.observers.Contains(doctorHandler))
            {
                doctorHandler.fileStorage.GetPatient(input).currentSession.RemoveObserver(doctorHandler);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "Ready to recieve Command";
            }
            protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
            return $"Failed to Remove : {input} : this patient does not exist or This Client was not subscribed to this patient \n" +
                $"Ready to recieve Command";

        }
    }

    public class D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }

            //TODO: add Regex to find wether or not message Confines to Json Format
            DoctorDataMessage message = JsonSerializer.Deserialize<DoctorDataMessage>(input);
            
            if (doctorHandler.fileStorage.PatientExists(message.PatientName))
            {
                doctorHandler.fileStorage.GetPatient(message.PatientName).currentSession.addMessage(input, CommunicationType.DOCTOR);
                protocol.ChangeState(new D_RecievingCommand(protocol, doctorHandler));
                return "Ready to recieve Command";
            }
            return "this patient does not exist \n" +
                "Ready to recieve Command";
        }
    }


}
