namespace Library_Mangement_System
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainPanel = new Panel();
            SlideIn = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            iconButton10 = new FontAwesome.Sharp.IconButton();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            panel3 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Card4 = new Panel();
            label15 = new Label();
            label10 = new Label();
            label5 = new Label();
            Card3 = new Panel();
            label14 = new Label();
            label9 = new Label();
            label4 = new Label();
            Card5 = new Panel();
            label16 = new Label();
            label11 = new Label();
            label6 = new Label();
            Card1 = new Panel();
            label12 = new Label();
            label8 = new Label();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            ActionType = new DataGridViewTextBoxColumn();
            Details = new DataGridViewTextBoxColumn();
            Timestamp = new DataGridViewTextBoxColumn();
            SideBar = new Panel();
            iconButton8 = new FontAwesome.Sharp.IconButton();
            iconButton7 = new FontAwesome.Sharp.IconButton();
            iconButton6 = new FontAwesome.Sharp.IconButton();
            iconButton5 = new FontAwesome.Sharp.IconButton();
            iconButton4 = new FontAwesome.Sharp.IconButton();
            iconButton3 = new FontAwesome.Sharp.IconButton();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            HeaderPanel = new Panel();
            UserLabel = new Label();
            Setting = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            MainPanel.SuspendLayout();
            SlideIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Card4.SuspendLayout();
            Card3.SuspendLayout();
            Card5.SuspendLayout();
            Card1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SideBar.SuspendLayout();
            HeaderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            MainPanel.Controls.Add(SlideIn);
            MainPanel.Controls.Add(panel3);
            MainPanel.Controls.Add(SideBar);
            MainPanel.Controls.Add(HeaderPanel);
            MainPanel.Location = new Point(0, 0);
            MainPanel.MaximumSize = new Size(1600, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1145, 722);
            MainPanel.TabIndex = 0;
            // 
            // SlideIn
            // 
            SlideIn.BackColor = Color.White;
            SlideIn.Controls.Add(pictureBox2);
            SlideIn.Controls.Add(pictureBox1);
            SlideIn.Controls.Add(iconButton10);
            SlideIn.Controls.Add(textBox2);
            SlideIn.Controls.Add(textBox1);
            SlideIn.Location = new Point(724, 7);
            SlideIn.Name = "SlideIn";
            SlideIn.Size = new Size(345, 211);
            SlideIn.TabIndex = 12;
            SlideIn.Visible = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = Properties.Resources.eye_4734271;
            pictureBox2.Location = new Point(277, 84);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 30);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.eye_4734271;
            pictureBox1.Location = new Point(277, 44);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 30);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // iconButton10
            // 
            iconButton10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            iconButton10.BackColor = Color.FromArgb(41, 128, 185);
            iconButton10.FlatAppearance.BorderSize = 0;
            iconButton10.FlatStyle = FlatStyle.Flat;
            iconButton10.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton10.ForeColor = Color.White;
            iconButton10.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButton10.IconColor = Color.Black;
            iconButton10.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton10.Location = new Point(12, 135);
            iconButton10.Name = "iconButton10";
            iconButton10.Size = new Size(312, 58);
            iconButton10.TabIndex = 2;
            iconButton10.Text = "Change Password";
            iconButton10.UseVisualStyleBackColor = false;
            iconButton10.Click += iconButton10_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.ForeColor = Color.DarkGray;
            textBox2.Location = new Point(12, 84);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "New Password";
            textBox2.Size = new Size(300, 34);
            textBox2.TabIndex = 1;
            textBox2.UseSystemPasswordChar = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.DarkGray;
            textBox1.Location = new Point(12, 44);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Old Password";
            textBox1.Size = new Size(300, 34);
            textBox1.TabIndex = 0;
            textBox1.UseSystemPasswordChar = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel1);
            panel3.Controls.Add(dataGridView1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(250, 80);
            panel3.Name = "panel3";
            panel3.Size = new Size(895, 642);
            panel3.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(Card4, 3, 0);
            tableLayoutPanel1.Controls.Add(Card3, 2, 0);
            tableLayoutPanel1.Controls.Add(Card5, 1, 0);
            tableLayoutPanel1.Controls.Add(Card1, 0, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(10, 10, 0, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(895, 192);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // Card4
            // 
            Card4.BackColor = Color.White;
            Card4.BorderStyle = BorderStyle.FixedSingle;
            Card4.Controls.Add(label15);
            Card4.Controls.Add(label10);
            Card4.Controls.Add(label5);
            Card4.ForeColor = Color.FromArgb(231, 76, 60);
            Card4.Location = new Point(676, 13);
            Card4.Name = "Card4";
            Card4.Padding = new Padding(15);
            Card4.Size = new Size(200, 144);
            Card4.TabIndex = 6;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(15, 85);
            label15.Name = "label15";
            label15.Size = new Size(112, 65);
            label15.TabIndex = 4;
            label15.Text = "123";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(15, 60);
            label10.Name = "label10";
            label10.Size = new Size(97, 28);
            label10.TabIndex = 1;
            label10.Text = "Overdue:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe MDL2 Assets", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(15, 15);
            label5.Name = "label5";
            label5.Size = new Size(57, 40);
            label5.TabIndex = 0;
            label5.Text = "";
            // 
            // Card3
            // 
            Card3.BackColor = Color.White;
            Card3.BorderStyle = BorderStyle.FixedSingle;
            Card3.Controls.Add(label14);
            Card3.Controls.Add(label9);
            Card3.Controls.Add(label4);
            Card3.ForeColor = Color.FromArgb(46, 204, 113);
            Card3.Location = new Point(455, 13);
            Card3.Name = "Card3";
            Card3.Padding = new Padding(15);
            Card3.Size = new Size(200, 144);
            Card3.TabIndex = 5;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.Location = new Point(15, 85);
            label14.Name = "label14";
            label14.Size = new Size(112, 65);
            label14.TabIndex = 4;
            label14.Text = "123";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(15, 60);
            label9.Name = "label9";
            label9.Size = new Size(105, 28);
            label9.TabIndex = 1;
            label9.Text = "Available:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe MDL2 Assets", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(15, 15);
            label4.Name = "label4";
            label4.Size = new Size(57, 40);
            label4.TabIndex = 0;
            label4.Text = "";
            // 
            // Card5
            // 
            Card5.BackColor = Color.White;
            Card5.BorderStyle = BorderStyle.FixedSingle;
            Card5.Controls.Add(label16);
            Card5.Controls.Add(label11);
            Card5.Controls.Add(label6);
            Card5.ForeColor = Color.FromArgb(241, 196, 15);
            Card5.Location = new Point(234, 13);
            Card5.Name = "Card5";
            Card5.Padding = new Padding(15);
            Card5.Size = new Size(200, 144);
            Card5.TabIndex = 4;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.Location = new Point(15, 85);
            label16.Name = "label16";
            label16.Size = new Size(112, 65);
            label16.TabIndex = 4;
            label16.Text = "123";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(15, 60);
            label11.Name = "label11";
            label11.Size = new Size(76, 28);
            label11.TabIndex = 2;
            label11.Text = "Issued:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe MDL2 Assets", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(15, 15);
            label6.Name = "label6";
            label6.Size = new Size(57, 40);
            label6.TabIndex = 0;
            label6.Text = "";
            // 
            // Card1
            // 
            Card1.BackColor = Color.White;
            Card1.BorderStyle = BorderStyle.FixedSingle;
            Card1.Controls.Add(label12);
            Card1.Controls.Add(label8);
            Card1.Controls.Add(label3);
            Card1.ForeColor = Color.FromArgb(41, 128, 185);
            Card1.Location = new Point(13, 13);
            Card1.Name = "Card1";
            Card1.Padding = new Padding(15);
            Card1.Size = new Size(200, 144);
            Card1.TabIndex = 3;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(15, 85);
            label12.Name = "label12";
            label12.Size = new Size(112, 65);
            label12.TabIndex = 2;
            label12.Text = "123";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(15, 60);
            label8.Name = "label8";
            label8.Size = new Size(74, 28);
            label8.TabIndex = 1;
            label8.Text = "Books:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe MDL2 Assets", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(52, 73, 94);
            label3.Location = new Point(15, 15);
            label3.Name = "label3";
            label3.Size = new Size(57, 40);
            label3.TabIndex = 0;
            label3.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ActionType, Details, Timestamp });
            dataGridView1.Location = new Point(0, 198);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(895, 444);
            dataGridView1.TabIndex = 7;
            // 
            // ActionType
            // 
            ActionType.HeaderText = "Action";
            ActionType.MinimumWidth = 8;
            ActionType.Name = "ActionType";
            ActionType.ReadOnly = true;
            ActionType.Width = 150;
            // 
            // Details
            // 
            Details.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Details.HeaderText = "Details";
            Details.MinimumWidth = 100;
            Details.Name = "Details";
            Details.ReadOnly = true;
            // 
            // Timestamp
            // 
            Timestamp.HeaderText = "Time";
            Timestamp.MinimumWidth = 8;
            Timestamp.Name = "Timestamp";
            Timestamp.ReadOnly = true;
            Timestamp.Width = 120;
            // 
            // SideBar
            // 
            SideBar.BackColor = Color.FromArgb(41, 128, 185);
            SideBar.Controls.Add(iconButton8);
            SideBar.Controls.Add(iconButton7);
            SideBar.Controls.Add(iconButton6);
            SideBar.Controls.Add(iconButton5);
            SideBar.Controls.Add(iconButton4);
            SideBar.Controls.Add(iconButton3);
            SideBar.Controls.Add(iconButton2);
            SideBar.Controls.Add(iconButton1);
            SideBar.Dock = DockStyle.Left;
            SideBar.ForeColor = Color.White;
            SideBar.Location = new Point(0, 80);
            SideBar.Name = "SideBar";
            SideBar.Size = new Size(250, 642);
            SideBar.TabIndex = 1;
            // 
            // iconButton8
            // 
            iconButton8.BackColor = Color.FromArgb(41, 128, 185);
            iconButton8.Cursor = Cursors.Hand;
            iconButton8.FlatAppearance.BorderSize = 0;
            iconButton8.FlatStyle = FlatStyle.Flat;
            iconButton8.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton8.ForeColor = Color.White;
            iconButton8.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            iconButton8.IconColor = Color.White;
            iconButton8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton8.IconSize = 32;
            iconButton8.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton8.Location = new Point(10, 399);
            iconButton8.Name = "iconButton8";
            iconButton8.Padding = new Padding(20, 0, 0, 0);
            iconButton8.Size = new Size(230, 45);
            iconButton8.TabIndex = 9;
            iconButton8.Text = "E&xit";
            iconButton8.TextAlign = ContentAlignment.MiddleLeft;
            iconButton8.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton8.UseVisualStyleBackColor = false;
            iconButton8.Click += iconButton8_Click;
            // 
            // iconButton7
            // 
            iconButton7.BackColor = Color.FromArgb(41, 128, 185);
            iconButton7.Cursor = Cursors.Hand;
            iconButton7.FlatAppearance.BorderSize = 0;
            iconButton7.FlatStyle = FlatStyle.Flat;
            iconButton7.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton7.ForeColor = Color.White;
            iconButton7.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            iconButton7.IconColor = Color.White;
            iconButton7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton7.IconSize = 32;
            iconButton7.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton7.Location = new Point(10, 345);
            iconButton7.Name = "iconButton7";
            iconButton7.Padding = new Padding(20, 0, 0, 0);
            iconButton7.Size = new Size(230, 45);
            iconButton7.TabIndex = 8;
            iconButton7.Text = "Logout";
            iconButton7.TextAlign = ContentAlignment.MiddleLeft;
            iconButton7.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton7.UseVisualStyleBackColor = false;
            iconButton7.Click += iconButton7_Click;
            // 
            // iconButton6
            // 
            iconButton6.BackColor = Color.FromArgb(41, 128, 185);
            iconButton6.Cursor = Cursors.Hand;
            iconButton6.FlatAppearance.BorderSize = 0;
            iconButton6.FlatStyle = FlatStyle.Flat;
            iconButton6.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton6.ForeColor = Color.White;
            iconButton6.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            iconButton6.IconColor = Color.White;
            iconButton6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton6.IconSize = 32;
            iconButton6.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton6.Location = new Point(10, 295);
            iconButton6.Name = "iconButton6";
            iconButton6.Padding = new Padding(20, 0, 0, 0);
            iconButton6.Size = new Size(230, 45);
            iconButton6.TabIndex = 7;
            iconButton6.Text = "Overdue";
            iconButton6.TextAlign = ContentAlignment.MiddleLeft;
            iconButton6.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton6.UseVisualStyleBackColor = false;
            // 
            // iconButton5
            // 
            iconButton5.BackColor = Color.FromArgb(41, 128, 185);
            iconButton5.Cursor = Cursors.Hand;
            iconButton5.FlatAppearance.BorderSize = 0;
            iconButton5.FlatStyle = FlatStyle.Flat;
            iconButton5.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton5.ForeColor = Color.White;
            iconButton5.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleUp;
            iconButton5.IconColor = Color.White;
            iconButton5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton5.IconSize = 32;
            iconButton5.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton5.Location = new Point(10, 240);
            iconButton5.Name = "iconButton5";
            iconButton5.Padding = new Padding(20, 0, 0, 0);
            iconButton5.Size = new Size(230, 45);
            iconButton5.TabIndex = 6;
            iconButton5.Text = "Return";
            iconButton5.TextAlign = ContentAlignment.MiddleLeft;
            iconButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton5.UseVisualStyleBackColor = false;
            // 
            // iconButton4
            // 
            iconButton4.BackColor = Color.FromArgb(41, 128, 185);
            iconButton4.Cursor = Cursors.Hand;
            iconButton4.FlatAppearance.BorderSize = 0;
            iconButton4.FlatStyle = FlatStyle.Flat;
            iconButton4.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton4.ForeColor = Color.White;
            iconButton4.IconChar = FontAwesome.Sharp.IconChar.ArrowCircleDown;
            iconButton4.IconColor = Color.White;
            iconButton4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton4.IconSize = 32;
            iconButton4.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton4.Location = new Point(10, 185);
            iconButton4.Name = "iconButton4";
            iconButton4.Padding = new Padding(20, 0, 0, 0);
            iconButton4.Size = new Size(230, 45);
            iconButton4.TabIndex = 5;
            iconButton4.Text = "Issue";
            iconButton4.TextAlign = ContentAlignment.MiddleLeft;
            iconButton4.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton4.UseVisualStyleBackColor = false;
            // 
            // iconButton3
            // 
            iconButton3.BackColor = Color.FromArgb(41, 128, 185);
            iconButton3.Cursor = Cursors.Hand;
            iconButton3.FlatAppearance.BorderSize = 0;
            iconButton3.FlatStyle = FlatStyle.Flat;
            iconButton3.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton3.ForeColor = Color.White;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.Users;
            iconButton3.IconColor = Color.White;
            iconButton3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton3.IconSize = 32;
            iconButton3.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton3.Location = new Point(10, 130);
            iconButton3.Name = "iconButton3";
            iconButton3.Padding = new Padding(20, 0, 0, 0);
            iconButton3.Size = new Size(230, 45);
            iconButton3.TabIndex = 4;
            iconButton3.Text = "Members";
            iconButton3.TextAlign = ContentAlignment.MiddleLeft;
            iconButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton3.UseVisualStyleBackColor = false;
            // 
            // iconButton2
            // 
            iconButton2.BackColor = Color.FromArgb(41, 128, 185);
            iconButton2.Cursor = Cursors.Hand;
            iconButton2.FlatAppearance.BorderSize = 0;
            iconButton2.FlatStyle = FlatStyle.Flat;
            iconButton2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton2.ForeColor = Color.White;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Book;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.IconSize = 32;
            iconButton2.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton2.Location = new Point(10, 75);
            iconButton2.Name = "iconButton2";
            iconButton2.Padding = new Padding(20, 0, 0, 0);
            iconButton2.Size = new Size(230, 45);
            iconButton2.TabIndex = 3;
            iconButton2.Text = "Books";
            iconButton2.TextAlign = ContentAlignment.MiddleLeft;
            iconButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton2.UseVisualStyleBackColor = false;
            iconButton2.Click += iconButton2_Click;
            // 
            // iconButton1
            // 
            iconButton1.BackColor = Color.FromArgb(41, 128, 185);
            iconButton1.Cursor = Cursors.Hand;
            iconButton1.FlatAppearance.BorderSize = 0;
            iconButton1.FlatStyle = FlatStyle.Flat;
            iconButton1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            iconButton1.ForeColor = Color.White;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.IconSize = 32;
            iconButton1.ImageAlign = ContentAlignment.MiddleLeft;
            iconButton1.Location = new Point(10, 20);
            iconButton1.Name = "iconButton1";
            iconButton1.Padding = new Padding(20, 0, 0, 0);
            iconButton1.Size = new Size(230, 45);
            iconButton1.TabIndex = 2;
            iconButton1.Text = "Manage Staff";
            iconButton1.TextAlign = ContentAlignment.MiddleLeft;
            iconButton1.TextImageRelation = TextImageRelation.ImageBeforeText;
            iconButton1.UseVisualStyleBackColor = true;
            iconButton1.Click += iconButton1_Click;
            // 
            // HeaderPanel
            // 
            HeaderPanel.BackColor = Color.White;
            HeaderPanel.Controls.Add(UserLabel);
            HeaderPanel.Controls.Add(Setting);
            HeaderPanel.Controls.Add(label1);
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.Name = "HeaderPanel";
            HeaderPanel.Size = new Size(1145, 80);
            HeaderPanel.TabIndex = 0;
            // 
            // UserLabel
            // 
            UserLabel.AutoSize = true;
            UserLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            UserLabel.ForeColor = Color.FromArgb(41, 128, 185);
            UserLabel.Location = new Point(911, 25);
            UserLabel.Name = "UserLabel";
            UserLabel.Size = new Size(69, 28);
            UserLabel.TabIndex = 0;
            UserLabel.Text = "Role...";
            // 
            // Setting
            // 
            Setting.BackColor = Color.Transparent;
            Setting.IconChar = FontAwesome.Sharp.IconChar.Cog;
            Setting.IconColor = Color.FromArgb(41, 128, 185);
            Setting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Setting.Location = new Point(1075, 12);
            Setting.Name = "Setting";
            Setting.Size = new Size(50, 50);
            Setting.TabIndex = 0;
            Setting.UseVisualStyleBackColor = false;
            Setting.Click += Setting_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(41, 128, 185);
            label1.Cursor = Cursors.Hand;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 25);
            label1.Name = "label1";
            label1.Size = new Size(515, 48);
            label1.TabIndex = 0;
            label1.Text = "The World Library Dashboard";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(1145, 722);
            Controls.Add(MainPanel);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            MainPanel.ResumeLayout(false);
            SlideIn.ResumeLayout(false);
            SlideIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            Card4.ResumeLayout(false);
            Card4.PerformLayout();
            Card3.ResumeLayout(false);
            Card3.PerformLayout();
            Card5.ResumeLayout(false);
            Card5.PerformLayout();
            Card1.ResumeLayout(false);
            Card1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            SideBar.ResumeLayout(false);
            HeaderPanel.ResumeLayout(false);
            HeaderPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel MainPanel;
        private Panel HeaderPanel;
        private Label label1;
        private Panel SideBar;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton3;
        private FontAwesome.Sharp.IconButton iconButton4;
        private FontAwesome.Sharp.IconButton iconButton5;
        private FontAwesome.Sharp.IconButton iconButton6;
        private FontAwesome.Sharp.IconButton iconButton7;
        private FontAwesome.Sharp.IconButton iconButton8;
        private Panel panel3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn ActionType;
        private DataGridViewTextBoxColumn Details;
        private DataGridViewTextBoxColumn Timestamp;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel Card1;
        private Label label12;
        private Label label8;
        private Label label3;
        private Panel Card5;
        private Label label16;
        private Label label11;
        private Label label6;
        private Panel Card3;
        private Label label14;
        private Label label9;
        private Label label4;
        private Panel Card4;
        private Label label15;
        private Label label10;
        private Label label5;
        private FontAwesome.Sharp.IconButton Setting;
        private Label UserLabel;
        private Panel SlideIn;
        private TextBox textBox2;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton iconButton10;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
    }
}