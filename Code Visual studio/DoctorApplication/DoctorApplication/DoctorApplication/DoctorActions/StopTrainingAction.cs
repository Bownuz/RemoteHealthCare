using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class StopTrainingAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public StopTrainingAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void StopTraining(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het stoppen van de training.");
                return;
            }

            var command = new
            {
                Action = "StopTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingssessie gestopt voor {clientName}.");
        }
    }
}
