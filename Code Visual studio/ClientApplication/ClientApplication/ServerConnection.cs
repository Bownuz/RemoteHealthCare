using ConnectionImplemented;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientApplication {
    internal class ServerConnection {
        public static event Action<string> NewDoctorMessage;
        private static bool isRunning;
        private static bool isInitialized;
        private static string patientIDString;
        private static string ergometerIDString;
        private static string heartRateMonitorIDString;
        private static DataHandler dataHandler;

        public static void HandleConnection(TcpClient tcpClient, DataHandler handler, Ergometer ergometer, string patientID, string ergometerID, string heartRateMonitorID) {
            NetworkStream stream = tcpClient.GetStream();
            isRunning = true;
            isInitialized = false;
            patientIDString = patientID;
            ergometerIDString = ergometerID;
            heartRateMonitorIDString = heartRateMonitorID;
            dataHandler = handler;

            // Start een nieuwe thread voor het lezen van berichten
            new Thread(() => ReadMessages(stream, ergometer, handler, tcpClient)).Start();

            while (isRunning) {
                Thread.Sleep(1000);

                if (isInitialized) {
                    string jsonData = handler.printDataAsJson();
                    Console.WriteLine("Sending Data: " + jsonData);
                    WriteData(tcpClient, jsonData);
                }
            }
        }

        private static void ReadMessages(NetworkStream stream, Ergometer ergometer, DataHandler handler, TcpClient tcpClient) {
            try {
                while (isRunning) {
                    if (stream.DataAvailable) {
                        string serverMessage = ReadTextMessage(stream);
                        if (!string.IsNullOrEmpty(serverMessage)) {
                            isRunning = ProcessMessage(serverMessage, ergometer, handler, tcpClient, isInitialized);
                        }
                    }
                    Thread.Sleep(100); 
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error reading from server: " + ex.Message);
                isRunning = false; 
            }
        }

        public static void WriteData(TcpClient client, string data) {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.WriteLine(data);
            stream.Flush();
        }

        // Gecombineerde methode om berichten te lezen
        public static string ReadTextMessage(NetworkStream stream) {
            var reader = new StreamReader(stream, Encoding.ASCII);
            return reader.ReadLine();
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
                NewDoctorMessage?.Invoke(serverMessage); // Vuur het event af met het nieuwe bericht
                return true;
            }
        }

        public static string InitialisePatient() {
            return dataHandler.ConvertPatientDataToJson(patientIDString, ergometerIDString, heartRateMonitorIDString);
        }
    }
}
