using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Library_Mangement_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(52, 152, 219);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(41, 128, 185);
        }

        //login button
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string hashedPassword = HashPassword(password);
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    con.Open();

                        //Check lock status
                    string checkLockQuery = "SELECT IsLocked, LastFailedAttempt FROM login WHERE username = @username";
                    using (SqlCommand lockCmd = new SqlCommand(checkLockQuery, con))
                    {
                        lockCmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader lockReader = lockCmd.ExecuteReader())
                        {
                            if (lockReader.Read())
                            {
                                bool isLocked = (bool)lockReader["IsLocked"];
                                DateTime? lastAttempt = lockReader["LastFailedAttempt"] as DateTime?;

                                if (isLocked)
                                {
                                    if (lastAttempt.HasValue && DateTime.Now.Subtract(lastAttempt.Value).TotalMinutes < 15)
                                    {
                                        MessageBox.Show("Your account is temporarily locked. Try again in a few minutes.", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    else
                                    {
                                        // Auto-unlock
                                        lockReader.Close();
                                        string unlockQuery = "UPDATE login SET IsLocked = 0, FailedAttempts = 0 WHERE username = @username";
                                        using (SqlCommand unlockCmd = new SqlCommand(unlockQuery, con))
                                        {
                                            unlockCmd.Parameters.AddWithValue("@username", username);
                                            unlockCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username not found.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    //Validate
                    string query = "SELECT Role FROM login WHERE username = @username AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            reader.Close();

                            // Reset failed attempts
                            string resetQuery = "UPDATE login SET FailedAttempts = 0, LastFailedAttempt = NULL WHERE username = @username";
                            using (SqlCommand resetCmd = new SqlCommand(resetQuery, con))
                            {
                                resetCmd.Parameters.AddWithValue("@username", username);
                                resetCmd.ExecuteNonQuery();
                            }

                            this.Hide();
                            Dashboard dash = new Dashboard(username, role);
                            dash.Show();
                        }
                        else
                        {
                            reader.Close();

                            // Increment failed attempts
                            string updateQuery = @"UPDATE login SET FailedAttempts = FailedAttempts + 1,LastFailedAttempt = GETDATE(),IsLocked = CASE WHEN FailedAttempts + 1 >= 5 THEN 1 ELSE 0 END WHERE username = @username";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@username", username);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Incorrect username or password. After 5 failed attempts, your account will be locked for 15 minutes.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        //hash password
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //show hide password
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PasswordVisible = !PasswordVisible;

            if (PasswordVisible)
            {
                textBox2.UseSystemPasswordChar = false;
                pictureBox1.Image = Properties.Resources.eye_18360006;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                pictureBox1.Image = Properties.Resources.eye_4734271;
            }
        }

        private bool PasswordVisible = true;
        private void Login_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.eye_4734271;
        }
    }
}
