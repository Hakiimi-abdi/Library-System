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
    public partial class Books : Form
    {
        public Books()
        {
            InitializeComponent();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        //function for isbn
        private bool IsValidISBN(string isbn)
        {
            return isbn.All(c => char.IsDigit(c) || (isbn.Length == 10 && c == 'X' && isbn.IndexOf(c) == 9));
        }

        //add book
        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();
            string author = textBox2.Text.Trim();
            string genre = comboBox1.SelectedItem?.ToString();
            string isbn = textBox3.Text.Trim();
            string publisher = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
             string.IsNullOrWhiteSpace(author) ||
             string.IsNullOrWhiteSpace(genre) ||
             string.IsNullOrWhiteSpace(isbn) ||
             string.IsNullOrWhiteSpace(publisher))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBox5.Text, out int year) || year < 1800 || year > DateTime.Now.Year)
            {
                MessageBox.Show("Please enter a valid publication year!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!int.TryParse(textBox6.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Normalize ISBN
            string cleanISBN = isbn.Replace("-", "").Replace(" ", "");

            if (!(cleanISBN.Length == 10 || cleanISBN.Length == 13) || !IsValidISBN(cleanISBN))
            {
                MessageBox.Show("Please enter a valid ISBN (10 or 13 digits)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    string checkBookQuery = "SELECT COUNT(*) FROM books WHERE ISBN = @ISBN OR title = @title";
                    using (SqlCommand checkCmd = new SqlCommand(checkBookQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ISBN", isbn);
                        checkCmd.Parameters.AddWithValue("@title", title);

                        int existingBooks = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (existingBooks > 0)
                        {
                            MessageBox.Show("A book with this ISBN or title already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query = "INSERT INTO Books (Title, Author, Genre, ISBN, Publisher, YearPublished, Quantity,AvailableCopies,TotalCopies) VALUES (@Title, @Author, @Genre, @ISBN, @Publisher, @Year, @Quantity,@Quantity,@Quantity)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@Genre", genre);
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        cmd.Parameters.AddWithValue("@Publisher", publisher);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        //update book
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int book_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["book_ID"].Value);
                string title = textBox1.Text.Trim();
                string author = textBox2.Text.Trim();
                string genre = comboBox1.SelectedItem?.ToString() ?? "";
                string isbn = textBox3.Text.Trim();
                string publisher = textBox4.Text.Trim();
                int year;

                if (!int.TryParse(textBox5.Text, out year) || year < 0)
                {
                    MessageBox.Show("Please enter a valid year!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox6.Text, out int addedQuantity) || addedQuantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        // Retrieve current quantity
                        string getQuantityQuery = "SELECT Quantity FROM Books WHERE book_ID = @book_ID";
                        using (SqlCommand getQuantityCmd = new SqlCommand(getQuantityQuery, conn, transaction))
                        {
                            getQuantityCmd.Parameters.AddWithValue("@book_ID", book_ID);
                            object quantityResult = getQuantityCmd.ExecuteScalar();
                            int currentQuantity = quantityResult != DBNull.Value ? Convert.ToInt32(quantityResult) : 0;

                            int newQuantity = currentQuantity + addedQuantity;

                            string updateQuery = "UPDATE Books SET Title=@Title, Author=@Author, Genre=@Genre, ISBN=@ISBN, Publisher=@Publisher, YearPublished=@Year, Quantity=@newQuantity, TotalCopies=@newQuantity, AvailableCopies=@newQuantity WHERE book_ID=@book_ID";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@book_ID", book_ID);
                                updateCmd.Parameters.AddWithValue("@Title", title);
                                updateCmd.Parameters.AddWithValue("@Author", author);
                                updateCmd.Parameters.AddWithValue("@Genre", genre);
                                updateCmd.Parameters.AddWithValue("@ISBN", isbn);
                                updateCmd.Parameters.AddWithValue("@Publisher", publisher);
                                updateCmd.Parameters.AddWithValue("@Year", year);
                                updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                                updateCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooks();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a book to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //delete book
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int book_ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["book_ID"].Value);

                string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Books WHERE book_ID=@book_ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@book_ID", book_ID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBooks();
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
                MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //populate textboxes from datagridview
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["Title"].Value.ToString();
                textBox2.Text = row.Cells["Author"].Value.ToString();
                comboBox1.SelectedItem = row.Cells["Genre"].Value.ToString();
                textBox3.Text = row.Cells["ISBN"].Value.ToString();
                textBox4.Text = row.Cells["Publisher"].Value.ToString();
                textBox5.Text = row.Cells["YearPublished"].Value.ToString();
                textBox6.Text = row.Cells["Quantity"].Value.ToString();

                dataGridView1.ReadOnly = true; // Makes the grid read-only
                dataGridView1.AllowUserToAddRows = false; // Prevents adding new rows directly
            }
        }

        //load books to datagridview
        private void LoadBooks()
        {
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT book_ID, title,author,ISBN,publisher,Yearpublished,quantity,AvailableCopies,Genre FROM books";
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

        //books load
        private void Books_Load(object sender, EventArgs e)
        {
            LoadBooks();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }

        //highlight row if available copies is 0
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
