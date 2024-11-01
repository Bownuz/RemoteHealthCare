using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class StartTrainingAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public StartTrainingAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void StartTraining(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het starten van de training.");
                return;
            }

            var command = new
            {
                Action = "StartTraining",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingssessie gestart voor {clientName}.");
        }
    }
}
