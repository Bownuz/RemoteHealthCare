using Server.Threads;
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

                //accepting client and making a new thread for this client
                TcpClient client = Clientlistener.AcceptTcpClient();
                Thread clientThread = new Thread(() => ClientThread.HandleClientThread(client));
                threads.Add(clientThread);
                clientThread.Start();

                //accepting doctor and making a new thread for this doctor
                TcpClient doctor = DoctorListener.AcceptTcpClient();
                Thread doctorThread = new Thread(() => DoctorThread.HandleDoctorThread(doctor) );
                threads.Add(doctorThread);
                doctorThread.Start();
            }
        }
    }
}
