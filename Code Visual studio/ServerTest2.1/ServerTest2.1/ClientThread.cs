using System.Net.Sockets;
using System.Text.Json;
using Server.DataStorage;
using Server.DataProtocol;
using Server.ObserverPattern;

namespace Server.Threads
{
    public class ClientThread : Subject
    {
        

        public static void HandleClientThread(TcpClient client)
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
                    }
                }
                break;
            }
        }
    }
}
