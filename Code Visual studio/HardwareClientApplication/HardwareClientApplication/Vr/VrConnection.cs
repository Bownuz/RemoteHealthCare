using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareClientApplication {
    internal class VrConnection {
        private static Socket client;
        private static Thread listener;
        private static bool DebugMessages = true;
        private static bool AutoCreateTunnel = true;

        public static void NewConnection(string ipAdress) {
            if (client != null)
                CloseConnection();

            IPAddress ipAddr = IPAddress.Parse(ipAdress);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6666);

            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(localEndPoint);

            listener = new Thread(ResponseListener);
            listener.Start();

            if (AutoCreateTunnel)
                CreateTunnel();
        }

        public static void CloseConnection() {
            listener.Abort();
            client.Close();
        }

        private static void ResponseListener() {
            for (; ; ) {
                RecieveJsonObjectFromServer();
            }
        }

        private static void CreateTunnel() {
            SendJsonObjectFromBytes(Encoding.ASCII.GetBytes("{\"id\":\"session/list\"}"));
            SendJsonObjectFromFile("session_list.json");
            string ms = RecieveJsonObjectFromServer();
            JsonDocument jsonDoc = JsonDocument.Parse(ms);
            string sessionID = jsonDoc.RootElement.GetProperty("data")[0].GetProperty("id").GetString();
            SendJsonObjectFromBytes(Encoding.UTF8.GetBytes($"{{\"id\": \"tunnel/create\", \"data\": {{\"session\": \"{sessionID}\", \"key\": \"\"}}}}"));
        }

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

        private static string RecieveJsonObjectFromServer() {
            byte[] sizeIncomingMessage = new byte[4];
            int byteSizeIncomingMessage = client.Receive(sizeIncomingMessage);
            int sizeMessage = BitConverter.ToInt32(sizeIncomingMessage, 0);

            byte[] message = new byte[sizeMessage];
            int byteMessage = client.Receive(message);
            if (DebugMessages) {
                Console.WriteLine("Message size from Server -> {0}", sizeMessage);
                Console.WriteLine("Message from Server -> {0}",
                  Encoding.ASCII.GetString(message, 0, byteMessage));
            }
            return Encoding.ASCII.GetString(message);
        }
    }
}
