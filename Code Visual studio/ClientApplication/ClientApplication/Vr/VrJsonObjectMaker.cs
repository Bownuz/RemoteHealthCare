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
            public List<double> heights { get; }

            public Data(List<int> size, List<double> heights) {
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
            List<double> heights = new List<double>();
            //Random random = new Random();
            for (int i = 0; i < width * height; i++) {
                heights.Add(0);
            }
            for (int i = 171; i < 210; i++) {
                double step = 5 - (5 * (209 - i) / 38.0);
                for (int j = 0; j < 256; j++) {
                    heights[i*256+j] = step;
                    //Console.WriteLine(i*256+j);
                    //Console.WriteLine(5 - (5 * (209 - i) / 38.0));
                }
            }
            for (int i = 210; i < 216; i++) {
                for (int j = 0; j < 256; j++) {
                    heights[i * 256 + j] = 5;
                }
            }
            for (int i = 216; i < 250; i++) {
                double step = 11 - (5 * (256 - i) / 34.0);
                Console.WriteLine(10 - (5 * (256 - i) / 34.0));
                for (int j = 0; j < 256; j++) {
                    heights[i * 256 + j] = step;
                }
            }
            for (int i = 250; i < 256; i++) {
                for (int j = 0; j < 256; j++) {
                    heights[i * 256 + j] = 10;
                }
            }
            Data data = new Data(new List<int> { width, height },heights);
            return JsonSerializer.Serialize(new ST("scene/terrain/add",data));
        }
    }
}
