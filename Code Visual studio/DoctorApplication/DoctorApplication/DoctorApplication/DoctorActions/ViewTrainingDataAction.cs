using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class ViewTrainingDataAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public ViewTrainingDataAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void ViewTrainingData(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het opvragen van trainingsdata.");
                return;
            }

            var command = new
            {
                Action = "Retrieve Data",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Trainingsdata opgevraagd voor {clientName}.");
        }
    }
}
