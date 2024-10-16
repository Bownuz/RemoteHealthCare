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
using System.Reflection;

namespace HardwareClientApplication {
    internal class ServerConnection {
        public static void HandleConnection(TcpClient tcpClient, DataHandler handler, Ergometer ergometer) {
            NetworkStream stream = tcpClient.GetStream();

            while (true) {
                Thread.Sleep(1000);
                Console.WriteLine(handler.printAsJson());
                WriteData(tcpClient, handler.printAsJson());
                if (stream.DataAvailable) {
                    string serverMessage = ReadTextMessage(tcpClient);
                    Console.WriteLine("hshsjjsjsjss");
                    if (!string.IsNullOrEmpty(serverMessage)) {
                        ProcessMessage(serverMessage, ergometer);
                    }
                }
            }
        }

        public static void WriteData(TcpClient client, String data) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.WriteLine(data);
            stream.Flush();
        }

        public static string ReadTextMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);

            return stream.ReadLine();
        }

        public static void ProcessMessage(String serverMessage, Ergometer ergometer) {
            if (serverMessage != null) {
                if (serverMessage.StartsWith("Resistance:")) {
                    ergometer.sendResistanceValueAsync(Convert.ToByte(serverMessage.Substring(11)));
                } else {
                    Console.WriteLine(serverMessage);
                }
                serverMessage = null;
            }
        }
    }
}
