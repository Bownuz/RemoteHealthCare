using DataProtocol;
using ServerTest2._1.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerTest2._1
{
    internal class ClientThread
    {
        String messageToDoctor { get; set; }

        void HandleClientThread(TcpClient client)
        {
            DataStorage.DataStorage data = new DataStorage.DataStorage();
            Session currentSession = new Session();
            while (true)
            {
                if (client.Connected)
                {
                    string recieved;
                    if ((recieved = DataProtocol.Messages.ReciveMessage(client)) != null)
                    {
                        Console.WriteLine("Recived: {0}", recieved);
                        ClientRecieveData jsonData = JsonSerializer.Deserialize<ClientRecieveData>(recieved);
                        Console.WriteLine("Speed: " + jsonData.BicycleSpeed + "\nHeartrate: " + jsonData.Heartrate + "\nDate: " + jsonData.dateTime);
                        currentSession.addMessagesRecived(recieved);
                        messageToDoctor = recieved;
                    }
                }
                break;
            }
        }
    }
}
