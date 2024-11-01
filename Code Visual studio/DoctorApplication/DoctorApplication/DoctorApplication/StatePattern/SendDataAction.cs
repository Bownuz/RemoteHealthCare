using DoctorApplication.StatePattern;
using System;

namespace DoctorApplication.DoctorActions {
    public class SendDataAction : DoctorState {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public SendDataAction(DoctorProtocol protocol, ClientenForm form, ServerConnection serverConnection) : base(protocol, form, serverConnection) {
        }

        public override void performAction() {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }

        public void SendData(string clientName, string message) {
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(message)) {
                form.UpdateStatus("Client naam en bericht mogen niet leeg zijn bij het versturen van gegevens.");
                return;
            }

            var command = new {
                Action = "Send Data",
                TargetClient = clientName,
                Message = message
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Bericht verzonden naar {clientName}.");
        }
    }
}
