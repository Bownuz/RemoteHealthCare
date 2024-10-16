using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Sockets;
using System.Windows;

namespace DoctorApplication {
    public partial class MainWindow : Window {
        private TcpClient doctorClient;

        public MainWindow() {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if(username == "doctor" && password == "password123") 
            {
                // Verbind met de server
                ConnectToServer();
            }
            else {
                MessageBox.Show("Foutieve gebruikersnaam of wachtwoord.");
            }
        }

        private void ConnectToServer() {
            try {
                doctorClient = new TcpClient("127.0.0.1", 4790); 
                MessageBox.Show("Verbinding gemaakt met de server!");
                
                StartMonitoring();
            }
            catch(Exception ex) {
                MessageBox.Show("Kan geen verbinding maken met de server: " + ex.Message);
            }
        }

        private void StartMonitoring() {
            
        }
    }
}
