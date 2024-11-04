using System.Net.Sockets;
using System.Text;

namespace Server.ThreadHandlers {
    public class MessageCommunication {
        public static string ReceiveMessage(NetworkStream networkStream) {
            var stream = new StreamReader(networkStream, Encoding.ASCII, true, 128);
            String line = stream.ReadLine();
            return line;
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

        public const String d_readyToRecieve = "Ready to receive command";
        public const String d_commandInvalid = "This command is not valid.";

        public const String d_enterLogin = "Enter Login Data";
        public const String d_incorrectLogin = "Incorrect Login";
        public const String d_correctLogin = "Login Successful";

        public const String d_retrieveData = "Retrieve Data";
        public const String d_subscribe = "Subscribe";
        public const String d_unsubscribe = "Unsubscribe";
        public const String d_sendData = "Send Data";
        public const String d_startSession = "Start Session";
        public const String d_endSession = "End Session";
        public const String d_retrieveDataResponse = "Which patient and date should data be retrieved from?";
        public const String d_subscribeResponse = "Which patient should be subscribed to?";
        public const String d_unsubscribeResponse = "Which patient should be unsubscribed from?";
        public const String d_sendDataResponse = "What data should be sent?";
        public const String d_startSessionResponse = "Which Patient should a session start?";
        public const String d_endSessionResponse = "Which Patient should a session End?";

        public const String d_personAlreadySubscribed = "This person is already subscribed to";
        public const String d_personNotSubscribed = "This person is not subscribed to";

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

    public struct PatientRecieveData {
        public String patientName { get; set; }
        public double BicycleSpeed { get; set; }
        public int Heartrate { get; set; }
        public DateTime dateTime { get; set; }

        public PatientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime) {
            this.patientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }

    public struct DoctorMessageWithList {
        public String Message { get; set; }
        public String[] PatientNames { get; set; }

        public DoctorMessageWithList(string message, string[] patientNames) {
            Message = message;
            PatientNames = patientNames;
        }
    }

    public struct DoctorInitialiseMessage {
        public String DoctorID { get; set; }
        public String DoctorName { get; set; }
        public String DoctorPassword { get; set; }

        public DoctorInitialiseMessage(string DoctorID, string DoctorName, string DoctorPassword) {
            this.DoctorID = DoctorID;
            this.DoctorName = DoctorName;
            this.DoctorPassword = DoctorPassword;
        }
    }

    public struct DoctorDataMessage {
        public String PatientName { get; set; }
        public String Message { get; set; }
        public int newResistance { get; set; }

        public DoctorDataMessage(string patientName, string message, int newResistance) {
            PatientName = patientName;
            Message = message;
            this.newResistance = newResistance;
        }
    }

    public struct DoctorFetchData {
        public String PatientName { get; set; }
        public DateTime SessionDate { get; set; }

        public DoctorFetchData(string patientName, DateTime sessionDate) {
            PatientName = patientName;
            SessionDate = sessionDate;
        }
    }

}

