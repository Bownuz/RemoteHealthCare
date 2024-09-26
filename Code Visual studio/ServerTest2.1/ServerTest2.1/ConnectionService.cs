using DataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionService
{
    class ConnectionService
    {
        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();
       
            TcpListener listener = new TcpListener(IPAddress.Any, 4789);
            listener.Start();

            while (true) {
                Console.WriteLine("Waiting for connections.......");

                //accepting client and making a new thread for this client
                TcpClient client = listener.AcceptTcpClient();
                
                Thread thread = new Thread(HandleClientThread);
                threads.Add(thread);
                thread.Start(client);
            }
            

            void HandleClientThread(object obj) {
                TcpClient client = obj as TcpClient;

            while (true) {
                string recived = DataProtocol.Messages.ReciveMessage(client);
                Console.WriteLine("Recived: {0}", recived);
                DataProtocol.Messages.SendMessage(client, "Message: " + recived + " Recived");
            }
                client.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}
