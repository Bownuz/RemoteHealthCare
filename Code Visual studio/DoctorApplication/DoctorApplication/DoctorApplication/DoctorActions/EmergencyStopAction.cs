using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions {
    public class EmergencyStopAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public EmergencyStopAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void EmergencyStop(string clientName) {
 
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
    }
}
