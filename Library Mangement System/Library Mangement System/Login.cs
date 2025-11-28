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

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    string query = "select Role from login where username = @username AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        string username = textBox1.Text.Trim();
                        string password = textBox2.Text.Trim();

                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            this.Hide();
                            Dashboard dash = new Dashboard(textBox1.Text, role);
                            dash.Show();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username or password.");
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
