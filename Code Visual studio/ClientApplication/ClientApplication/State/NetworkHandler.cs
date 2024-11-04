using ConnectionImplemented;
using Server;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ClientApplication.State {
    public class NetworkHandler {
        public event Action<string> NewDoctorMessage;
        public TcpClient tcpClient;
        private Ergometer ergoMeter;
        public DataHandler dataHandler;
        public DataProtocol protocol;
        public NetworkStream networkStream;

        internal NetworkHandler(Ergometer ergometer) {
            this.ergoMeter = ergometer;
            this.protocol = new DataProtocol(this);
            StartConnectionWithServer();
        }

        internal void AddDataHandler(DataHandler dataHandler) {
            this.dataHandler = dataHandler;
        }

        private void StartConnectionWithServer() {
            this.tcpClient = new TcpClient("192.168.1.107", 4789);
            networkStream = tcpClient.GetStream();
        }

        public async Task HandleNetworkThread() {
            var serverCommands = new List<string> { "Ready to recieve data", "Goodbye", "Welcome Client", "add failed message", "No current Session Active" };
            var serverErrors = new List<string> { "This message was not a Json String" };

            while (tcpClient.Connected) {
                string recievedMessage;
                if ((recievedMessage = MessageCommunication.ReceiveMessage(networkStream)) == null) {
                    continue;
                }

                if (!serverCommands.Contains(recievedMessage)) {
                    NewDoctorMessage?.Invoke(recievedMessage);
                }

                await protocol.processInput(recievedMessage);
            }
        }

        public void CloseConnection() {
            if (tcpClient?.Connected == true) {
                protocol.ChangeState(new Exit(protocol, this));

                protocol.processInput("Goodbye");
            }
        }
    }
}
