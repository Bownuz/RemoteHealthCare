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

        private int ExtractHeartRate(byte[] data) {
            if(data.Length < 2) {
                return -1;
            }

            
            bool is16Bit = (data[0] & 0x01) != 0;

            int heartRate;
            if(is16Bit && data.Length >= 3) {
                // 16-bit hartslag
                heartRate = data[2] | (data[3] << 8);
            }
            else {
                // 8-bit hartslag
                heartRate = data[1];
            }

            // Controleer of de hartslag binnen een realistische range valt
            if(heartRate >= 30 && heartRate <= 220) {
                return heartRate;
            }       
            return -1;
        }
    }
}

