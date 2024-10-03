using System;

namespace ConnectionImplemented {
    internal class HeartRateMonitor : BleDevice {
        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement") {
        }

        public override double ConvertData(byte[] rawData) {
            int heartRate = ExtractHeartRate(rawData);
            if (heartRate != -1) {
                return heartRate;
            } else {
                return 0;
            }
        }
        private int ExtractHeartRate(byte[] data)
        {        
            if (data.Length < 2)
            {
                return -1;
            }
         
            int heartRate = data[1];                  
            if (heartRate >= 30 && heartRate <= 220)
            {
                return heartRate;
            }         
           return -1;
        }
    }
}

