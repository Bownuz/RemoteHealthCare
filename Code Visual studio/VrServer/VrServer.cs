using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace VrServer {
    class VrServer {
        private static Socket? client;

        private static void SendJsonObjectFromFile(string filename) {
            byte[] jsonObject = File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\..\\json_files\\{filename}");
            byte[] sizeJsonObject = BitConverter.GetBytes(jsonObject.Length);

            client.Send(sizeJsonObject);
            client.Send(jsonObject);
        }

        private static void SendJsonObjectFromBytes(byte[] jsonObject) {
            byte[] sizeJsonObject = BitConverter.GetBytes(jsonObject.Length);

            client.Send(sizeJsonObject);
            client.Send(jsonObject);
        }

        private static string RecieveJsonObjectFromFile() {
            byte[] sizeIncomingMessage = new byte[4];
            int byteSizeIncomingMessage = client.Receive(sizeIncomingMessage);
            int sizeMessage = BitConverter.ToInt32(sizeIncomingMessage);
            Console.WriteLine("Message size from Server -> {0}", sizeMessage);

            byte[] message = new byte[sizeMessage];
            int byteMessage = client.Receive(message);
            Console.WriteLine("Message from Server -> {0}",
              Encoding.ASCII.GetString(message, 0, byteMessage));
            return Encoding.ASCII.GetString(message);
        }

        public static void Main(string[] args) {
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6666);

            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(localEndPoint);

            SendJsonObjectFromFile("session_list.json");
            string ms = RecieveJsonObjectFromFile();
            JsonDocument jsonDoc = JsonDocument.Parse(ms);
            string sessionID = jsonDoc.RootElement.GetProperty("data")[0].GetProperty("id").GetString();
            SendJsonObjectFromBytes(Encoding.UTF8.GetBytes($"{{\"id\": \"tunnel/create\", \"data\": {{\"session\": \"{sessionID}\", \"key\": \"\"}}}}"));

            RecieveJsonObjectFromFile();

            string filename = "";
            for (; ; ) {
                Console.Write("File name of Json object to send: ");
                filename = Console.ReadLine();
                if (filename.Equals(""))
                    break;

                SendJsonObjectFromFile(filename);
                RecieveJsonObjectFromFile();
            }
            client.Close();
        }
    }
}