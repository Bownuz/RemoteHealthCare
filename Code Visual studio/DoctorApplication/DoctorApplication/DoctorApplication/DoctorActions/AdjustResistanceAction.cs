using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class AdjustResistanceAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public AdjustResistanceAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void AdjustResistance(string clientName, int resistance)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het aanpassen van de weerstand.");
                return;
            }

            var command = new
            {
                Action = "AdjustResistance",
                TargetClient = clientName,
                Resistance = resistance
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Weerstand aangepast voor {clientName} naar {resistance}.");
        }
    }
}
