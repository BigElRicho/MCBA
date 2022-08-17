using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBA.src.Model.Accounts
{
    public class AccountManager
    {
        private string ConnectionString { get; }

        public List<Account> Accounts { get; set; }

        public AccountManager(string connectionString)
        {
            ConnectionString = connectionString;

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "select * from Account";
            var reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            Accounts = dataTable.Select().Select(x =>
                new Account((int)x["AccountNumber"], (string)x["AccountType"], (int)x["CustomerID"])).ToList();
            //Print newly created customer objects.
            //foreach (var account in Accounts)
            //{
            //    Console.WriteLine($"Account Number: {account.AccountNumber}");
            //    Console.WriteLine($"Account Type: {account.AccountType}");
            //    Console.WriteLine($"CustomerID: {account.CustomerID}");
            //}
        }

        public void InsertAccount(Account account)
        {
            decimal stype = 100.00m;
            decimal ctype = 500.00m;
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Making sure the properties are getting passed along correctly.
            //Console.WriteLine("Insert Account Method:");
            //Console.WriteLine($"Account Number: {account.AccountNumber}");
            //Console.WriteLine($"Account Type: {account.AccountType}");
            //Console.WriteLine($"CustomerID: {account.CustomerID}");


            var command = connection.CreateCommand();
            command.CommandText =
                "insert into Account (AccountNumber, AccountType, CustomerID, Balance) values (@AccountNumber, @AccountType, @CustomerID, @Balance)";
            command.Parameters.AddWithValue("AccountNumber", account.AccountNumber);
            command.Parameters.AddWithValue("AccountType", account.AccountType);
            command.Parameters.AddWithValue("CustomerID", account.CustomerID);
            if (account.AccountType == "S") 
            {
                command.Parameters.AddWithValue("Balance", stype);
            }
            else if(account.AccountType == "C")
            {
                command.Parameters.AddWithValue("Balance", ctype);
            }
            command.ExecuteNonQuery();
        }
    }
}
