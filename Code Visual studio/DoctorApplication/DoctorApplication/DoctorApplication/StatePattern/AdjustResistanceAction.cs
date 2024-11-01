using DoctorApplication.StatePattern;
using System;

namespace DoctorApplication.DoctorActions {
    public class AdjustResistanceAction : DoctorState {
        public AdjustResistanceAction(DoctorProtocol protocol, ClientenForm form, ServerConnection serverConnection) : base(protocol, form, serverConnection) { }


        public override void ProcessInput(string input) {
            throw new NotImplementedException();
        }

        public override void performAction() {
            throw new NotImplementedException();
        }


        public void AdjustResistance(string clientName, int resistance) {
            if (string.IsNullOrEmpty(clientName)) {
                form.UpdateStatus("Client naam mag niet leeg zijn bij het aanpassen van de weerstand.");
                return;
            }

            var command = new {
                Action = "AdjustResistance",
                TargetClient = clientName,
                Resistance = resistance
            };
            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Weerstand aangepast voor {clientName} naar {resistance}.");
        }

    }
}

