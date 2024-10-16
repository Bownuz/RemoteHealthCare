using DataProtocol;
using ServerTest2._1.DataStorage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace ConnectionService {
    class ConnectionService {
        private static Dictionary<string, TcpClient> connectedClients = new Dictionary<string, TcpClient>();
        private static Dictionary<string, Person> patients = new Dictionary<string, Person>();
        private static Dictionary<string, Session> activeSessions = new Dictionary<string, Session>();

        static void Main(string[] args) {
            List<Thread> threads = new List<Thread>();
            TcpListener clientListener = new TcpListener(IPAddress.Any, 4789);
            TcpListener doctorListener = new TcpListener(IPAddress.Any, 4790);
            clientListener.Start();
            doctorListener.Start();

            while(true) {
                Console.WriteLine("Starting up server and waiting for connections.....");

                TcpClient client = clientListener.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClientThread(client));
                threads.Add(clientThread);
                clientThread.Start();

                TcpClient doctor = doctorListener.AcceptTcpClient();
                Thread doctorThread = new Thread(() => HandleDoctorThread(doctor));
                threads.Add(doctorThread);
                doctorThread.Start();
            }
        }

        private static void HandleClientThread(TcpClient client) {
            while(true) {
                if(client.Connected) {
                    string received;
                    if((received = DataProtocol.Messages.ReciveMessage(client)) != null) {
                        Console.WriteLine("Received from client: {0}", received);
                        ClientRecieveData jsonData = JsonSerializer.Deserialize<ClientRecieveData>(received);

                        if(!connectedClients.ContainsKey(jsonData.patientName)) {
                            connectedClients.Add(jsonData.patientName, client);
                            patients[jsonData.patientName] = new Person(jsonData.patientName);
                            Console.WriteLine($"Client {jsonData.patientName} connected.");
                        }

                        UpdateClientData(jsonData);
                    }
                }
                else {
                    Console.WriteLine("Client disconnected.");
                    break;
                }
            }
        }



        private static void HandleDoctorThread(TcpClient doctor) {
            while(true) {
                string received = DataProtocol.Messages.ReciveMessage(doctor);
                if(string.IsNullOrEmpty(received)) {
                    Console.WriteLine("Doctor disconnected.");
                    break;
                }

                Console.WriteLine("Received from doctor: " + received);
                var command = JsonSerializer.Deserialize<Command>(received);
                ProcessDoctorCommand(command);
            }
        }

        private static void ProcessDoctorCommand(Command command) {
            switch(command.CommandType) {
                case "START_SESSION":
                    StartSessionForClient(command.ClientId, command.StartTime);
                    break;
                case "STOP_SESSION":
                    StopSessionForClient(command.ClientId, command.EndTime);
                    break;
                case "SEND_MESSAGE":
                    SendMessageToClient(command.ClientId, command.Message);
                    break;
                case "ADJUST_RESISTANCE":
                    AdjustResistanceForClient(command.ClientId, command.Resistance);
                    break;
                case "EMERGENCY_STOP":
                    EmergencyStopForClient(command.ClientId);
                    break;
                default:
                    Console.WriteLine("Unknown command received from doctor.");
                    break;
            }
        }

        private static void StartSessionForClient(string clientId, DateTime startTime) {
            if(connectedClients.ContainsKey(clientId)) {
                Console.WriteLine($"Starting session for client: {clientId} at {startTime}");
                Session session = new Session {
                    StartTime = startTime
                };
                activeSessions[clientId] = session;
            }
            else {
                Console.WriteLine($"Client {clientId} not found.");
            }
        }

        private static void StopSessionForClient(string clientId, DateTime endTime) {
            if(activeSessions.ContainsKey(clientId)) {
                Console.WriteLine($"Stopping session for client: {clientId} at {endTime}");
                var session = activeSessions[clientId];
                session.EndTime = endTime;
                activeSessions.Remove(clientId);
            }
            else {
                Console.WriteLine($"No active session found for client {clientId}.");
            }
        }


        private static void SendMessageToClient(string clientId, string message) {
            if(connectedClients.TryGetValue(clientId, out TcpClient client)) {
                Console.WriteLine($"Sending message to client {clientId}: {message}");
                DataProtocol.Messages.SendMessage(client, message);
            }
            else {
                Console.WriteLine($"Client {clientId} not found.");
            }
        }

        private static void AdjustResistanceForClient(string clientId, int resistance) {
            if(connectedClients.TryGetValue(clientId, out TcpClient client)) {
                Console.WriteLine($"Adjusting resistance for client {clientId} to {resistance}");
            }
            else {
                Console.WriteLine($"Client {clientId} not found.");
            }
        }

        private static void EmergencyStopForClient(string clientId) {
            if(connectedClients.TryGetValue(clientId, out TcpClient client)) {
                Console.WriteLine($"Emergency stop triggered for client {clientId}");
                DataProtocol.Messages.SendMessage(client, "EMERGENCY_STOP");
            }
            else {
                Console.WriteLine($"Client {clientId} not found.");
            }
        }

        private static void UpdateClientData(ClientRecieveData jsonData) {
            Console.WriteLine($"Updated client data: Name={jsonData.patientName}, Speed={jsonData.BicycleSpeed}, Heart Rate={jsonData.Heartrate}, Date={jsonData.dateTime}");
        }
    }
}
