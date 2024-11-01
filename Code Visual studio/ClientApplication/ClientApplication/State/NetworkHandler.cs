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
            this.tcpClient = new TcpClient("192.168.178.58", 4789);
            NetworkStream stream = tcpClient.GetStream();
            Task.Run(async () => await HandleNetworkThread());
        }

        public async Task HandleNetworkThread() {
            this.protocol = new DataProtocol(this);
            var serverCommands = new List<string> { "Ready to recieve data", "Goodbye", "Welcome Client", "add failed message" };

            while (tcpClient.Connected) {
                Thread.Sleep(500);
                string recievedMessage;
                string response;
                if ((recievedMessage = await MessageCommunication.RecieveMessage(tcpClient)) == null) {
                    continue;
                }

                if (recievedMessage.StartsWith("HeartRate:")) {
                    ergoMeter.ChangeResistanceOfBike(byte.Parse(recievedMessage));
                }
                if (!serverCommands.Contains(recievedMessage)) {
                    NewDoctorMessage?.Invoke(recievedMessage);
                }

                response = protocol.processInput(recievedMessage);
                if (response != "") {
                    MessageCommunication.SendMessage(tcpClient, response);
                    if (response.Equals("Goodbye")) {
                        tcpClient.Close();
                    }
                }
            }
        }
    }

}
