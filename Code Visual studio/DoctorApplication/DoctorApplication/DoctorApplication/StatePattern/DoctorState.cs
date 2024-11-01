using DoctorApplication;
using DoctorApplication.DoctorActions;
using DoctorApplication.StatePattern;

public abstract class DoctorState {
    DoctorProtocol protocol;
    ClientenForm form;
    ServerConnection serverConnection;

    public DoctorState(DoctorProtocol protocol, ClientenForm form, ServerConnection serverConnection) {
        this.protocol = protocol;
        this.form = form;
        this.serverConnection = serverConnection;
    }

    public abstract void ProcessInput(string input);
    public abstract void performAction();
}
