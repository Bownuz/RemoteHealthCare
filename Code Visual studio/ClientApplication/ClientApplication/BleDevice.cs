using Avans.TI.BLE;
using ClientApplication;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionImplemented {
    public abstract class BleDevice {
        private String currentData;
        private String service;
        private String characteristic;
        private double currentValue;
        public BLE bleDevice;
        public DataHandler handler;

        public BleDevice(String service, String characteristic) {
            this.currentData = "";
            this.service = service;
            this.characteristic = characteristic;
            this.bleDevice = new BLE();
        }

        public async Task ConnectToBLE_Device(String deviceName) {
            int errorCode = 0;
            do {
                Console.Write(".");
                errorCode = await bleDevice.OpenDevice(deviceName);
            }
            while (errorCode != 0);
            Console.WriteLine("Device conncected");

            do {
                Console.Write(".");
                errorCode = await bleDevice.SetService(service);
            }
            while (errorCode != 0);
            Console.WriteLine("Service Set!");

            bleDevice.SubscriptionValueChanged += SubscriptionValueChanged;
            do {
                Console.Write(".");
                errorCode = await bleDevice.SubscribeToCharacteristic(characteristic);
            }
            while (errorCode != 0);
            Console.WriteLine("Subscribed to characteristic!");
        }

        public void SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs subsciptionEvent) {
            ConvertData(subsciptionEvent.Data);
            UpdateDataToHandler();
        }

        public void SubscriptionValueChanged(byte[] rawData) {
            ConvertData(rawData);
            UpdateDataToHandler();
        }

        public abstract void UpdateDataToHandler();

        public abstract void ConvertData(byte[] rawData);

        internal void setDataHandler(DataHandler dataHandler) {
            this.handler = dataHandler;
        }
    }
}
