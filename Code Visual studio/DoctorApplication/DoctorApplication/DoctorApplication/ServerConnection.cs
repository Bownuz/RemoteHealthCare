using DoctorApplication;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class ServerConnection
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;
    private NetworkStream networkStream;
    private CancellationTokenSource cancellationTokenSource;

    public event Action<ClientData> OnDataReceived;

    public NetworkStream NetworkStream => networkStream;

    public ServerConnection() { }

    public async Task ConnectToServer(string serverAddress, int port)
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            networkStream = client.GetStream();

            reader = new StreamReader(networkStream);
            writer = new StreamWriter(networkStream) { AutoFlush = true };

            cancellationTokenSource = new CancellationTokenSource();
            await ListenForLiveData(cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
            Disconnect();
            throw new Exception("Kan geen verbinding maken: " + ex.Message, ex);
        }
    }

    private async Task ListenForLiveData(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string jsonData = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(jsonData))
                {
                    try
                    {
                        var clientData = JsonSerializer.Deserialize<ClientData>(jsonData);
                        if (clientData != null)
                        {
                            OnDataReceived?.Invoke(clientData);
                        }
                        else
                        {
                            Console.WriteLine("Ontvangen client data is null of onvolledig.");
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine("JSON-deserialisatie fout: " + ex.Message);
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Verbinding gesloten: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Fout bij het ontvangen van gegevens: " + ex.Message, ex);
        }
    }

    public void Disconnect()
    {
        try
        {
            cancellationTokenSource?.Cancel();
            client?.Close();
            networkStream?.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fout bij het verbreken van de verbinding: " + ex.Message);
        }
        finally
        {
            client = null;
            networkStream = null;
            reader = null;
            writer = null;
        }
    }

    public void StartTrainingForClient(string clientName)
    {
        if (string.IsNullOrEmpty(clientName))
        {
            Console.WriteLine("Client naam mag niet leeg zijn voor StartTraining.");
            return;
        }

        var command = new
        {
            Action = "StartTraining",
            TargetClient = clientName
        };
        SendCommandToServer(command);
    }

    public void StopTrainingForClient(string clientName)
    {
        if (string.IsNullOrEmpty(clientName))
        {
            Console.WriteLine("Client naam mag niet leeg zijn voor StopTraining.");
            return;
        }

        var command = new
        {
            Action = "StopTraining",
            TargetClient = clientName
        };
        SendCommandToServer(command);
    }

    public void SendEmergencyStopToClient(string clientName)
    {
        if (string.IsNullOrEmpty(clientName))
        {
            Console.WriteLine("Client naam mag niet leeg zijn voor EmergencyStop.");
            return;
        }

        var command = new
        {
            Action = "EmergencyStop",
            TargetClient = clientName,
            Message = "NOODSTOP",
            newResistance = 255
        };
        SendCommandToServer(command);
    }

    public void SendCommandToServer(object command)
    {
        try
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command mag niet null zijn.");
            }

            string jsonCommand = JsonSerializer.Serialize(command);
            writer.WriteLine(jsonCommand);
            writer.Flush();
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("Command is niet ingesteld: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fout bij het verzenden van commando: " + ex.Message);
        }
    }
}
