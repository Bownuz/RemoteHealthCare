using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.Vr {
    internal class VrInit {
        public static void StandardInit(string ipAddr) {
            VrScene.NewScene(ipAddr);
            VrScene.NewNodeSceneAndTerrain();
            VrScene.DeleteGroundPlaneNode();
            VrScene.AddSceneNodeLayer();

            VrScene.NewRoute();
            VrScene.AddRoadToNewestRoute();
            VrScene.GetScene();

            VrScene.FollowNewestRoute();
        }
    }
}
