using System;
using System.Threading.Tasks;

namespace SimulatorApplication {
    class ClientConnect {
        static async Task Main(string[] args) {
            
            Simulator simulator = new Simulator();

            // Start de simulatie
            await simulator.StartSimulation();
        }
    }
}
