using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compe561_Project1
{
    public partial class initialForm : Form
    {
        //As seen in the SMCOM270 help documentation for Projects 4/5
        public static initialForm form1Var = null;

        public initialForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// From main menu, upon clicking this button takes us to our familiar form from project 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manageCustomersButton_Click(object sender, EventArgs e)
        {
            form1Var = this;
            this.Hide();
            editCustomer editCustomerForm = new editCustomer();
            editCustomerForm.InstanceRef = this;
            editCustomerForm.Show();
        }

        /// <summary>
        /// Takes us to our book managing page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manageBooksButton_Click(object sender, EventArgs e)
        {
            form1Var = this;
            this.Hide();
            bookForm bookFormForm = new bookForm();
            bookFormForm.InstanceRef = this;
            bookFormForm.Show();
        }

        /// <summary>
        /// Pressing this button takes us to our usual form from project 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placeOrderButton_Click(object sender, EventArgs e)
        {
            form1Var = this;
            this.Hide();
            Main_Menu placeOrderForm = new Main_Menu();
            placeOrderForm.InstanceRef = this;
            placeOrderForm.Show();
        }
    }
}
