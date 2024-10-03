using Avans.TI.BLE;
using HardwareClientApplication;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionImplemented {
    internal abstract class BleDevice {
        private String currentData;
        private BLE bleDevice;
        private String service;
        private String characteristic;
        private double currentValue;
        protected DataHandler handler;


        protected BleDevice(String service, String characteristic) {
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
            Console.WriteLine("subscribed to characteristic!");
        }   

        public void SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs subsciptionEvent) {
            ConvertData(subsciptionEvent.Data);
            updateDataToHandler();
        }

        protected abstract void updateDataToHandler();

        public abstract double ConvertData(byte[] rawData);

        internal void setDataHandler(DataHandler dataHandler)
        {
            this.handler = dataHandler;
        }
    }
}
