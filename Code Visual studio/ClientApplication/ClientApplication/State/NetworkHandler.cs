using ConnectionImplemented;
using Server;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication.State {
    public class NetworkHandler {
        public event Action<string> NewDoctorMessage;
        public TcpClient tcpClient;
        private Ergometer ergoMeter;
        public DataHandler dataHandler;
        private DataProtocol protocol;

        internal NetworkHandler(Ergometer ergometer) {
            this.ergoMeter = ergometer;

            StartConnectionWithServer();
        }

        internal void AddDataHandler(DataHandler dataHandler) {
            this.dataHandler = dataHandler;

            //Zorgt ervoor dat hij de processInput niet wordt gecalled is hij nog niet in de Initializing state zit
            while (protocol.state.ToString() == "ClientApplication.State.Connecting") {
                Thread.Sleep(1000);
            }
            MessageCommunication.SendMessage(tcpClient, protocol.processInput("Added dataHandler"));
        }

        private void StartConnectionWithServer() {
            this.tcpClient = new TcpClient("127.0.0.1", 4789);
            Task.Run(async () => await HandleNetworkThread());
        }

        public async Task HandleNetworkThread() {
            this.protocol = new DataProtocol(this);
            var serverCommands = new List<string> { "Ready to recieve data", "Goodbye", "Welcome Client", "add failed message", "No current Session Active" };
            var serverErrors = new List<string> { "This message was not a Json String" };

            while (tcpClient.Connected) {
                Thread.Sleep(500);
                string recievedMessage;
                string response;
                if ((recievedMessage = await MessageCommunication.RecieveMessage(tcpClient)) == null) {
                    continue;
                }
                if (recievedMessage == "Goodbye") {
                    tcpClient.Close();
                }

                if (recievedMessage.StartsWith("Resistance:")) {
                    ergoMeter.ChangeResistanceOfBike(byte.Parse(recievedMessage));
                }
                if (!serverCommands.Contains(recievedMessage)) {
                    NewDoctorMessage?.Invoke(recievedMessage);
                } 

                if (serverErrors.Contains(recievedMessage)) {
                    Console.WriteLine(recievedMessage);
                }

                response = protocol.processInput(recievedMessage);
                if (response != "") {
                    MessageCommunication.SendMessage(tcpClient, response);
                                    }
            }
        }

        public void CloseConnection() {
            if (tcpClient?.Connected == true) {
                protocol.ChangeState(new Exit(protocol, this));

                string exitMessage = protocol.processInput("Goodbye");
                MessageCommunication.SendMessage(tcpClient, exitMessage);
            }
        }
    }
}
