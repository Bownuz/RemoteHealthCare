

using Server.Patterns.State;

namespace Server.DataProtocol.doctor
{
    class WelcomeDoctor : State
    {
        public WelcomeDoctor(DataProtocol protocol) : base(protocol)
        {
        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class LoginDoctor : State
    {
        public LoginDoctor(DataProtocol protocol) : base(protocol)
        {
        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }



}
