using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class SendDataAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public SendDataAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void SendData(string clientName, string message) {
            var command = new {
                Action = "SendData",
                TargetClient = clientName,
                Message = message
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Bericht verzonden naar {clientName}.");
        }
    }
}
