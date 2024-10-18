

using Server.Patterns.State;
using Server.ThreadHandlers;

namespace Server.DataProtocol.doctor
{
    class WelcomeDoctor : State
    {
        public WelcomeDoctor(DataProtocol protocol, CommunicationThread thread) : base(protocol,thread)
        {
        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class LoginDoctor : State
    {
        public LoginDoctor(DataProtocol protocol, CommunicationThread thread) : base(protocol, thread)
        {
        }

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }



}
