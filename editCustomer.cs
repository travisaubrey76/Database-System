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

namespace Compe561_Project1
{
    /// <summary>
    /// Main Comment Information found above method definitions. Also - main summary block found in intial form1.cs file
    /// </summary>
    public partial class editCustomer : Form
    {
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

        //Variables needed for MENU START INITIALIZATION
        ArrayList myArrayList = new ArrayList();
        ArrayList lineArrayList = new ArrayList();

        //Using the below to check for existing data firstly
        string existingFirstName = "";
        string existingLastName = "";
        string existingAddress = "";
        string existingCity = "";
        string existingState = "";
        string existingZip = "";
        string existingPhoneNumber = "";
        string existingEmail = "";

        public editCustomer()
        {
            InitializeComponent();

            //opSelectComboBox.Items.Add(customerOne); BRING FROM FILE
            addNewCustomerButton.Visible = false; //Starting with this set to false - needs to be triggered on.
            //initial update
            updateCustomers();
        }


        /// <summary>
        /// General IO method to update customers based on what's in the associated Database. Will be my general function call to update comboBox selections
        /// </summary>
        private void updateCustomers()
        {
            //Reset myArraylist
            myArrayList.Clear();

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

                    //Only used for visual representation of successful database connection
                    Console.WriteLine($"{firstName} {lastName} | {address} | {city} {state}, {zip} | {phone} | {email}\n");

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


                    myArrayList.Add(cust);

                }

                //Simple check to see if we successfully retreived our book information prior to sending it to our form

                if (myArrayList.Count > 0)
                {
                    //Everytime this function is called, it is meant to refresh the current contents iteratively, so we begin that by flushing the current contents
                    opSelectComboBox.Items.Clear();

                    //This portion would be where I send my books into the form application combobox
                    foreach (Customer customer in myArrayList)
                    {
                        opSelectComboBox.Items.Add(customer.FirstName + " " + customer.LastName);
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

        /// <summary>
        /// Closing current menu and going back to the main_menu. Taken from provided tutorial documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            InstanceRef.Show();
        }

        /// <summary>
        /// 
        /// Project 5 Updates: Mainly converted the function to save customer information to a database instead of a .txt file.
        /// 
        /// Save button press activates data validity protocol. Checks all available boxes (ensuring non-empty cells) for valid data entry. If everything checks out then proceed to edit selected customer.
        /// This method does not add a new customer - I added a secondary functionality for that very purpose. 
        /// 
        /// Currently - only data validation being checked is if the zip code is 5 digits in length.
        /// 
        /// Future updates can include in class quiz material to data check telephone number validity as well as the length of phone numbers matching the database length
        /// 
        /// Credit: None anymore for this portion, previous project required help with rewriting a text file, but updated a database is much simpler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            //Scope Variables
            int zip = 0;
            Boolean zipGood = false;

            //Iterative Check for empty or white space entries
            if (String.IsNullOrWhiteSpace(firstName_TextBox.Text))
            {
                MessageBox.Show($"Please enter a first name.");
                firstName_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(lastName_TextBox.Text))
            {
                MessageBox.Show($"Please enter a last name.");
                lastName_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(address_TextBox.Text))
            {
                MessageBox.Show($"Please enter an address.");
                address_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(city_TextBox.Text))
            {
                MessageBox.Show($"Please enter a city.");
                city_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(state_TextBox.Text))
            {
                MessageBox.Show($"Please enter a state.");
                state_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(zip_TextBox.Text))
            {
                MessageBox.Show($"Please enter a zip code.");
                zip_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(phone_TextBox.Text))
            {
                MessageBox.Show($"Please enter a phone number.");
                phone_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(email_TextBox.Text))
            {
                MessageBox.Show($"Please enter an email address.");
                email_TextBox.Focus();
            }
            else //Every text box has an input, now lets check for data validity
            {
                //Since its 2019, I will be more lenient on data entries
                if(zip_TextBox.Text.Length != 5)
                {
                    MessageBox.Show($"Please enter a valid zip code.");
                    zip_TextBox.Focus();
                }
                else
                {
                    try //Try to convert zip code to integers and if it works set a flag signalling zip code is valid
                    {
                        zip = int.Parse(zip_TextBox.Text);
                        zipGood = true;
                    }
                    catch (InvalidCastException)
                    {
                        //Should do a general check and throw here, but i'll get to it later.
                    }
                }
                
            }

            //at this point we can measure whether all data validation has been completed and if so we can save the current customer information to our database file
            if (zipGood) //FLAGS GO HERE, EVERY FLAG must check out prior to actually openning/writing to files
            {
                //Firstly, we need to traverse our customer table to check if the user already exists
                var dbCon = DBConnection.Instance();
                dbCon.DatabaseName = "bookstore";

                if (dbCon.IsConnect())
                {
                    //Checking if current boxes already exist and if so then a flag will be sent to prevent an update.
                    string query = "SELECT COUNT(1) FROM customer WHERE first = @first AND last = @last AND address = @address AND city = @city AND state = @state AND zip = @zip AND phone = @phone AND email = @email";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@first", firstName_TextBox.Text);
                    cmd.Parameters.AddWithValue("@last", lastName_TextBox.Text);
                    cmd.Parameters.AddWithValue("@address", address_TextBox.Text);
                    cmd.Parameters.AddWithValue("@city", city_TextBox.Text);
                    cmd.Parameters.AddWithValue("@state", state_TextBox.Text);
                    cmd.Parameters.AddWithValue("@zip", zip_TextBox.Text);
                    cmd.Parameters.AddWithValue("@phone", phone_TextBox.Text);
                    cmd.Parameters.AddWithValue("@email", email_TextBox.Text);

                    var reader = cmd.ExecuteReader();

                    //If there exists an entry in the database with the exact values present in the textboxes then don't update the customer information
                    if(reader.Read().Equals("1"))
                    {
                        Console.WriteLine("Customer Already Exists on Database, Will not Update.");
                        opSelectComboBox.Enabled = true; //Just in case it was disabled somewhere else
                    }
                    else //Otherwise - we can update the customer information
                    {
                        reader.Close();
                        //Customer with the same first names will either update the first or all customers with that name.
                        query = "UPDATE customer SET first = @first, last = @last, address = @address, city = @city, state = @state, zip = @zip, phone = @phone, email = @email WHERE first = @ExistingFirst";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@first", firstName_TextBox.Text);
                        cmd.Parameters.AddWithValue("@last", lastName_TextBox.Text);
                        cmd.Parameters.AddWithValue("@address", address_TextBox.Text);
                        cmd.Parameters.AddWithValue("@city", city_TextBox.Text);
                        cmd.Parameters.AddWithValue("@state", state_TextBox.Text);
                        cmd.Parameters.AddWithValue("@zip", zip_TextBox.Text);
                        cmd.Parameters.AddWithValue("@phone", phone_TextBox.Text);
                        cmd.Parameters.AddWithValue("@email", email_TextBox.Text);
                        cmd.Parameters.AddWithValue("@ExistingFirst", existingFirstName);
                        cmd.ExecuteNonQuery();



                        updateCustomers();
                        opSelectComboBox.Text = $"{firstName_TextBox.Text} {lastName_TextBox.Text}";
                        opSelectComboBox.Enabled = true;
                    }

                    dbCon.Close();
                }
                else
                {
                    Console.WriteLine("Database Connection Error.");
                }
            }
            else //if data flags are not good then we cannot save the user
            {
                MessageBox.Show($"Cannot Save User");
            }
        }

        /// <summary>
        /// Method populates all text boxes based on whichever customer is selected. Also stores selected information in some back channels enabling us to access them later if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach(Customer customer in myArrayList)
            {
                    if (opSelectComboBox.Text.Equals(customer.FirstName + " " + customer.LastName)) //Check each customer against every customer and if a match occurs fill the text boxes
                    {
                        firstName_TextBox.Text = customer.FirstName;
                        lastName_TextBox.Text = customer.LastName;
                        address_TextBox.Text = customer.Address;
                        city_TextBox.Text = customer.City;
                        state_TextBox.Text = customer.State;
                        zip_TextBox.Text = customer.Zip;
                        phone_TextBox.Text = customer.PhoneNumber;
                        email_TextBox.Text = customer.Email;

                        //Create existing data in case something was changed - we can update the DB accordingly
                        existingFirstName = firstName_TextBox.Text;
                        existingLastName = lastName_TextBox.Text;
                        existingAddress = address_TextBox.Text;
                        existingCity = city_TextBox.Text;
                        existingState = state_TextBox.Text;
                        existingZip = zip_TextBox.Text;
                        existingPhoneNumber = phone_TextBox.Text;
                        existingEmail = email_TextBox.Text;
                    } 
            }

        }

        /// <summary>
        /// Simple method - I didn't want to incorporate multiple functions into our current "Save" button, so instead I opted to utilize a sepearate "hidden" button that appends to our current file. 
        /// In the long run, this is probably better practice since I don't have to clear and rewrite a file when adding a new user, instead I am simply opening and writing to the end - probably better 
        /// on memory and since this program isn't memory/processor intensive at all, I see no harm in doing it this way. let me know if you disagree.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCustomerButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please enter brand new customer information and then click save!");
            opSelectComboBox.Enabled = false;
            saveButton.Enabled = false;
            addNewCustomerButton.Visible = true;
            firstName_TextBox.Text = "";
            lastName_TextBox.Text = "";
            address_TextBox.Text = "";
            city_TextBox.Text = "";
            state_TextBox.Text = "";
            zip_TextBox.Text = "";
            phone_TextBox.Text = "";
            email_TextBox.Text = "";

            firstName_TextBox.Focus();
        }

        /// <summary>
        /// Basic cancel functionality. Upon press, triggers some flags to determine what happens next
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            opSelectComboBox.Enabled = true;
            saveButton.Enabled = true;
            addNewCustomerButton.Visible = false;
        }

        /// <summary>
        /// When New Customer button is pressed, this button shows itself and upon click appends this new user! After Data validation of course. This appends to customers table in database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewCustomerButton_Click(object sender, EventArgs e)
        {
            //Scope Variables
            int zip = 0;
            Boolean zipGood = false;

            //Iterative Check for empty or white space entries
            if (String.IsNullOrWhiteSpace(firstName_TextBox.Text))
            {
                MessageBox.Show($"Please enter a first name.");
                firstName_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(lastName_TextBox.Text))
            {
                MessageBox.Show($"Please enter a last name.");
                lastName_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(address_TextBox.Text))
            {
                MessageBox.Show($"Please enter an address.");
                address_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(city_TextBox.Text))
            {
                MessageBox.Show($"Please enter a city.");
                city_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(state_TextBox.Text))
            {
                MessageBox.Show($"Please enter a state.");
                state_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(zip_TextBox.Text))
            {
                MessageBox.Show($"Please enter a zip code.");
                zip_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(phone_TextBox.Text))
            {
                MessageBox.Show($"Please enter a phone number.");
                phone_TextBox.Focus();
            }
            else if (String.IsNullOrWhiteSpace(email_TextBox.Text))
            {
                MessageBox.Show($"Please enter an email address.");
                email_TextBox.Focus();
            }
            else //Every text box has an input, now lets check for data validity
            {
                //Since its 2019, I will be more lenient on data entries
                if (zip_TextBox.Text.Length != 5)
                {
                    MessageBox.Show($"Please enter a valid zip code.");
                    zip_TextBox.Focus();
                }
                else
                {
                    try //Try to convert zip code to integers and if it works set a flag signalling zip code is valid
                    {
                        zip = int.Parse(zip_TextBox.Text);
                        zipGood = true;
                    }
                    catch (InvalidCastException)
                    {
                        //Should do a general check and throw here, but i'll get to it later.
                    }
                }

            }

            //at this point we can measure whether all data validation has been completed and if so we can save the current customer information to our database
            if (zipGood) //FLAGS GO HERE
            {
                //Firstly, we need to traverse our customer table to check if the user already exists
                var dbCon = DBConnection.Instance();
                dbCon.DatabaseName = "bookstore";

                if (dbCon.IsConnect())
                {
                    //Checking if current boxes already exist and if so then a flag will be sent to prevent an update.
                    string query = "SELECT COUNT(1) FROM customer WHERE first = @first AND last = @last AND address = @address AND city = @city AND state = @state AND zip = @zip AND phone = @phone AND email = @email";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    cmd.Parameters.AddWithValue("@first", firstName_TextBox.Text);
                    cmd.Parameters.AddWithValue("@last", lastName_TextBox.Text);
                    cmd.Parameters.AddWithValue("@address", address_TextBox.Text);
                    cmd.Parameters.AddWithValue("@city", city_TextBox.Text);
                    cmd.Parameters.AddWithValue("@state", state_TextBox.Text);
                    cmd.Parameters.AddWithValue("@zip", zip_TextBox.Text);
                    cmd.Parameters.AddWithValue("@phone", phone_TextBox.Text);
                    cmd.Parameters.AddWithValue("@email", email_TextBox.Text);

                    var reader = cmd.ExecuteReader();

                    //If there exists an entry in the database with the exact values present in the textboxes then don't insert the new customer information since he/she/it already exists
                    if (reader.Read().Equals("1"))
                    {
                        Console.WriteLine("Customer Already Exists on Database, Will not Add new customer.");
                        opSelectComboBox.Enabled = true; //Just in case it was disabled somewhere else
                    }
                    else //Otherwise - we can update the customer information
                    {
                        //string query = "INSERT INTO books VALUES(@Title, @Author, @ISBN, @Price)";
                        reader.Close();

                        //Customer with the same first names will either update the first or all customers with that name.. In addition we are allowing the auto-increment of our table to take form
                        query = "INSERT INTO customer (first, last, address, city, state, zip, phone, email) VALUES(@first, @last, @address, @city, @state, @zip, @phone, @email)";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@first", firstName_TextBox.Text);
                        cmd.Parameters.AddWithValue("@last", lastName_TextBox.Text);
                        cmd.Parameters.AddWithValue("@address", address_TextBox.Text);
                        cmd.Parameters.AddWithValue("@city", city_TextBox.Text);
                        cmd.Parameters.AddWithValue("@state", state_TextBox.Text);
                        cmd.Parameters.AddWithValue("@zip", zip_TextBox.Text);
                        cmd.Parameters.AddWithValue("@phone", phone_TextBox.Text);
                        cmd.Parameters.AddWithValue("@email", email_TextBox.Text);
                        cmd.ExecuteNonQuery();

                        updateCustomers();
                        opSelectComboBox.Text = $"{firstName_TextBox.Text} {lastName_TextBox.Text}"; //let the combo box reflect what the user just added
                        opSelectComboBox.Enabled = true;
                    }

                    dbCon.Close();
                }
                else
                {
                    Console.WriteLine("Database Connection Error.");
                }
            }
            else //if data flags are not good then we cannot save the user
            {
                MessageBox.Show($"Cannot Create User");
            }

            //after adding we need to update our customers and then set the focus in the combo box to our new customer
            //updateCustomers();
            //at the end - after processing - disable button
            addNewCustomerButton.Visible = false;
            saveButton.Enabled = true;
        }
    }
}
