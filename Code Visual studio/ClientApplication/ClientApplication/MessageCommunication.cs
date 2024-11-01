using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;


namespace ClientApplication {
    public class MessageCommunication {
        public static async Task<string> RecieveMessage(TcpClient client) {
            try {
                var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
                Console.WriteLine("Wachten op bericht...");
                string message = await stream.ReadLineAsync();
                Console.WriteLine("Bericht ontvangen: " + message);
                return message;
            }
            catch (IOException ex) {
                Console.WriteLine("Fout bij ontvangen bericht: " + ex.Message);
                return null;
            }
        }

        public static async Task SendMessage(TcpClient client, string message) {
            //MessageBox.Show(message);
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            await stream.WriteLineAsync(message);
            await stream.FlushAsync();
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
