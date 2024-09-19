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
            // Byte 0: Data Page Number (7 bits)
            int dataPageNumber = rawData[0] & 0x7F;  // Bits 0:6 voor het Data Page nummer
            Console.WriteLine($"Data Page Number: {dataPageNumber}");

            // Byte 0 (Bit 7): Page Change Toggle
            bool pageChangeToggle = (rawData[0] & 0x80) != 0;
            Console.WriteLine($"Page Change Toggle: {pageChangeToggle}");

            // Byte 1: Flags (1 bit, Bit 0: Stop Indicator)
            bool isStopped = (rawData[1] & 0x01) == 1;  // Bit 0 voor stopindicator
            Console.WriteLine($"Fiets gestopt: {isStopped}");

            // Byte 4-5: Bike Speed Event Time (2 bytes)
            int bikeSpeedEventTime = (rawData[5] << 8) | rawData[4];  // MSB en LSB combineren
            double eventTimeInSeconds = bikeSpeedEventTime / 1024.0;  // omrekenen naar seconden
            Console.WriteLine($"Fietssnelheid evenement tijd: {eventTimeInSeconds} seconden");

            // Byte 6-7: Cumulative Speed Revolution Count (2 bytes)
            int cumulativeSpeedRevolutionCount = (rawData[7] << 8) | rawData[6];  // MSB en LSB combineren
            Console.WriteLine($"Cumulatief aantal wielomwentelingen: {cumulativeSpeedRevolutionCount}");

            return "";
        }

        public void sendEncodedMessage(int resistanceValue)
        {

        }

    }
}
