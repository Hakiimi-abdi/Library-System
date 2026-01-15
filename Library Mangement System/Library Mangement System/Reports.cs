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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Library_Mangement_System
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        //Borrowing summery report
        private DataTable GetBorrowingReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                bb.BorrowID,
                m.fullname AS Member,
                b.title AS Book,
                bb.BorrowDate,
                bb.DueDate,
                bb.ReturnDate,
                bb.Status,
                DATEDIFF(day, bb.DueDate, GETDATE()) AS DaysOverdue
            FROM BorrowedBooks bb
            JOIN members m ON bb.memberID = m.memberID
            JOIN books b ON bb.book_ID = b.book_ID
            WHERE bb.BorrowDate BETWEEN @FromDate AND @ToDate
            ORDER BY bb.BorrowDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //Overdue Books Report
        private DataTable GetOverdueReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            // Get FineRate from app.config (not from database)
            decimal fineRate = 0.25m; // Default value
            if (decimal.TryParse(ConfigurationManager.AppSettings["FineRate"], out decimal configRate))
            {
                fineRate = configRate;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                m.fullname AS Member,
                m.email AS Contact,
                b.title AS Book,
                b.author AS Author,
                bb.BorrowDate,
                bb.DueDate,
                DATEDIFF(day, bb.DueDate, GETDATE()) AS DaysOverdue,
                (DATEDIFF(day, bb.DueDate, GETDATE()) * @FineRate) AS FineAmount,
                bb.Status
            FROM BorrowedBooks bb
            JOIN members m ON bb.memberID = m.memberID
            JOIN books b ON bb.book_ID = b.book_ID
            WHERE bb.Status = 'Borrowed' 
                AND bb.DueDate < GETDATE()
                AND bb.ReturnDate IS NULL
                AND bb.BorrowDate BETWEEN @FromDate AND @ToDate
            ORDER BY DaysOverdue DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@FineRate", fineRate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //popular Books Report
        private DataTable GetPopularBooksReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                ROW_NUMBER() OVER (ORDER BY COUNT(bb.BorrowID) DESC) AS Rank,
                b.title AS BookTitle,
                b.author AS Author,
                b.Genre,
                COUNT(bb.BorrowID) AS TimesBorrowed,
                b.AvailableCopies,
                b.TotalCopies,
                CAST(COUNT(bb.BorrowID) * 100.0 / (SELECT COUNT(*) FROM BorrowedBooks WHERE BorrowDate BETWEEN @FromDate AND @ToDate) AS DECIMAL(5,2)) AS Percentage
            FROM books b
            LEFT JOIN BorrowedBooks bb ON b.book_ID = bb.book_ID 
                AND bb.BorrowDate BETWEEN @FromDate AND @ToDate
            GROUP BY b.book_ID, b.title, b.author, b.Genre, b.AvailableCopies, b.TotalCopies
            HAVING COUNT(bb.BorrowID) > 0 OR @FromDate <= @ToDate
            ORDER BY TimesBorrowed DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //Member Activity Report
        private DataTable GetMemberActivityReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                m.memberID,
                m.fullname AS MemberName,
                m.email,
                m.JoinDate,
                COUNT(bb.BorrowID) AS TotalBorrows,
                SUM(CASE WHEN bb.Status = 'Borrowed' AND bb.DueDate < GETDATE() THEN 1 ELSE 0 END) AS OverdueCount,
                SUM(CASE WHEN bb.Status = 'Returned' THEN 1 ELSE 0 END) AS ReturnedCount,
                MAX(bb.BorrowDate) AS LastBorrowDate
            FROM members m
            LEFT JOIN BorrowedBooks bb ON m.memberID = bb.memberID 
                AND bb.BorrowDate BETWEEN @FromDate AND @ToDate
            GROUP BY m.memberID, m.fullname, m.email, m.JoinDate
            ORDER BY TotalBorrows DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //Inventory status report
        private DataTable GetInventoryReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                b.book_ID AS BookID,
                b.title AS Title,
                b.author AS Author,
                b.ISBN,
                b.Genre,
                b.TotalCopies,
                b.AvailableCopies,
                (b.TotalCopies - b.AvailableCopies) AS BorrowedCopies,
                CAST(b.AvailableCopies * 100.0 / NULLIF(b.TotalCopies, 0) AS DECIMAL(5,2)) AS AvailabilityPercentage,
                COUNT(bb.BorrowID) AS TimesBorrowedInPeriod
            FROM books b
            LEFT JOIN BorrowedBooks bb ON b.book_ID = bb.book_ID 
                AND bb.BorrowDate BETWEEN @FromDate AND @ToDate
            GROUP BY b.book_ID, b.title, b.author, b.ISBN, b.Genre, b.TotalCopies, b.AvailableCopies
            ORDER BY b.title";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //Fines Collection Report
        private DataTable GetFinesReport(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["library"].ConnectionString;

            // Get FineRate from app.config
            decimal fineRate = 0.25m; // Default value
            if (decimal.TryParse(ConfigurationManager.AppSettings["FineRate"], out decimal configRate))
            {
                fineRate = configRate;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = @"
            SELECT 
                m.fullname AS Member,
                m.email AS Contact,
                b.title AS Book,
                bb.DueDate,
                DATEDIFF(day, bb.DueDate, COALESCE(bb.ReturnDate, GETDATE())) AS DaysOverdue,
                (DATEDIFF(day, bb.DueDate, COALESCE(bb.ReturnDate, GETDATE())) * @FineRate) AS FineAmount,
                CASE 
                    WHEN bb.ReturnDate IS NOT NULL THEN 'Paid'
                    ELSE 'Pending'
                END AS PaymentStatus,
                bb.ReturnDate
            FROM BorrowedBooks bb
            JOIN members m ON bb.memberID = m.memberID
            JOIN books b ON bb.book_ID = b.book_ID
            WHERE bb.DueDate < COALESCE(bb.ReturnDate, GETDATE())
                AND bb.BorrowDate BETWEEN @FromDate AND @ToDate
            ORDER BY FineAmount DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@FineRate", fineRate);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //
        private void GenerateReport()
        {
            string reportType = comboBox1.SelectedItem?.ToString() ?? "Borrowing Summary";
            DateTime fromDate = dateTimePicker1.Value;
            DateTime toDate = dateTimePicker2.Value.AddDays(1).AddSeconds(-1);

            DataTable reportData = new DataTable();

            switch (reportType)
            {
                case "Borrowing Summary":
                    reportData = GetBorrowingReport(fromDate, toDate);
                    break;
                case "Overdue Books":
                    reportData = GetOverdueReport(fromDate, toDate);
                    break;
                case "Popular Books":
                    reportData = GetPopularBooksReport(fromDate, toDate);
                    break;
                case "Member Activity":
                    reportData = GetMemberActivityReport(fromDate, toDate);
                    break;
                case "Inventory Status":
                    reportData = GetInventoryReport(fromDate, toDate);
                    break;
                case "Fines Collection":
                    reportData = GetFinesReport(fromDate, toDate);
                    break;
            }

            // Bind to DataGridView
            dataGridView1.DataSource = reportData;

            // Update statistics
            UpdateStatistics(reportData, reportType);
        }

        //statistics card
        private void UpdateStatistics(DataTable data, string reportType)
        {
            label5.Text = $"Total Records: {data.Rows.Count}";

            if (reportType == "Overdue Books" || reportType == "Fines Collection")
            {
                decimal totalFines = 0;
                foreach (DataRow row in data.Rows)
                {
                    if (row["FineAmount"] != DBNull.Value)
                    {
                        decimal fine = Convert.ToDecimal(row["FineAmount"]);
                        totalFines += fine;
                    }
                }
                label6.Text = $"Total Fines: ${totalFines:F2}";
                label7.Text = $"Report: {reportType}";
                label8.Text = $"Period: {dateTimePicker1.Value:MMM dd} - {dateTimePicker2.Value:MMM dd}";
            }
            else if (reportType == "Borrowing Summary")
            {
                int active = 0, returned = 0;
                foreach (DataRow row in data.Rows)
                {
                    if (row["Status"] != DBNull.Value)
                    {
                        string status = row["Status"].ToString();
                        if (status == "Borrowed") active++;
                        else if (status == "Returned") returned++;
                    }
                }
                label6.Text = $"Active: {active}";
                label7.Text = $"Returned: {returned}";
                label8.Text = $"Report: {reportType}";
            }
            else
            {
                label6.Text = $"Report: {reportType}";
                label7.Text = $"Period: {dateTimePicker1.Value:MMM dd} - {dateTimePicker2.Value:MMM dd}";
                label8.Text = $"Generated: {DateTime.Now:hh:mm tt}";
            }
        }

        //generate report button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                GenerateReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        //print button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No data to print. Please generate a report first.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();
                printDocument.DefaultPageSettings.Landscape = true; // Better for library reports
                printDialog.Document = printDocument;

                // Tracking row index for multi-page printing
                int rowIndex = 0;

                printDocument.PrintPage += (s, ev) =>
                {
                    float currentY = 40;
                    float leftMargin = 50;
                    float columnWidth = (ev.PageBounds.Width - 100) / dataGridView1.Columns.Count;

                    // 1. Draw Logo from Resources
                    try
                    {
                        byte[] imageBytes = Properties.Resources.library_logo;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            System.Drawing.Image logo = System.Drawing.Image.FromStream(ms);
                            ev.Graphics.DrawImage(logo, (ev.PageBounds.Width / 2) - 30, currentY, 60, 60);
                            currentY += 70;
                        }
                    }
                    catch { /* Logo skipped if error */ }

                    // 2. Draw Header
                    System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
                    System.Drawing.Font subFont = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
                    string title = "THE WORLD LIBRARY";
                    string reportInfo = $"Report: {comboBox1.Text} | Period: {dateTimePicker1.Value:yyyy-MM-dd} to {dateTimePicker2.Value:yyyy-MM-dd}";

                    ev.Graphics.DrawString(title, titleFont, Brushes.DarkBlue, (ev.PageBounds.Width / 2) - (ev.Graphics.MeasureString(title, titleFont).Width / 2), currentY);
                    currentY += 30;
                    ev.Graphics.DrawString(reportInfo, subFont, Brushes.Black, (ev.PageBounds.Width / 2) - (ev.Graphics.MeasureString(reportInfo, subFont).Width / 2), currentY);
                    currentY += 40;

                    // 3. Draw Table Headers
                    System.Drawing.Font headerFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                    float currentX = leftMargin;

                    // Draw Header Background
                    ev.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(41, 128, 185)), leftMargin, currentY, ev.PageBounds.Width - 100, 25);

                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        ev.Graphics.DrawString(col.HeaderText, headerFont, Brushes.White, new RectangleF(currentX, currentY + 5, columnWidth, 20));
                        currentX += columnWidth;
                    }
                    currentY += 30;

                    // 4. Draw Rows
                    System.Drawing.Font rowFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);

                    while (rowIndex < dataGridView1.Rows.Count)
                    {
                        DataGridViewRow row = dataGridView1.Rows[rowIndex];
                        if (!row.IsNewRow)
                        {
                            currentX = leftMargin;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                ev.Graphics.DrawString(cell.Value?.ToString() ?? "", rowFont, Brushes.Black, new RectangleF(currentX, currentY, columnWidth, 20));
                                currentX += columnWidth;
                            }
                            currentY += 20;
                        }
                        rowIndex++;

                        // Check if we need a new page
                        if (currentY > ev.PageBounds.Height - 60)
                        {
                            ev.HasMorePages = true;
                            return;
                        }
                    }

                    // If we reach here, we are done
                    ev.HasMorePages = false;
                    rowIndex = 0; // Reset for next time
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                    MessageBox.Show("Report sent to printer.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Print error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //export pdf
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export. Please generate a report first.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"LibraryReport_{comboBox1.Text.Replace(" ", "")}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, new FileStream(saveDialog.FileName, FileMode.Create));
                    pdfDoc.Open();

                    // Add logo
                    try
                    {
                        byte[] imageBytes = Properties.Resources.library_logo;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(ms.ToArray());
                            logo.ScaleToFit(70f, 70f); // Professional small size
                            logo.Alignment = Element.ALIGN_CENTER;
                            logo.SpacingAfter = 10f;
                            pdfDoc.Add(logo);
                        }
                    }
                    catch (Exception ex) { Debug.WriteLine("Logo load failed: " + ex.Message); }

                    iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                    Paragraph title = new Paragraph($"Library Management System - Report", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    pdfDoc.Add(new Paragraph($"Report Type: {comboBox1.Text}"));
                    pdfDoc.Add(new Paragraph($"Period: {dateTimePicker1.Value:yyyy-MM-dd} to {dateTimePicker2.Value:yyyy-MM-dd}"));
                    pdfDoc.Add(new Paragraph($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n"));

                    PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                        cell.BackgroundColor = new BaseColor(41, 128, 185); // Your blue color
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfTable.AddCell(cell);
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? ""));
                                pdfTable.AddCell(pdfCell);
                            }
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();

                    MessageBox.Show($"PDF exported successfully!\n{saveDialog.FileName}", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting PDF: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //export excel
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export. Please generate a report first.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"LibraryReport_{comboBox1.Text.Replace(" ", "")}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Report");

                        worksheet.Cell(1, 1).Value = "Library Management System - Report";
                        worksheet.Range(1, 1, 1, dataGridView1.Columns.Count).Merge();
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        worksheet.Cell(2, 1).Value = $"Report Type: {comboBox1.Text}";
                        worksheet.Cell(3, 1).Value = $"Period: {dateTimePicker1.Value:yyyy-MM-dd} to {dateTimePicker2.Value:yyyy-MM-dd}";
                        worksheet.Cell(4, 1).Value = $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";

                        int rowIndex = 6;
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cell(rowIndex, i + 1).Value = dataGridView1.Columns[i].HeaderText;
                            worksheet.Cell(rowIndex, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(rowIndex, i + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(41, 128, 185); // Your blue
                            worksheet.Cell(rowIndex, i + 1).Style.Font.FontColor = XLColor.White;
                        }

                        rowIndex++;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                {
                                    worksheet.Cell(rowIndex, i + 1).Value = row.Cells[i].Value?.ToString();
                                }
                                rowIndex++;
                            }
                        }

                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show($"Excel file exported successfully!\n{saveDialog.FileName}", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting Excel: {ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
