using System;

namespace ServerTest2._1.DataStorage {
    internal class Command {
        public string CommandType { get; set; }
        public string ClientId { get; set; }
        public string Message { get; set; }
        public int Resistance { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Command(string commandType, string clientId, string message = null, int resistance = 0, DateTime startTime = default, DateTime endTime = default) {
            CommandType = commandType;
            ClientId = clientId;
            Message = message;
            Resistance = resistance;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
