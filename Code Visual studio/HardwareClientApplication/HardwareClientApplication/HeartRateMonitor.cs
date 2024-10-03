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

<<<<<<< Updated upstream
        private int ExtractHeartRate(byte[] data) {
            // Controleer of de data minstens 2 bytes bevat
            if (data.Length < 2) {
=======
        private int ExtractHeartRate(byte[] data)
        {
         
            if (data.Length < 2)
            {
>>>>>>> Stashed changes
                return -1;
            }
         
            int heartRate = data[1];

<<<<<<< Updated upstream
            // Controleer of de hartslag binnen een realistische range valt
            if (heartRate >= 30 && heartRate <= 220) {
=======
            
            if (heartRate >= 30 && heartRate <= 220)
            {
>>>>>>> Stashed changes
                return heartRate;
            }

           
            return -1;
        }
    }
}

