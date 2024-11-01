using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplication.DoctorActions {
    public class AdjustResistanceAction {
        private ServerConnection serverConnection;
        private ClientenForm form;

        public AdjustResistanceAction(ClientenForm form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
        }

        public void AdjustResistance(string clientName, int resistance) {
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
