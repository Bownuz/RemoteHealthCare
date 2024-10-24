using DoctorApplication;

public class DoctorState {
    private ClientenForm form;
    private ServerConnection serverConnection;

    public DoctorState(ClientenForm form, ServerConnection serverConnection) {
        this.form = form;
        this.serverConnection = serverConnection;
    }

    public void AdjustResistanceForClient(string clientName, int resistance) {
        var command = new {
            Action = "AdjustResistance",
            TargetClient = clientName,
            Resistance = resistance
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Weerstand aangepast voor {clientName} naar {resistance}.");
    }

    public void StartTrainingForClient(string clientName) {
        var command = new {
            Action = "StartTraining",
            TargetClient = clientName
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Trainingssessie gestart voor {clientName}.");
    }

    public void StopTrainingForClient(string clientName) {
        var command = new {
            Action = "StopTraining",
            TargetClient = clientName
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Trainingssessie gestopt voor {clientName}.");
    }

    public void EmergencyStopForClient(string clientName) {
        var command = new {
            Action = "EmergencyStop",
            TargetClient = clientName
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Noodstop uitgevoerd voor {clientName}.");
    }

    public void SendDataToClient(string clientName, string message) {
        var command = new {
            Action = "SendData",
            TargetClient = clientName,
            Message = message
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Bericht verzonden naar {clientName}.");
    }

    public void ViewTrainingDataForClient(string clientName) {
        var command = new {
            Action = "ViewTrainingData",
            TargetClient = clientName
        };
        serverConnection.SendCommandToServer(command);
        form.UpdateStatus($"Trainingsdata opgevraagd voor {clientName}.");
    }
}
