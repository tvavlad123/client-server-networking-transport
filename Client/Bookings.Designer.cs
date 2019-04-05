namespace Transport.Controller
{
    partial class Bookings
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
            this.firstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.noPlaces = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lastName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.noPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(112, 182);
            this.firstName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(208, 26);
            this.firstName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "First name:";
            // 
            // noPlaces
            // 
            this.noPlaces.Location = new System.Drawing.Point(112, 223);
            this.noPlaces.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.noPlaces.Name = "noPlaces";
            this.noPlaces.Size = new System.Drawing.Size(207, 26);
            this.noPlaces.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "No of places:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 273);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Book";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(109, 7);
            this.label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 20);
            this.label.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Last name:";
            // 
            // lastName
            // 
            this.lastName.Location = new System.Drawing.Point(112, 146);
            this.lastName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(208, 26);
            this.lastName.TabIndex = 8;
            // 
            // Bookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.lastName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noPlaces);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.firstName);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Bookings";
            this.Text = "Booking";
            ((System.ComponentModel.ISupportInitialize)(this.noPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown noPlaces;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lastName;
    }
}