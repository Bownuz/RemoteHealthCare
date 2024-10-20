using Avans.TI.BLE;
using ConnectionImplemented;
using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication {
    internal class DataHandler {
        public string deviceName { get; set; }
        public int currentSpeed { get; set; }
        public int currentHeartRate { get; set; }
        Ergometer ergometer;
        HeartRateMonitor heartRateMonitor;
        Boolean simulatorIsActive;
        BleDevice[] bleDevices;
        JsonData jsondata;

        public DataHandler(BleDevice[] bleDevices, string heartRateMonitorID, string ergometerID, Ergometer ergometer, HeartRateMonitor heartRateMonitor, Boolean simulatorIsActive) {
            currentHeartRate = 0;
            currentSpeed = 0;
            this.bleDevices = bleDevices;
            this.ergometer = ergometer;
            this.heartRateMonitor = heartRateMonitor;
            this.simulatorIsActive = simulatorIsActive;
            this.jsondata = new JsonData();

            foreach (BleDevice device in bleDevices) {
                initializeDeviceAsync(device, heartRateMonitorID, ergometerID);
                device.setDataHandler(this);
            }

            if (simulatorIsActive) {
                //TcpClient client = new TcpClient("192.168.178.101", 4790);
                TcpClient client = new TcpClient("localhost", 4790);
                Thread connectionThread = new Thread(() => SimulatorConnection.HandleConnection(client, this));
                connectionThread.Start();
            }
        }

        private async Task initializeDeviceAsync(BleDevice device, string heartRateMonitorID, string ergometerID) {
            String deviceName = null;
            if (device is HeartRateMonitor) {
                deviceName = heartRateMonitorID;
            } else if (device is Ergometer) {
                deviceName = ergometerID;
            }

            if (!simulatorIsActive) {
                await device.ConnectToBLE_Device(deviceName);
            }
        }

        public void SimulateBLEData(byte[] bikeData, byte[] heartRateData) {
            Console.WriteLine(bikeData);
            Console.WriteLine(heartRateData);
            ergometer.SubscriptionValueChanged(bikeData);
            heartRateMonitor.SubscriptionValueChanged(heartRateData);
        }

        internal void updateCurrentHeartRate(int heartRate) {
            currentHeartRate = heartRate;
        }

        internal void updateCurrentSpeed(int speed) {
            currentSpeed = speed;
        }

        internal String printDataAsJson() {
            JsonData sessionData = new JsonData(currentSpeed, currentHeartRate, DateTime.Now);
            return JsonSerializer.Serialize<JsonData>(sessionData);
        }

        internal string ConvertPatientDataToJson() {
            JsonData patientInfo = new JsonData(currentSpeed, currentHeartRate, DateTime.Now);
            return JsonSerializer.Serialize<JsonData>(patientInfo);
        }
    }
}
