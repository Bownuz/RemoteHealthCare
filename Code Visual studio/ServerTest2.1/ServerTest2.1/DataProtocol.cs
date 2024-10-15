using System.Net.Sockets;
using System.Text;

namespace Server.DataProtocol {
    class Messages {
        public static string ReciveMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                return stream.ReadLine();
            }
        }

        public static void SendMessage(TcpClient client, string message) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII, -1, true);
            {
                stream.WriteLine(message);
                stream.Flush();
            }
        }
    }

    internal struct ClientRecieveData
    {
        public String patientName { get; set; }
        public double BicycleSpeed {get; set; }
        public int Heartrate { get; set; }
        public DateTime dateTime { get; set; }

        public ClientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime)
        {
            this.patientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }

    internal struct ÇlientSendData
    { 
        public String Message { get; }
        public int Resistance { get;}

        public ÇlientSendData(string message, int resistance)
        {
            this.Message = message;
            this.Resistance = resistance;
        }
    }

    internal struct DoctorRecieveData { 
        public String Message { get; }
        public int Resistance { get; }

        public DoctorRecieveData(string message, int resistance)
        {
            this.Message = message;
            this.Resistance = resistance;
        }
    }

    internal struct DoctorSendData { 
        public String dataType { get; }
        public String clientData { get; }

        public DoctorSendData(string dataType, String data)
        {
            this.dataType = dataType;
            this.clientData = clientData;
        }
    }



}
