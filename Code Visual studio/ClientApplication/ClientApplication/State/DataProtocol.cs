using ClientApplication;
using ClientApplication.State;
using System;


namespace Server {
    public class DataProtocol {
        public State state;

        public DataProtocol(NetworkHandler networkHandler) {
            this.state = new Connecting(this, networkHandler);
        }
        public String processInput(String input) {
            return state.CheckInput(input);
        }

        public void ChangeState(State newState) {
            this.state = newState;
        }
    }
}

