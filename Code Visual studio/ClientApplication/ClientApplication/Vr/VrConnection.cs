using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication {
    internal class VrConnection {
        private static Socket client;
        private static Thread listener;
        private static string tunnelID;
        private static List<string> responses;
        private static List<string> responsesFromTunnel;
        private static bool DebugMessages = true;

        public static void NewConnection(string ipAdress) {
            NewConnection(ipAdress, false);
        }

        public static void NewConnection(string ipAdress, bool AutoCreateTunnel) {
            if (client != null)
                CloseConnection();

            IPAddress ipAddr = IPAddress.Parse(ipAdress);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6666);

            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(localEndPoint);

            if (AutoCreateTunnel)
                VrConnection.AutoCreateTunnel();

            NewListener();
            responses = new List<string>();
            responsesFromTunnel = new List<string>();
        }

        public static void CloseConnection() {
            AbortListener();
            client.Close();
            tunnelID = null;
        }

        private static void ResponseListener() {
            try {
                for (; ; ) {
                    string resp = RecieveJsonObjectFromServer();
                    responses.Add(resp);
                    JsonDocument rsp = JsonDocument.Parse(resp);
                    if (rsp.RootElement.GetProperty("id").GetString().Equals("tunnel/send")) {
                        responsesFromTunnel.Add(resp);
                    }
                }
            }
            catch (ThreadAbortException) {
                Console.WriteLine("Thread '{0}' aborted.", Thread.CurrentThread.Name);
            }
        }

        public static string GetMostRecentResponseUsingString(string searchPattern) {
            for (int i = responses.Count-1; i > 0; i--) {
                if (JsonDocument.Parse(responses[i]).RootElement.GetProperty("data").GetProperty("id").GetString().Equals(searchPattern))
                    return responses[i];
            }
            return null;
        }

        public static string GetMostRecentTunnelResponseUsingString(string searchPattern) {
            for (int i = responsesFromTunnel.Count-1; i > -1; i--) {
                if (JsonDocument.Parse(responsesFromTunnel[i]).RootElement.GetProperty("data").GetProperty("data").GetProperty("id").GetString().Equals(searchPattern))
                    return responsesFromTunnel[i];
            }
            return null;
        }

        private static void AutoCreateTunnel() {
            SendJsonObjectFromFile("session_list.json");
            JsonDocument sessionList = JsonDocument.Parse(RecieveJsonObjectFromServer());
            string sessionID = sessionList.RootElement.GetProperty("data")[sessionList.RootElement.GetProperty("data").GetArrayLength()-1].GetProperty("id").GetString();
            SendJsonObjectFromBytes(Encoding.UTF8.GetBytes($"{{ \"id\": \"tunnel/create\", \"data\": {{\"session\": \"{sessionID}\", \"key\": \"\"}}}}"));
            JsonDocument tunnelIdDoc = JsonDocument.Parse(RecieveJsonObjectFromServer());
            tunnelID = tunnelIdDoc.RootElement.GetProperty("data").GetProperty("id").GetString();
        }

        private static void AbortListener() {
            listener.Abort();
        }

        private static void NewListener() {
            listener = new Thread(ResponseListener);
            listener.Start();
        }

        public static void CreateTunnel() {
            if (client == null)
                return;

            AbortListener();
            TunnelCreater();
            NewListener();
        }

        private static void TunnelCreater() {
            SendJsonObjectFromFile("session_list.json");
            JsonDocument sessionList = JsonDocument.Parse(RecieveJsonObjectFromServer());
            string sessionID = sessionList.RootElement.GetProperty("data")[sessionList.RootElement.GetProperty("data").GetArrayLength()-1].GetProperty("id").GetString();
            SendJsonObjectFromBytes(Encoding.UTF8.GetBytes($"{{\"id\": \"tunnel/create\", \"data\": {{\"session\": \"{sessionID}\", \"key\": \"\"}}}}"));
            JsonDocument tunnelIdDoc = JsonDocument.Parse(RecieveJsonObjectFromServer());
            tunnelID = tunnelIdDoc.RootElement.GetProperty("data").GetProperty("id").GetString();
        }

        public static void SendJsonObjectViaTunnelFromFile(string filename) {
            SendJsonObjectViaTunnelFromBytes(File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\Vr\\json_files\\{filename}"));
        }

        public static void SendJsonObjectViaTunnelFromBytes(byte[] jsonObject) {
            if (tunnelID == null)
                return;
            SendJsonObjectFromBytes(Encoding.ASCII.GetBytes($"{{\"id\" : \"tunnel/send\", \"data\" : {{ \"dest\" : \"{tunnelID}\", \"data\" : {Encoding.ASCII.GetString(jsonObject)} }} }}"));
        }

        public static void SendJsonObjectFromFile(string filename) {
            if (client == null)
                return;

            byte[] jsonObject = File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\Vr\\json_files\\{filename}");
            byte[] sizeJsonObject = BitConverter.GetBytes(jsonObject.Length);

            client.Send(sizeJsonObject);
            client.Send(jsonObject);
        }

        public static void SendJsonObjectFromBytes(byte[] jsonObject) {
            if (client == null)
                return;

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
