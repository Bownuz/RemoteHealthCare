

using System.Threading;

namespace DoctorApplication.StatePattern {
    public class DoctorProtocol {
        DoctorState doctorState;


        public void Respond(string input) {
            doctorState.ProcessInput(input);
        }

        public void changeState(DoctorState newState) { 
        doctorState = newState;
        }
    }
}
