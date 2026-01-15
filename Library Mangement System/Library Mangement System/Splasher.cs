using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Mangement_System
{
    public partial class Splasher : Form
    {
        private System.Windows.Forms.Timer fadeTimer;
        private System.Windows.Forms.Timer loadTimer;
        private int loadStep = 0;
        public Splasher()
        {
            InitializeComponent();
            this.Opacity = 0;
            StartAnimations();
        }

        private void StartAnimations()
        {
            // Fade in animation
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1.0)
                    this.Opacity += 0.05;
                else
                    fadeTimer.Stop();
            };
            fadeTimer.Start();

            // Loading animation
            loadTimer = new System.Windows.Forms.Timer();
            loadTimer.Interval = 500;
            loadTimer.Tick += timer_Tick;
            loadTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            loadStep++;

            if (loadStep <= 5)
            {
                progressBar1.Value = loadStep * 20;
            }

            switch (loadStep)
            {
                case 1:
                    label3.Text = "✓ Initializing database connection...";
                    label3.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
                    break;
                case 2:
                    label4.Text = "✓ Loading application modules...";
                    label4.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
                    break;
                case 3:
                    label5.Text = "✓ Preparing user interface...";
                    label5.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
                    break;
                case 4:
                    label6.Text = "✓ Loading security protocols...";
                    label6.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
                    break;
                case 5:
                    // close after delay
                    loadTimer.Stop();

                    System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();
                    closeTimer.Interval = 1000; // Wait 1 second
                    closeTimer.Tick += (s, e2) =>
                    {
                        closeTimer.Stop();

                        // Fade out
                        System.Windows.Forms.Timer fadeOut = new System.Windows.Forms.Timer();
                        fadeOut.Interval = 20;
                        fadeOut.Tick += (s2, e3) =>
                        {
                            if (this.Opacity > 0)
                                this.Opacity -= 0.1;
                            else
                            {
                                fadeOut.Stop();
                                this.Close();
                            }
                        };
                        fadeOut.Start();
                    };
                    closeTimer.Start();
                    break;
            }
        }

    }
}
