﻿using Server.DataProtocol;

namespace Server.Patterns.State
{
    internal abstract class State
    {
        protected DataProtocol.DataProtocol protocol;

        public State(DataProtocol.DataProtocol protocol)
        {
            this.protocol = protocol;
        }

        public abstract String CheckInput(String input);
    }
}
