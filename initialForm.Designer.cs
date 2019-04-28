namespace Compe561_Project1
{
    partial class initialForm
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
            this.manageCustomersButton = new System.Windows.Forms.Button();
            this.manageBooksButton = new System.Windows.Forms.Button();
            this.placeOrderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // manageCustomersButton
            // 
            this.manageCustomersButton.Location = new System.Drawing.Point(67, 56);
            this.manageCustomersButton.Name = "manageCustomersButton";
            this.manageCustomersButton.Size = new System.Drawing.Size(150, 25);
            this.manageCustomersButton.TabIndex = 0;
            this.manageCustomersButton.Text = "Manage Customers";
            this.manageCustomersButton.UseVisualStyleBackColor = true;
            this.manageCustomersButton.Click += new System.EventHandler(this.manageCustomersButton_Click);
            // 
            // manageBooksButton
            // 
            this.manageBooksButton.Location = new System.Drawing.Point(67, 118);
            this.manageBooksButton.Name = "manageBooksButton";
            this.manageBooksButton.Size = new System.Drawing.Size(150, 25);
            this.manageBooksButton.TabIndex = 1;
            this.manageBooksButton.Text = "Manage Books";
            this.manageBooksButton.UseVisualStyleBackColor = true;
            this.manageBooksButton.Click += new System.EventHandler(this.manageBooksButton_Click);
            // 
            // placeOrderButton
            // 
            this.placeOrderButton.Location = new System.Drawing.Point(67, 174);
            this.placeOrderButton.Name = "placeOrderButton";
            this.placeOrderButton.Size = new System.Drawing.Size(150, 25);
            this.placeOrderButton.TabIndex = 2;
            this.placeOrderButton.Text = "Place Order";
            this.placeOrderButton.UseVisualStyleBackColor = true;
            this.placeOrderButton.Click += new System.EventHandler(this.placeOrderButton_Click);
            // 
            // initialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.placeOrderButton);
            this.Controls.Add(this.manageBooksButton);
            this.Controls.Add(this.manageCustomersButton);
            this.Name = "initialForm";
            this.Text = "initialForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button manageCustomersButton;
        private System.Windows.Forms.Button manageBooksButton;
        private System.Windows.Forms.Button placeOrderButton;
    }
}