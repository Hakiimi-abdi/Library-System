namespace Library_Mangement_System
{
    partial class Splasher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splasher));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            progressBar1 = new ProgressBar();
            panel2 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            timer = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(progressBar1);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(580, 391);
            panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.dashboard_logo;
            pictureBox1.Location = new Point(41, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(77, 75);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F);
            label7.ForeColor = Color.FromArgb(149, 165, 166);
            label7.Location = new Point(136, 365);
            label7.Name = "label7";
            label7.Size = new Size(277, 21);
            label7.TabIndex = 4;
            label7.Text = "Version 1.0.0 ©️ 2025 TheWorldLibrary";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(20, 330);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(540, 20);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(248, 249, 250);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(20, 200);
            panel2.Name = "panel2";
            panel2.Size = new Size(540, 120);
            panel2.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(52, 73, 94);
            label6.Location = new Point(20, 95);
            label6.Name = "label6";
            label6.Size = new Size(259, 25);
            label6.TabIndex = 3;
            label6.Text = "🔒Loading security protocols...";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(52, 73, 94);
            label5.Location = new Point(20, 70);
            label5.Name = "label5";
            label5.Size = new Size(235, 25);
            label5.TabIndex = 2;
            label5.Text = "⌛Preparing user interface...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(52, 73, 94);
            label4.Location = new Point(20, 45);
            label4.Name = "label4";
            label4.Size = new Size(284, 25);
            label4.TabIndex = 1;
            label4.Text = "🔃 Loading application modules...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(52, 73, 94);
            label3.Location = new Point(20, 20);
            label3.Name = "label3";
            label3.Size = new Size(302, 25);
            label3.TabIndex = 0;
            label3.Text = "✔️ Initializing database connection...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(52, 73, 94);
            label2.Location = new Point(20, 111);
            label2.Name = "label2";
            label2.Size = new Size(387, 48);
            label2.TabIndex = 1;
            label2.Text = "THE WORLD LIBRARY";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // Splasher
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 128, 185);
            ClientSize = new Size(600, 406);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Splasher";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Splasher";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
        private ProgressBar progressBar1;
        private Label label6;
        private Label label7;
        private System.Windows.Forms.Timer timer;
        private PictureBox pictureBox1;
    }
}