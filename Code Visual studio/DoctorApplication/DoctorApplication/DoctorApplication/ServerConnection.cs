using DoctorApplication;
using DoctorApplication.StatePattern;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ServerConnection {
    public TcpClient client { get; set; }
    public NetworkStream networkStream { get; set; }
    public readonly DoctorProtocol protocol;
    public readonly UserControl currentForm;
    public Form mainForm;

    public ServerConnection(Form form) {
        protocol = new DoctorProtocol(this);
        this.mainForm = form;
        this.currentForm = currentForm;
    }

    public void ConnectToServer(string ipAdress) {
        client = new TcpClient("145.49.9.63", 4790);
        networkStream = client.GetStream();
    }

    internal async Task RunConnection() {
        while (true) {
            String recievedMessage;
            if ((recievedMessage = MessageCommunication.ReceiveMessage(networkStream)) == null) {
                continue;
            }
            await protocol.Respond(recievedMessage);
        }
    }
}
