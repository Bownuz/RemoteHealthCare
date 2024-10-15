namespace Server.DataStorage
{
    class Session
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
        }
    }

    internal enum ClientType
    {
        CLIENT,
        DOCTOR
    }
}
