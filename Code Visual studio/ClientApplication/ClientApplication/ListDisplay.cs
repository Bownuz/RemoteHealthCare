using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace ConnectionImplemented {
    internal class ListDisplay {
        private System.Windows.Forms.ListBox deviceListBox;

        public ListDisplay(System.Windows.Forms.ListBox deviceListBox) {
            this.deviceListBox = deviceListBox;
        }

        public void ShowDeviceList() {
            BLE bleBike = new BLE();
            Thread.Sleep(4000);
            List<string> BluetoothDeviceList = bleBike.ListDevices();
            foreach (string device in BluetoothDeviceList) {
                deviceListBox.Items.Add(device);
            }
        }

        public void ShowServiceList() {
            BLE ble = new BLE();
            var services = ble.GetServices;
            foreach (var service in services) {
                deviceListBox.Items.Add($"Service: {service.Name}");
            }
        }

        public void ShowCharacteristicsList() {
            BLE ble = new BLE();
            var Characteristics = ble.GetCharacteristics;
            foreach (var characteristic in Characteristics) {
                deviceListBox.Items.Add($"Characteristic: {characteristic.Name}");
            }
        }
    }
}
