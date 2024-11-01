using DoctorApplication;
using System;
using System.IO;
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
        reader = new StreamReader(networkStream);
        writer = new StreamWriter(networkStream) { AutoFlush = true };
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

    public void StartTrainingForClient(string clientName) {
        var command = new {
            Action = "StartTraining",
            TargetClient = clientName
        };
        SendCommandToServer(command);
    }

    public void StopTrainingForClient(string clientName) {
        var command = new {
            Action = "StopTraining",
            TargetClient = clientName
        };
        SendCommandToServer(command);
    }

    public void SendEmergencyStopToClient(string clientName) {
        var command = new {
            Action = "EmergencyStop",
            TargetClient = clientName,
            Message = "NOODSTOP",
            newResistance = 255
        };
        SendCommandToServer(command);
    }

    public void SendCommandToServer(object command) {
        try {
            string jsonCommand = JsonSerializer.Serialize(command);
            writer.WriteLine(jsonCommand);
        }
        catch(Exception ex) {
            Console.WriteLine("Fout bij het verzenden van commando: " + ex.Message);
        }
    }
}
