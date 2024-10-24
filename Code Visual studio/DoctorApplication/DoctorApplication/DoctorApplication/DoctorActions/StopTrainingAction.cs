using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class StopTrainingAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public StopTrainingAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void StopTraining(string clientName) {
            var command = new {
                Action = "StopTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingssessie gestopt voor {clientName}.");
        }
    }
}
