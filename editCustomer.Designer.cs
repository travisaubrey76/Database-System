namespace Compe561_Project1
{
    partial class editCustomer
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
            this.backButton = new System.Windows.Forms.Button();
            this.opSelectComboBox = new System.Windows.Forms.ComboBox();
            this.fn_Label = new System.Windows.Forms.Label();
            this.address_Label = new System.Windows.Forms.Label();
            this.city_label = new System.Windows.Forms.Label();
            this.phone_Label = new System.Windows.Forms.Label();
            this.email_Label = new System.Windows.Forms.Label();
            this.firstName_TextBox = new System.Windows.Forms.TextBox();
            this.address_TextBox = new System.Windows.Forms.TextBox();
            this.city_TextBox = new System.Windows.Forms.TextBox();
            this.phone_TextBox = new System.Windows.Forms.TextBox();
            this.email_TextBox = new System.Windows.Forms.TextBox();
            this.lastName_Label = new System.Windows.Forms.Label();
            this.lastName_TextBox = new System.Windows.Forms.TextBox();
            this.state_Label = new System.Windows.Forms.Label();
            this.state_TextBox = new System.Windows.Forms.TextBox();
            this.zip_Label = new System.Windows.Forms.Label();
            this.zip_TextBox = new System.Windows.Forms.TextBox();
            this.newCustomerButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addNewCustomerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(870, 25);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 25);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // opSelectComboBox
            // 
            this.opSelectComboBox.FormattingEnabled = true;
            this.opSelectComboBox.Location = new System.Drawing.Point(12, 12);
            this.opSelectComboBox.Name = "opSelectComboBox";
            this.opSelectComboBox.Size = new System.Drawing.Size(500, 21);
            this.opSelectComboBox.TabIndex = 1;
            this.opSelectComboBox.Text = "Edit an Existing User";
            this.opSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.opSelectComboBox_SelectedIndexChanged);
            // 
            // fn_Label
            // 
            this.fn_Label.AutoSize = true;
            this.fn_Label.Location = new System.Drawing.Point(10, 60);
            this.fn_Label.Name = "fn_Label";
            this.fn_Label.Size = new System.Drawing.Size(60, 13);
            this.fn_Label.TabIndex = 2;
            this.fn_Label.Text = "First Name:";
            // 
            // address_Label
            // 
            this.address_Label.AutoSize = true;
            this.address_Label.Location = new System.Drawing.Point(10, 120);
            this.address_Label.Name = "address_Label";
            this.address_Label.Size = new System.Drawing.Size(48, 13);
            this.address_Label.TabIndex = 3;
            this.address_Label.Text = "Address:";
            // 
            // city_label
            // 
            this.city_label.AutoSize = true;
            this.city_label.Location = new System.Drawing.Point(10, 180);
            this.city_label.Name = "city_label";
            this.city_label.Size = new System.Drawing.Size(27, 13);
            this.city_label.TabIndex = 4;
            this.city_label.Text = "City:";
            // 
            // phone_Label
            // 
            this.phone_Label.AutoSize = true;
            this.phone_Label.Location = new System.Drawing.Point(10, 240);
            this.phone_Label.Name = "phone_Label";
            this.phone_Label.Size = new System.Drawing.Size(41, 13);
            this.phone_Label.TabIndex = 5;
            this.phone_Label.Text = "Phone:";
            // 
            // email_Label
            // 
            this.email_Label.AutoSize = true;
            this.email_Label.Location = new System.Drawing.Point(10, 300);
            this.email_Label.Name = "email_Label";
            this.email_Label.Size = new System.Drawing.Size(35, 13);
            this.email_Label.TabIndex = 6;
            this.email_Label.Text = "Email:";
            // 
            // firstName_TextBox
            // 
            this.firstName_TextBox.Location = new System.Drawing.Point(75, 60);
            this.firstName_TextBox.Name = "firstName_TextBox";
            this.firstName_TextBox.Size = new System.Drawing.Size(350, 20);
            this.firstName_TextBox.TabIndex = 2;
            // 
            // address_TextBox
            // 
            this.address_TextBox.Location = new System.Drawing.Point(75, 120);
            this.address_TextBox.Name = "address_TextBox";
            this.address_TextBox.Size = new System.Drawing.Size(770, 20);
            this.address_TextBox.TabIndex = 4;
            // 
            // city_TextBox
            // 
            this.city_TextBox.Location = new System.Drawing.Point(75, 177);
            this.city_TextBox.Name = "city_TextBox";
            this.city_TextBox.Size = new System.Drawing.Size(300, 20);
            this.city_TextBox.TabIndex = 5;
            // 
            // phone_TextBox
            // 
            this.phone_TextBox.Location = new System.Drawing.Point(75, 240);
            this.phone_TextBox.Name = "phone_TextBox";
            this.phone_TextBox.Size = new System.Drawing.Size(300, 20);
            this.phone_TextBox.TabIndex = 8;
            // 
            // email_TextBox
            // 
            this.email_TextBox.Location = new System.Drawing.Point(75, 300);
            this.email_TextBox.Name = "email_TextBox";
            this.email_TextBox.Size = new System.Drawing.Size(500, 20);
            this.email_TextBox.TabIndex = 9;
            // 
            // lastName_Label
            // 
            this.lastName_Label.AutoSize = true;
            this.lastName_Label.Location = new System.Drawing.Point(452, 60);
            this.lastName_Label.Name = "lastName_Label";
            this.lastName_Label.Size = new System.Drawing.Size(61, 13);
            this.lastName_Label.TabIndex = 12;
            this.lastName_Label.Text = "Last Name:";
            // 
            // lastName_TextBox
            // 
            this.lastName_TextBox.Location = new System.Drawing.Point(519, 60);
            this.lastName_TextBox.Name = "lastName_TextBox";
            this.lastName_TextBox.Size = new System.Drawing.Size(325, 20);
            this.lastName_TextBox.TabIndex = 3;
            // 
            // state_Label
            // 
            this.state_Label.AutoSize = true;
            this.state_Label.Location = new System.Drawing.Point(452, 180);
            this.state_Label.Name = "state_Label";
            this.state_Label.Size = new System.Drawing.Size(35, 13);
            this.state_Label.TabIndex = 14;
            this.state_Label.Text = "State:";
            // 
            // state_TextBox
            // 
            this.state_TextBox.Location = new System.Drawing.Point(493, 177);
            this.state_TextBox.Name = "state_TextBox";
            this.state_TextBox.Size = new System.Drawing.Size(120, 20);
            this.state_TextBox.TabIndex = 6;
            // 
            // zip_Label
            // 
            this.zip_Label.AutoSize = true;
            this.zip_Label.Location = new System.Drawing.Point(693, 180);
            this.zip_Label.Name = "zip_Label";
            this.zip_Label.Size = new System.Drawing.Size(25, 13);
            this.zip_Label.TabIndex = 16;
            this.zip_Label.Text = "Zip:";
            // 
            // zip_TextBox
            // 
            this.zip_TextBox.Location = new System.Drawing.Point(724, 177);
            this.zip_TextBox.Name = "zip_TextBox";
            this.zip_TextBox.Size = new System.Drawing.Size(120, 20);
            this.zip_TextBox.TabIndex = 7;
            // 
            // newCustomerButton
            // 
            this.newCustomerButton.Location = new System.Drawing.Point(870, 75);
            this.newCustomerButton.Name = "newCustomerButton";
            this.newCustomerButton.Size = new System.Drawing.Size(100, 25);
            this.newCustomerButton.TabIndex = 11;
            this.newCustomerButton.Text = "New Customer";
            this.newCustomerButton.UseVisualStyleBackColor = true;
            this.newCustomerButton.Click += new System.EventHandler(this.newCustomerButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(870, 125);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 25);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(870, 175);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 25);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addNewCustomerButton
            // 
            this.addNewCustomerButton.Location = new System.Drawing.Point(750, 300);
            this.addNewCustomerButton.Name = "addNewCustomerButton";
            this.addNewCustomerButton.Size = new System.Drawing.Size(150, 25);
            this.addNewCustomerButton.TabIndex = 14;
            this.addNewCustomerButton.Text = "Add New Customer";
            this.addNewCustomerButton.UseVisualStyleBackColor = true;
            this.addNewCustomerButton.Click += new System.EventHandler(this.addNewCustomerButton_Click);
            // 
            // editCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 361);
            this.Controls.Add(this.addNewCustomerButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.newCustomerButton);
            this.Controls.Add(this.zip_TextBox);
            this.Controls.Add(this.zip_Label);
            this.Controls.Add(this.state_TextBox);
            this.Controls.Add(this.state_Label);
            this.Controls.Add(this.lastName_TextBox);
            this.Controls.Add(this.lastName_Label);
            this.Controls.Add(this.email_TextBox);
            this.Controls.Add(this.phone_TextBox);
            this.Controls.Add(this.city_TextBox);
            this.Controls.Add(this.address_TextBox);
            this.Controls.Add(this.firstName_TextBox);
            this.Controls.Add(this.email_Label);
            this.Controls.Add(this.phone_Label);
            this.Controls.Add(this.city_label);
            this.Controls.Add(this.address_Label);
            this.Controls.Add(this.fn_Label);
            this.Controls.Add(this.opSelectComboBox);
            this.Controls.Add(this.backButton);
            this.Name = "editCustomer";
            this.Text = "frmCustomer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ComboBox opSelectComboBox;
        private System.Windows.Forms.Label fn_Label;
        private System.Windows.Forms.Label address_Label;
        private System.Windows.Forms.Label city_label;
        private System.Windows.Forms.Label phone_Label;
        private System.Windows.Forms.Label email_Label;
        private System.Windows.Forms.TextBox firstName_TextBox;
        private System.Windows.Forms.TextBox address_TextBox;
        private System.Windows.Forms.TextBox city_TextBox;
        private System.Windows.Forms.TextBox phone_TextBox;
        private System.Windows.Forms.TextBox email_TextBox;
        private System.Windows.Forms.Label lastName_Label;
        private System.Windows.Forms.TextBox lastName_TextBox;
        private System.Windows.Forms.Label state_Label;
        private System.Windows.Forms.TextBox state_TextBox;
        private System.Windows.Forms.Label zip_Label;
        private System.Windows.Forms.TextBox zip_TextBox;
        private System.Windows.Forms.Button newCustomerButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addNewCustomerButton;
    }
}