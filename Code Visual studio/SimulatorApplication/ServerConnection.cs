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
using System.Reflection;
using SimulatorApplication;

namespace HardwareClientApplication {
    internal class ServerConnection {
        public static void HandleConnection(TcpClient tcpClient, Simulator simulator) {
            NetworkStream stream = tcpClient.GetStream();

            while (true) {
                string data = simulator.getData();
                Thread.Sleep(1000);
                WriteData(tcpClient, data);
            }
        }

        public static void WriteData(TcpClient client, String data) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.WriteLine(data);
            stream.Flush();
        }
    }
}
