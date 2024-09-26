using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataProtocol {
    class Messages {
        public static string ReciveMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            {
                return stream.ReadLine();
            }
        }

        public static void SendMessage(TcpClient client, string message) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII, -1, true);
            {
                stream.WriteLine(message);
                stream.Flush();
            }
        }

    }
}
