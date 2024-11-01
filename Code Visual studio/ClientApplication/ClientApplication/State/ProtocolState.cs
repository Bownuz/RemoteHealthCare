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
using System.Windows;
using System.Windows.Markup;

namespace ClientApplication.State {
    public class Connecting : State {
        public Connecting(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            if (input.Equals("Welcome Client")) {
                protocol.ChangeState(new Initializing(protocol, networkHandler));
            }
            return "";
        }
    }

    public class Initializing : State {

        public Initializing(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            string patientInfo = null;
            if (input.Equals("Added dataHandler")) {
                patientInfo = networkHandler.dataHandler.PatientInitialisationMessage();

                protocol.ChangeState(new SendData(protocol, networkHandler));
            }

            return patientInfo;
        }
    }

    public class SendData : State {
        public SendData(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            string patientData = null;
            if (input.Equals("Ready to recieve data")) {
                patientData = networkHandler.dataHandler.printDataAsJson();
            }
            return patientData;
        }
    }

    public class Exit : State {
        public Exit(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            return "Goodbye";
        }
    }
}
