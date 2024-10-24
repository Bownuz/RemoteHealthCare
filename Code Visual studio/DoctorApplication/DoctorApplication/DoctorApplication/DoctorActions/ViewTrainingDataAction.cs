using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class ViewTrainingDataAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public ViewTrainingDataAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void ViewTrainingData(string clientName) {
            var command = new {
                Action = "ViewTrainingData",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingsdata opgevraagd voor {clientName}.");
        }
    }
}

