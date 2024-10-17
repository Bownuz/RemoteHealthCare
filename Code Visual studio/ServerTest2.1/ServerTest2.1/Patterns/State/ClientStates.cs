



namespace Server.Patterns.State.Client

{
    class WelcomeClient : State
    {
        public WelcomeClient(DataProtocol.DataProtocol protocol) : base(protocol)
        {

        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
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
