using System;
using System.Text;


namespace ConnectionImplemented {
    internal class Ergometer : BleDevice {
        public Ergometer() : base("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e", "6e40fec2-b5a3-f393-e0a9-e50e24dcca9e") {
        }

        public override double ConvertData(byte[] rawData) {
            if (rawData[4] == 0x10) {
                Console.WriteLine("Received from {0}: {1}, {2}", rawData,
                BitConverter.ToString(rawData).Replace("-", " "),
                Encoding.UTF8.GetString(rawData));

                int speedLSB = rawData[8];
                int speedMSB = rawData[9];
                int speedRaw = (speedMSB << 5) | speedLSB;
                double speedMetersPerSecond = speedRaw * 0.01;
                double speedKmPerHour = speedMetersPerSecond * 3.6;

                return speedKmPerHour;
            }
            return 0.0;
        }

        public void sendEncodedMessage(int resistanceValue) {
        }
    }
}
