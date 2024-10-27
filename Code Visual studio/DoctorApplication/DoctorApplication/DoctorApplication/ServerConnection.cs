﻿using DoctorApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class ServerConnection {
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;
    private NetworkStream networkStream;
    private CancellationTokenSource cancellationTokenSource;

    public event Action<ClientData> OnDataReceived;

    public ServerConnection(NetworkStream stream) {
        this.networkStream = stream;
    }

    public async Task ConnectToServer(string serverAddress, int port) {
        try {
            client = new TcpClient(serverAddress, port);
            networkStream = client.GetStream();
            
            reader = new StreamReader(networkStream);
            writer = new StreamWriter(networkStream) { AutoFlush = true };

            cancellationTokenSource = new CancellationTokenSource();
            await ListenForLiveData(cancellationTokenSource.Token);
        }
        catch(Exception ex) {
            throw new Exception("Kan geen verbinding maken: " + ex.Message, ex);
        }
    }

    private async Task ListenForLiveData(CancellationToken cancellationToken) {
        try {
            while(!cancellationToken.IsCancellationRequested) {
                string jsonData = await reader.ReadLineAsync();
                if(!string.IsNullOrEmpty(jsonData)) {
                    var clientData = JsonSerializer.Deserialize<ClientData>(jsonData);
                    OnDataReceived?.Invoke(clientData);
                }
            }
        }
        catch(IOException ex) {
            Console.WriteLine("Verbinding gesloten: " + ex.Message);
        }
        catch(Exception ex) {
            throw new Exception("Fout bij het ontvangen van gegevens: " + ex.Message, ex);
        }
    }

    public void Disconnect() {
        try {
            cancellationTokenSource?.Cancel();
            client?.Close();
            networkStream?.Close();
        }
        catch(Exception ex) {
            Console.WriteLine("Fout bij het verbreken van de verbinding: " + ex.Message);
        }
    }

    public void SendMessageToClient(string message, string clientName) {
        SendCommandToServer(new {
            Action = "SendMessage",
            TargetClient = clientName,
            Message = message
        });
    }

    public void StartTrainingForClient(string clientName) {
        SendCommandToServer(new {
            Action = "StartTraining",
            TargetClient = clientName
        });
    }

    public void StopTrainingForClient(string clientName) {
        SendCommandToServer(new {
            Action = "StopTraining",
            TargetClient = clientName
        });
    }

    public void SendEmergencyStopToClient(string clientName) {
        SendCommandToServer(new {
            Action = "EmergencyStop",
            TargetClient = clientName,
            ResistanceLevel = 100
        });
    }

    public async Task<List<TrainingData>> GetTrainingDataForClientAsync(string clientName) {
        try {
            SendCommandToServer(new {
                Action = "GetTrainingData",
                TargetClient = clientName
            });

            string response = await reader.ReadLineAsync();
            if(!string.IsNullOrEmpty(response)) {
                return JsonSerializer.Deserialize<List<TrainingData>>(response);
            }
            else {
                Console.WriteLine($"Geen trainingsdata ontvangen voor {clientName}");
                return new List<TrainingData>();
            }
        }
        catch(Exception ex) {
            throw new Exception($"Fout bij het ophalen van trainingsdata voor {clientName}: " + ex.Message, ex);
        }
    }

    public void SendCommandToServer(object command) {
        try {
            string jsonCommand = JsonSerializer.Serialize(command);
            writer.WriteLine(jsonCommand);
        }
        catch(Exception ex) {
            throw new Exception("Fout bij het verzenden van commando: " + ex.Message, ex);
        }
    }
}
