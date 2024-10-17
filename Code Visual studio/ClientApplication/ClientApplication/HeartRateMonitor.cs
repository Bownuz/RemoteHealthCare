using System;


namespace ConnectionImplemented {
    internal class HeartRateMonitor : BleDevice {
        private int heartRate { get; set; }

        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement") {
        }

        public override void ConvertData(byte[] rawData) {
            int heartRate = ExtractHeartRate(rawData);
            if (heartRate != -1) {
                this.heartRate = heartRate;
            }
        }
        private int ExtractHeartRate(byte[] data) {
            if (data.Length < 2) {
                return -1;
            }

            int heartRate = data[1];
            if (heartRate >= 30 && heartRate <= 220) {
                return heartRate;
            }
            return -1;
        }

        protected override void updateDataToHandler() {
            base.handler.updateCurrentHeartRate(heartRate);
        }

        public double getCurrentHeartRate() {
            return heartRate;
        }
    }
}

