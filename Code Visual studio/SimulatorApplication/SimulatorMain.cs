using HardwareClientApplication;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimulatorApplication {
    class SimulatorMain {
        static async Task Main(string[] args) {
            Simulator simulator = new Simulator();
            TcpListener listener = new TcpListener(IPAddress.Any, 4788);
            listener.Start();
            Console.WriteLine("Simulator is listening on port 4788...");
            simulator.StartSimulation();

            while (true) {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, simulator));
                connectionThread.Start();
                Console.WriteLine("Verbonden met de client.");
            }
        }
    }
}
