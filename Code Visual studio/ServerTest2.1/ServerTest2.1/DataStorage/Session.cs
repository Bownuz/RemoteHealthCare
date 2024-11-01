using Server.Patterns.Observer;
using Server.ThreadHandlers;


namespace Server.DataStorage {
    public class Session : Subject {
        public DateTime sessionStart { get; set; }
        public DateTime sessionEnd { get; set; }
        public String ergometerName { get; set; }
        public String heartRateMonitorName { get; set; }
        public List<string> clientMessages { get; set; }
        public List<string> doctorMessages { get; set; }

        public Session(DateTime sessionStart, string ergometerName, string heartRateMonitorName) {
            clientMessages = new List<string>();
            doctorMessages = new List<string>();
            this.sessionStart = sessionStart;
            this.ergometerName = ergometerName;
            this.heartRateMonitorName = heartRateMonitorName;
        }

        public void addMessage(String message, CommunicationType messageType) {
            switch (messageType) {
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

        public String getLatestMessage(CommunicationType messageType) {
            switch (messageType) {
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

