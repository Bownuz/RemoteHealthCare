using System;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class ClientenForm : Form
    {
        public ClientenForm()
        {
            InitializeComponent();
            serverConnection = new ServerConnection();
            serverConnection.OnDataReceived += UpdateDataGridView;
            //this.Load += new System.EventHandler(this.ClientenForm_Load);

        }
        private async void ClientenForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Verbind met de server
                await serverConnection.ConnectToServer("localhost", 4790);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void ClientenForm_Load(object sender, EventArgs e)
        //{
        //    ClientenGridView.Rows.Add("John Doe", "5", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), "72", "22");
        //    ClientenGridView.Rows.Add("Jane Smith", "3", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), "85", "25");
        //    ClientenGridView.Rows.Add("Sam Wilson", "4", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), "90", "18");
        //}
        private void ClientenGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {       
        }
        private void UpdateDataGridView(ClientData clientData)
        {
    
            foreach (DataGridViewRow row in ClientenGridView.Rows)
            {
                if (row.Cells["PatientName"].Value?.ToString() == clientData.PatientName)
                {
                    // Update bestaande rij
                    row.Cells["Resistance"].Value = clientData.Resistance;
                    row.Cells["HeartRate"].Value = clientData.HeartRate;
                    row.Cells["Speed"].Value = clientData.Speed;
                    row.Cells["LastUpdateColumn"].Value = clientData.LastUpdate;
                    return;
                }
            }         
            ClientenGridView.Rows.Add(
                clientData.PatientName,
                clientData.Resistance,
                clientData.LastUpdate,
                clientData.HeartRate,
                clientData.Speed
            );
        }
    }
}

    
