using Server;

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
            while (networkHandler.dataHandler == null) {
            }

            string patientInfo = networkHandler.dataHandler.PatientInitialisationMessage();
            protocol.ChangeState(new SendData(protocol, networkHandler));

            return patientInfo;
        }
    }

    public class SendData : State {
        public SendData(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            string response = "";
            if (input.Equals("Ready to recieve data")) {
                response = networkHandler.dataHandler.printDataAsJson();
                return response;
            }
            return response;
        }
    }

    public class Exit : State {
        public Exit(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override string CheckInput(string input) {
            if (input.Equals("Goodbye")) {
                return "Quit Communication";
            }
            return "";
        }
    }
}
