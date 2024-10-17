using ConnectionImplemented;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HardwareClientApplication {
    internal class DataHandler {
        public string deviceName { get; set; }
        public double currentSpeed { get; set; }
        public double currentHeartRate { get; set; }
        BleDevice[] bleDevices;

        public DataHandler(BleDevice[] bleDevices, string heartRateMonitorID, string ergometerID) {
            currentHeartRate = 0;
            currentSpeed = 0;
            this.bleDevices = bleDevices;

            foreach (BleDevice device in bleDevices) {
                initializeDeviceAsync(device, heartRateMonitorID, ergometerID);
                device.setDataHandler(this);
            }
        }

        private async Task initializeDeviceAsync(BleDevice device, string heartRateMonitorID, string ergometerID) {
            String deviceName = null;
            if (device is HeartRateMonitor) {
                deviceName = heartRateMonitorID;
            } else if (device is Ergometer) {
                deviceName = ergometerID;
            }

            await device.ConnectToBLE_Device(deviceName);
        }

        internal void updateCurrentHeartRate(double heartRate) {
            currentHeartRate = heartRate;
        }

        internal void updateCurrentSpeed(double speed) {
            currentSpeed = speed;
        }

        internal String printAsJson() {
            JsonData data = new JsonData(currentSpeed, (int)currentHeartRate, DateTime.Now);
            return JsonSerializer.Serialize<JsonData>(data);
        }
    }
}
