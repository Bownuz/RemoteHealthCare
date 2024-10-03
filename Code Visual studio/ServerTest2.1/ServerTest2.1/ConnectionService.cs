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
                //TcpClient client = obj as TcpClient;

                while (true)
                {
                    string recived = DataProtocol.Messages.ReciveMessage(client);
                    Console.WriteLine("Recived: {0}", recived);
                    //DataProtocol.Messages.SendMessage(client, "Message: " + recived + " Recived");
                    JsonData jsonData = JsonSerializer.Deserialize<JsonData>(recived);


                    Console.WriteLine("Speed: " + jsonData.BicycleSpeed + "\nHeartrate: " + jsonData.Heartrate + "\nDate: " + jsonData.dateTime);

                }
            }
        }
        internal struct JsonData
        {
            public double BicycleSpeed { get; set; }
            public int Heartrate { get; set;}
            public DateTime dateTime { get; set; }

            public JsonData(double bicycleSpeed, int heartrate, DateTime dateTime)
            {
                BicycleSpeed = bicycleSpeed;
                Heartrate = heartrate;
                this.dateTime = dateTime;
            }
        }
    }
}
