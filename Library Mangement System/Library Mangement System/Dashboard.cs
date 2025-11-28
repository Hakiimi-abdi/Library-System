using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Library_Mangement_System.StaffManagement;

namespace Library_Mangement_System
{
    public partial class Dashboard : Form
    {
        private string userRole;
        private string username;

        public Dashboard(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.userRole = role;
            UserLabel.Text = $"Role: {userRole}";
            ApplyRoleRestrictions();
        }

        // Open a child form inside the panel
        private Form activeForm = null;
        private void openchildform(Form childFrom)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childFrom;
            childFrom.TopLevel = false;
            childFrom.FormBorderStyle = FormBorderStyle.None;
            childFrom.Dock = DockStyle.Fill;
            panel3.Controls.Add(childFrom);
            panel3.Tag = childFrom;
            childFrom.BringToFront();
            childFrom.Show();
        }

        //Restrict Access
        private void ApplyRoleRestrictions()
        {
            if (userRole == "Librarian")
            {
                iconButton1.Visible = false;
            }
            AdjustButtonLayout();
        }

        //Adjust button layout
        private void AdjustButtonLayout()
        {
            int yoffset = 10;
            for (int i = SideBar.Controls.Count - 1; i >= 0; i--)
            {
                if (SideBar.Controls[i] is Button btn && btn.Visible)
                {
                    btn.Location = new Point(btn.Location.X, yoffset);
                    yoffset += btn.Height + 10;
                }
            }
        }

        //statistic cards
        private void loadDashboardSummary()
        {
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                // Count total books
                string queryTotal = "SELECT COUNT(*) FROM books";
                using (SqlCommand cmd = new SqlCommand(queryTotal, conn))
                {
                    label12.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }

        //center the dashboard
        private void Dashboard_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width > MainPanel.Width)
            {
                MainPanel.Left = (this.ClientSize.Width - MainPanel.Width) / 2;
            }
            else
            {
                MainPanel.Left = 0;
            }
        }

        //open staffManagement
        private void iconButton1_Click(object sender, EventArgs e)
        {
            openchildform(new StaffManagement());
        }

        //load dashboard
        private void Dashboard_Load(object sender, EventArgs e)
        {
            AdjustButtonLayout();
            loadDashboardSummary();
        }

        //logout
        private void iconButton7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        //exit
        private void iconButton8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //open books
        private void iconButton2_Click(object sender, EventArgs e)
        {
            openchildform(new Books());
        }

        // settings slide in
        private void Setting_Click(object sender, EventArgs e)
        {
            SlideIn.Visible = !SlideIn.Visible;
            if (SlideIn.Visible)
            {
                SlideIn.BringToFront();
            }

        }

        // change password
        private void iconButton10_Click(object sender, EventArgs e)
        {
            string oldpassword = textBox1.Text.Trim();
            string newpassword = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(oldpassword) || string.IsNullOrEmpty(newpassword))
            {
                MessageBox.Show("Please fill in both fields.");
                return;
            }



            if (newpassword.Length < 6 || !Regex.IsMatch(newpassword, @"[A-Z]") || !Regex.IsMatch(newpassword, @"[\d]") || !Regex.IsMatch(newpassword, @"[\W]"))
            {
                MessageBox.Show("Password must be at least 6 characters long and include an uppercase letter, a number, and a special character.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string checkQuery = "SELECT COUNT(*) FROM login WHERE username = @username AND password = @password";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        checkCmd.Parameters.AddWithValue("@Password", oldpassword);

                        int match = (int)checkCmd.ExecuteScalar();
                        if (match == 0)
                        {
                            MessageBox.Show("Old password is incorrect.");
                            return;
                        }
                    }
                    string updateQuery = "UPDATE login SET password = @newPassword WHERE username = @username";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@username", username);
                        updateCmd.Parameters.AddWithValue("@newPassword", newpassword);

                        int rows = updateCmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Password changed successfully.");
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Password change failed.");
                        }
                    }


                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //password show hide
        private bool PasswordVisible = true;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PasswordVisible = !PasswordVisible;

            if (PasswordVisible)
            {
                textBox1.UseSystemPasswordChar = false;
                pictureBox1.Image = Properties.Resources.eye_18360006;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                pictureBox1.Image = Properties.Resources.eye_4734271;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (PasswordVisible)
            {
                textBox2.UseSystemPasswordChar = false;
                pictureBox2.Image = Properties.Resources.eye_18360006;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                pictureBox2.Image = Properties.Resources.eye_4734271;
            }
        }
    }
}
