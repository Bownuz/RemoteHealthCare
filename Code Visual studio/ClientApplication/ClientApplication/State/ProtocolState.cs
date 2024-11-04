using Server;

namespace ClientApplication.State {
    public class Connecting : State {
        public Connecting(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override void CheckInput(string input) {
            if (input.Equals("Welcome Client")) {
                protocol.ChangeState(new Initializing(protocol, networkHandler));
            }
        }
    }

    public class Initializing : State {

        public Initializing(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override void CheckInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto NotJson;
            }
            MessageCommunication.SendMessage(networkHandler.networkStream, input);
            return;


        NotJson: if (input.Equals(ValidMessages.p_readyToRecieve)) {
                protocol.ChangeState(new SendData(protocol, networkHandler));
                MessageCommunication.SendMessage(networkHandler.networkStream, networkHandler.dataHandler.printDataAsJson());
            }
        }
    }

    public class SendData : State {
        public SendData(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override void CheckInput(string input) {
            if (input.Equals("Ready to recieve data")) {
                MessageCommunication.SendMessage(networkHandler.networkStream, networkHandler.dataHandler.printDataAsJson());
            }
        }
    }

    public class Exit : State {
        public Exit(DataProtocol protocol, NetworkHandler networkHandler) : base(protocol, networkHandler) {
        }

        public override void CheckInput(string input) {
            if (input.Equals("Goodbye")) {
                MessageCommunication.SendMessage(networkHandler.networkStream, "Quit Communication");
            }
        }
    }
}
