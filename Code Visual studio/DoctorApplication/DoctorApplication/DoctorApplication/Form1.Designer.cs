namespace DoctorApplication
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeUpdater = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();

            // Form eigenschappen
            this.BackColor = System.Drawing.Color.WhiteSmoke; // Zachte achtergrondkleur
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Text = "Login Scherm";

            // Username TextBox
            this.UsernameTextBox.Location = new System.Drawing.Point(200, 80);
            this.UsernameTextBox.Size = new System.Drawing.Size(150, 25);
            this.UsernameTextBox.Name = "UsernameTextBox";

            // Password TextBox
            this.PasswordTextBox.Location = new System.Drawing.Point(200, 120);
            this.PasswordTextBox.Size = new System.Drawing.Size(150, 25);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*'; // Verberg wachtwoord

            // DateLabel
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(50, 30);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(0, 17);

            // TimeLabel
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(250, 30);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(0, 17);

            // Close Button
            this.CloseButton.Location = new System.Drawing.Point(320, 250);
            this.CloseButton.Size = new System.Drawing.Size(70, 30);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Text = "Sluiten";
            this.CloseButton.BackColor = System.Drawing.Color.LightCoral;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);

            // Login Button
            this.LoginButton.Location = new System.Drawing.Point(150, 200);
            this.LoginButton.Size = new System.Drawing.Size(100, 35);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Text = "Inloggen";
            this.LoginButton.BackColor = System.Drawing.Color.LightBlue;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);

            // label1 (Username label)
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 80);
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.Name = "label1";
            this.label1.Text = "Uw Gebruikersnaam:";

            // label2 (Password label)
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 120);
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.Name = "label2";
            this.label2.Text = "Uw Wachtwoord:";

            // TimeUpdater Timer
            this.timeUpdater.Interval = 1000;
            this.timeUpdater.Tick += new System.EventHandler(this.UpdateDateTimeLabel);

            // Voeg de controls toe aan het formulier
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timeUpdater;
    }
}
