using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApplication.Vr {
    internal class SceneTerrain {

        public struct Data {
            public List<int> size { get; }
            public List<int> heights { get; }

            public Data(List<int> size, List<int> heights) {
                this.size = size; this.heights = heights;
            }
        }

        public struct ST {
            public string id { get; }
            public Data data { get; }

            public ST(string id, Data data) {
                this.id = id; this.data = data;
            }
        }

        public static string SceneTerrainAdd(int width, int height) {
            List<int> heights = new List<int>();
            for (int i = 0; i < width*height; i++) {
                heights.Add(0);
            }
            Data data = new Data(new List<int> { width, height },heights);
            return JsonSerializer.Serialize(new ST("scene/terrain/add",data));
        }
    }
}
