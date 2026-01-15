using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
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
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library_Mangement_System
{
    public partial class Members : Form
    {
        public Members()
        {
            InitializeComponent();
        }

        // register new user
        private void button1_Click(object sender, EventArgs e)
        {
            string fullname = textBox2.Text.Trim();
            string email = textBox3.Text.Trim();
            string address = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullname) ||
             string.IsNullOrWhiteSpace(email) ||
             string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM members WHERE email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("A member with this email already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query = "INSERT INTO members (fullname, email, address) VALUES (@fullname, @email, @address)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fullname", fullname);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@address", address);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMembers();
                        ClearFields();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //update existing user
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int memberID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["memberID"].Value);
                string fullname = textBox2.Text.Trim();
                string email = textBox3.Text.Trim();
                string address = textBox4.Text.Trim();

                if (string.IsNullOrWhiteSpace(fullname) ||
               string.IsNullOrWhiteSpace(email) ||
               string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE members SET fullname=@fullname, email=@email, address=@address WHERE memberID=@memberID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("fullname", fullname);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@address", address);
                            cmd.Parameters.AddWithValue("@memberID", memberID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMembers();
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
                MessageBox.Show("Please select a Member to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //delete existing user
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int memberID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["memberID"].Value);

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                       // Check if the member has any unreturned books
                        string checkQuery = "SELECT COUNT(*) FROM BorrowedBooks WHERE memberID = @memberID AND ReturnDate IS NULL";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@memberID", memberID);
                            int unreturnedCount = (int)checkCmd.ExecuteScalar();

                            if (unreturnedCount > 0)
                            {
                                MessageBox.Show("Cannot delete this member. They have unreturned books.", "Deletion Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }


                        string query = "DELETE FROM members WHERE memberID=@memberID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@memberID", memberID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMembers();
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
                MessageBox.Show("Please select a Member to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //clear fields
        private int selectedmemberID = 0;
        private void ClearFields()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            selectedmemberID = 0;
            textBox1.Focus();
        }

        //load existing members to DGV
        private void LoadMembers()
        {

            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM  members";
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

        //load member form
        private void Members_Load(object sender, EventArgs e)
        {
            LoadMembers();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.ReadOnly = true; // Makes the grid read-only
            dataGridView1.AllowUserToAddRows = false; // Prevents adding new rows directly
        }

        // search fields
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM members WHERE fullname LIKE @search OR email LIKE @search OR address LIKE @search OR memberID LIKE @search OR joindate LIKE @search";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Search", "%" + searchValue + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    dataGridView1.DataSource = dataTable;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //populate fields from the DGV
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["fullname"].Value.ToString();
                textBox3.Text = row.Cells["email"].Value.ToString();
                textBox4.Text = row.Cells["address"].Value.ToString();

            }
        }
    }
}
