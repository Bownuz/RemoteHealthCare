using ConnectionImplemented;
using HardwareClientApplication;
using System;
using System.Windows.Forms;

namespace ClientApplication {
    internal partial class SignInScreen : UserControl {
        private DataHandler dataHandler;
        private Form mainForm;
        private ListDisplay listDisplay;
        public SignInScreen(DataHandler dataHandler, Form mainForm) {
            InitializeComponent();
            this.dataHandler = dataHandler;
            this.mainForm = mainForm;
            listDisplay = new ListDisplay(availableDevicesListBox);
        }

        private void CloseButton(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Do you want to close this window", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) {
                this.ParentForm?.Close();
            }
        }

        private void SubmitButton(object sender, EventArgs e) {
            //MessageBox.Show($"Welcome: " + textBox1.Text);
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)) {
                //dataHandler.startDataHandler(textBox2.Text, textBox3.Text);
                ChangeScreen();
            } else {
                MessageBox.Show("You haven't filled everything in.");
            }
        }

        private void label2_Click(object sender, EventArgs e) {
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void BikeNumberTextBox(object sender, EventArgs e) {
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
        }

        private void ChangeScreen() {
            ClientInfoScreen clientInfoScreen = new ClientInfoScreen(dataHandler, mainForm);
            mainForm.Controls.Clear();
            mainForm.Controls.Add(clientInfoScreen);
            clientInfoScreen.Dock = DockStyle.Fill;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void ShowAvailableDevices(object sender, EventArgs e) {
            listDisplay.ShowDeviceList();
        }
    }
}
