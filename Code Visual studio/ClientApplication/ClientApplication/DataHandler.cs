using ConnectionImplemented;
using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication {
    public class DataHandler {
        public string deviceName { get; set; }
        public int currentSpeed { get; set; }
        public int currentHeartRate { get; set; }
        private string patientName;
        private string ergometerID;
        private string heartRateMonitorID;
        Ergometer ergometer;
        HeartRateMonitor heartRateMonitor;
        Boolean simulatorIsActive;
        BleDevice[] bleDevices;
        PatientSendData jsondata;

        public DataHandler(BleDevice[] bleDevices, string heartRateMonitorID, string ergometerID, string patientName, Ergometer ergometer, HeartRateMonitor heartRateMonitor, Boolean simulatorIsActive) {
            currentHeartRate = 0;
            currentSpeed = 0;
            this.bleDevices = bleDevices;
            this.ergometer = ergometer;
            this.patientName = patientName;
            this.heartRateMonitor = heartRateMonitor;
            this.simulatorIsActive = simulatorIsActive;
            this.heartRateMonitorID = heartRateMonitorID;
            this.ergometerID = ergometerID;
            this.jsondata = new PatientSendData();

            foreach (BleDevice device in bleDevices) {
                initializeDeviceAsync(device, heartRateMonitorID, ergometerID);
                device.setDataHandler(this);
            }

            if (simulatorIsActive) {
                TcpClient client = new TcpClient("145.49.9.63", 4789);
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
            ergometer.SubscriptionValueChanged(bikeData);
            heartRateMonitor.SubscriptionValueChanged(heartRateData);
        }

        internal void updateCurrentHeartRate(int heartRate) {
            currentHeartRate = heartRate;
        }

        internal void updateCurrentSpeed(int speed) {
            currentSpeed = speed;
        }

        public string printDataAsJson() {
            PatientSendData sessionData = new PatientSendData(patientName, currentSpeed, currentHeartRate, DateTime.Now);
            return JsonSerializer.Serialize<PatientSendData>(sessionData);
        }

        internal string PatientInitialisationMessage() {
            PatientInitialisationMessage jsonPatientID = new PatientInitialisationMessage(patientName, ergometerID, heartRateMonitorID, DateTime.Now);
            this.patientName = patientName;
            return JsonSerializer.Serialize<PatientInitialisationMessage>(jsonPatientID);
        }
    }
}
