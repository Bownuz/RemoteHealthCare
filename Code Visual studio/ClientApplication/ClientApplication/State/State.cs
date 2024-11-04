using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.State {
    public abstract class State {
        public DataProtocol protocol;
        public NetworkHandler networkHandler;

        public State(DataProtocol protocol, NetworkHandler networkHandler) {
            this.protocol = protocol;
            this.networkHandler = networkHandler;
        }

        public abstract void CheckInput(String input);

    }
}
