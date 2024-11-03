using System.Text.RegularExpressions;

namespace Server.Patterns.State {
    public abstract class State {
        protected DataProtocol protocol;
        protected Regex jsonRegex;

        protected State(DataProtocol protocol) {
            this.protocol = protocol;
            jsonRegex = new Regex("^\\{.+\\}$");
        }

        public abstract void CheckInput(String input);
        public abstract bool isJson(String input);

    }

}

