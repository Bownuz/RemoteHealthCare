using Server.DataProtocol.doctor;
using Server.DataStorage;
using Server.Patterns.State;
using Server.Patterns.State.Client;
using Server.ThreadHandlers;

namespace Server.DataProtocol {
    internal class DataProtocol
    {
        State State;

        public DataProtocol(ClientType clientType, CommunicationThread communicationThread)
        {
            switch (clientType) {
                case ClientType.CLIENT:
                    this.State = new WelcomeClient(this);
                    break;
                case ClientType.DOCTOR:
                    this.State = new WelcomeDoctor(this);
                    break;
            
            }
        }

        internal void processMessage(String incommingMessage)
        {
            State.CheckInput(incommingMessage);
        }

        public void changeState(State newState) { 
         this.State = newState;
        }
    }

    internal struct ClientRecieveData
    {
        public String patientName { get; set; }
        public double BicycleSpeed {get; set; }
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
