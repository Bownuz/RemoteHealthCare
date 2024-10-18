using DoctorApplication;
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
    private SslStream sslStream;
    private CancellationTokenSource cancellationTokenSource;

    public event Action<ClientData> OnDataReceived;

    public async Task ConnectToServer(string serverAddress, int port) {
        try {
            client = new TcpClient(serverAddress, port);
            sslStream = new SslStream(client.GetStream(), false, ValidateServerCertificate, null);
            await sslStream.AuthenticateAsClientAsync("ServerName");

            reader = new StreamReader(sslStream);
            writer = new StreamWriter(sslStream) { AutoFlush = true };

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
            // Handle connection closed or network error gracefully
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
            sslStream?.Close();
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

    public List<TrainingData> GetTrainingDataForClient(string clientName) {
        // For demonstration purposes, simulate training data
        return new List<TrainingData>
        {
            new TrainingData { TimeStamp = DateTime.Now.AddMinutes(-5), Value = 25 },
            new TrainingData { TimeStamp = DateTime.Now.AddMinutes(-3), Value = 30 },
            new TrainingData { TimeStamp = DateTime.Now.AddMinutes(-1), Value = 28 },
        };
    }

    private void SendCommandToServer(object command) {
        try {
            string jsonCommand = JsonSerializer.Serialize(command);
            writer.WriteLine(jsonCommand);
        }
        catch(Exception ex) {
            throw new Exception("Fout bij het verzenden van commando: " + ex.Message, ex);
        }
    }

    private static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
        // TODO: Implement proper certificate validation
        return true;
    }
}

public class TrainingData {
    public DateTime TimeStamp { get; set; }
    public double Value { get; set; }
}
