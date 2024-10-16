namespace DataProtocol {
    internal struct ClientRecieveData {
        public string PatientName { get; set; }
        public double BicycleSpeed { get; set; }
        public int Heartrate { get; set; }
        public DateTime DateTime { get; set; }

        public ClientRecieveData(string patientName, double bicycleSpeed, int heartrate, DateTime dateTime) {
            PatientName = patientName;
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            DateTime = dateTime;
        }
    }
}

