using Server.ThreadHandlers;
using System.Net;
using System.Net.Sockets;

namespace Server.ConnectionService
{
    class ConnectionService
    {
        static void Main(string[] args)
        {
            //server
            List<Thread> threads = new List<Thread>();

            TcpListener Clientlistener = new TcpListener(IPAddress.Any, 4789);
            TcpListener DoctorListener = new TcpListener(IPAddress.Any, 4790);
            Clientlistener.Start();
            DoctorListener.Start();

            while (true)
            {
                Console.WriteLine("Starting up server and waiting for connections.....");

                if (Clientlistener.Pending())
                {
                    TcpClient client = Clientlistener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    threads.Add(clientThread);
                    clientThread.Start();
                }

                if (DoctorListener.Pending())
                {
                    TcpClient doctor = DoctorListener.AcceptTcpClient();
                    Thread doctorThread = new Thread(() => HandleDoctor(doctor));
                    threads.Add(doctorThread);
                    doctorThread.Start();
                }
            }
        }

        static void HandleClient(TcpClient client) {
            ClientThread clientThread = new ClientThread();
            clientThread.HandleThread(client);
        }

        static void HandleDoctor(TcpClient doctor) {
            DoctorThread doctorThread = new DoctorThread();
            doctorThread.HandleThread(doctor);
        }

    }
}
