using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareClientApplication.Vr {
    internal class VrScene {
        // create scene
        public static void NewScene(string ipAdressVrServer) {
            VrConnection.NewConnection(ipAdressVrServer, true);
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_reset.json");
        }

        public static void GetScene() {
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_get.json");
        }

        public static void NewNodeSceneAndTerrain() {
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_terrain_add.json");
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_node_add.json");
        }

        public static void NewRoute() {
            VrConnection.SendJsonObjectViaTunnelFromFile("route_add.json");
        }

        // create route?
        // send data scene
    }
}
