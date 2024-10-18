namespace DoctorApplication {
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
            this.ClientenGridView.Location = new System.Drawing.Point(122, 54);
            this.ClientenGridView.Name = "ClientenGridView";
            this.ClientenGridView.Size = new System.Drawing.Size(602, 235);
            this.ClientenGridView.TabIndex = 0;
            this.ClientenGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientenGridView_CellContentClick);
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
            this.MessageTextBox.Location = new System.Drawing.Point(83, 329);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(400, 20);
            this.MessageTextBox.TabIndex = 1;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(500, 329);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(100, 23);
            this.SendMessageButton.TabIndex = 2;
            this.SendMessageButton.Text = "Verzenden";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // StartTrainingButton
            // 
            this.StartTrainingButton.Location = new System.Drawing.Point(83, 370);
            this.StartTrainingButton.Name = "StartTrainingButton";
            this.StartTrainingButton.Size = new System.Drawing.Size(100, 23);
            this.StartTrainingButton.TabIndex = 3;
            this.StartTrainingButton.Text = "Start Training";
            this.StartTrainingButton.UseVisualStyleBackColor = true;
            this.StartTrainingButton.Click += new System.EventHandler(this.StartTrainingButton_Click);
            // 
            // StopTrainingButton
            // 
            this.StopTrainingButton.Location = new System.Drawing.Point(200, 370);
            this.StopTrainingButton.Name = "StopTrainingButton";
            this.StopTrainingButton.Size = new System.Drawing.Size(100, 23);
            this.StopTrainingButton.TabIndex = 4;
            this.StopTrainingButton.Text = "Stop Training";
            this.StopTrainingButton.UseVisualStyleBackColor = true;
            this.StopTrainingButton.Click += new System.EventHandler(this.StopTrainingButton_Click);
            // 
            // NoodstopButton
            // 
            this.NoodstopButton.Location = new System.Drawing.Point(320, 370);
            this.NoodstopButton.Name = "NoodstopButton";
            this.NoodstopButton.Size = new System.Drawing.Size(100, 23);
            this.NoodstopButton.TabIndex = 5;
            this.NoodstopButton.Text = "Noodstop";
            this.NoodstopButton.UseVisualStyleBackColor = true;
            this.NoodstopButton.Click += new System.EventHandler(this.NoodstopButton_Click);
            // 
            // ViewPreviousDataButton
            // 
            this.ViewPreviousDataButton.Location = new System.Drawing.Point(440, 370);
            this.ViewPreviousDataButton.Name = "ViewPreviousDataButton";
            this.ViewPreviousDataButton.Size = new System.Drawing.Size(120, 23);
            this.ViewPreviousDataButton.TabIndex = 6;
            this.ViewPreviousDataButton.Text = "Bekijk Gegevens";
            this.ViewPreviousDataButton.UseVisualStyleBackColor = true;
            this.ViewPreviousDataButton.Click += new System.EventHandler(this.ViewPreviousDataButton_Click);
            // 
            // AdjustResistanceButton
            // 
            this.AdjustResistanceButton.Location = new System.Drawing.Point(200, 410);
            this.AdjustResistanceButton.Name = "AdjustResistanceButton";
            this.AdjustResistanceButton.Size = new System.Drawing.Size(120, 23);
            this.AdjustResistanceButton.TabIndex = 8;
            this.AdjustResistanceButton.Text = "Pas Weerstand Aan";
            this.AdjustResistanceButton.UseVisualStyleBackColor = true;
            this.AdjustResistanceButton.Click += new System.EventHandler(this.AdjustResistanceButton_Click);
            // 
            // ResistanceTextBox
            // 
            this.ResistanceTextBox.Location = new System.Drawing.Point(83, 410);
            this.ResistanceTextBox.Name = "ResistanceTextBox";
            this.ResistanceTextBox.Size = new System.Drawing.Size(100, 20);
            this.ResistanceTextBox.TabIndex = 7;
            // 
            // ViewTrainingDataButton
            // 
            this.ViewTrainingDataButton.Location = new System.Drawing.Point(320, 410);
            this.ViewTrainingDataButton.Name = "ViewTrainingDataButton";
            this.ViewTrainingDataButton.Size = new System.Drawing.Size(120, 23);
            this.ViewTrainingDataButton.TabIndex = 9;
            this.ViewTrainingDataButton.Text = "Toon Trainingsdata";
            this.ViewTrainingDataButton.UseVisualStyleBackColor = true;
            this.ViewTrainingDataButton.Click += new System.EventHandler(this.ViewPreviousDataButton_Click);
            // 
            // ClientenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Text = "ClientenForm";
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