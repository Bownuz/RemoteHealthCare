using DoctorApplication.StatePattern;
using System;

namespace DoctorApplication.DoctorActions {
    public class EmergencyStopAction : DoctorState {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public EmergencyStopAction(DoctorProtocol protocol, ClientenForm form, ServerConnection serverConnection) : base(protocol, form, serverConnection) {
        }

        public void EmergencyStop(string clientName) {
            if (string.IsNullOrEmpty(clientName)) {
                form.UpdateStatus("Client naam mag niet leeg zijn bij noodstop.");
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
            form.UpdateStatus($"Noodstop uitgevoerd voor {clientName} met weerstand 255.");
        }

        public override void performAction() {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }
    }
}
