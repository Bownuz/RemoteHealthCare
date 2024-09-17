using Avans.TI.BLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientapplication {
    internal class HeartRateConnection : IHardwareConnection
    {
        public void Connect(string serialNumber) {
            throw new NotImplementedException();
        }

        public void Disconnect(string serialNumber) {
            throw new NotImplementedException();
        }

        public void SubscribtionValueChanged(object sender, BLESubscriptionValueChangedEventArgs args) {
            throw new NotImplementedException();
        }
    }
}
