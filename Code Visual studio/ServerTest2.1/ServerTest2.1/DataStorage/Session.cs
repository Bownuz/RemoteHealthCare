using Server.Patterns.Observer;
using Server.ThreadHandlers;
using System.Runtime.Intrinsics.Arm;


namespace Server.DataStorage
{
    public class Session : Subject
    {
        DateTime sessionStart { get; }
        DateTime sessionEnd { get; }
        String ergometerName { get; }
        String heartRateMonitorName { get; }
        List<string> clientMessages { get; }
        List<string> doctorMessages { get; }

        public Session(DateTime sessionStart, string ergometerName, string heartRateMonitorName)
        {
            clientMessages = new List<string>();
            doctorMessages = new List<string>();
            this.sessionStart = sessionStart;
            this.ergometerName = ergometerName;
            this.heartRateMonitorName = heartRateMonitorName;
        }

        public void addMessage(String message, CommunicationType messageType)
        {
            switch (messageType)
            {
                case CommunicationType.PATIENT:
                    clientMessages.Add(message);
                    break;
                case CommunicationType.DOCTOR:
                    doctorMessages.Add(message);
                    break;
                default:
                    throw new Exception("this messageType is not supported");
            }

            UpdateObservers(messageType, this);
        }

        public String getLatestMessage(CommunicationType messageType)
        {
            switch (messageType)
            {
                case CommunicationType.PATIENT:
                    if (clientMessages.Count > 0)
                        return clientMessages[^1];
                    return "this list is empty";
                case CommunicationType.DOCTOR:
                    if (doctorMessages.Count > 0)
                        return doctorMessages[^1];
                    return "this list is empty";
                default:
                    throw new Exception("this messageType is not supported");


            }

        }
    }
}

