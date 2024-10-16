using System;
using System.Text.Json;


namespace HardwareClientApplication {
    internal struct JsonData {
        public double BicycleSpeed { get; }
        public int Heartrate { get; }
        public DateTime dateTime { get; }

        public JsonData(double bicycleSpeed, int heartrate, DateTime dateTime) {
            BicycleSpeed = bicycleSpeed;
            Heartrate = heartrate;
            this.dateTime = dateTime;
        }
    }
}
