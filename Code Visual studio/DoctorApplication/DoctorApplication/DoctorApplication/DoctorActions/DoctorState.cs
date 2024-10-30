using DoctorApplication;
using DoctorApplication.DoctorActions;

public class DoctorState {
    public AdjustResistanceAction adjustResistance;
    public StartTrainingAction startTraining;
    public StopTrainingAction stopTraining;
    public EmergencyStopAction emergencyStop;
    public SendDataAction sendData;
    public ViewTrainingDataAction viewTrainingData;
    public SubscribeAction subscribe;
    public UnsubscribeAction unsubscribe;

    public DoctorState(ClientenForm form, ServerConnection serverConnection) {
        adjustResistance = new AdjustResistanceAction(form, serverConnection);
        startTraining = new StartTrainingAction(form, serverConnection);
        stopTraining = new StopTrainingAction(form, serverConnection);
        emergencyStop = new EmergencyStopAction(form, serverConnection);
        sendData = new SendDataAction(form, serverConnection);
        viewTrainingData = new ViewTrainingDataAction(form, serverConnection);
        subscribe = new SubscribeAction(form, serverConnection);
        unsubscribe = new UnsubscribeAction(form, serverConnection);
    }
}
