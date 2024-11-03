using DoctorApplication.StatePattern;
using System;

public abstract class DoctorAbstractState {
    protected DoctorProtocol protocol;
    protected ServerConnection serverConnection;

    public DoctorAbstractState(DoctorProtocol protocol, ServerConnection serverConnection) {
        this.protocol = protocol;
        this.serverConnection = serverConnection;
    }

    public abstract void ProcessInput(string input);
    public abstract void PerformAction(string data);
}
