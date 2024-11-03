using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication {
    public partial class LoginForm : UserControl {
        private ServerConnection serverConnection;
        private Form mainForm;

        public LoginForm(Form form) {
            InitializeComponent();
            this.serverConnection = new ServerConnection(form);
            this.mainForm = form;

            Task.Run(async () => serverConnection.RunConnection());
        }



        private async void LoginButton_Click(object sender, EventArgs e) {
            string doctorIdText = DoctorIDTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(doctorIdText) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                MessageBox.Show("Vul DoctorID, gebruikersnaam en wachtwoord in.");
                return;
            }

            var loginData = new {
                DoctorID = doctorIdText,
                DoctorName = username,
                DoctorPassword = password
            };

            String JsonLoginString = JsonSerializer.Serialize(loginData);

            serverConnection.protocol.doctorState.PerformAction(JsonLoginString);

        }

        private void OpenClientenForm() {
            ClientenForm clientenForm = new ClientenForm(serverConnection);
            clientenForm.Show();
            this.Hide();
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        public void showNextScreen() {
            ClientenForm clientInfoScreen = new ClientenForm(serverConnection);
            mainForm.Controls.Clear();
            mainForm.Controls.Add(clientInfoScreen);
            clientInfoScreen.Dock = DockStyle.Fill;
        }

    }
}
