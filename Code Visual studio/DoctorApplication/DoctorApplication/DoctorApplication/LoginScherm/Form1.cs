using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication {
    public partial class Form1 : Form {
        private NetworkStream networkStream;
        private TcpClient doctorClient;

        public Form1() {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e) {
            string doctorIdText = DoctorIDTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(doctorIdText) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                MessageBox.Show("Vul DoctorID, gebruikersnaam en wachtwoord in.");
                return;
            }

            if (!int.TryParse(doctorIdText, out int doctorId)) {
                MessageBox.Show("Voer een geldig DoctorID in.");
                return;
            }

            await LoginDoctor(doctorIdText, username, password);
        }

        private async Task LoginDoctor(String doctorId, string username, string password) {

            networkStream = Program.client.GetStream();

            var loginData = new {
                DoctorID = doctorId,
                DoctorName = username,
                DoctorPassword = password
            };

            SendCommandToServer(loginData);

            using (var reader = new System.IO.StreamReader(networkStream)) {
                string response = await reader.ReadLineAsync();
                if (response.Contains("Login Successful")) {
                    MessageBox.Show("Inlog succesvol.");
                    OpenClientenForm();
                } else {
                    MessageBox.Show("Ongeldige inloggegevens.");
                }

            }
        }

        private void SendCommandToServer(object command) {
            try {
                string jsonCommand = System.Text.Json.JsonSerializer.Serialize(command);
                using (var writer = new System.IO.StreamWriter(networkStream) { AutoFlush = true }) {
                    writer.WriteLine(jsonCommand);
                }
                MessageBox.Show("Commando succesvol verzonden.");
            } catch (Exception ex) {
                MessageBox.Show("Fout bij het verzenden van commando: " + ex.Message);
            }
        }

        private void OpenClientenForm() {
            ClientenForm clientenForm = new ClientenForm(networkStream);
            clientenForm.Show();
            this.Hide();
        }
        private void CloseButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
