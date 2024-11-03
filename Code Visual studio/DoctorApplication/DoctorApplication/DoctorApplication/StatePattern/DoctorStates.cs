using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;

namespace DoctorApplication.StatePattern {
    public class Connecting : DoctorAbstractState {
        public Connecting(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string ipAdress) {
            serverConnection.ConnectToServer(ipAdress);
        }

        public override void ProcessInput(string input) {
            if (input.Equals("Enter Login Data")) {
                protocol.changeState(new Login(protocol, serverConnection));
            }
        }

    }


    public class Login : DoctorAbstractState {
        public Login(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string message) {
            MessageCommunication.SendMessage(serverConnection.networkStream, message);
        }

        public override void ProcessInput(string input) {
            if (input.Equals("Login Successful")) {
                protocol.changeState(new CommandType(protocol, serverConnection));
                serverConnection.mainForm.Invoke((MethodInvoker)delegate {
                    ClientenForm clientInfoScreen = new ClientenForm(serverConnection);
                    serverConnection.mainForm.Controls.Clear();
                    serverConnection.mainForm.Controls.Add(clientInfoScreen);
                    clientInfoScreen.Dock = DockStyle.Fill;
                });
                protocol.changeState(new CommandType(protocol, serverConnection));
            }
        }
    }

    public class CommandType : DoctorAbstractState {
        public CommandType(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string data) {
            switch (data) {
                case "Subscribe":
                    protocol.changeState(new SubscribeAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
            }
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }


    public class SendDataAction : DoctorAbstractState {
        public SendDataAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }

    public class ViewTrainingDataAction : DoctorAbstractState {
        public ViewTrainingDataAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }

    public class SubscribeAction : DoctorAbstractState {
        private List<string> names;

        public SubscribeAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            names = new List<string>();
        }

        public override void PerformAction(string patientName) {
            names.Add(patientName);
        }

        public override void ProcessInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto notJson;
            }

            DoctorMessageWithList message = JsonSerializer.Deserialize<DoctorMessageWithList>(input);
            if (message.Message.Equals("Which patient should be subscribed to?")) {
                MessageCommunication.SendMessage(serverConnection.networkStream, names[0]);
                names.RemoveAt(0);
                return;
            }

            notJson: if (input.Equals("Ready to receive command")) {
                if (names.Count == 0) {
                    protocol.changeState(new CommandType(protocol, serverConnection));
                } else {
                    MessageCommunication.SendMessage(serverConnection.networkStream, "Subscribe");
                }

            }
        }
    }

    public class UnsubscribeAction : DoctorAbstractState {
        public UnsubscribeAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string data) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }

    public class StopTrainingAction : DoctorAbstractState {
        public StopTrainingAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }

    public class StartTrainingAction : DoctorAbstractState {
        public StartTrainingAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }

}
