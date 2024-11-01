using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoctorApplication;

namespace DoctorApplication
{
    public partial class Form1 : Form
    {
        private ServerConnection serverConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            string doctorIdText = DoctorIDTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(doctorIdText) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vul DoctorID, gebruikersnaam en wachtwoord in.");
                return;
            }

            if (!int.TryParse(doctorIdText, out int doctorId))
            {
                MessageBox.Show("Voer een geldig DoctorID in.");
                return;
            }

            await LoginDoctor(doctorId, username, password);
        }

        private async Task LoginDoctor(int doctorId, string username, string password)
        {
            try
            {
                serverConnection = new ServerConnection();
                await serverConnection.ConnectToServer("localhost", 4790);

                var loginData = new
                {
                    DoctorID = doctorId,
                    DoctorName = username,
                    DoctorPassword = password
                };

                serverConnection.SendCommandToServer(loginData);

                using (var reader = new StreamReader(serverConnection.NetworkStream))
                {
                    string response = await reader.ReadLineAsync();
                    if (response != null && response.Contains("Login Successful"))
                    {
                        MessageBox.Show("Inlog succesvol.");
                        OpenClientenForm();
                    }
                    else
                    {
                        MessageBox.Show("Ongeldige inloggegevens.");
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Kan geen verbinding maken met de server: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is een fout opgetreden: " + ex.Message);
            }
        }

        private void OpenClientenForm()
        {
            ClientenForm clientenForm = new ClientenForm(serverConnection);
            clientenForm.Show();
            this.Hide();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            serverConnection?.Disconnect();
        }
    }
}
