using System.Net.Sockets;
using System.Text;


namespace Server.ThreadHandlers
{
    internal class MessageCommunication
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

    public struct ClientInitialisationMessage
    {
        public String ClientName { get; set; }
        public String ConnectedErgometer {  get; set; }
        public String ConnectedHeartRateMonitor {  get; set; }
        public DateTime dateTime {  get; set; }

        public ClientInitialisationMessage(string clientName, string connectedErgometer, string connectedHeartRateMonitor, DateTime dateTime)
        {
            ClientName = clientName;
            ConnectedErgometer = connectedErgometer;
            ConnectedHeartRateMonitor = connectedHeartRateMonitor;
            this.dateTime = dateTime;
        }
    }

    internal struct ClientHealthData
    {
        public double BicycleSpeed { get; }
        public int Heartrate { get; }
        public DateTime dateTime { get; }

        public ClientHealthData(double bicycleSpeed, int heartrate, DateTime dateTime)
        {
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }

}
