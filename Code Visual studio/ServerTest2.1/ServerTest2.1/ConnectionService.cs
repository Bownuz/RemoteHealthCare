using DataProtocol;
using ServerTest2._1.DataStorage;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace ConnectionService
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
                Thread clientThread = new Thread(() => HandleClientThread(client));
                threads.Add(clientThread);
                clientThread.Start();

                //accepting docotr and making a new thread for this doctor
                TcpClient doctor = DoctorListener.AcceptTcpClient();
                Thread doctorThread = new Thread(() => HandleDoctorThread(doctor));
                threads.Add(doctorThread);
                doctorThread.Start();
            }


            void HandleClientThread(TcpClient client)
            {
                DataStorage data = new DataStorage();
                Session currentSession = new Session();

                while (true)
                {
                    if (client.Connected) {
                        string recieved;
                        if ((recieved = DataProtocol.Messages.ReciveMessage(client)) != null)
                        {
                            Console.WriteLine("Recived: {0}", recieved);
                            ClientRecieveData jsonData = JsonSerializer.Deserialize<ClientRecieveData>(recieved);
                            Console.WriteLine("Speed: " + jsonData.BicycleSpeed + "\nHeartrate: " + jsonData.Heartrate + "\nDate: " + jsonData.dateTime);
                            currentSession.addMessagesRecived(recieved);
                        }
                    }
                    break;
                }
            }

            void HandleDoctorThread(TcpClient doctor) { 
            
            
            }

        }
        
    }
}
