namespace Server.Patterns.State.Client

{
    class WelcomeClient(DataProtocol.DataProtocol protocol) : State(protocol)
    {
        public override string CheckInput(string input)
        {
            this.protocol.changeState(new InitializePatient(this.protocol));
            return "Welcome Client";
        }   
    }

    class InitializePatient : State
    {
        public InitializePatient(DataProtocol.DataProtocol protocol) : base(protocol)
        {

        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class RecievingData : State
    {
        public RecievingData(DataProtocol.DataProtocol protocol) : base(protocol)
        {

        }
        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}
