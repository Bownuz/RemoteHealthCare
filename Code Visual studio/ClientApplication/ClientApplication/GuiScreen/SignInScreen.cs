﻿using ConnectionImplemented;
using ClientApplication;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ClientApplication.State;

namespace ClientApplication {
    internal partial class SignInScreen : UserControl {
        private DataHandler dataHandler = null;
        private Form mainForm;
        private ListDisplay listDisplay;
        private Ergometer ergometer;
        private HeartRateMonitor heartRateMonitor;
        private Handler handler;
        private Boolean simulatorActive = false;

        public SignInScreen(Form mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
            listDisplay = new ListDisplay(listBox1);
            listBox1.Items.Add("");
            listBox1.Items.Add("");
            listDisplay.ShowDeviceList();

            StartConnectionWithServer();
        }

        private void CloseButton(object sender, EventArgs e) {
            DialogResult dialog = MessageBox.Show("Do you want to close this window", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes) {
                this.ParentForm?.Close();
            }
        }

        private void SubmitButton(object sender, EventArgs e) {
            //MessageBox.Show($"Welcome: " + textBox1.Text);
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && textBox2.Text.StartsWith("Tacx Flux") || simulatorActive && !string.IsNullOrWhiteSpace(textBox1.Text)) {
                StartClient();
                ChangeScreen();
                //dataHandler.setpatientInfo(textBox1.Text, textBox2.Text, textBox3.Text);
            } else {
                MessageBox.Show("You haven't filled everything in, or the bike id doens't begin with Tacx Flux");
            }
        }

        private void StartClient() {
            this.heartRateMonitor = new HeartRateMonitor();
            this.ergometer = new Ergometer();
            BleDevice[] bleDevices = { ergometer, heartRateMonitor };

            this.dataHandler = new DataHandler(bleDevices, textBox3.Text, textBox2.Text, textBox1.Text, ergometer, heartRateMonitor, simulatorActive);
            handler.addDataHandler(dataHandler);
        }

        public void StartConnectionWithServer() {
            //TcpClient client = new TcpClient("localhost", 4789);
            //Thread connectionThread = new Thread(() => ServerConnection.HandleConnection(client, handler, ergometer, textBox1.Text, textBox2.Text, textBox3.Text));
            //connectionThread.Start();

            TcpClient client = new TcpClient("localhost", 4789);
            this.handler = new Handler(client, ergometer, textBox1.Text, textBox2.Text, textBox3.Text);
            Thread connectionThread = new Thread(() => handler.HandleThread());
            connectionThread.Start();

            //TcpClient client = Clientlistener.AcceptTcpClient();
            //Thread clientThread = new Thread(() => HandleClient(client, fileStorage));
            //clientThread.Start();
        }

        private void UsernameTextBox(object sender, EventArgs e) {
            string username = textBox1.Text;
        }

        private void BikeNumberTextBox(object sender, EventArgs e) {
            string bikeID = textBox2.Text;
        }

        private void HeartRateMonitorTextBox(object sender, EventArgs e) {
            string heartRateMonitorID = textBox3.Text;
        }

        private void ChangeScreen() {
            ClientInfoScreen clientInfoScreen = new ClientInfoScreen(dataHandler, mainForm, ergometer, heartRateMonitor, handler);
            mainForm.Controls.Clear();
            mainForm.Controls.Add(clientInfoScreen);
            clientInfoScreen.Dock = DockStyle.Fill;
        }

        private void SignInScreen_Load(object sender, EventArgs e) {
        }

        private void label2_Click(object sender, EventArgs e) {
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private void label4_Click(object sender, EventArgs e) {
        }

        private void SwitchDevice(object sender, EventArgs e) {
            DialogResult changeInputDataDialog;
            if (simulatorActive) {
                changeInputDataDialog = MessageBox.Show("Do you want to switch to real device", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            } else {
                changeInputDataDialog = MessageBox.Show("Do you want to switch to simulator", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (changeInputDataDialog == DialogResult.Yes) {
                simulatorActive = !simulatorActive;
                textBox2.Visible = !textBox2.Visible;
                label2.Visible = !label2.Visible;
                label3.Visible = !label3.Visible;
                label4.Visible = !label4.Visible;
                textBox3.Visible = !textBox3.Visible;
                listBox1.Visible = !listBox1.Visible;
                
                if (simulatorActive) {
                    button3.Text = "Simulator";
                } else {
                    button3.Text = "Real device";
                }
            }
        }
    }
}
