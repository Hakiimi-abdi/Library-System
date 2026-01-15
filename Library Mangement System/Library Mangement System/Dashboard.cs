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
using System.Security.Cryptography;

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

        //go back to dashboard
        private void ShowDashboardHome()
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }

            loadDashboardSummary();
            loadRecentActivity();

            MainPanel.Visible = true;
            MainPanel.BringToFront();
            if (_activeSidebarButton != null)
            {
                _activeSidebarButton.BackColor = Color.Transparent;
            }
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
            int yoffset = pictureBox1.Bottom + 20;
            var orderedButtons = new List<Button>
    {
        iconButton1,
        iconButton2,
        iconButton3,
        iconButton4,
        iconButton5,
        iconButton6,
        iconButton9,
        iconButton7,
        iconButton8
    };

            foreach (var btn in orderedButtons)
            {
                if (btn.Visible)
                {
                    // extra space before Logout
                    if (btn == iconButton7)
                    {
                        yoffset += 60;
                    }

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

                // Count borrowed books
                string queryBorrowed = "SELECT COUNT(*) FROM BorrowedBooks WHERE Status = 'Borrowed'";
                using (SqlCommand cmd = new SqlCommand(queryBorrowed, conn))
                {
                    label16.Text = cmd.ExecuteScalar().ToString();
                }

                // Count overdue books
                string queryOverdue = "SELECT COUNT(*) FROM BorrowedBooks WHERE DueDate < GETDATE() AND Status = 'Borrowed'";
                using (SqlCommand cmd = new SqlCommand(queryOverdue, conn))
                {
                    label15.Text = cmd.ExecuteScalar().ToString();

                }

                //count members
                string queryTotalMembers = "SELECT COUNT(*) FROM members";
                using (SqlCommand cmd = new SqlCommand(queryTotalMembers, conn))
                {
                    label14.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }

        //recent activity section
        private void loadRecentActivity()
        {
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            // This query grabs the 10 most recent records from BorrowedBooks
            string query = @"SELECT TOP 10 
    CASE WHEN Status = 'Borrowed' THEN 'ISSUE' ELSE 'RETURN' END AS Action, 
    B.title + ' (' + CAST(B.book_ID AS VARCHAR) + ') to ' + M.FullName AS Details, 
    BorrowDate AS Time 
FROM BorrowedBooks BB
JOIN Books B ON BB.book_ID = B.book_ID
JOIN Members M ON BB.memberID = M.memberID
ORDER BY BorrowDate DESC";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    dataGridView1.Rows.Clear();

                    while (reader.Read())
                    {
                        string action = reader["Action"].ToString();
                        string details = reader["Details"].ToString();
                        string time = Convert.ToDateTime(reader["Time"]).ToString("MMM dd, HH:mm");

                        int rowIndex = dataGridView1.Rows.Add(action, details, time);

                        if (action == "ISSUE")
                            dataGridView1.Rows[rowIndex].Cells[0].Style.ForeColor = Color.SteelBlue;
                        else if (action == "RETURN")
                            dataGridView1.Rows[rowIndex].Cells[0].Style.ForeColor = Color.ForestGreen;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Activity Log Error: " + ex.Message);
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

        //highlight sidebar button

        private Button _activeSidebarButton = null;
        private void HighlightSidebarButton(Button selectedButton)
        {
            if (_activeSidebarButton != null)
            {
                _activeSidebarButton.BackColor = Color.Transparent;
                _activeSidebarButton.FlatAppearance.BorderSize = 0;
                _activeSidebarButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }

            selectedButton.BackColor = Color.FromArgb(100, 255, 255, 255); // translucent white
            selectedButton.FlatAppearance.BorderColor = Color.SteelBlue;
            selectedButton.FlatAppearance.BorderSize = 2;
            selectedButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            _activeSidebarButton = selectedButton;
        }

        //open staffManagement
        private void iconButton1_Click(object sender, EventArgs e)
        {
            openchildform(new StaffManagement());
            HighlightSidebarButton(iconButton1);
        }

        //load dashboard
        private void Dashboard_Load(object sender, EventArgs e)
        {
            AdjustButtonLayout();
            loadDashboardSummary();
            loadRecentActivity();
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
            HighlightSidebarButton(iconButton2);
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

            string hashedOldPassword = HashPassword(oldpassword);
            string hashedNewPassword = HashPassword(newpassword);


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
                        checkCmd.Parameters.AddWithValue("@Password", hashedOldPassword);

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
                        updateCmd.Parameters.AddWithValue("@newPassword", hashedNewPassword);

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

        //open dashboard
        private void label1_Click(object sender, EventArgs e)
        {
            ShowDashboardHome();
        }

        //open issue form
        private void iconButton4_Click(object sender, EventArgs e)
        {
            openchildform(new Borrow());
            HighlightSidebarButton(iconButton4);
        }

        //open members form
        private void iconButton3_Click(object sender, EventArgs e)
        {
            openchildform(new Members());
            HighlightSidebarButton(iconButton3);
        }

        //open return form
        private void iconButton5_Click(object sender, EventArgs e)
        {
            openchildform(new Return());
            HighlightSidebarButton(iconButton5);
        }

        //open overdue form
        private void iconButton6_Click(object sender, EventArgs e)
        {
            openchildform(new OverDue());
            HighlightSidebarButton(iconButton6);
        }

        //open reports form
        private void iconButton9_Click(object sender, EventArgs e)
        {
            openchildform(new Reports());
            HighlightSidebarButton(iconButton9);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
