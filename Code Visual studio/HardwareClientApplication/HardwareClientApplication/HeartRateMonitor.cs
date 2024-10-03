using System;


namespace ConnectionImplemented {
    internal class HeartRateMonitor : BleDevice {
        private double heartRate { get; set; }


        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement") {
        }

        public override double ConvertData(byte[] rawData) {
            heartRate = rawData[2];
            return rawData[2];
        }

        protected override void updateDataToHandler()
        {
            base.handler.updateCurrentHeartRate(heartRate);
        }
    }
}
