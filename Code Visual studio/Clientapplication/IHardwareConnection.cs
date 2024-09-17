using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avans.TI.BLE;

namespace Clientapplication {
    public interface IHardwareConnection {
        void Connect(String serialNumber);
        
        void Disconnect(String serialNumber);

        void SubscribtionValueChanged(Object sender, BLESubscriptionValueChangedEventArgs args);
    
    }
}
