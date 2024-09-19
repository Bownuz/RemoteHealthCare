using System;


namespace ConnectionImplemented
{
    internal class HeartRateMonitor : BleDevice
    {
        public HeartRateMonitor() : base("HeartRate", "HeartRateMeasurement")
        {

        }

        public override string ConvertData(byte[] rawData)
        {
            return "raw bpm data: " + rawData[2].ToString();

        }
    }
}
