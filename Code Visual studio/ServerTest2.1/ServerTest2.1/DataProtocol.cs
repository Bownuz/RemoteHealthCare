using Server.DataProtocol.doctor;
using Server.DataStorage;
using Server.Patterns.State;
using Server.ThreadHandlers;
using Server.Patterns.State.Client;

namespace Server.DataProtocol
{
    internal class DataProtocol
    {
        private State State;
        private CommunicationThread communicationThread;

        public DataProtocol(ClientType clientType, CommunicationThread communicationThread)
        {
            this.communicationThread = communicationThread;

            switch (clientType)
            {
                case ClientType.CLIENT:
                    this.State = new WelcomeClient(this);
                    break;
                case ClientType.DOCTOR:
                    this.State = new WelcomeDoctor(this);
                    break;

            }
        }

        internal String processMessage(String incommingMessage)
        {
            return State.CheckInput(incommingMessage);
        }

        public void changeState(State newState)
        {
            if (newState is InitializePatient)
            { 
                InitializePatient initializePatient = (InitializePatient)newState;
                initializePatient.setCommunication(communicationThread);
                this.State = initializePatient;
                return;
            }

            this.State = newState;
        }
    }

    internal struct ClientRecieveData
    {
        public String patientName { get; set; }
        public double BicycleSpeed { get; set; }
        public int Heartrate { get; set; }
        public DateTime dateTime { get; set; }

        public ClientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime)
        {
            this.patientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }


}
