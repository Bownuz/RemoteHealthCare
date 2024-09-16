using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Clientapplication {
    internal class ServerConnection {
        Socket connection;
        ServerReciever serverReciever = new ServerReciever();
    }
}
