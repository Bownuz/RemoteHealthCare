using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;  
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using ServerTest2._1;

namespace ConnectionService
{
    class ConnectionService
    {
        static void Main(string[] args)
        {
            //json uitlezen
            //String jsonTest = "{\"BicycleSpeed\":15,\"Heartrate\":64,\"dateTime\":\"2024-10-03T17:03:05.7715391+02:00\"}";
            //JsonData jsonData = JsonSerializer.Deserialize<JsonData>(jsonTest);

            //Console.WriteLine(jsonTest);
            //Console.WriteLine(jsonData.dateTime);


            //Console.WriteLine(jsonTest);

            //tofile
            //Session session = new Session();
            //session.addMessagesSend("Heart rate ofzo");
            //session.addMessagesRecived("ga langzamer fietsen ofz");
            //session.addMessagesRecived("DoeIS");

            //Person testPerson1 = new Person("testpersoon 1");
            //testPerson1.addSessions(session);

            //DataStorage.DataStorage.SaveToFile("" + testPerson1);

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
                //client.Close();
                //Console.WriteLine("Connection closed");
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
