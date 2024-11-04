using System;
using System.IO;
using System.Net.Sockets;
using System.Text;


namespace ClientApplication {
    public class MessageCommunication {
        public static string ReceiveMessage(NetworkStream networkStream) {
            var stream = new StreamReader(networkStream, Encoding.ASCII, true, 128);
            return stream.ReadLine();
        }

        public static void SendMessage(NetworkStream networkStream, string message) {
            var stream = new StreamWriter(networkStream, Encoding.ASCII, 128, true);
            stream.WriteLine(message);
            stream.Flush();
        }

    }

    public static class ValidMessages {
        public const String p_welcome = "Welcome Client";
        public const String p_readyToRecieve = "Ready to recieve data";
        public const String p_noSessionActive = "No current Session Active";

        public const String a_patientNotExist = "This patient does not exist";
        public const String a_notJson = "This message was not a Json String";
        public const String a_goodbye = "Goodbye";
        public const String a_quit = "Quit Communication";
    }
    public struct PatientInitialisationMessage {
        public String ClientName { get; set; }
        public String ConnectedErgometer { get; set; }
        public String ConnectedHeartRateMonitor { get; set; }
        public DateTime dateTime { get; set; }

        public PatientInitialisationMessage(string clientName, string connectedErgometer, string connectedHeartRateMonitor, DateTime dateTime) {
            ClientName = clientName;
            ConnectedErgometer = connectedErgometer;
            ConnectedHeartRateMonitor = connectedHeartRateMonitor;
            this.dateTime = dateTime;
        }
    }

    public struct PatientSendData {
        public string clientName { get; set; }
        public int bicycleSpeed { get; set; }
        public int heartrate { get; set; }
        public DateTime dateTime { get; set; }

        public PatientSendData(string clientName, int bicycleSpeed, int heartrate, DateTime dateTime) {
            this.clientName = clientName;
            this.bicycleSpeed = bicycleSpeed;
            this.heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }
}
