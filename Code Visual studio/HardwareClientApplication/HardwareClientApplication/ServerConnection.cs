using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace HardwareClientApplication {
    internal class ServerConnection {
        public static void HandleConnection() {
            TcpClient client = new TcpClient("145.49.11.240", 4789);
            while(true) {
                WriteTextMessage(client, "Test");
                WriteTextMessage(client, "Bye");

                if (ReadTextMessage(client) == "Bye")
                    break;
            }
            client.Close();
        }

        public static void WriteTextMessage(TcpClient client, string message) {
            Dictionary<String, String> test = new Dictionary<String, String fietsSnelheid>;
            String sendJsonMessage = JsonSerializer.Serialize(csn);
            Console.WriteLine(sendJsonMessage);
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            {
                stream.WriteLine(sendJsonMessage);
                stream.Flush();
            }
        }

        public static string ReadTextMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                return stream.ReadLine();
            }
        }
    }
}
