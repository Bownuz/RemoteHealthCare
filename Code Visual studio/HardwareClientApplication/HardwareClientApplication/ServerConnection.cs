﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareClientApplication {
    internal class ServerConnection {

        public static void HandleConnection() {
            TcpClient client = new TcpClient("127.0.0.1", 4789);
        }
    }
}
