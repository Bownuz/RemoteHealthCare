using Server.Patterns.State;
using Server.Patterns.State.DoctorStates;
using Server.Patterns.State.PatientStates;
using Server.ThreadHandlers;

namespace Server {
    public class DataProtocol {
        private State State;

        public DataProtocol(CommunicationType communicationType, CommunicationHandler communicationHandler) {
            switch (communicationType) {
                case CommunicationType.PATIENT:
                    this.State = new P_Welcome(this, (PatientHandler)communicationHandler);
                    break;
                case CommunicationType.DOCTOR:
                    this.State = new D_Welcome(this, (DoctorHandler)communicationHandler);
                    break;

            }
        }
        public async Task processInput(String input) {
            State.CheckInput(input);
        }

        public void ChangeState(State newState) {
            this.State = newState;

        }



    }
}

