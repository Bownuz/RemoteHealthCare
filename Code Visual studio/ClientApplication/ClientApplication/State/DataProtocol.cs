using ClientApplication;
using ClientApplication.State;
using System;


namespace Server {
    public class DataProtocol {
        private State state;

        public DataProtocol(Handler handler) {
            //this.state = new Connecting(this, handler);
            this.state = new Initializing(this, handler);
        }
        public String processInput(String input) {
            return state.CheckInput(input);
        }

        public void ChangeState(State newState) {
            this.state = newState;
        }
    }
}

