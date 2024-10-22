using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Server.ThreadHandlers
{
	public class MessageCommunication
	{
        public static string ReciveMessage(TcpClient client)
        {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                return stream.ReadLine();
            }
        }

        public static void SendMessage(TcpClient client, string message)
        {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII, -1, true);
            {
                stream.WriteLine(message);
                stream.Flush();
            }
        }

    }

    public struct PatientInitialisationMessage
    {
        public String ClientName { get; set; }
        public String ConnectedErgometer { get; set; }
        public String ConnectedHeartRateMonitor { get; set; }
        public DateTime dateTime { get; set; }

        public PatientInitialisationMessage(string clientName, string connectedErgometer, string connectedHeartRateMonitor, DateTime dateTime)
        {
            ClientName = clientName;
            ConnectedErgometer = connectedErgometer;
            ConnectedHeartRateMonitor = connectedHeartRateMonitor;
            this.dateTime = dateTime;
        }
    }

    public struct PatientRecieveData
    {
        public String patientName { get; set; }
        public double BicycleSpeed { get; set; }
        public int Heartrate { get; set; }
        public DateTime dateTime { get; set; }

        public PatientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime)
        {
            this.patientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }

}

