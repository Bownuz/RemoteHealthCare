using Server.DataProtocol;
using Server.ThreadHandlers;

namespace Server.Patterns.State
{
    internal abstract class State
    {
        protected DataProtocol.DataProtocol protocol;
        protected CommunicationThread thread;

        public State(DataProtocol.DataProtocol protocol, CommunicationThread thread)
        {
            this.protocol = protocol;
            this.thread = thread;
        }

        public abstract String CheckInput(String input);
    }
}
