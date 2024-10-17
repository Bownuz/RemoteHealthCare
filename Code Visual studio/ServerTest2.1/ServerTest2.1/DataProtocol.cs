namespace Server.DataProtocol {
    internal class DataProtocol
    {


        internal void processMessage(String incommingMessage)
        {
            throw new NotImplementedException();
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
