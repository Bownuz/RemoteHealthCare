using DataProtocol;
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

            TcpListener listener = new TcpListener(IPAddress.Any, 4789);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Starting up server and waiting for connections.....");

                //accepting client and making a new thread for this client
                TcpClient client = listener.AcceptTcpClient();

                Thread thread = new Thread(() => HandleClientThread(client));
                threads.Add(thread);
                thread.Start();
            }


            void HandleClientThread(TcpClient client)
            {
                while (true)
                {
                    string recived = DataProtocol.Messages.ReciveMessage(client);
                    Console.WriteLine("Recived: {0}", recived);
                    //DataProtocol.Messages.SendMessage(client, "Message: " + recived + " Recived");
                    ClientRecieveData jsonData = JsonSerializer.Deserialize<ClientRecieveData>(recived);


                    Console.WriteLine("Speed: " + jsonData.BicycleSpeed + "\nHeartrate: " + jsonData.Heartrate + "\nDate: " + jsonData.DateTime);

                }
            }
        }
        
    }
}
