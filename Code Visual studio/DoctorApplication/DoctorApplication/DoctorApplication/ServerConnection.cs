using DoctorApplication;
using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

public class ServerConnection
{
    private TcpClient client;
    private StreamReader reader;
    private SslStream sslStream;

    
    public event Action<ClientData> OnDataReceived;

    public async Task ConnectToServer(string serverAddress, int port)
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            sslStream = new SslStream(client.GetStream(), false, ValidateServerCertificate, null);
            sslStream.AuthenticateAsClient("ServerName");
            reader = new StreamReader(sslStream);

            // Start met luisteren naar inkomende gegevens
            await ListenForLiveData();
        }
        catch (Exception ex)
        {
            throw new Exception("Kan geen verbinding maken: " + ex.Message);
        }
    }

    private async Task ListenForLiveData()
    {
        while (true)
        {
            try
            {
                
                string jsonData = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(jsonData))
                {
                    
                    var clientData = JsonSerializer.Deserialize<ClientData>(jsonData);
                    OnDataReceived?.Invoke(clientData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fout bij het ontvangen van gegevens: " + ex.Message);
            }
        }
    }

    
    private static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
}
