using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Compe561_Project1
{
    public partial class bookForm : Form
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
                    if(Connection.State == ConnectionState.Open)
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
        /// Book Class to help facilitate all the operations I need to do for each book object.
        /// </summary>
        private class Book
        {
            public string Author;
            public string ISBN;
            public string Price;
            public string Title;

            public Book(string author, string iSBN, string price, string title)
            {
                Author = author;
                ISBN = iSBN;
                Price = price;
                Title = title;
            }

            //Defailt constructor needed when not implicitly creating books as per part 1 of this project
            public Book()
            {

            }
        }

        //Variables needed for window intiliazation
        ArrayList myArrayList = new ArrayList();

        //Boolean Flags
        Boolean newButtonPressed = false;

        //Going to actually do the checking here in the code rather than use the DB
        string existingTitle = "";
        string existingAuthor = "";
        string existingISBN = "";
        string existingPrice = "";


        /// <summary>
        /// Some of my connection mechanisms were borrowed from the provided help documentation as well from the following website to assist with my connecting
        /// to my server: https://stackoverflow.com/questions/21618015/how-to-connect-to-mysql-database
        /// </summary>
        public bookForm()
        {
            InitializeComponent();

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
                    Book bk = new Book();
                    bk.Title = bookTitle;
                    bk.Author = bookAuthor;
                    bk.ISBN = bookISBN;
                    bk.Price = bookPrice;

                    myArrayList.Add(bk);

                }

                //Simple check to see if we successfully retreived our book information prior to sending it to our form
                if (myArrayList.Count > 0)
                {
                    foreach (Book bk_data in myArrayList)
                    {
                        bookComboBox.Items.Add(bk_data.Title);
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
        }

        /// <summary>
        /// Everytime the user selects a different book the text boxes will populate with the appropriate information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Book book in myArrayList)
            {
                if (bookComboBox.Text.Equals(book.Title)) //Check each book against all books and if a match occurs fill the text boxes
                {

                    titleTextBox.Text = book.Title;
                    authorTextBox.Text = book.Author;
                    isbnTextBox.Text = book.ISBN;
                    priceTextBox.Text = book.Price;
                }

            }

            //Going to actually do the checking here in the code rather than use the DB
            existingTitle = titleTextBox.Text;
            existingAuthor = authorTextBox.Text;
            existingISBN = isbnTextBox.Text;
            existingPrice = priceTextBox.Text;
        }

        /// <summary>
        /// Simple back button to gracefull close this window and open the previous window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            InstanceRef.Show();
        }

        /// <summary>
        /// This is a flag setter function - if the user wants to add a new book then this button needs to be pressed first. In turn this will disable the combo box and clear all text boxes for a new book to be clearly defined.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newBookButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please enter brand new book information and then click save!");
            bookComboBox.Enabled = false;
            newButtonPressed = true;
            titleTextBox.Text = "";
            authorTextBox.Text = "";
            isbnTextBox.Text = "";
            priceTextBox.Text = "";
            titleTextBox.Focus();
        }

        /// <summary>
        /// Simple cancel button - reads predetermined flags and enacts specific functionality accordingly. For instance, if we are adding a new book, but want to cancel - then we need to reset our combo
        /// box to reflect the first option from all of our books adn then refill the text boxes with that books information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (newButtonPressed) //Considers one of the two functions for this button
            {
                newButtonPressed = false;
                bookComboBox.Enabled = true;

                Book bk = new Book();
                bk = (Book)myArrayList[0];

                bookComboBox.Text = bk.Title;

                //Refreshes Text Boxes
                foreach (Book book in myArrayList)
                {
                    if (bookComboBox.Text.Equals(book.Title)) //Check each book against all books and if a match occurs fill the text boxes
                    {
                        titleTextBox.Text = book.Title;
                        authorTextBox.Text = book.Author;
                        isbnTextBox.Text = book.ISBN;
                        priceTextBox.Text = book.Price;
                    }
                }
            }
        }

        /// <summary>
        /// For help with proper insertion into database I utilized tips from the following site:
        /// https://stackoverflow.com/questions/23301582/how-do-i-to-insert-data-into-an-sql-table-using-c-sharp-as-well-as-implement-an
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?.", "Confirmation Box", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                //This portion will insert the new book into the database
                if (newButtonPressed)
                {
                    newButtonPressed = false;
                    bookComboBox.Enabled = true;
                    //Insert new book into the database
                    var dbCon = DBConnection.Instance();
                    dbCon.DatabaseName = "bookstore";

                    if (dbCon.IsConnect())
                    {

                        string query = "INSERT INTO books VALUES(@Title, @Author, @ISBN, @Price)";
                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@Title", titleTextBox.Text);
                        cmd.Parameters.AddWithValue("@Author", authorTextBox.Text);
                        cmd.Parameters.AddWithValue("@ISBN", isbnTextBox.Text);
                        cmd.Parameters.AddWithValue("@Price", priceTextBox.Text);
                        cmd.ExecuteNonQuery();

                        //Still need the below to bring back the items that were newly added
                        query = "SELECT * FROM books";
                        cmd = new MySqlCommand(query, dbCon.Connection);

                        //Reset our myArrayList
                        myArrayList.Clear();
                        bookComboBox.Items.Clear();

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
                            Book bk = new Book();
                            bk.Title = bookTitle;
                            bk.Author = bookAuthor;
                            bk.ISBN = bookISBN;
                            bk.Price = bookPrice;

                            myArrayList.Add(bk);

                        }
                        //Since we are for sure adding something, we don't need to check if myArrayList is empty or not
                        foreach (Book bk_data in myArrayList)
                        {
                            bookComboBox.Items.Add(bk_data.Title);
                        }

                        dbCon.Close();
                    }
                    else
                    {
                        Console.WriteLine("Database Connection Error.");
                    }
                }
                else //We are not adding a new book, but possibly editing an existing book at this point
                {
                    //If no edit was actually made then do nothing since no DB update is required
                    if (string.Equals(titleTextBox.Text, existingTitle) && string.Equals(authorTextBox.Text, existingAuthor) && string.Equals(isbnTextBox.Text, existingISBN) && string.Equals(priceTextBox.Text, existingPrice))
                    {
                        //Do nothing
                        MessageBox.Show("Nothing Was Changed - No DB update required.");
                    }
                    else //otherwise do an update to the existing entry
                    {
                        var dbCon = DBConnection.Instance();
                        dbCon.DatabaseName = "bookstore";

                        if (dbCon.IsConnect())
                        {

                            string query = "UPDATE books SET Title = @Title, Author = @Author, ISBN = @ISBN, Price = @Price WHERE Title = @ExistingTitle";
                            var cmd = new MySqlCommand(query, dbCon.Connection);
                            cmd.Parameters.AddWithValue("@Title", titleTextBox.Text);
                            cmd.Parameters.AddWithValue("@Author", authorTextBox.Text);
                            cmd.Parameters.AddWithValue("@ISBN", isbnTextBox.Text);
                            cmd.Parameters.AddWithValue("@Price", priceTextBox.Text);
                            cmd.Parameters.AddWithValue("@ExistingTitle", existingTitle);
                            cmd.ExecuteNonQuery();

                            //Still need the below to bring back the items that were newly updated
                            query = "SELECT * FROM books";
                            cmd = new MySqlCommand(query, dbCon.Connection);

                            //Reset our myArrayList
                            myArrayList.Clear();
                            bookComboBox.Items.Clear();

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
                                Book bk = new Book();
                                bk.Title = bookTitle;
                                bk.Author = bookAuthor;
                                bk.ISBN = bookISBN;
                                bk.Price = bookPrice;

                                myArrayList.Add(bk);

                            }
                            //Since we are for sure adding something, we don't need to check if myArrayList is empty or not
                            foreach (Book bk_data in myArrayList)
                            {
                                bookComboBox.Items.Add(bk_data.Title);
                            }

                            bookComboBox.Text = titleTextBox.Text;

                            dbCon.Close();

                        }
                    }
                }
            }
            else if(dr == DialogResult.No) //User entered a no in the confirmation box
            {
                //Basically allow the user to change information unless one of the cancels is pressed.
            }
            else //Cancel was pressed.
            {
                newButtonPressed = false;
                bookComboBox.Enabled = true;
            }

            
        }
    }
}
