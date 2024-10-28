using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace Server.ThreadHandlers {
    public class MessageCommunication {
        public static string ReceiveMessage(NetworkStream networkStream) {
            var stream = new StreamReader(networkStream, Encoding.ASCII);
            return stream.ReadLine();
        }

        public static void SendMessage(NetworkStream networkStream, string message) {
            var stream = new StreamWriter(networkStream, Encoding.ASCII, -1, true);
            stream.WriteLine(message);
            stream.Flush();
        }
    }
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

public struct AddRemoveObserverMessage {
    public String Message { get; set; }
    public String[] PatientNames { get; set; }

    public AddRemoveObserverMessage(string message, string[] patientNames) {
        Message = message;
        PatientNames = patientNames;
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

