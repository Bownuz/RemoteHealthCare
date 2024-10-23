using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class TestServer
{
    private TcpListener listener;
    private Dictionary<string, PatientData> patientsData = new Dictionary<string, PatientData>();
    private Dictionary<string, SslStream> connectedClients = new Dictionary<string, SslStream>();

    public TestServer()
    {
        
        patientsData["client1"] = new PatientData { Name = "client1", HeartRate = 70, Speed = 25, Resistance = 5 };
        patientsData["client2"] = new PatientData { Name = "client2", HeartRate = 65, Speed = 22, Resistance = 4 };
        patientsData["client3"] = new PatientData { Name = "client3", HeartRate = 80, Speed = 30, Resistance = 6 };
    }

    public async Task StartAsync(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Server is gestart op poort {port}");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client); 
        }
    }

    private async Task HandleClientAsync(TcpClient tcpClient)
    {
        Console.WriteLine("Client verbonden.");

        using (var sslStream = new SslStream(tcpClient.GetStream(), false, (sender, certificate, chain, sslPolicyErrors) => true))
        {
            await sslStream.AuthenticateAsServerAsync(GetServerCertificate(), false, false);
            using (var reader = new StreamReader(sslStream))
            using (var writer = new StreamWriter(sslStream) { AutoFlush = true })
            {
                while (tcpClient.Connected)
                {
                    try
                    {
                        string jsonData = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(jsonData)) continue;

                        var command = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonData);
                        if (command.ContainsKey("Action"))
                        {
                            string action = command["Action"].ToString();
                            string targetClient = command.ContainsKey("TargetClient") ? command["TargetClient"].ToString() : null;

                            switch (action)
                            {
                                case "RegisterClient":
                                    if (!string.IsNullOrEmpty(targetClient))
                                    {
                                        RegisterClient(targetClient, sslStream);
                                        Console.WriteLine($"{targetClient} geregistreerd.");
                                        await writer.WriteLineAsync($"Client {targetClient} geregistreerd.");
                                    }
                                    break;

                                case "GetLiveData":
                                    if (patientsData.ContainsKey(targetClient))
                                    {
                                        _ = SendContinuousUpdatesAsync(targetClient);
                                    }
                                    break;

                                default:
                                    Console.WriteLine("Onbekend commando ontvangen.");
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fout: {ex.Message}");
                    }
                }
            }
        }

        tcpClient.Close();
    }

    private void RegisterClient(string clientName, SslStream stream)
    {
        if (!connectedClients.ContainsKey(clientName))
        {
            connectedClients[clientName] = stream;
        }
    }

    private async Task SendContinuousUpdatesAsync(string clientName)
    {
        if (patientsData.ContainsKey(clientName) && connectedClients.ContainsKey(clientName))
        {
            var sslStream = connectedClients[clientName];
            var writer = new StreamWriter(sslStream) { AutoFlush = true };

            while (true)
            {
                try
                {
                    var patientData = patientsData[clientName];
                    patientData.HeartRate = GenerateRandomValue(patientData.HeartRate, 60, 100); 
                    patientData.Speed = GenerateRandomValue(patientData.Speed, 20, 40); 
                    patientData.Resistance = GenerateRandomValue(patientData.Resistance, 3, 10); 

                    string jsonData = JsonSerializer.Serialize(patientData);
                    await writer.WriteLineAsync(jsonData);

                    await Task.Delay(1000);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Verbinding met {clientName} verbroken: {ex.Message}");
                    connectedClients.Remove(clientName); 
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fout bij het verzenden van live data: {ex.Message}");
                }
            }
        }
    }

    private int GenerateRandomValue(int currentValue, int minValue, int maxValue)
    {
        Random rand = new Random();
        return rand.Next(Math.Max(minValue, currentValue - 5), Math.Min(maxValue, currentValue + 5));
    }

    private System.Security.Cryptography.X509Certificates.X509Certificate GetServerCertificate()
    {
        return new System.Security.Cryptography.X509Certificates.X509Certificate2("server.pfx", "password");
    }

    public static void Main(string[] args)
    {
        var server = new TestServer();
        server.StartAsync(4790).Wait();
    }
}

public class PatientData
{
    public string Name { get; set; }
    public int HeartRate { get; set; }
    public int Speed { get; set; }
    public int Resistance { get; set; }
}
