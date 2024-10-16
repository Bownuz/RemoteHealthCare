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
    internal struct ÇlientSendData
    { 
        public String Message { get; }
        public int Resistance { get;}

        public ÇlientSendData(string message, int resistance)
        {
            this.Message = message;
            this.Resistance = resistance;
        }
    }

    internal struct DoctorRecieveData { 
        public String Message { get; }
        public int Resistance { get; }

        public DoctorRecieveData(string message, int resistance)
        {
            this.Message = message;
            this.Resistance = resistance;
        }
    }

    internal struct DoctorSendData { 
        public String dataType { get; }
        public String clientData { get; }

        public DoctorSendData(string dataType, String data)
        {
            this.dataType = dataType;
            this.clientData = clientData;
        }
    }



}
