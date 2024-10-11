using System;
using System.Collections.Generic;
using System.Linq;
using ClientData;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataProtocol {
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
        public String patientName { get;}
        public double BicycleSpeed {get;}
        public int Heartrate { get; }
        public DateTime DateTime { get;}

        public ClientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime)
        {
            this.patientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            DateTime = dateTime;
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
