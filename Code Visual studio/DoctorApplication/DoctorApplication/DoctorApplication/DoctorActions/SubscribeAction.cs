using System;
using System.Text.Json;
using DoctorApplication;

namespace DoctorApplication.DoctorActions {
    public class SubscribeAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public SubscribeAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void Subscribe(string clientName) {
            var command = new {
                Action = "Subscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Subscribed to {clientName}.");
        }
    }

    public class UnsubscribeAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public UnsubscribeAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void Unsubscribe(string clientName) {
            var command = new {
                Action = "Unsubscribe",
                TargetClient = clientName
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Unsubscribed from {clientName}.");
        }
    }
}
