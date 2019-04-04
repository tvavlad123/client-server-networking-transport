namespace Transport.Controller
{
    partial class TransportWindow
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
            this.ridesView = new System.Windows.Forms.DataGridView();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seats = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Rides = new System.Windows.Forms.TabPage();
            this.Bookings = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.searchByHour = new System.Windows.Forms.TextBox();
            this.searchByDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchByDestination = new System.Windows.Forms.TextBox();
            this.bookingsView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ridesView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Rides.SuspendLayout();
            this.Bookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bookingsView)).BeginInit();
            this.SuspendLayout();
            // 
            // ridesView
            // 
            this.ridesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ridesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Destination,
            this.Date,
            this.Hour,
            this.Seats});
            this.ridesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ridesView.Location = new System.Drawing.Point(3, 3);
            this.ridesView.Name = "ridesView";
            this.ridesView.RowTemplate.Height = 33;
            this.ridesView.Size = new System.Drawing.Size(1228, 583);
            this.ridesView.TabIndex = 0;
            // 
            // Destination
            // 
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "Destination";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Hour
            // 
            this.Hour.HeaderText = "Hour";
            this.Hour.Name = "Hour";
            // 
            // Seats
            // 
            this.Seats.HeaderText = "Seats";
            this.Seats.Name = "Seats";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Rides);
            this.tabControl1.Controls.Add(this.Bookings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1250, 636);
            this.tabControl1.TabIndex = 1;
            // 
            // Rides
            // 
            this.Rides.Controls.Add(this.ridesView);
            this.Rides.Location = new System.Drawing.Point(8, 39);
            this.Rides.Name = "Rides";
            this.Rides.Padding = new System.Windows.Forms.Padding(3);
            this.Rides.Size = new System.Drawing.Size(1234, 589);
            this.Rides.TabIndex = 0;
            this.Rides.Text = "Rides";
            this.Rides.UseVisualStyleBackColor = true;
            // 
            // Bookings
            // 
            this.Bookings.Controls.Add(this.button3);
            this.Bookings.Controls.Add(this.button2);
            this.Bookings.Controls.Add(this.button1);
            this.Bookings.Controls.Add(this.label3);
            this.Bookings.Controls.Add(this.searchByHour);
            this.Bookings.Controls.Add(this.searchByDate);
            this.Bookings.Controls.Add(this.label2);
            this.Bookings.Controls.Add(this.label1);
            this.Bookings.Controls.Add(this.searchByDestination);
            this.Bookings.Controls.Add(this.bookingsView);
            this.Bookings.Location = new System.Drawing.Point(8, 39);
            this.Bookings.Name = "Bookings";
            this.Bookings.Padding = new System.Windows.Forms.Padding(3);
            this.Bookings.Size = new System.Drawing.Size(1234, 589);
            this.Bookings.TabIndex = 1;
            this.Bookings.Text = "Search & Bookings";
            this.Bookings.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LimeGreen;
            this.button3.Location = new System.Drawing.Point(1108, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 56);
            this.button3.TabIndex = 9;
            this.button3.Text = "Book ride";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.button2.Location = new System.Drawing.Point(1108, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 56);
            this.button2.TabIndex = 8;
            this.button2.Text = "Sign out";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 56);
            this.button1.TabIndex = 7;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(631, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Hour (HH-mm):";
            // 
            // searchByHour
            // 
            this.searchByHour.Location = new System.Drawing.Point(636, 31);
            this.searchByHour.Name = "searchByHour";
            this.searchByHour.Size = new System.Drawing.Size(191, 31);
            this.searchByHour.TabIndex = 5;
            // 
            // searchByDate
            // 
            this.searchByDate.Location = new System.Drawing.Point(324, 33);
            this.searchByDate.Name = "searchByDate";
            this.searchByDate.Size = new System.Drawing.Size(191, 31);
            this.searchByDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Date (yyyy-MM-dd):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destination:";
            // 
            // searchByDestination
            // 
            this.searchByDestination.Location = new System.Drawing.Point(11, 31);
            this.searchByDestination.Name = "searchByDestination";
            this.searchByDestination.Size = new System.Drawing.Size(191, 31);
            this.searchByDestination.TabIndex = 1;
            // 
            // bookingsView
            // 
            this.bookingsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bookingsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.SeatNo});
            this.bookingsView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bookingsView.Location = new System.Drawing.Point(3, 184);
            this.bookingsView.Name = "bookingsView";
            this.bookingsView.RowTemplate.Height = 33;
            this.bookingsView.Size = new System.Drawing.Size(1228, 402);
            this.bookingsView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ClientName";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // SeatNo
            // 
            this.SeatNo.HeaderText = "SeatNo";
            this.SeatNo.Name = "SeatNo";
            this.SeatNo.Width = 200;
            // 
            // TransportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 636);
            this.Controls.Add(this.tabControl1);
            this.Name = "TransportWindow";
            this.Text = "TransportWindow";
            ((System.ComponentModel.ISupportInitialize)(this.ridesView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Rides.ResumeLayout(false);
            this.Bookings.ResumeLayout(false);
            this.Bookings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bookingsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ridesView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Bookings;
        private System.Windows.Forms.DataGridView bookingsView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hour;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seats;
        private System.Windows.Forms.TabPage Rides;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchByDestination;
        private System.Windows.Forms.TextBox searchByDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchByHour;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeatNo;
    }
}