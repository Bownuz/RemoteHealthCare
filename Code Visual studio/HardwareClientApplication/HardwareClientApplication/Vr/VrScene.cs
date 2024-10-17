using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareClientApplication.Vr {
    internal class VrScene {
        // create scene
        public static void NewScene(string ipAdressVrServer) {
            //VrConnection.NewConnection(ipAdressVrServer);
            //VrConnection.CreateTunnel();
            VrConnection.NewConnection(ipAdressVrServer, true);
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_get.json");
        }
        // send data scene
    }
}
