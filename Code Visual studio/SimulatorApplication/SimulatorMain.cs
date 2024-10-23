using HardwareClientApplication;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimulatorApplication {
    class SimulatorMain {
        static async Task Main(string[] args) {
            Simulator simulator = new Simulator();
            TcpListener listener = new TcpListener(IPAddress.Any, 4790);
            listener.Start();
            Console.WriteLine("Simulator is listening on port 4790...");
            //bool connected = false;

            // Start de simulatie
            simulator.StartSimulation();

            //while (!connected) {
            while (true) {
                //if (listener.Pending()) {
                    //try {
                    // Probeer verbinding te maken met de client
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Client connected.");
                    Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, simulator));
                    connectionThread.Start();
                    //connected = true; // Verbinding is gelukt
                    Console.WriteLine("Verbonden met de client.");
                //}
                //catch (SocketException) {
                //    // Verbinding mislukt, probeer opnieuw na een pauze
                //    Console.WriteLine("Kan geen verbinding maken met de client. Probeer opnieuw...");
                //    await Task.Delay(5000); // Wacht 5 seconden voordat je het opnieuw probeert
                //}
            }
        }
    }
}
