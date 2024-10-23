using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ConnectionImplemented;
using System.Reflection;

namespace ClientApplication {
    internal class ServerConnection {
        private static Boolean isRunning;
        private static Boolean isInitialized;
        private static string patientIDString;
        private static string ergometerIDString;
        private static string heartRateMonitorIDString;
        private static DataHandler dataHandler;
        private static string doctorMessage;

        public static void HandleConnection(TcpClient tcpClient, DataHandler handler, Ergometer ergometer, string patientID, string ergometerID, string heartRateMonitorID) {
            NetworkStream stream = tcpClient.GetStream();
            isRunning = true;
            isInitialized = false;
            patientIDString = patientID;
            ergometerIDString = ergometerID;
            heartRateMonitorIDString = heartRateMonitorID;
            dataHandler = handler;


            while (isRunning) {
                Thread.Sleep(1000);

                if (stream.DataAvailable) {
                    string serverMessage = ReadTextMessage(tcpClient);
                    if (!string.IsNullOrEmpty(serverMessage)) {
                        isRunning = ProcessMessage(serverMessage, ergometer, handler, tcpClient, isInitialized);
                    }
                }

                if (isInitialized) {
                    string jsonData = handler.printDataAsJson();
                    Console.WriteLine("Sending Data: " + jsonData);
                    WriteData(tcpClient, jsonData);
                }
            }
        }

        public static void WriteData(TcpClient client, String data) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.WriteLine(data);
            stream.Flush();
        }

        public static string ReadTextMessage(TcpClient client) {
            var stream = new StreamReader(client.GetStream(), Encoding.ASCII);

            return stream.ReadLine();
        }

        public static bool ProcessMessage(string serverMessage, Ergometer ergometer, DataHandler handler, TcpClient tcpClient, bool isInitialized) {
            if (serverMessage.StartsWith("Welcome Client")) {
                Console.WriteLine("Server: " + serverMessage);

                string patientData = InitialisePatient();
                WriteData(tcpClient, patientData);

                return true;
            } else if (serverMessage.StartsWith("Ready to receive data")) {
                Console.WriteLine("Server: " + serverMessage);
                isInitialized = true;

                return true;
            } else if (serverMessage.StartsWith("Resistance:")) {
                Console.WriteLine("Server: " + serverMessage);
                byte resistance = Convert.ToByte(serverMessage.Substring(11));
                ergometer.sendResistanceValueAsync(resistance);

                return true;
            } else if (serverMessage.StartsWith("Goodbye")) {
                Console.WriteLine("Server: " + serverMessage);

                return false;
            } else {
                doctorMessage = serverMessage;
                return true;
            }
        }

        public static string getDocterMessage() {
                return doctorMessage;
        }

        public static string InitialisePatient() {
            return dataHandler.ConvertPatientDataToJson(patientIDString, ergometerIDString, heartRateMonitorIDString);
        }
    }
}
