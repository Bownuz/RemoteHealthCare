

using Server.Patterns.State;
using Server.ThreadHandlers;

namespace Server.DataProtocol.doctor
{
    class WelcomeDoctor(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {

        public override string CheckInput(string input)
        {
            protocol.changeState(new LoginDoctor(protocol, thread));
            return "Enter Login Data";
        }
    }

    class LoginDoctor (DataProtocol protocol, CommunicationThread thread): State(protocol, thread)
    {

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class RecievingCommand(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {

        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class FetchingData(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class Subscribing(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class Unsubscribing(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

    class SendingData(DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            throw new NotImplementedException();
        }
    }

}
