using System.Net.Sockets;
using System.Text.Json;
using Server.DataStorage;
using Server.DataProtocol;
using ServerTest2._1.Patterns.Observer;

namespace Server.ThreadHandlers
{
    public class ClientThread : Subject
    {


        public void HandleThread(TcpClient client)
        {
            DataStorage.FileStorage data = new DataStorage.FileStorage();
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
                        NotifyAll();
                    }
                }
                break;
            }
        }
    }
}
