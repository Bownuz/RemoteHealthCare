using System;

namespace ConnectionImplemented {
    internal class HeartRateMonitor : BleDevice {
        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement") {
        }

        public override string ConvertData(byte[] rawData) {
            int heartRate = ExtractHeartRate(rawData);
            if(heartRate != -1) {
                return "Hartslag: " + heartRate + " bpm";
            }
            else {
                return "Geen geldige hartslag gevonden.";
            }
        }

        private int ExtractHeartRate(byte[] data)
        {
            // Controleer of de data minstens 2 bytes bevat
            if (data.Length < 2)
            {
                return -1;
            }

            // Lees de hartslag uit de tweede byte (8-bits)
            int heartRate = data[1];

            // Controleer of de hartslag binnen een realistische range valt
            if (heartRate >= 30 && heartRate <= 220)
            {
                return heartRate;
            }

            // Als de hartslag buiten de realistische range valt, retourneer -1
            return -1;
        }
    }
}

