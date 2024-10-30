using ConnectionImplemented;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication.State {
    public class Handler {
        public event Action<string> NewDoctorMessage;
        protected bool isRunning;
        protected bool isInitialized;
        protected string patientID;
        protected string ergometerID;
        protected string heartRateMonitorID;
        protected TcpClient tcpClient;
        internal DataHandler dataHandler;

        internal Handler(TcpClient tcpClient, Ergometer ergometer, string patientID, string ergometerID, string heartRateMonitorID) {
            NetworkStream stream = tcpClient.GetStream();
            this.tcpClient = tcpClient;
            this.isRunning = true;
            this.isInitialized = false;
            this.patientID = patientID;
            this.ergometerID = ergometerID;
            this.heartRateMonitorID = heartRateMonitorID;
        }

        internal void addDataHandler(DataHandler dataHandler) {
            this.dataHandler = dataHandler;
        }

        public void HandleThread() {
            DataProtocol protocol = new DataProtocol(this);
            //new Thread(() => MessageCommunication.RecieveMessage(tcpClient)).Start();

            while (tcpClient.Connected) {
                string recievedMessage;
                string response;
                if ((recievedMessage = MessageCommunication.RecieveMessage(tcpClient)) != null) {
                    NewDoctorMessage?.Invoke(recievedMessage);
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
}
