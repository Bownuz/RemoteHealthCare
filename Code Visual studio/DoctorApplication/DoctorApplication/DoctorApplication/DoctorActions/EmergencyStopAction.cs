using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class EmergencyStopAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public EmergencyStopAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void EmergencyStop(string clientName) {
            var command = new {
                Action = "EmergencyStop",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Noodstop uitgevoerd voor {clientName}.");
        }
    }
}
