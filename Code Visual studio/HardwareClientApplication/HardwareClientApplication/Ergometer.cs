using System;


namespace ConnectionImplemented
{
    internal class Ergometer : BleDevice
    {
        public Ergometer() : base("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e", "6e40fec2-b5a3-f393-e0a9-e50e24dcca9e")
        {
        }

        public override string ConvertData(byte[] rawData)
        {
            double speedKmPerHour = 0;
                if (rawData[4] == 0x10) {
                    int speedLSB = rawData[8];
                    int speedMSB = rawData[9];
                    int speedRaw = (speedMSB << 5) | speedLSB;

                   
                    double speedMetersPerSecond = speedRaw * 0.001;
                    speedKmPerHour = speedMetersPerSecond * 3.6;
                    Console.WriteLine($"Snelheid: {speedKmPerHour} km/h");
               
                }


            return $"Snelheid: {speedKmPerHour} km/h";
        }

        public void sendEncodedMessage(int resistanceValue)
        {

        }

    }
}
