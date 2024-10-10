using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VrServer {
    class VrServer {
        private static Socket? client;

        public static void Main(string[] args) {
            // create the socket connection
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 6666);

            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(localEndPoint);

            string filename = "";
            for (; ; ) {
                Console.Write("File name of Json object to send: ");
                filename = Console.ReadLine();
                // stops program if there is no input
                if (filename.Equals(""))
                    break;
                // gets json object as an array of bytes since that's what will eventually be send
                byte[] jsonObject = File.ReadAllBytes($"{Directory.GetCurrentDirectory()}\\..\\..\\..\\json_files\\{filename}");
                // GetBytes always returns an array with 4 bytes
                byte[] sizeJsonObject = BitConverter.GetBytes(jsonObject.Length);

                client.Send(sizeJsonObject);
                client.Send(jsonObject);

                // recieves and prints the size packet
                byte[] sizeIncomingMessage = new byte[4];
                int byteSizeIncomingMessage = client.Receive(sizeIncomingMessage);
                Console.WriteLine("Message size from Server -> {0}", BitConverter.ToInt32(sizeIncomingMessage));

                // recieves and prints the json object packet
                byte[] message = new byte[byteSizeIncomingMessage];
                int byteMessage = client.Receive(message);
                Console.WriteLine("Message from Server -> {0}",
                  Encoding.ASCII.GetString(message, 0, byteMessage));
            }
            client.Close();
        }
    }
}