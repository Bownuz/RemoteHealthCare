using System;
using System.Text.Json;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class SubscribeAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public SubscribeAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void Subscribe(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het abonneren.");
                return;
            }

            var command = new
            {
                Action = "Subscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Subscribed to {clientName}.");
        }
    }

    public class UnsubscribeAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public UnsubscribeAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void Unsubscribe(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het afmelden.");
                return;
            }

            var command = new
            {
                Action = "Unsubscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Unsubscribed from {clientName}.");
        }
    }
}
