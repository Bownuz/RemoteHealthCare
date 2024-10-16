﻿using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConnectionImplemented {
    internal class ListDisplay {
        public static void ShowDeviceList() {
            BLE bleBike = new BLE();
            Thread.Sleep(4000);
            List<string> BluetoothDeviceList = bleBike.ListDevices();
            Console.WriteLine("The available devices are : ");
            foreach (string device in BluetoothDeviceList) {
                Console.WriteLine($"Device: {device}");
            }
        }

        public static void ShowServiceList() {
            BLE ble = new BLE();
            var services = ble.GetServices;
            Console.WriteLine("The available services are : ");
            foreach (var service in services) {
                Console.WriteLine($"Service: {service.Name}");
            }
        }

        public static void ShowCharacteristicsList() {
            BLE ble = new BLE();
            var Characteristics = ble.GetCharacteristics;
            Console.WriteLine("The available charateristics are : ");
            foreach (var characteristic in Characteristics) {
                Console.WriteLine($"Service: {characteristic.Name}");
            }
        }
    }
}
