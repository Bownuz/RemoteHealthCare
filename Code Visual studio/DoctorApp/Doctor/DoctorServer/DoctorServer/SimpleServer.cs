using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DoctorApp {
    class SimpleServer {
        static void Main(string[] args) {
            // Start de server op poort 4790
            TcpListener server = new TcpListener(IPAddress.Any, 4790);
            server.Start();
            Console.WriteLine("Server gestart. Wachten op verbinding...");

            // Accepteer verbinding van de dokter-client
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client verbonden!");

            NetworkStream stream = client.GetStream();

            // Ontvang inloggegevens van de dokter
            byte[] buffer = new byte[1024];
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            string credentials = Encoding.ASCII.GetString(buffer, 0, byteCount);
            Console.WriteLine("Inloggegevens ontvangen: " + credentials);

            // Valideer de inloggegevens (voor deze test zijn alle inlogs succesvol)
            byte[] successMessage = Encoding.ASCII.GetBytes("Success");
            stream.Write(successMessage, 0, successMessage.Length);
            Console.WriteLine("Login succesvol!");

            // Stuur regelmatig gegevens naar de dokter-client
            while(true) {
                string dataToSend = $"Speed: {new Random().Next(10, 30)} km/h, Heartrate: {new Random().Next(60, 100)} bpm";
                byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Data naar client gestuurd: " + dataToSend);

                // Wacht 2 seconden voor de volgende update
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}

