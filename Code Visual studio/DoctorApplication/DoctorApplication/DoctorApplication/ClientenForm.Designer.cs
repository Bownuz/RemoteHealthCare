﻿namespace DoctorApplication {
    partial class ClientenForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.ClientenGridView = new System.Windows.Forms.DataGridView();
            this.PatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastUpdateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeartRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectClientColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.StartTrainingButton = new System.Windows.Forms.Button();
            this.StopTrainingButton = new System.Windows.Forms.Button();
            this.NoodstopButton = new System.Windows.Forms.Button();
            this.ViewPreviousDataButton = new System.Windows.Forms.Button();
            this.AdjustResistanceButton = new System.Windows.Forms.Button();
            this.ResistanceTextBox = new System.Windows.Forms.TextBox();
            this.ViewTrainingDataButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ClientenGridView)).BeginInit();
            this.SuspendLayout();

            // 
            // Form eigenschappen
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke; // Zachte achtergrondkleur
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Cliënten Monitoring";

            // 
            // ClientenGridView
            // 
            this.ClientenGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientenGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientName,
            this.Resistance,
            this.LastUpdateColumn,
            this.HeartRate,
            this.Speed,
            this.SelectClientColumn});
            this.ClientenGridView.Location = new System.Drawing.Point(50, 50);
            this.ClientenGridView.Name = "ClientenGridView";
            this.ClientenGridView.Size = new System.Drawing.Size(700, 200);
            this.ClientenGridView.TabIndex = 0;

            // 
            // PatientName
            // 
            this.PatientName.HeaderText = "Patiëntnaam";
            this.PatientName.Name = "PatientName";

            // 
            // Resistance
            // 
            this.Resistance.HeaderText = "Weerstandniveau";
            this.Resistance.Name = "Resistance";

            // 
            // LastUpdateColumn
            // 
            this.LastUpdateColumn.HeaderText = "Laatste Update";
            this.LastUpdateColumn.Name = "LastUpdateColumn";

            // 
            // HeartRate
            // 
            this.HeartRate.HeaderText = "Hartslag (bpm)";
            this.HeartRate.Name = "HeartRate";

            // 
            // Speed
            // 
            this.Speed.HeaderText = "Snelheid (km/h)";
            this.Speed.Name = "Speed";

            // 
            // SelectClientColumn
            // 
            this.SelectClientColumn.HeaderText = "Selecteer";
            this.SelectClientColumn.Name = "SelectClientColumn";
            this.SelectClientColumn.Width = 60;

            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(50, 270);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(500, 25);
            this.MessageTextBox.TabIndex = 1;

            // 
            // SendMessageButton
            // 
            this.SendMessageButton.BackColor = System.Drawing.Color.LightBlue;
            this.SendMessageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendMessageButton.Location = new System.Drawing.Point(580, 270);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(100, 30);
            this.SendMessageButton.TabIndex = 2;
            this.SendMessageButton.Text = "Verzenden";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);

            // 
            // StartTrainingButton
            // 
            this.StartTrainingButton.BackColor = System.Drawing.Color.LightGreen;
            this.StartTrainingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartTrainingButton.Location = new System.Drawing.Point(50, 320);
            this.StartTrainingButton.Name = "StartTrainingButton";
            this.StartTrainingButton.Size = new System.Drawing.Size(120, 35);
            this.StartTrainingButton.TabIndex = 3;
            this.StartTrainingButton.Text = "Start Training";
            this.StartTrainingButton.UseVisualStyleBackColor = true;
            this.StartTrainingButton.Click += new System.EventHandler(this.StartTrainingButton_Click);

            // 
            // StopTrainingButton
            // 
            this.StopTrainingButton.BackColor = System.Drawing.Color.LightSalmon;
            this.StopTrainingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopTrainingButton.Location = new System.Drawing.Point(180, 320);
            this.StopTrainingButton.Name = "StopTrainingButton";
            this.StopTrainingButton.Size = new System.Drawing.Size(120, 35);
            this.StopTrainingButton.TabIndex = 4;
            this.StopTrainingButton.Text = "Stop Training";
            this.StopTrainingButton.UseVisualStyleBackColor = true;
            this.StopTrainingButton.Click += new System.EventHandler(this.StopTrainingButton_Click);

            // 
            // NoodstopButton
            // 
            this.NoodstopButton.BackColor = System.Drawing.Color.LightCoral;
            this.NoodstopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NoodstopButton.Location = new System.Drawing.Point(310, 320);
            this.NoodstopButton.Name = "NoodstopButton";
            this.NoodstopButton.Size = new System.Drawing.Size(120, 35);
            this.NoodstopButton.TabIndex = 5;
            this.NoodstopButton.Text = "Noodstop";
            this.NoodstopButton.UseVisualStyleBackColor = true;
            this.NoodstopButton.Click += new System.EventHandler(this.NoodstopButton_Click);

            // 
            // ViewPreviousDataButton
            // 
            this.ViewPreviousDataButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ViewPreviousDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewPreviousDataButton.Location = new System.Drawing.Point(440, 320);
            this.ViewPreviousDataButton.Name = "ViewPreviousDataButton";
            this.ViewPreviousDataButton.Size = new System.Drawing.Size(150, 35);
            this.ViewPreviousDataButton.TabIndex = 6;
            this.ViewPreviousDataButton.Text = "Bekijk Gegevens";
            this.ViewPreviousDataButton.UseVisualStyleBackColor = true;
            this.ViewPreviousDataButton.Click += new System.EventHandler(this.ViewPreviousDataButton_Click);

            // 
            // AdjustResistanceButton
            // 
            this.AdjustResistanceButton.BackColor = System.Drawing.Color.LightYellow;
            this.AdjustResistanceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AdjustResistanceButton.Location = new System.Drawing.Point(180, 370);
            this.AdjustResistanceButton.Name = "AdjustResistanceButton";
            this.AdjustResistanceButton.Size = new System.Drawing.Size(120, 35);
            this.AdjustResistanceButton.TabIndex = 8;
            this.AdjustResistanceButton.Text = "Pas Weerstand Aan";
            this.AdjustResistanceButton.UseVisualStyleBackColor = true;
            this.AdjustResistanceButton.Click += new System.EventHandler(this.AdjustResistanceButton_Click);

            // 
            // ResistanceTextBox
            // 
            this.ResistanceTextBox.Location = new System.Drawing.Point(50, 380);
            this.ResistanceTextBox.Name = "ResistanceTextBox";
            this.ResistanceTextBox.Size = new System.Drawing.Size(120, 25);
            this.ResistanceTextBox.TabIndex = 7;

            // 
            // ViewTrainingDataButton
            // 
            this.ViewTrainingDataButton.BackColor = System.Drawing.Color.LightCyan;
            this.ViewTrainingDataButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewTrainingDataButton.Location = new System.Drawing.Point(310, 370);
            this.ViewTrainingDataButton.Name = "ViewTrainingDataButton";
            this.ViewTrainingDataButton.Size = new System.Drawing.Size(150, 35);
            this.ViewTrainingDataButton.TabIndex = 9;
            this.ViewTrainingDataButton.Text = "Toon Trainingsdata";
            this.ViewTrainingDataButton.UseVisualStyleBackColor = true;
            this.ViewTrainingDataButton.Click += new System.EventHandler(this.ViewPreviousDataButton_Click);

            // 
            // ClientenForm
            // 
            this.Controls.Add(this.ViewTrainingDataButton);
            this.Controls.Add(this.AdjustResistanceButton);
            this.Controls.Add(this.ResistanceTextBox);
            this.Controls.Add(this.StopTrainingButton);
            this.Controls.Add(this.StartTrainingButton);
            this.Controls.Add(this.NoodstopButton);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ViewPreviousDataButton);
            this.Controls.Add(this.ClientenGridView);
            this.Name = "ClientenForm";
            this.Load += new System.EventHandler(this.ClientenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ClientenGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView ClientenGridView;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.TextBox ResistanceTextBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.Button StartTrainingButton;
        private System.Windows.Forms.Button StopTrainingButton;
        private System.Windows.Forms.Button NoodstopButton;
        private System.Windows.Forms.Button ViewPreviousDataButton;
        private System.Windows.Forms.Button AdjustResistanceButton;
        private System.Windows.Forms.Button ViewTrainingDataButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeartRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClientColumn;
    }
}
