using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ClientApplication {
    internal class SimulatorConnection {
        public static void HandleConnection(TcpClient tcpClient, DataHandler handler) {
            NetworkStream stream = tcpClient.GetStream();

            while (true) {
                Thread.Sleep(1000);
                string simulatorData = ReadTextMessage(tcpClient);
                if (!string.IsNullOrEmpty(simulatorData)) {
                    ProcessMessage(simulatorData, handler);
                }
            }
        }

        public static string ReadTextMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII, true, 128);

            return stream.ReadLine();
        }

        public static void ProcessMessage(String simulatorData, DataHandler handler) {
            if (simulatorData != null) {
                var parts = Regex.Split(simulatorData, @"Bike:|HeartBeat:|,\s*");
                if (parts.Length >= 5) {
                    string bikeDeviceId = parts[1].Trim();
                    string bikeData = parts[2].Trim();

                    string heartRateDeviceId = parts[3].Trim();
                    string heartRateData = parts[4].Trim();

                    byte[] bikeRawData = ConvertDataToByteArray(bikeData);
                    byte[] heartRateRawData = ConvertDataToByteArray(heartRateData);

                    ConvertData(bikeRawData);
                    handler.SimulateBLEData(bikeRawData, heartRateRawData);
                } else {
                    Console.WriteLine("Onvoldoende gegevens ontvangen.");
                }

            }
        }

        private static void ConvertData(byte[] rawData) {
            if (rawData[4] == 0x10) {

                int speedLSB = rawData[8];
                int speedMSB = rawData[9];
                int speedRaw = (speedMSB << 5) | speedLSB;
                double speedMetersPerSecond = speedRaw * 0.01;
                double speedKmPerHour = speedMetersPerSecond * 3.6;
            }
        }

        private static byte[] ConvertDataToByteArray(string data) {
            string[] parts = data.Trim().Split(' ');
            byte[] byteArray = new byte[parts.Length];

            for (int i = 0; i < parts.Length; i++) {
                byteArray[i] = Convert.ToByte(parts[i], 16);
            }

            return byteArray;
        }
    }
}
