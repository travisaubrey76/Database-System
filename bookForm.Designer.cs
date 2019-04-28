namespace Compe561_Project1
{
    partial class bookForm
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
            this.bookComboBox = new System.Windows.Forms.ComboBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.isbnLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.newBookButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.isbnTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bookComboBox
            // 
            this.bookComboBox.FormattingEnabled = true;
            this.bookComboBox.Location = new System.Drawing.Point(50, 40);
            this.bookComboBox.Name = "bookComboBox";
            this.bookComboBox.Size = new System.Drawing.Size(400, 21);
            this.bookComboBox.TabIndex = 0;
            this.bookComboBox.Text = "Edit an Existing Book";
            this.bookComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(25, 100);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 10;
            this.titleLabel.Text = "Title:";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(25, 130);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(41, 13);
            this.authorLabel.TabIndex = 11;
            this.authorLabel.Text = "Author:";
            // 
            // isbnLabel
            // 
            this.isbnLabel.AutoSize = true;
            this.isbnLabel.Location = new System.Drawing.Point(25, 160);
            this.isbnLabel.Name = "isbnLabel";
            this.isbnLabel.Size = new System.Drawing.Size(35, 13);
            this.isbnLabel.TabIndex = 12;
            this.isbnLabel.Text = "ISBN:";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(25, 190);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(34, 13);
            this.priceLabel.TabIndex = 13;
            this.priceLabel.Text = "Price:";
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(697, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // newBookButton
            // 
            this.newBookButton.Location = new System.Drawing.Point(697, 41);
            this.newBookButton.Name = "newBookButton";
            this.newBookButton.Size = new System.Drawing.Size(75, 23);
            this.newBookButton.TabIndex = 2;
            this.newBookButton.Text = "New Book";
            this.newBookButton.UseVisualStyleBackColor = true;
            this.newBookButton.Click += new System.EventHandler(this.newBookButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(697, 70);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(697, 99);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(72, 97);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(550, 20);
            this.titleTextBox.TabIndex = 5;
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(72, 127);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(400, 20);
            this.authorTextBox.TabIndex = 6;
            // 
            // isbnTextBox
            // 
            this.isbnTextBox.Location = new System.Drawing.Point(72, 157);
            this.isbnTextBox.Name = "isbnTextBox";
            this.isbnTextBox.Size = new System.Drawing.Size(400, 20);
            this.isbnTextBox.TabIndex = 7;
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(72, 187);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(200, 20);
            this.priceTextBox.TabIndex = 8;
            // 
            // bookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 257);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.isbnTextBox);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.newBookButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.isbnLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.bookComboBox);
            this.Name = "bookForm";
            this.Text = "bookForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox bookComboBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label isbnLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button newBookButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox authorTextBox;
        private System.Windows.Forms.TextBox isbnTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
    }
}