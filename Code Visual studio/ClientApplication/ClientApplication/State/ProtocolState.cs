using ConnectionImplemented;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ClientApplication.State {
    public class Connecting : State {
        public Connecting(DataProtocol protocol, Handler handler) : base(protocol, handler) {
        }

        public override string CheckInput(string input) {
            protocol.ChangeState(new Initializing(protocol, handler));
            return "Patient Connected";
        }
    }

    public class Initializing : State {

        public Initializing(DataProtocol protocol, Handler handler) : base(protocol, handler) {
        }

        public override string CheckInput(string input) {

            string patientInfo = null;
            if (input.Equals("Welcome Client")) {
                while (handler.dataHandler == null) {
                    Console.WriteLine("Wacht op initialisatie van dataHandler...");
                    Thread.Sleep(1000);
                }
                patientInfo = handler.dataHandler.PatientInitialisationMessage();

                protocol.ChangeState(new SendData(protocol, handler));
            }
            return patientInfo;
        }
    }

    public class SendData : State {
        public SendData(DataProtocol protocol, Handler handler) : base(protocol, handler) {
        }

        public override string CheckInput(string input) {
            string patientData = null;
            if (input.Equals("Ready to recieve data")) {
                patientData = handler.dataHandler.printDataAsJson();
            }
            return patientData;
        }
    }

    public class Exit : State {
        public Exit(DataProtocol protocol, Handler handler) : base(protocol, handler) {
        }

        public override string CheckInput(string input) {
            return "";
        }
    }
}
