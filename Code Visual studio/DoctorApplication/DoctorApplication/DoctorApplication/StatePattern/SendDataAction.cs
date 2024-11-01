using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class SendDataAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public SendDataAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void SendData(string clientName, string message)
        {
            if (string.IsNullOrEmpty(clientName) || string.IsNullOrEmpty(message))
            {
                form.UpdateStatus("Client naam en bericht mogen niet leeg zijn bij het versturen van gegevens.");
                return;
            }

            var command = new
            {
                Action = "Send Data",
                TargetClient = clientName,
                Message = message
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Bericht verzonden naar {clientName}.");
        }
    }
}
