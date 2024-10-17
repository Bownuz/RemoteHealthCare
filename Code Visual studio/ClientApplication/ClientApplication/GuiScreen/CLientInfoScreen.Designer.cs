namespace ClientApplication {
    partial class ClientInfoScreen {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.HeartRatePatient = new System.Windows.Forms.Label();
            this.SpeedPatient = new System.Windows.Forms.Label();
            this.Resistance = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(49, 54);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 84);
            this.listBox1.TabIndex = 0;
            // 
            // HeartRatePatient
            // 
            this.HeartRatePatient.AutoSize = true;
            this.HeartRatePatient.Location = new System.Drawing.Point(384, 81);
            this.HeartRatePatient.Name = "HeartRatePatient";
            this.HeartRatePatient.Size = new System.Drawing.Size(19, 16);
            this.HeartRatePatient.TabIndex = 1;
            this.HeartRatePatient.Text = "---";
            this.HeartRatePatient.Click += new System.EventHandler(this.SpeedPatientLabel);
            // 
            // SpeedPatient
            // 
            this.SpeedPatient.AutoSize = true;
            this.SpeedPatient.Location = new System.Drawing.Point(209, 81);
            this.SpeedPatient.Name = "SpeedPatient";
            this.SpeedPatient.Size = new System.Drawing.Size(19, 16);
            this.SpeedPatient.TabIndex = 2;
            this.SpeedPatient.Text = "---";
            this.SpeedPatient.Click += new System.EventHandler(this.HeartRatePatientLabel);
            // 
            // Resistance
            // 
            this.Resistance.AutoSize = true;
            this.Resistance.Location = new System.Drawing.Point(550, 81);
            this.Resistance.Name = "Resistance";
            this.Resistance.Size = new System.Drawing.Size(19, 16);
            this.Resistance.TabIndex = 3;
            this.Resistance.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(373, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Heartbeat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(523, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Resistance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(209, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Time";
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Location = new System.Drawing.Point(217, 165);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(19, 16);
            this.Time.TabIndex = 8;
            this.Time.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Date";
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.Location = new System.Drawing.Point(384, 165);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(19, 16);
            this.Date.TabIndex = 10;
            this.Date.Text = "---";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(1730, 27);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(4);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(157, 70);
            this.CloseButton.TabIndex = 11;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ClientInfoScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Resistance);
            this.Controls.Add(this.SpeedPatient);
            this.Controls.Add(this.HeartRatePatient);
            this.Controls.Add(this.listBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClientInfoScreen";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label HeartRatePatient;
        private System.Windows.Forms.Label SpeedPatient;
        private System.Windows.Forms.Label Resistance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Button CloseButton;
    }
}
