using System;

namespace DoctorApplication.StatePattern {
    public class Connecting : DoctorAbstractState {
        public Connecting(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override Boolean performAction(string data) {
            serverConnection.ConnectToServer();
            protocol.changeState(new Login(protocol, serverConnection));
            return true;
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }

    }




    public class Login : DoctorAbstractState {
        public Login(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override Boolean performAction(string message) {
            if (!message.Equals("Login Successful")) {
                MessageCommunication.SendMessage(serverConnection.networkStream, message);
                return false;
            } else {
                return true;
            }

        }

        public override void ProcessInput(string input) {
            if (input.Equals("Login Successful")) {
                protocol.changeState(new CommandType(protocol, serverConnection));
            }

        }
    }

    public class CommandType : DoctorAbstractState {
        public CommandType(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override Boolean performAction(string data) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }


    public class SendDataAction : DoctorAbstractState {
        public SendDataAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override Boolean performAction(string action) {
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

        public override Boolean performAction(string action) {
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
        public SubscribeAction(DoctorProtocol protocol, ServerConnection serverConnection) : base(protocol, serverConnection) {
        }

        public override Boolean performAction(string action) {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
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

        public override Boolean performAction(string data) {
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

        public override Boolean performAction(string action) {
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

        public override Boolean performAction(string action) {
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

        public override Boolean performAction(string action) {
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
