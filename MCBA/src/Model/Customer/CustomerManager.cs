using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MCBA.src.Model.Customer
{
    public class CustomerManager
    {
        
            private string ConnectionString { get; }

            public List<Customer> Customers { get; set; }

            public CustomerManager(string connectionString)
            {
                ConnectionString = connectionString;

                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select * from Customer";
                var reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                Customers = dataTable.Select().Select(x =>
                    new Customer((int)x["CustomerID"], (string)x["Name"], (string)x["Address"], (string)x["City"], (string)x["PostCode"])).ToList();
                //Print newly created customer objects.
                //foreach(var customer in Customers)
                //{
                //    Console.WriteLine($"CustomerID: {customer.CustomerID}");
                //    Console.WriteLine($"Name: {customer.Name}");
                //    Console.WriteLine($"Address: {customer.Address}");
                //    Console.WriteLine($"City: {customer.City}");
                //    Console.WriteLine($"Postcode: {customer.PostCode}");
                //}
            }

            public void InsertCustomer(Customer customer)
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                //Making sure the properties are getting passed along correctly.
                //Console.WriteLine("Insert Customer Method:");
                //Console.WriteLine($"CustomerID: {customer.CustomerID}");
                //Console.WriteLine($"Name: {customer.Name}");
                //Console.WriteLine($"Address: {customer.Address}");
                //Console.WriteLine($"City: {customer.City}");
                //Console.WriteLine($"Postcode: {customer.PostCode}");

            var command = connection.CreateCommand();
                command.CommandText =
                    "insert into Customer (CustomerID, Name, Address, City, PostCode) values (@CustomerID, @Name, @Address, @City, @PostCode)";
                command.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("Name", customer.Name);
                command.Parameters.AddWithValue("Address", customer.Address);
                command.Parameters.AddWithValue("City", customer.City);
                command.Parameters.AddWithValue("PostCode", customer.PostCode);
                command.ExecuteNonQuery();
        }
        
    }
}
