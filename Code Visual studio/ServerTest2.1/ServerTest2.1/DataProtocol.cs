using System.Net.Sockets;
using System.Text;

namespace Server.DataProtocol {
    internal class Protocol
    {
        internal void processMessage(String incommingMessage)
        {
            throw new NotImplementedException();
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
