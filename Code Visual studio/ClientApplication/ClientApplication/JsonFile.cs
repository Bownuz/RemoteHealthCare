using System;
using System.Text.Json;


namespace ClientApplication {
    internal struct JsonPatientData {
        public int bicycleSpeed { get; }
        public int heartrate { get; }
        public DateTime dateTime { get; }

        public JsonPatientData(int bicycleSpeed, int heartrate, DateTime dateTime) {
            this.bicycleSpeed = bicycleSpeed;
            this.heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }

    internal struct JsonPatientID {
        public string patientID { get; }
        public string ergometerID { get; }
        public string heartRateMonitorID { get; }
        public DateTime dateTime { get; }

        public JsonPatientID(string patientID, string ergometerID, string heartRateMonitorID, DateTime dateTime) {
            this.patientID = patientID;
            this.ergometerID = ergometerID;
            this.heartRateMonitorID = heartRateMonitorID;
            this.dateTime = dateTime;
        }
    }
}
