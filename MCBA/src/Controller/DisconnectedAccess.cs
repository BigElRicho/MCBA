using MCBA.src.Model.Customer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBA.src.Controller
{
    public class DisconnectedAccess
    {
        public static string ConnectionString = "Server=rmit.australiaeast.cloudapp.azure.com;Database=s3407957_a1;Uid=s3407957_a1;Password=abc123";

        public void DisconnectedReadAccess()
        {
            const string connectionString = "Server=rmit.australiaeast.cloudapp.azure.com;Database=s3407957_a1;Uid=s3407957_a1;Password=abc123";
            DisconnectedReadAccess(connectionString);
        }

        private void DisconnectedReadAccess(string connectionString)
        {
            //Below is similar to using a try catch statement. It will automatically close the connection when done.
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var customerCommand = new SqlCommand("Select * from Customer", connection);
            var AccountCommand = new SqlCommand("Select * from Account", connection);
            var LoginCommand = new SqlCommand("Select * from Login", connection);
            var TransactionCommand = new SqlCommand("Select * from Transaction", connection);

            //Load data from database for customers.
            var customerTable = new DataTable();
            new SqlDataAdapter(customerCommand).Fill(customerTable);


            Console.WriteLine("Customer Details:\n");
            foreach(var row in customerTable.Select())
            {
                Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}",
                    row["CustomerID"],
                    row["Name"],
                    row["Address"],
                    row["City"],
                    row["Postcode"]);
            }

            //Load data from database for accounts.
            var accountTable = new DataTable();
            new SqlDataAdapter(AccountCommand).Fill(accountTable);

            Console.WriteLine("Account Details:\n");
            foreach (var row in accountTable.Select())
            {
                Console.WriteLine("{0}\n{1}\n{2}\n{3}",
                    row["AccountNumber"],
                    row["AccountType"],
                    row["CustomerID"],
                    row["Balance"]);
            }

            //Load data from database for login credentials.
            var loginTable = new DataTable();
            new SqlDataAdapter(customerCommand).Fill(loginTable);

            Console.WriteLine("Login Details:\n");
            foreach (var row in loginTable.Select())
            {
                Console.WriteLine("{0}\n{1}\n{2}",
                    row["LoginID"],
                    row["CustomerID"],
                    row["PasswordHash"]);
            }

            //Load data from database for transactions.
            var transactionTable = new DataTable();
            new SqlDataAdapter(customerCommand).Fill(transactionTable);

            Console.WriteLine("Transaction Details:\n");
            foreach (var row in transactionTable.Select())
            {
                Console.WriteLine("{0}\n{1}\n{2}",
                    row["LoginID"],
                    row["CustomerID"],
                    row["PasswordHash"]);
            }


        }

        public void InsertCustomer(Customer customer)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                "insert into Person (CustomerID, Name, Address, City, PostCode) values (@CustomerID, @Name, @Address, @City, @PostCode)";
            command.Parameters.AddWithValue("personID", customer.CustomerID);
            command.Parameters.AddWithValue("firstName", customer.Name);
            command.Parameters.AddWithValue("lastName", customer.Address);
            command.Parameters.AddWithValue("lastName", customer.City);
            command.Parameters.AddWithValue("lastName", customer.PostCode);
            command.ExecuteNonQuery();
        }
    }
}
