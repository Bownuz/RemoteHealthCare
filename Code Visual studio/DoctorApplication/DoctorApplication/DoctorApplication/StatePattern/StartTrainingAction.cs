using System;
using DoctorApplication;
using DoctorApplication.StatePattern;

namespace DoctorApplication.DoctorActions
{
    public class StartTrainingAction : DoctorState
    {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public StartTrainingAction(DoctorProtocol protocol, ClientenForm form, ServerConnection serverConnection) : base(protocol, form, serverConnection)
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
        public override void performAction()
        {
            throw new NotImplementedException();
        }

        public override void ProcessInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}
