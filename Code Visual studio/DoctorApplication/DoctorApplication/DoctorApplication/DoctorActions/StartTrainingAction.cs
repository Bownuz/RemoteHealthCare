using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class StartTrainingAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public StartTrainingAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void StartTraining(string clientName) {
            var command = new {
                Action = "StartTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingssessie gestart voor {clientName}.");
        }
    }
}
