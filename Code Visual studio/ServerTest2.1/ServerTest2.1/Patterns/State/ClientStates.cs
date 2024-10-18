using Server.DataStorage;
using Server.ThreadHandlers;
using System.Text.Json;

namespace Server.Patterns.State.Client

{
    class WelcomeClient(DataProtocol.DataProtocol protocol) : State(protocol)
    {
        public override string CheckInput(string input)
        {
            this.protocol.changeState(new InitializePatient(protocol));
            return "Welcome Client";
        }   
    }

    class InitializePatient(DataProtocol.DataProtocol protocol) : State(protocol)
    {
        CommunicationThread? thread;

        public void setCommunication(CommunicationThread thread) { 
            this.thread = thread;
        }

        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication")) {
                return "Goodbye";
            }

            ClientInitialisationMessage dataFromMessage = JsonSerializer.Deserialize<ClientInitialisationMessage>(input);
            Person connectedPerson;
            FileStorage storageFromThread = thread.fileStorage;

            if (!storageFromThread.PatientExists(dataFromMessage.ClientName))
            {
                storageFromThread.AddPatient(dataFromMessage.ClientName);
            }
            connectedPerson = storageFromThread.GetPatient(dataFromMessage.ClientName);
            connectedPerson.currentSession = 
                new Session(
                    dataFromMessage.dateTime,
                    dataFromMessage.ConnectedErgometer,
                    dataFromMessage.ConnectedHeartRateMonitor
                    );
            connectedPerson.addSession(connectedPerson.currentSession);

            protocol.changeState(new RecievingData(protocol));
            return "Ready to recieve data";
        }
    }

    class RecievingData(DataProtocol.DataProtocol protocol) : State(protocol)
    {
        public override string CheckInput(string input)
        {
            if (input.Equals("Quit Communication"))
            {
                return "Goodbye";
            }



            return "Ready to recieve data";
        }
    }
}
