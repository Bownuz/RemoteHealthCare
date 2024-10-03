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
using ConnectionImplemented;

namespace HardwareClientApplication {
    internal class ServerConnection {
        public static void HandleConnection(TcpClient tcpClient) {
            
            while (true) {

                if (ReadTextMessage(tcpClient) == "Bye"){
                    break;
                }
            }
        }

        public static void WriteData(TcpClient client, Dictionary<String, String> data) {
            String sendJsonMessage = JsonSerializer.Serialize(data);
            Console.Write(sendJsonMessage);
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
