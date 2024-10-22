using System;

namespace Server.Patterns.State
{
	public abstract class State
	{
		protected DataProtocol protocol;

        protected State(DataProtocol protocol)
        {
            this.protocol = protocol;
        }

        public abstract String CheckInput(String input);

	}

}

