namespace DoctorApplication.StatePattern {
    public class DoctorProtocol {
        public DoctorAbstractState doctorState { get; private set; }

        public DoctorProtocol(ServerConnection serverConnection) {
            this.doctorState = new Connecting(this, serverConnection);
        }

        public void Respond(string input) {
            doctorState.ProcessInput(input);
        }

        public void changeState(DoctorAbstractState newState) {
            doctorState = newState;
        }
    }
}
