using ClientApplication;
using ClientApplication.State;
using System;
using System.Threading.Tasks;


namespace Server {
    public class DataProtocol {
        public State state;

        public DataProtocol(NetworkHandler networkHandler) {
            this.state = new Connecting(this, networkHandler);
        }
        public async Task processInput(String input) {
            state.CheckInput(input);
        }

        public void ChangeState(State newState) {
            this.state = newState;
        }
    }
}

