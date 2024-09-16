using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientapplication {
    internal class Connection {
        HeartRateConnection heartRateConnection = new HeartRateConnection();
        BicycleConnection bicycleConnection = new BicycleConnection();
        ServerConnection serverConnection = new ServerConnection();
    }
}
