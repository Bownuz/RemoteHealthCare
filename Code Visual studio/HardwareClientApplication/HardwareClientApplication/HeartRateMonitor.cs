using System;


namespace ConnectionImplemented {
    internal class HeartRateMonitor : BleDevice {
        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement") {
        }

        public override double ConvertData(byte[] rawData) {
            return rawData[2];
        }
    }
}
