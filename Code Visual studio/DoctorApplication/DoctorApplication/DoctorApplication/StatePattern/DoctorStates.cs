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
                case ValidMessages.d_sendData:
                    protocol.changeState(new SendDataAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
                case ValidMessages.d_retrieveData:
                    protocol.changeState(new ViewTrainingDataAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
                case ValidMessages.d_subscribe:
                    protocol.changeState(new SubscribeAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
                case ValidMessages.d_unsubscribe:
                    protocol.changeState(new UnsubscribeAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
                case ValidMessages.d_startSession:
                    protocol.changeState(new StartTrainingAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
                case ValidMessages.d_endSession:
                    protocol.changeState(new StopTrainingAction(protocol, serverConnection));
                    MessageCommunication.SendMessage(serverConnection.networkStream, data);
                    break;
            }
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }


    public class SendDataAction : DoctorAbstractState {
        private List<string> dataMessages;
        public SendDataAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            dataMessages = new List<string>();
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto notJson;
            }

            DoctorMessageWithList message = JsonSerializer.Deserialize<DoctorMessageWithList>(input);
            if (message.Message.Equals(ValidMessages.d_sendDataResponse)) {
                MessageCommunication.SendMessage(serverConnection.networkStream, dataMessages[0]);
                dataMessages.RemoveAt(0);
                return;
            }

        notJson: if (input.Equals(ValidMessages.d_readyToRecieve)) {
                if (dataMessages.Count == 0) {
                    protocol.changeState(new CommandType(protocol, serverConnection));
                } else {
                    MessageCommunication.SendMessage(serverConnection.networkStream, ValidMessages.d_sendData);
                }

            }
        }
    }

    public class ViewTrainingDataAction : DoctorAbstractState {
        private List<string> names;

        public ViewTrainingDataAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            names = new List<string>();
        }

        public override void PerformAction(string patientName) {
            names.Add(patientName);
        }

        public override void ProcessInput(string input) {
            Console.WriteLine("I wanna die");
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
        private List<string> names;
        public UnsubscribeAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            names = new List<string>();
        }

        public override void PerformAction(string data) {
            names.Add(data);
        }

        public override void ProcessInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto notJson;
            }

            DoctorMessageWithList message = JsonSerializer.Deserialize<DoctorMessageWithList>(input);
            if (message.Message.Equals(ValidMessages.d_unsubscribeResponse)) {
                MessageCommunication.SendMessage(serverConnection.networkStream, names[0]);
                names.RemoveAt(0);
                return;
            }

        notJson: if (input.Equals(ValidMessages.d_readyToRecieve)) {
                if (names.Count == 0) {
                    protocol.changeState(new CommandType(protocol, serverConnection));
                } else {
                    MessageCommunication.SendMessage(serverConnection.networkStream, ValidMessages.d_subscribe);
                }
            }
        }
    }

    public class StopTrainingAction : DoctorAbstractState {
        private List<string> names;
        public StopTrainingAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            names = new List<string>();
        }

        public override void PerformAction(string action) {
            names.Add(action);
        }

        public override void ProcessInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto notJson;
            }

            DoctorMessageWithList message = JsonSerializer.Deserialize<DoctorMessageWithList>(input);
            if (message.Message.Equals(ValidMessages.d_endSessionResponse)) {
                MessageCommunication.SendMessage(serverConnection.networkStream, names[0]);
                names.RemoveAt(0);
                return;
            }

        notJson: if (input.Equals(ValidMessages.d_readyToRecieve)) {
                if (names.Count == 0) {
                    protocol.changeState(new CommandType(protocol, serverConnection));
                } else {
                    MessageCommunication.SendMessage(serverConnection.networkStream, ValidMessages.d_subscribe);
                }
            }
        }
    }

    public class StartTrainingAction : DoctorAbstractState {
        private List<string> names;
        public StartTrainingAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
            names = new List<string>();
        }

        public override void PerformAction(string action) {
            names.Add(action);
        }

        public override void ProcessInput(string input) {
            if (!input.StartsWith("{") && !input.EndsWith("}")) {
                goto notJson;
            }

            DoctorMessageWithList message = JsonSerializer.Deserialize<DoctorMessageWithList>(input);
            if (message.Message.Equals(ValidMessages.d_startSessionResponse)) {
                MessageCommunication.SendMessage(serverConnection.networkStream, names[0]);
                names.RemoveAt(0);
                return;
            }

        notJson: if (input.Equals(ValidMessages.d_readyToRecieve)) {
                if (names.Count == 0) {
                    protocol.changeState(new CommandType(protocol, serverConnection));
                } else {
                    MessageCommunication.SendMessage(serverConnection.networkStream, ValidMessages.d_subscribe);
                }
            }
        }
    }
}
