namespace DoctorApplication {
    partial class ClientenForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.ClientenGridView.Location = new System.Drawing.Point(83, 45);
            this.ClientenGridView.Name = "ClientenGridView";
            this.ClientenGridView.Size = new System.Drawing.Size(600, 158);
            this.ClientenGridView.TabIndex = 0;
            this.ClientenGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClientenGridView_CellContentClick);
            // 
            // PatientName
            // 
            this.PatientName.HeaderText = "Patiëntnaam: ";
            this.PatientName.Name = "PatientName";
            // 
            // Resistance
            // 
            this.Resistance.HeaderText = "Weerstand niveau: ";
            this.Resistance.Name = "Resistance";
            // 
            // LastUpdateColumn
            // 
            this.LastUpdateColumn.HeaderText = "Laatste Update";
            this.LastUpdateColumn.Name = "LastUpdateColumn";
            // 
            // HeartRate
            // 
            this.HeartRate.HeaderText = "Hartslag in bpm : ";
            this.HeartRate.Name = "HeartRate";
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Snelheid in km/h : ";
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
            this.MessageTextBox.Size = new System.Drawing.Size(529, 20);
            this.MessageTextBox.TabIndex = 1;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(636, 327);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(75, 23);
            this.SendMessageButton.TabIndex = 2;
            this.SendMessageButton.Text = "Verzenden";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // ClientenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.MessageTextBox);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeartRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectClientColumn;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Button SendMessageButton;
    }
}
