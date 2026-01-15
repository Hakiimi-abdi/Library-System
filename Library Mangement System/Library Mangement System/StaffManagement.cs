using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Library_Mangement_System
{
    public partial class StaffManagement : Form
    {
        public StaffManagement()
        {
            InitializeComponent();
        }

        //register new user
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string role = comboBox1.SelectedItem?.ToString();
            string email = textBox3.Text.Trim();
            string address = textBox4.Text.Trim();

            string hashedPassword = HashPassword(password);


            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(role))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6 || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[\d]") || !Regex.IsMatch(password, @"[\W]"))
            {
                MessageBox.Show("Password must be at least 6 characters long and include an uppercase letter, a number, and a special character.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "insert into login(username,password,Role,Email,address) values(@username,@password,@Role,@email,@address)";
                    using SqlCommand cmd = new SqlCommand(query, conn);
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@address", address);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User registered successfully", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStaff();
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //update existing user
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int staffID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["staffID"].Value);
                string username = textBox1.Text.Trim();
                string password = textBox2.Text.Trim();
                string role = comboBox1.SelectedItem?.ToString();
                string email = textBox3.Text.Trim();
                string address = textBox4.Text.Trim();

                if (string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(address) ||
                    string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hashedPassword = HashPassword(password);


                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        string updateQuery = "UPDATE login SET role=@role, username=@username, password=@password, email=@email, address=@address WHERE staffID=@staffID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@staffID", staffID);
                            updateCmd.Parameters.AddWithValue("@role", role);
                            updateCmd.Parameters.AddWithValue("@username", username);
                            updateCmd.Parameters.AddWithValue("@password", hashedPassword);
                            updateCmd.Parameters.AddWithValue("@email", email);
                            updateCmd.Parameters.AddWithValue("@address", address);
                            updateCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Staff updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStaff();
                        ClearFields();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a staff to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //delete existing user
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int staffID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["staffID"].Value);

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM login WHERE staffID=@staffID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@staffID", staffID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("staff deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStaff();
                            ClearFields();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a staff to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //search staff details
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox5.Text;
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT staffId, role, username, email, address FROM Login WHERE staffID LIKE @search OR username LIKE @search OR email LIKE @search OR address LIKE @search";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Search", "%" + searchValue + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                dataGridView1.DataSource = dataTable;

            }
        }

        //Clear input fields
        private void ClearFields()
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            selectedStaffID = 0;
            comboBox1.Focus();
        }
        //Load Staff Data
        private void LoadStaff()
        {
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT staffID, role,username,email,address FROM login";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //populate fields on datagridview
        int selectedStaffID = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                row.Selected = true;
                selectedStaffID = Convert.ToInt32(row.Cells["staffID"].Value);
                comboBox1.SelectedItem = row.Cells["role"].Value.ToString();
                textBox1.Text = row.Cells["username"].Value.ToString();
                textBox3.Text = row.Cells["email"].Value.ToString();
                textBox4.Text = row.Cells["address"].Value.ToString();
                dataGridView1.ReadOnly = true; // Makes the grid read-only
                dataGridView1.AllowUserToAddRows = false; // Prevents adding new rows directly
            }
        }

        //hash password

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool PasswordVisible = true;

        //load the form
        private void StaffManagement_Load(object sender, EventArgs e)
        {
            LoadStaff();
            pictureBox1.Image = Properties.Resources.eye_4734271;
        }

        //password visibility
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
    }
}
