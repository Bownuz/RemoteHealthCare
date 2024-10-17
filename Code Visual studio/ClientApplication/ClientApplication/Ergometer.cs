using Avans.TI.BLE;
using System;
using System.Text;
using System.Threading.Tasks;


namespace ConnectionImplemented {
    internal class Ergometer : BleDevice {
        public double speedKmPerHour { get; set; }
        private byte resistance;

        public Ergometer() : base("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e", "6e40fec2-b5a3-f393-e0a9-e50e24dcca9e") {
        }

        public override void ConvertData(byte[] rawData) {
            if (rawData[4] == 0x10) {
                Console.WriteLine("Received from {0}: {1}, {2}", rawData,
                BitConverter.ToString(rawData).Replace("-", " "),
                Encoding.UTF8.GetString(rawData));

                int speedLSB = rawData[8];
                int speedMSB = rawData[9];
                int speedRaw = (speedMSB << 5) | speedLSB;
                double speedMetersPerSecond = speedRaw * 0.01;
                speedKmPerHour = speedMetersPerSecond * 3.6;

            }
        }

        public async Task sendResistanceValueAsync(byte resistanceValue) {
            resistance = resistanceValue;
            resistanceValue *= 2;
            byte[] resistanceData = new byte[13];

            resistanceData[0] = 0xA4;
            resistanceData[1] = 0x09;
            resistanceData[2] = 0x4E;
            resistanceData[3] = 0x05;
            resistanceData[4] = 0x30;
            for (int i = 5; i < 10; i++) {
                resistanceData[i] = 0xFF;
            }
            resistanceData[11] = resistanceValue;
            byte checksum = 0;
            for (int i = 0; i < 12; i++) {
                checksum += resistanceData[i];
            }
            resistanceData[12] = (byte)(checksum % 256);

            int errorCode = await bleDevice.WriteCharacteristic("6e40fec3-b5a3-f393-e0a9-e50e24dcca9e", resistanceData);
            if (errorCode != 0) {
                Console.WriteLine("Kon de weerstand niet instellen.");
            } else {
                Console.WriteLine($"Weerstand ingesteld op {resistanceValue}%.");
            }
        }

        protected override void updateDataToHandler() {
            base.handler.updateCurrentSpeed(speedKmPerHour);
        }

        public double getCurrentSpeed() {
            return speedKmPerHour;
        }

        public byte getResistanceValue() {
            return resistance;
        }
    }
}
