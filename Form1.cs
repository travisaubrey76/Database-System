using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Author: Travis Aubrey
/// Red id: 814041534
/// Date Completed: 2/11/2019
/// Project 2 Date Completed: 2/18/2019
/// Project 4 Date Completed: 3/27/2019
/// Project 5 Date Completed: 4/11/2019 --Extra Credit Completed-- Writes to orders.txt file.
/// 
/// Summary: Basic intro program to summarize my knowledge on C# as learned through SDSU's CompE 361 class. Most methods known, but a lot of shortcuts
///          were found via research! Everything borrowed, manipulated and turned mine have been documented. Please let me know if this type of documentation
///          is sufficient or if more is needed!
///          
///         Project 2 utilizes the same as the above, but we are grabbing the book information from an existing file and then upon confirming the order, writing
///         the order to a new file deemed "orders.txt"
///         
///         Project 4 utilizes the same initial framework as the above, but now we are adding a customer database form. Using a comboBox, we are traversing a .txt file
///         that serves as our current customer database. Complete with their information, we can add a new customer or edit a current customer. additional functionality can
///         be added as this entire program has been programmed modularly.
///         
/// Summary Update (Project 4): Adding a new form to control [edit/create] new/existing users for our bookstore application. For individual method definitions see the 
///                             associated program file.
///                             
/// Summary Update (Project 5): Had to give everything a makeover to allow database access and scalability. Some idealogies from previous methods stuck around, but ultimately - I needed
///                             to change the framework of my overall code. Easy code readability was my focus since this project spanned a couple thousand lines of code and I
///                             understand and have experienced how difficult it can be to keep track of all my variables and functions. With this in mind, I utilzied similar method naming
///                             conventions allthroughout my code.
///                             
///                             I originally thought that I would have to use Microsoft SQL server database to be able to access my newly created database, but upon further research I was
///                             able to utilize our MySQL database through XAMPP by looking through the web and applying what I found to my current case (Of course, I cited my inspiration 
///                             where I used it).
/// 
/// Changelog: Changed the way I created objects. Changed where my Books class is located from seperate file to same file as Main_Menu (just makes
///            it a tad easier for a small program like this so I don't get lost amongst many class files when there aren't many necessary)
///            
///            (3/26/2019): Adding a new form to control customers information.
///            (4/8/2019) : Adding a new form to control Book
///            (4/9/2019) : Adding a new main menu form.
///            
/// PROJECT 2 CHANGELOG: Since this same framework is being used to update the reading and writing portion. I will just amend my current code to accomodate the new
///                      requirements. Therefore, following changes will apear in this section as necessary.
///                      
/// PROJECT 4 CHANGELOG: customers.txt has to be populated with at least 1 customer to start. (Further testing is needed to accomodate an empty customer base)
///                      -The above is not the case now that i've added an independent add new user function
///                      
/// Project 5 CHANGELOG: Changed essentially everything to reflect database connectivity.
/// 
/// Ideas:
///         -Thinking about utilizing additional techniques, if I can remember them.
///         -Can decrease variable memory size if needed for further optomizations. (Not needed)
///         -Can definitely add more data entry checking in second form. If time is acquired and the process required, it can be done.
///         
/// Credits (if any): Seen above methods in their comment sections.
///     
///         I found something that was a bit of a pain... by default a Windows Form application does not allow you to see the console.. so a Console.WriteLine("anything"); 
///         could not be seen for debugging... I was able to find a workaround to show me what I wanted to see and I found out how to enable a different output here:
///         https://stackoverflow.com/questions/4362111/how-do-i-show-a-console-output-window-in-a-forms-application
///         This allowed me to have both my forms application load and see a debugging window via Console. This will be standard when running the program to grade it - I don't think
///         its that terrible as it also shows input and if any errors occur it will be easier to detect as well.
/// 
/// 
/// Comments/Concerns: Email me at travisaubrey76@gmail.com
/// 
/// </summary>



namespace Compe561_Project1
{
    public partial class Main_Menu : Form
    {
        //As seen in the SMCOM270 help documentation for Projects 4/5
        public static Main_Menu form1Var = null;

        private Form instanceRef = null;

        /// <summary>
        /// Taken from provided tutorial documentation
        /// </summary>
        public Form InstanceRef
        {
            get
            {
                return instanceRef;
            }
            set
            {
                instanceRef = value;
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/21618015/how-to-connect-to-mysql-database
        /// </summary>
        public class DBConnection
        {
            private DBConnection()
            {
            }

            private string databaseName = string.Empty;
            public string DatabaseName
            {
                get { return databaseName; }
                set { databaseName = value; }
            }

            public string Password { get; set; }
            private MySqlConnection connection = null;
            public MySqlConnection Connection
            {
                get { return connection; }
            }

            private static DBConnection _instance = null;
            public static DBConnection Instance()
            {
                if (_instance == null)
                    _instance = new DBConnection();
                return _instance;
            }

            /// <summary>
            /// Information required to connect to mysql db is found from the my.ini file associated in the XAMPP configuration
            /// Assistance for the issue of DB connection error when returning to the Book Form after the intitial call was found here:
            /// https://stackoverflow.com/questions/43389412/c-sharp-connection-must-be-valid-and-open-when-fetching-multiple-tables
            /// </summary>
            /// <returns></returns>
            public bool IsConnect()
            {
                bool result = false;

                if (Connection == null)
                {
                    if (String.IsNullOrEmpty(databaseName))
                        return false;
                    string connstring = string.Format("Server=localhost; database={0}; UID=root;", databaseName);
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                }

                if (Connection != null)
                {
                    //Since we are able to return to this book form, we have to make sure that our connectoin maintains useabiltiy
                    if (Connection.State == ConnectionState.Open)
                    {
                        result = true;
                    }
                    else //If connection is not already opened, open it.
                    {
                        connection.Open();
                        result = true;
                    }

                }

                return result;
            }

            public void Close()
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Required books class
        /// </summary>
        private class Books
        {
            public string Author;
            public string ISBN;
            public string Price;
            public string Title;

            public Books(string author, string iSBN, string price, string title)
            {
                Author = author;
                ISBN = iSBN;
                Price = price;
                Title = title;
            }
            
            //Defailt constructor needed when not implicitly creating books as per part 1 of this project
            public Books()
            {

            }
        }

        /// <summary>
        /// Customer Class to help facilitate all the operations I need to do for each customer object.
        /// </summary>
        private class Customer
        {
            public string ID;
            public string FirstName;
            public string LastName;
            public string Address;
            public string City;
            public string State;
            public string Zip;
            public string PhoneNumber;
            public string Email;

            public Customer(string id, string firstName, string lastName, string address, string city, string state, string zip, string phoneNumber, string email)
            {
                ID = id;
                FirstName = firstName;
                LastName = lastName;
                Address = address;
                City = city;
                State = state;
                Zip = zip;
                PhoneNumber = phoneNumber;
                Email = email;
            }

            //Defailt constructor needed when not implicitly creating books as per part 1 of this project
            public Customer()
            {

            }
        }


        //Global variables for total end price.
        double subTotal = 0;
        double totalPrice = 0;
        double tax = 0;

        //This is where I needed systems.Collection
        ArrayList myArrayList = new ArrayList();
        ArrayList custArrayList = new ArrayList();
        //String recordIn;
        //String[] fields;

        //List<String> encodingStringArray = new List<string>();


        //CONSTANT STRINGS defined for what is in my book.txt file
        const String fileTitle1 = "testTitle1";
        const String fileTitle2 = "testTitle2";

        //Variables to grab current customer selection
        //Using the below to check for existing data firstly
        string currentCustomerID = "";
        string currentCustomerFirstName = "";
        string currentCustomerLastName = "";
        string currentCustomerAddress = "";
        string currentCustomerCity = "";
        string currentCustomerState = "";
        string currentCustomerZip = "";
        string currentCustomerPhoneNumber = "";
        string currentCustomerEmail = "";


        /// <summary> REVIEW
        /// This is where I would need to call my loops to read the book information from the text file so that it loads as the form is loaded.
        /// </summary>
        public Main_Menu()
        {
            InitializeComponent();

            updateCustomers(); //Populate the Customer Combo Box Upon Form startup

            DataGridSetup();
            quantityUserInputTextBox.Text = "1";

            //Preset the subtotal, tax, and total boxes
            subtotalTextBox.Text = subTotal.ToString("C");
            taxTextBox.Text = tax.ToString("C");
            totalTextBox.Text = totalPrice.ToString("C");


            //Testing FileStream Write capabilities
            //FileStream test = new FileStream("testtext.txt", FileMode.Create, FileAccess.Write);
            //StreamWriter testWriter = new StreamWriter(test);
            //string text = "test";
            //testWriter.WriteLine(text);
            //testWriter.Close();
            //test.Close();

            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "bookstore";


            if (dbCon.IsConnect())
            {

                string query = "SELECT * FROM books";
                var cmd = new MySqlCommand(query, dbCon.Connection);



                var reader = cmd.ExecuteReader();
                //Go through the query to grab the books and insert them into our own book objects
                while (reader.Read())
                {
                    string bookTitle = reader.GetString(0);
                    string bookAuthor = reader.GetString(1);
                    string bookISBN = reader.GetString(2);
                    string bookPrice = reader.GetString(3);
                    //Only used for visual representation of successful database connection
                    Console.WriteLine($"{bookTitle} {bookAuthor} {bookISBN} {bookPrice}\n");

                    //Procedurally add our retrieved book information
                    Books bk = new Books();
                    bk.Title = bookTitle;
                    bk.Author = bookAuthor;
                    bk.ISBN = bookISBN;
                    bk.Price = bookPrice;

                    myArrayList.Add(bk);

                }

                //Simple check to see if we successfully retreived our book information prior to sending it to our form
                if (myArrayList.Count > 0)
                {
                    foreach (Books bk_data in myArrayList)
                    {
                        bookSelectComboBox.Items.Add(bk_data.Title);
                    }

                }
                else
                {
                    Console.WriteLine($"Could not retrieve book information from database.");
                }


                dbCon.Close();
            }
            else
            {
                Console.WriteLine("Database Connection Error.");
            }

            customerSelectComboBox.Focus();
        }

        
        /// <summary> REVIEW
        /// Adds the selected book into the DataGridView
        /// 
        /// Also added some data type validation to check for empty or bad user input selections
        /// 
        /// Attempted a try catch block, but couldn't get a simple isNullorWhitespace() check to work correctly in the try section
        /// 
        /// Calling update totals function in most places ensuring constant updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTitleButton_Click(object sender, EventArgs e)
        {
            int qty;

            if (String.IsNullOrWhiteSpace(quantityUserInputTextBox.Text))
            {
                MessageBox.Show($"Please enter a valid number greater than 0! Currently quantity is empty.");
                quantityUserInputTextBox.Text = "1";
                quantityUserInputTextBox.Focus();
            }
            else if (customerSelectComboBox.Text.Equals("Select a Customer"))
            {
                MessageBox.Show("Please select a customer from the combo box.");
                customerSelectComboBox.Focus();
            }
            else if (bookSelectComboBox.Text.Equals("Select a Title"))
            {
                MessageBox.Show("Please select a title from the combo box");
                bookSelectComboBox.Focus();
            }
            else if ( (qty = int.Parse(quantityUserInputTextBox.Text)) != 0 )
            {
                foreach (Books book in myArrayList)
                {
                    if (bookSelectComboBox.Text.Equals(book.Title)) //Check each book against all books and if a match occurs fill the datagridview
                    {
                        dataGridView1.Rows.Add(book.Title, "$"+book.Price, qty.ToString(), (qty * double.Parse(book.Price)));//(qty * int.Parse(bookPriceTextBox.Text)).ToString("C"));

                        quantityUserInputTextBox.Text = "1"; //resetting qty number back to one after a title is added to our cart
                        
                        bookSelectComboBox.Focus();
                        updateTotals();
                    }
                    //else //Keep entries blank
                    //{
                    //    author_TextBox.Text = "";
                    //    bookPriceTextBox.Text = "";
                    //    isbn_TextBox.Text = "";
                    //    quantityUserInputTextBox.Text = "1";
                    //    MessageBox.Show($"Please select a valid book - currently, a book is not selected!");
                    //    bookSelectComboBox.Focus(); //Per part 2 instructions
                    //    updateTotals();
                    //}
                }
            }
            else //entered quanitity is 0 thus don't add anything to data grid view
            {
                MessageBox.Show($"The Quantity Box is a zero value, please enter a number greater than 0.");
                quantityUserInputTextBox.Focus();

            }
        }

        /// <summary>
        /// Function called to update totals as books are added
        /// 
        /// In addition, I was able to utilize the int.parse method with this:
        /// https://stackoverflow.com/questions/4094334/how-to-format-a-currency-string-to-integer
        /// </summary>
        private void updateTotals()
        {
            subTotal = 0;

            // Since the headers count as a row... if there is 1 row it means the grid is empty
            if(dataGridView1.Rows.Count <= 1) //Check if my data grid is empty
            {
                //Do nothing essentially
                //MessageBox.Show($"EMPTY"); <-- This was for testing in Lab1
            }
            else //If not empty
            {
                for (int row = 0; row < dataGridView1.Rows.Count - 1; row++)
                {
                    //subTotal += int.Parse(dataGridView1.Rows[row].Cells[3].Value.ToString());
                    subTotal += double.Parse(dataGridView1.Rows[row].Cells[3].Value.ToString());
                }

                tax = (subTotal * .1); //Was told to use 10% tax
                totalPrice = subTotal + tax;
            }

            subtotalTextBox.Text = subTotal.ToString("C");
            taxTextBox.Text = tax.ToString("C");
            totalTextBox.Text = totalPrice.ToString("C");

        }

        /// <summary> REVIEW
        /// Called when user selects something from the title select combo box. useful since the form loads empty. If you need to change the amount
        /// of needed books then it can be easily entered by copying and pasting the final statement and incrementing the index marker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bookSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            foreach (Books book in myArrayList)
            {
                if (bookSelectComboBox.Text.Equals(book.Title)) //Check each book against all books and if a match occurs fill the text boxes
                {
                    author_TextBox.Text = book.Author;
                    isbn_TextBox.Text = book.ISBN;
                    bookPriceTextBox.Text = book.Price;
                }
            }
        }


        /// <summary>
        /// This is where I would be outputting my confirmed order to order.txt
        /// In addition, for the second lab - I did some research outside of the available documents for a more efficinet way to handle
        /// IO exceptions as per the requirements. I stumbled upon using a specific keyword within a try/catch block that allows us to
        /// open/create the order.txt file and then test for an io exception thus eliminating a "racing" condition. Please see the
        /// following link for more information and where the inspiration came from:
        /// https://stackoverflow.com/questions/86766/how-to-properly-handle-exceptions-when-performing-file-io
        /// 
        /// In addition, from the MSDN docs, I also found out that the using statement automatically flushes the streamwriter in a
        /// more efficient manner than what is shown on the slides:
        /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
        /// 
        /// Wanted to find a clever way to add a timestamp to a persistent orders page to differentiate when different orders took place.
        /// Found that in the following MSDN documentation:
        /// https://docs.microsoft.com/en-us/dotnet/api/system.datetime.toshorttimestring?redirectedfrom=MSDN&view=netframework-4.7.2#System_DateTime_ToShortTimeString
        /// 
        /// String formatting to help make the writing to file be consistent and pretty :)
        /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmOrderButton_Click(object sender, EventArgs e)
        {

            //string outputString = "";

            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show($"Purchase Order is empty, please add some books!");
            }
            else
            {
                MessageBox.Show($"Purchase Order is complete!");


                //This is where we send the information to an output file
                try
                {
                    //This will create my file and flush the StreamWriter for me! Very useful! C:\Users\Travis\Desktop\TravisAubrey_Lab4\TravisAubrey_Lab1\Compe561_Project1\bin\Debug
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Travis\Desktop\TravisAubrey_Lab4\TravisAubrey_Lab1\Compe561_Project1\bin\Debug\orders.txt", true))
                    {
                        file.WriteLine($"------------------------  Order Created: {DateTime.Now.ToString("HH:mm:ss tt")} -------------- Order By: {currentCustomerFirstName} {currentCustomerLastName}"); //<--- This will be my order dividing header

                        
                            for (int row = 0; row < dataGridView1.Rows.Count - 1; row++) //Below adds information to file.
                            {
                                

                                file.WriteLine($"Book: {dataGridView1.Rows[row].Cells[0].Value.ToString(), -60} Price:{dataGridView1.Rows[row].Cells[1].Value.ToString(), -10}" +
                                    $"{"QTY:", -4} {dataGridView1.Rows[row].Cells[2].Value.ToString(), -3} {"Total:", -6} ${dataGridView1.Rows[row].Cells[3].Value.ToString(), -10}");
                            }
                            file.WriteLine("--------------------> Subtotal:{0}   Tax:{1}   Total:{2} <--------------------", subtotalTextBox.Text.ToString(), taxTextBox.Text.ToString(), totalTextBox.Text.ToString());
                        
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("An IO exception has occured.");
                }
                catch (Exception) //This will catch any generic error and throw a message to the user.
                {
                    MessageBox.Show("A Generic Error has occured.");
                }

                //Now to insert this confirmed and completed order to our database
                var dbCon = DBConnection.Instance();
                dbCon.DatabaseName = "bookstore";

                if (dbCon.IsConnect())
                {
                        //Customer with the same first names will either update the first or all customers with that name.
                        string query = "INSERT INTO orders (customer_id, sub_total, tax, total, order_date) VALUES(@CustID, @SubTotal, @Tax, @Total, @OrderDate)";
                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@CustID", currentCustomerID);
                        cmd.Parameters.AddWithValue("@SubTotal", subtotalTextBox.Text.ToString());
                        cmd.Parameters.AddWithValue("@Tax", taxTextBox.Text.ToString());
                        cmd.Parameters.AddWithValue("@Total", totalTextBox.Text.ToString());
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();

                    dbCon.Close();
                }
                else
                {
                    Console.WriteLine("Database Connection Error.");
                }



                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                subTotal = 0;
                tax = 0;
                totalPrice = 0;

                subtotalTextBox.Text = subTotal.ToString("C");
                taxTextBox.Text = tax.ToString("C");
                totalTextBox.Text = totalPrice.ToString("C");
            }
        }


        /// <summary>
        /// Idea on how to clear the entire datagrid since everything is manually entered found here:
        /// https://stackoverflow.com/questions/3744882/datagridview-clear
        /// 
        /// Also resets the totals section to 0.
        /// 
        /// I looked through the textbook trying to find an easy way to not have to create a new form to ask the "are you sure" question...
        /// BUT, i was able to find some information regarding the MessageBox method and then did some digging and found the following
        /// information:
        /// https://stackoverflow.com/questions/5414270/messagebox-buttons
        /// Really came in clutch!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelOrderButton_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("Are you sure you want to cancel this order?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                subTotal = 0;
                tax = 0;
                totalPrice = 0;

                subtotalTextBox.Text = subTotal.ToString("C");
                taxTextBox.Text = tax.ToString("C");
                totalTextBox.Text = totalPrice.ToString("C");
            }
            else
            {
                //Then the user clicked no... and therefore nothing changes
            }
            
        }

        /// <summary>
        /// Information and how-to learned from:
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.datagridview?view=netframework-4.7.2
        /// </summary>
        private void DataGridSetup()
        {
            //this.Controls.Add(dataGridView1);
            dataGridView1.ColumnCount = 4;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[0].Name = "Title";
            dataGridView1.Columns[1].Name = "Price";
            dataGridView1.Columns[2].Name = "QTY";
            dataGridView1.Columns[3].Name = "Line Total";

            //Try and set specific columns to be a currency representative column
            dataGridView1.Columns[1].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "c";

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            //dataGridView1.Dock = DockStyle.Fill;

        }

        /// <summary>
        /// Goes back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placeOrderBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
            InstanceRef.Show();
        }

        /// <summary>
        /// When a customer is selected - we will pull all their information from the database enabling us to accurately attach an order to a specific customer. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customerSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Customer customer in custArrayList)
            {
                if (customerSelectComboBox.Text.Equals(customer.FirstName + " " + customer.LastName)) //Check each customer against every customer until a match occurs
                {
                //Once we have our selected customer - save their information for reporting purposes.
                    currentCustomerID = customer.ID;
                    currentCustomerFirstName = customer.FirstName;
                    currentCustomerLastName = customer.LastName;
                    currentCustomerAddress = customer.Address;
                    currentCustomerCity = customer.City;
                    currentCustomerState = customer.State;
                    currentCustomerZip = customer.Zip;
                    currentCustomerPhoneNumber = customer.PhoneNumber;
                    currentCustomerEmail = customer.Email;

                    //Console.WriteLine($"{currentCustomerFirstName} {currentCustomerLastName} Selected.");
                }

            }
        }

        /// <summary>
        /// General IO method to update customers based on what's in the associated Database. Will be my general function call to update comboBox selections
        /// </summary>
        private void updateCustomers()
        {
            //Reset myArraylist
            custArrayList.Clear();


            var dbCon = DBConnection.Instance();
            dbCon.DatabaseName = "bookstore";


            if (dbCon.IsConnect())
            {

                string query = "SELECT * FROM customer";
                var cmd = new MySqlCommand(query, dbCon.Connection);



                var reader = cmd.ExecuteReader();
                //Go through the query to grab the books and insert them into our own book objects
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string address = reader.GetString(3);
                    string city = reader.GetString(4);
                    string state = reader.GetString(5);
                    string zip = reader.GetString(6); 
                    string phone = reader.GetString(7);
                    string email = reader.GetString(8);

                    //Procedurally add our retrieved customer information
                    Customer cust = new Customer();
                    cust.ID = id;
                    cust.FirstName = firstName;
                    cust.LastName = lastName;
                    cust.Address = address;
                    cust.City = city;
                    cust.State = state;
                    cust.Zip = zip;
                    cust.PhoneNumber = phone;
                    cust.Email = email;


                    custArrayList.Add(cust);

                }

                //Simple check to see if we successfully retreived our book information prior to sending it to our form

                if (custArrayList.Count > 0)
                {
                    //Everytime this function is called, it is meant to refresh the current contents iteratively, so we begin that by flushing the current contents
                    customerSelectComboBox.Items.Clear();

                    //This portion would be where I send my books into the form application combobox
                    foreach (Customer customer in custArrayList)
                    {
                        customerSelectComboBox.Items.Add(customer.FirstName + " " + customer.LastName);
                    }

                }
                else
                {
                    Console.WriteLine($"Could not retrieve customer information from database.");
                }


                dbCon.Close();
            }
            else
            {
                Console.WriteLine("Database Connection Error.");
            }
        }
    }
}
