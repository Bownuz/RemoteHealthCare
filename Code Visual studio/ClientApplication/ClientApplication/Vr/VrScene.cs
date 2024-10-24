using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication.Vr {
    internal class VrScene {
        private static string scene;
        private static string newestRoute;
        private static string nodeIdRoute;
        public static void NewScene(string ipAdressVrServer) {
            VrConnection.NewConnection(ipAdressVrServer, true);
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_reset.json");
            for (; ; ) {
                string newScene = VrConnection.GetMostRecentTunnelResponseUsingString("scene/reset");
                if (newScene == null) {
                    Thread.Sleep(10);
                    continue;
                }
                scene = newScene;
                break;
            }
        }

        public static void GetScene() {
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_get.json");
            for (; ; ) {
                string newScene = VrConnection.GetMostRecentTunnelResponseUsingString("scene/get");
                if (newScene == null) {
                    Thread.Sleep(10);
                    continue;
                }
                scene = newScene;
                break;
            }
        }

        public static void NewNodeSceneAndTerrain() {
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_terrain_add.json");
            VrConnection.SendJsonObjectViaTunnelFromFile("scene_node_add.json");
            GetScene();
        }

        public static void NewRoute() {
            VrConnection.SendJsonObjectViaTunnelFromFile("route_add.json");
            for (; ; ) {
                string newRoute = VrConnection.GetMostRecentTunnelResponseUsingString("route/add");
                if (newRoute == null) {
                    Thread.Sleep(10);
                    continue;
                }
                newestRoute = newRoute;
                break;
            }
        }

        public static void FollowNewestRoute() {
            nodeIdRoute = GetNodeUUIDFromSceneChildren("Camera");
            string routeId = JsonDocument.Parse(newestRoute).RootElement.GetProperty("data").GetProperty("data").GetProperty("data").GetProperty("uuid").ToString();
            VrConnection.SendJsonObjectViaTunnelFromBytes(Encoding.ASCII.GetBytes($@"{{
                                    ""id"" : ""route/follow"",
                                    ""data"" : {{
                                        ""route"" : ""{routeId}"",
                                        ""node"" : ""{nodeIdRoute}"",
                                        ""speed"" : 0.0,
                                        ""offset"" : 0.0,
                                        ""rotate"" : ""XZ"",
                                        ""smoothing"" : 1.0,
                                        ""followHeight"" : false,
                                        ""rotateOffset"" : [ 0, 0, 0 ],
                                        ""positionOffset"" : [ 0, 0, 0 ]
                                    }}
                                }}"));
        }

        public static void ChangeRouteSpeed(double speed) {
            if (nodeIdRoute == null)
                return;

            VrConnection.SendJsonObjectViaTunnelFromBytes(Encoding.ASCII.GetBytes($@"{{
                                    ""id"" : ""route/follow/speed"",
                                    ""data"" : {{
                                        ""node"" : ""{nodeIdRoute}"",
                                        ""speed"" : {speed.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}
                                    }}
                                }}"));
        }

        public static void AddRoadToNewestRoute() {
            VrConnection.SendJsonObjectViaTunnelFromBytes(Encoding.ASCII.GetBytes($@"{{
                                    ""id"" : ""scene/road/add"",
                                    ""data"" : {{
                                        ""route"" : ""{JsonDocument.Parse(newestRoute).RootElement.GetProperty("data").GetProperty("data").GetProperty("data").GetProperty("uuid").ToString()}"",
                                        ""diffuse"" : ""data/NetworkEngine/textures/tarmac_diffuse.png"",
		                                ""normal"" : ""data/NetworkEngine/textures/tarmac_normal.png"",
		                                ""specular"" : ""data/NetworkEngine/textures/tarmac_specular.png"",
                                        ""heightoffset"" : 0.01
                                    }}
                                }}"

                ));
        }

        private static string GetNodeUUIDFromSceneChildren(string name) {
            JsonElement sceneComponents = JsonDocument.Parse(scene).RootElement.GetProperty("data").GetProperty("data").GetProperty("data").GetProperty("children");
            string nodeId = null;
            for (int i = sceneComponents.GetArrayLength() - 1; i > -1; i--) {
                if (sceneComponents[i].GetProperty("name").ToString().Equals(name))
                    nodeId = sceneComponents[i].GetProperty("uuid").ToString();
            }
            return nodeId;
        }

        public static void DeleteGroundPlaneNode() {
            JsonElement sceneComponents = JsonDocument.Parse(scene).RootElement.GetProperty("data").GetProperty("data").GetProperty("data").GetProperty("children");
            string nodeId = null;
            for (int i = sceneComponents.GetArrayLength() - 1; i > -1; i--) {
                if (sceneComponents[i].GetProperty("name").ToString().Equals("GroundPlane"))
                    nodeId = sceneComponents[i].GetProperty("uuid").ToString();
            }
            VrConnection.SendJsonObjectViaTunnelFromBytes(Encoding.ASCII.GetBytes($@"{{
                                    ""id"" : ""scene/node/delete"",
                                    ""data"" : {{
                                        ""id"" : ""{GetNodeUUIDFromSceneChildren("GroundPlane")}""
                                    }}
                                }}"

                ));
        }
    }
}
