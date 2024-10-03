using ConnectionImplemented;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HardwareClientApplication
{
    internal class DataHandler
    {
        public double currentSpeed { get; set; }
        public double currentHeartRate { get; set; }
        BleDevice[] bleDevices;

        public DataHandler(BleDevice[] bleDevices)
        {
            currentHeartRate = 0;
            currentSpeed = 0;
            this.bleDevices = bleDevices;
            foreach (BleDevice device in bleDevices)
            {
                initializeDeviceAsync(device);
                device.setDataHandler(this);
            }
        }

        private async Task initializeDeviceAsync(BleDevice device)
        {
            String deviceName;
            if (device is HeartRateMonitor)
            {
                Console.Write("enter heart rate device: ");
            }
            else if (device is Ergometer)
            {
                Console.Write("enter ergometer device: ");

            }
            
            deviceName = Console.ReadLine();
            await device.ConnectToBLE_Device(deviceName);
        }

        internal void updateCurrentHeartRate(double heartRate)
        {
            currentHeartRate = heartRate;
        }

        internal void updateCurrentSpeed(double speed)
        {
            currentSpeed = speed;
        }

        internal String printAsJson()
        {
            JsonData data = new JsonData(currentSpeed, (int)currentHeartRate, DateTime.Now);
            return JsonSerializer.Serialize<JsonData>(data);
        }
    }
}
