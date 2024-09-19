using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorApplication {
    internal class AntConverter {
        public static void Main(string[] args) {
            Console.WriteLine(Convert());
        }

        public static String Convert() {
            Random random = new Random();
            int range = 70;
            double rDouble = random.NextDouble() * range;
            
            rDouble = rDouble / 3.6;
            rDouble = rDouble / 0.01;
            byte[] bytes = BitConverter.GetBytes(rDouble);
            //int speedLSB = bytes[0];
            //int speedMSB = bytes[1];
            Console.WriteLine(rDouble);
            Console.WriteLine(BitConverter.ToString(bytes));
            return $"A4 09 4E 05 10 F8 00 C4 {bytes[0]:X} {bytes[1]:X} 60 20 8A";
        }
    }
}
