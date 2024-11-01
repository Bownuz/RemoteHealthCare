using System;
using DoctorApplication;

namespace DoctorApplication.DoctorActions
{
    public class EmergencyStopAction
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public EmergencyStopAction(ClientenForm form, ServerConnection serverConnection)
        {
            this.form = form ?? throw new ArgumentNullException(nameof(form), "Form mag niet null zijn.");
            this.serverConnection = serverConnection ?? throw new ArgumentNullException(nameof(serverConnection), "ServerConnection mag niet null zijn.");
        }

        public void EmergencyStop(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                form.UpdateStatus("Client naam mag niet leeg zijn bij noodstop.");
                return;
            }

            string message = $"PatientName ; {clientName}, \"message\" NOODSTOP, new resistance 255";

            var command = new
            {
                Action = "EmergencyStop",
                TargetClient = clientName,
                Message = message,
                Resistance = 255
            };

            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Noodstop uitgevoerd voor {clientName} met weerstand 255.");
        }
    }
}
