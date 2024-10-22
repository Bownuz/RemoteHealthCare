using Server.Patterns.Observer;

namespace Server.DataStorage
{
    class Session : Subject
    {
        DateTime sessionStart { get; }
        DateTime sessionEnd { get; }
        String ergometerName { get; }
        string heartRateMonitorName { get; }
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

        public void addMessage(String message, ClientType messageType)
        {
            switch (messageType)
            {
                case ClientType.CLIENT:
                    clientMessages.Add(message);
                    break;
                case ClientType.DOCTOR:
                    doctorMessages.Add(message);
                    break;
                default:
                    throw new Exception("this messageType is not supported");
            }

            UpdateAll(messageType);
        }

        public String getLatestMessage(ClientType messageType) { 
            switch(messageType)
            { 
                case ClientType.CLIENT:
                    if (doctorMessages.Count > 0)
                        return clientMessages[^1];
                    return "this list is empty";
                case ClientType.DOCTOR:
                    if (clientMessages.Count > 0)
                        return doctorMessages[^1];
                    return "this list is empty";
                default:
                    throw new Exception("this messageType is not supported");
                            
            
            }
        }
    }


    public enum ClientType
    {
        CLIENT,
        DOCTOR
    }

}
