﻿using System;
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

        //oude methode
        public void SendData(string clientName, string message) {
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(message)) {

                return;
            }

            var command = new {
                Action = "Send Data",
                TargetClient = clientName,
                Message = message
            };
            serverConnection.SendCommandToServer(command);

        }

        //oude methode
        public void AdjustResistance(string clientName, int resistance) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "AdjustResistance",
                TargetClient = clientName,
                Resistance = resistance
            };
            serverConnection.SendCommandToServer(command);

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

        //oude methode
        public void ViewTrainingData(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "Retrieve Data",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);

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


        //oude methode
        public void Subscribe(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "Subscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);

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

        //oude methode
        public void Unsubscribe(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "Unsubscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);

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

        //oude methode
        public void StopTraining(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "StopTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);

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

        //oude methode
        public void StartTraining(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }

            var command = new {
                Action = "StartTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);

        }

    }

    public class EmergencyStopAction : DoctorAbstractState {
        public EmergencyStopAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override void PerformAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }

        //oude methode
        public void EmergencyStop(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {

                return;
            }


            string message = $"PatientName ; {clientName}, \"message\" NOODSTOP, new resistance 255";

            var command = new {
                Action = "EmergencyStop",
                TargetClient = clientName,
                Message = message,
                Resistance = 255
            };

            serverConnection.SendCommandToServer(command);

        }


    }

}
