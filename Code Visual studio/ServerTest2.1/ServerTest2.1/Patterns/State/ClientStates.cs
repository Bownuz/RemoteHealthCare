using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;

namespace Server.Patterns.State.Client

{
    class WelcomeClient(DataProtocol.DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            this.protocol.changeState(new InitializePatient(protocol, thread));
            return "Welcome Client";
        }   
    }

    class InitializePatient(DataProtocol.DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            //TODO: add regex to ensure correct formatting
            ClientInitialisationMessage dataFromMessage = JsonSerializer.Deserialize<ClientInitialisationMessage>(input);
            Person connectedPerson;
            FileStorage storageFromThread = thread.fileStorage;

            if (!storageFromThread.PatientExists(dataFromMessage.ClientName))
            {
                storageFromThread.AddPatient(dataFromMessage.ClientName);
            }
            thread.Person = storageFromThread.GetPatient(dataFromMessage.ClientName);
            connectedPerson = thread.Person;
            connectedPerson.currentSession = 
                new Session(
                    dataFromMessage.dateTime,
                    dataFromMessage.ConnectedErgometer,
                    dataFromMessage.ConnectedHeartRateMonitor
                    );
            connectedPerson.addSession(connectedPerson.currentSession);
            thread.UpdateAll();

            protocol.changeState(new RecievingData(protocol, thread));
            return "Ready to recieve data";
        }
    }

    class RecievingData(DataProtocol.DataProtocol protocol, CommunicationThread thread) : State(protocol, thread)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }

            //TODO: add regex to ensure correct formatting before saving message
            thread.Person.currentSession.addMessage(input, ClientType.CLIENT);
            thread.UpdateAll();
            return "Ready to recieve data";
        }
    }
}
