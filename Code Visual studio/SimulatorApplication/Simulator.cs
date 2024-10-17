﻿using System;
using System.Threading.Tasks;

namespace SimulatorApplication {
    public class Simulator {
        private static Random rand = new Random();
        private string rawBikeData;
        private string bikeID;
        private string heartRateData;
        private string heartRateID;

        public async Task StartSimulation() {
            int currentSpeed = 25;
            while (true) {
                // Case 1: Page 16 - Willekeurige snelheid                
                double randomSpeedKmh = GenerateRandomSpeed(1, 70, currentSpeed);
                currentSpeed = (int) randomSpeedKmh;
                Console.WriteLine($"Willekeurige snelheid: {randomSpeedKmh} km/h");

                // Bereken hexadecimale waarden voor LSB en MSB op basis van de snelheid
                (string speedLSBHex, string speedMSBHex) = CalculateSpeedHex(randomSpeedKmh);

                // Simuleer Page 16 data met de gegenereerde snelheid
                // Hier zorgen we ervoor dat de data in totaal 12 hex bytes is, inclusief de header en de snelheid
                bikeID = "6e40fec2-b5a3-f393-e0a9-e50e24dcca9e";
                rawBikeData = $"A4 09 4E 05 10 00 00 00 {speedLSBHex} {speedMSBHex} 00 00";
                SimuleerData(bikeID, rawBikeData);
                // Wacht even om simulatie te vertragen
                await Task.Delay(1000);

                // Case 2: Page 25 - Vaste data (met 12 hex bytes totaal)
                SimuleerData("6e40fec2-b5a3-f393-e0a9-e50e24dcca9e", "A4 09 4E 05 19 00 00 00 00 00 00 00");

                // Wacht even om simulatie te vertragen
                await Task.Delay(1000);

                // Case 3: Hartslagdata (12 hex bytes)
                heartRateData = "16 8C 5B 03 70 01";
                heartRateID = "00002a37-0000-1000-8000-00805f9b34fb";
                SimuleerData(heartRateID, heartRateData);

                // Wacht even om simulatie te vertragen
                await Task.Delay(1000);
            }
        }

        private void SimuleerData(string serviceId, string data) {
            // Simuleer het ontvangen van data
            Console.WriteLine($"Received from {serviceId}: {data}");
        }

        private double GenerateRandomSpeed(int minKmh, int maxKmh, int currentSpeed) {
            // Genereer een willekeurige snelheid tussen minKmh en maxKmh km/h
            // Ook kan de willekeurige snelheid maar maximaal 5 km/h onder of boven de huidige snelheid zijn
            return rand.Next(Math.Max(minKmh, currentSpeed - 5), Math.Min(maxKmh, currentSpeed + 6));
        }

        private (string, string) CalculateSpeedHex(double speedKmh) {
            // Converteer snelheid van km/h naar m/s
            double speedMetersPerSecond = speedKmh / 3.6;

            // Vermenigvuldig met 1000 om het om te zetten naar een geheel getal in 0.001 m/s
            int speedRaw = (int)(speedMetersPerSecond * 1000);

            // Splits de 16-bits waarde in LSB en MSB
            int speedLSB = speedRaw & 0xFF;       // De laagste byte (LSB)
            int speedMSB = (speedRaw >> 8) & 0xFF; // De hoogste byte (MSB)

            // Converteer naar hexadecimale stringwaarden
            string speedLSBHex = speedLSB.ToString("X2"); // LSB als hex waarde
            string speedMSBHex = speedMSB.ToString("X2"); // MSB als hex waarde

            return (speedLSBHex, speedMSBHex);
        }

        public string getData() {
            return "Bike:" + bikeID + rawBikeData + "HeartBeat:" + heartRateID + heartRateData;
        }
    }
}

