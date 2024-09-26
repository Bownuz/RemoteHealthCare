using Avans.TI.BLE;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionImplemented
{
    internal abstract class BleDevice
    {
        private String currentData;
        private BLE bleDevice;
        private String service;
        private String characteristic;
        protected BleDevice(String service, String characteristic)
        {
            this.currentData = "";
            this.service = service;
            this.characteristic = characteristic;
            this.bleDevice = new BLE();
        }

        public async Task ConnectToBLE_Device(String deviceName)
        {
            //await Task.Run(async () => {
                int errorCode = 0;
                do
                {
                    Console.Write(".");
                    errorCode = await bleDevice.OpenDevice(deviceName);
                }
                while (errorCode != 0);
                Console.WriteLine("Device conncected");

                do
                {
                    Console.Write(".");
                    errorCode = await bleDevice.SetService(service);
                }
                while (errorCode != 0);
                Console.WriteLine("Service Set!");

                bleDevice.SubscriptionValueChanged += SubscriptionValueChanged;
                do
                {
                    Console.Write(".");
                    errorCode = await bleDevice.SubscribeToCharacteristic(characteristic);
                }
                while (errorCode != 0);
                Console.WriteLine("subscribed to characteristic!");
            //});
        }

        public void SubscriptionValueChanged(object sender, BLESubscriptionValueChangedEventArgs subsciptionEvent)
        {
           

            Console.WriteLine(ConvertData(subsciptionEvent.Data));
        }

        public abstract String ConvertData(byte[] rawData);

    }
}
