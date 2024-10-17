using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class Form1 : Form
    {
        private SslStream sslStream;
        private TcpClient doctorClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            
            if (username == "doctor" && password == "doctor123")
            {
                try
                {
                    
                    doctorClient = new TcpClient("localhost", 4790);

                    
                    sslStream = new SslStream(doctorClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                    
                    sslStream.AuthenticateAsClient("localhost");

                    
                    MessageBox.Show("Beveiligde verbinding gemaakt met de server!");

                    
                    ClientenForm clientenForm = new ClientenForm();
                    this.Hide();
                    clientenForm.Show();
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show("Kan geen verbinding maken: " + ex.Message);
                }
            }
            else
            {
                
                MessageBox.Show("Ongeldige gebruikersnaam of wachtwoord.");
            }
        }

        // Certificaat validatiefunctie
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            progressBarLoading.MarqueeAnimationSpeed = 30;

            await Task.Delay(3000);

            progressBarLoading.Visible = false;

            
            timeUpdater.Interval = 1000;  
            timeUpdater.Tick += new EventHandler(UpdateDateTimeLabel);  
            timeUpdater.Start();  
        }

        private void UpdateDateTimeLabel(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateLabel.Text = $"Vandaag is het: {now.ToString("dddd, dd MMMM yyyy")}, Tijd: {now.ToString("HH:mm:ss")}";
        }

  
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }
    }
}
