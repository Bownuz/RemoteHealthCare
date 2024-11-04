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
            this.SubscribeButton = new System.Windows.Forms.Button();
            this.UnsubscribeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.ClientenGridView.Location = new System.Drawing.Point(50, 50);
            this.ClientenGridView.Name = "ClientenGridView";
            this.ClientenGridView.Size = new System.Drawing.Size(603, 200);
            this.ClientenGridView.TabIndex = 0;
            this.ClientenGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientenGridView_CellContentClick_1);
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
            this.MessageTextBox.Location = new System.Drawing.Point(50, 294);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(380, 20);
            this.MessageTextBox.TabIndex = 1;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.BackColor = System.Drawing.Color.LightBlue;
            this.SendMessageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendMessageButton.Location = new System.Drawing.Point(620, 284);
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
            this.StopTrainingButton.Click += new System.EventHandler(this.EndTrainingButton_Click);
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
            this.AdjustResistanceButton.Location = new System.Drawing.Point(470, 370);
            this.AdjustResistanceButton.Name = "AdjustResistanceButton";
            this.AdjustResistanceButton.Size = new System.Drawing.Size(120, 35);
            this.AdjustResistanceButton.TabIndex = 8;
            this.AdjustResistanceButton.Text = "Pas Weerstand Aan";
            this.AdjustResistanceButton.UseVisualStyleBackColor = true;
            this.AdjustResistanceButton.Click += new System.EventHandler(this.AdjustResistanceButton_Click);
            // 
            // ResistanceTextBox
            // 
            this.ResistanceTextBox.Location = new System.Drawing.Point(440, 294);
            this.ResistanceTextBox.Name = "ResistanceTextBox";
            this.ResistanceTextBox.Size = new System.Drawing.Size(150, 20);
            this.ResistanceTextBox.TabIndex = 7;
            this.ResistanceTextBox.TextChanged += new System.EventHandler(this.ResistanceTextBox_TextChanged);
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
            // SubscribeButton
            // 
            this.SubscribeButton.BackColor = System.Drawing.Color.LightGreen;
            this.SubscribeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubscribeButton.Location = new System.Drawing.Point(49, 361);
            this.SubscribeButton.Name = "SubscribeButton";
            this.SubscribeButton.Size = new System.Drawing.Size(121, 44);
            this.SubscribeButton.TabIndex = 10;
            this.SubscribeButton.Text = "Subscribe";
            this.SubscribeButton.UseVisualStyleBackColor = true;
            this.SubscribeButton.Click += new System.EventHandler(this.SubscribeButton_Click);
            // 
            // UnsubscribeButton
            // 
            this.UnsubscribeButton.BackColor = System.Drawing.Color.LightSalmon;
            this.UnsubscribeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnsubscribeButton.Location = new System.Drawing.Point(180, 361);
            this.UnsubscribeButton.Name = "UnsubscribeButton";
            this.UnsubscribeButton.Size = new System.Drawing.Size(120, 44);
            this.UnsubscribeButton.TabIndex = 11;
            this.UnsubscribeButton.Text = "Unsubscribe";
            this.UnsubscribeButton.UseVisualStyleBackColor = true;
            this.UnsubscribeButton.Click += new System.EventHandler(this.UnsubscribeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bericht voor patient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "nieuwe weerstand waarde";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ClientenForm
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.Controls.Add(this.SubscribeButton);
            this.Controls.Add(this.UnsubscribeButton);
            this.Name = "ClientenForm";
            this.Size = new System.Drawing.Size(796, 445);
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
        private System.Windows.Forms.Button SubscribeButton;
        private System.Windows.Forms.Button UnsubscribeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeartRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClientColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
