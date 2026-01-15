using Library_System;
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
    public partial class Borrow : Form
    {
        public Borrow()
        {
            InitializeComponent();
        }

        //Borrow Book button
        private void button1_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int memberID = Convert.ToInt32(comboBox1.SelectedValue);
                    int book_ID = Convert.ToInt32(comboBox2.SelectedValue);

                    // borrowing limit
                    int maxBorrowLimit = 3;

                    string borrowCountQuery = "SELECT COUNT(*) FROM BorrowedBooks WHERE memberID = @memberID AND Status = 'Borrowed'";
                    using (SqlCommand borrowCountCmd = new SqlCommand(borrowCountQuery, conn, transaction))

                    {
                        borrowCountCmd.Parameters.AddWithValue("@memberID", memberID);
                        int borrowedBooks = Convert.ToInt32(borrowCountCmd.ExecuteScalar());

                        if (borrowedBooks >= maxBorrowLimit)
                        {
                            MessageBox.Show($"You have reached the borrowing limit of {maxBorrowLimit} books. Please return a book before borrowing more.", "Borrowing Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                    // Retrieve overdue days for the member
                    string overdueQuery = "SELECT SUM(DATEDIFF(DAY, DueDate, GETDATE())) FROM BorrowedBooks WHERE memberID = @memberID AND Status = 'Borrowed' AND DueDate < GETDATE()";
                    using (SqlCommand overdueCmd = new SqlCommand(overdueQuery, conn, transaction))
                    {
                        overdueCmd.Parameters.AddWithValue("@memberID", memberID);
                        object overdueDaysResult = overdueCmd.ExecuteScalar();
                        int overdueDays = overdueDaysResult != DBNull.Value ? Convert.ToInt32(overdueDaysResult) : 0;

                        if (overdueDays > 0)
                        {
                            double fineRate;
                            if (!double.TryParse(ConfigurationManager.AppSettings["FineRate"], out fineRate))
                            {
                                MessageBox.Show("FineRate configuration is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            double fineAmount = overdueDays * fineRate;

                            MessageBox.Show($"You have an unpaid fine of ${fineAmount}. Please settle it before borrowing another book.", "Borrowing Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }
                    }

                    if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a valid member and book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        transaction.Rollback();
                        return;
                    }

                    //borrowed days
                    DateTime borrowDate = DateTime.Now;
                    DateTime dueDate = DateTime.Today.AddDays(7);
                    label5.Visible = true;
                    label5.Text = dueDate.ToString("yyyy/MM/dd");

                    string query = "INSERT INTO BorrowedBooks (memberID, book_ID, BorrowDate, DueDate) VALUES (@memberID, @book_ID, @BorrowDate, @DueDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@memberID", memberID);
                        cmd.Parameters.AddWithValue("@book_ID", book_ID);
                        cmd.Parameters.AddWithValue("@BorrowDate", borrowDate);
                        cmd.Parameters.AddWithValue("@DueDate", dueDate);
                        cmd.ExecuteNonQuery();
                    }
                    string updateQuery = "UPDATE books SET AvailableCopies = AvailableCopies - 1 WHERE book_ID = @book_ID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@book_ID", book_ID);
                        updateCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadBooks();
                    loadBorrowedBooks();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //load members to ComboBox1
        private void loadMembers()
        {
            string connstring = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                string query = "SELECT memberID, fullname FROM members";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "fullname";
                comboBox1.ValueMember = "memberID";
            }
        }

        //Load books to ComboBox2
        private void loadBooks()
        {
            string connstring = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT book_ID, title FROM books WHERE AvailableCopies > 0";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "title";
                    comboBox2.ValueMember = "book_ID";

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        // load books information to DGV
        private void loadBorrowedBooks()
        {
            string connstring = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                try
                {
                    conn.Open();
                    string query = "Select title,Author,ISBN,publisher,Yearpublished,Genre, AvailableCopies from books";

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

        //load issue form
        private void Borrow_Load(object sender, EventArgs e)
        {
            loadMembers();
            loadBooks();
            loadBorrowedBooks();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        //search field
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT * FROM books WHERE title LIKE @search OR author LIKE @search OR ISBN LIKE @search OR Publisher LIKE @search OR YearPublished LIKE @search OR Genre LIKE @search";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Search", "%" + searchValue + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                dataGridView1.DataSource = dataTable;
            }
        }

        //highlight our off stock books
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "AvailableCopies")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int availableCopies))
                {
                    if (availableCopies == 0)
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}
