using System;
using System.Collections.Generic;
using System.Net.Http;
using MCBA.src.Model.Customer;
using Newtonsoft.Json;
using MCBA.src.Model.Login;
using SimpleHashing;
using Microsoft.Data.SqlClient;
using System.Linq;
using MCBA.src.Model.Accounts;

namespace MCBA.src.Controller
{
    public class BankSystemController
    {
        public static string ConnectionString { get; set; }

        public static string Username { get; set; }
        private static int CustomerID { get; set; }

        private static List<Login> LoginInfo { get; set; }
        private static List<Customer> Customers { get; set; }
        public static CustomerManager CustomerManager { get; private set; }
        public static AccountManager AccountManager { get; private set; }

        public static void Main()
        {
            ConnectionString = "Server = rmit.australiaeast.cloudapp.azure.com; Database = s3407957_a1; Uid = s3407957_a1; Password = abc123";
            //var database = new DisconnectedAccess();
            //database.DisconnectedReadAccess();
            CustomerManager = new CustomerManager(ConnectionString);
            AccountManager = new AccountManager(ConnectionString);
            GetAndSaveCustomers();
            GetAndSaveAccounts();
            //LoadData();
            LoadLoginInfo();
            Login();
            Menu();
        }

        public static void Menu()
        {
            var userInput = "";
            while (userInput != "6")
            {
                Console.WriteLine("\nWelcome to MCBA Banking System.");
                Console.WriteLine($"Currently logged in as: {BankSystemController.Username}");
                Console.WriteLine("[1] Deposit\n" + "[2] Withdraw\n" + "[3] Transfer\n" + "[4] My Statement\n" + "[5] Logout\n" + "[6] Exit\n");
                Console.WriteLine("\n\nEnter an option: ");
                userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine($"Menu option incomplete.");
                        Menu();
                        break;
                    case "2":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine($"Menu option incomplete.");
                        Menu();
                        break;
                    case "3":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine($"Menu option incomplete.");
                        Menu();
                        break;
                    case "4":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine($"Menu option incomplete.");
                        Menu();
                        break;
                    case "5":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine($"Menu option incomplete.");
                        Menu();
                        break;
                    case "6":
                        Console.WriteLine($"\nYou selected: {userInput}");
                        Console.WriteLine("Confirm that you want to exit [y/n]: ");
                        userInput = Console.ReadLine();
                        if(userInput == "y")
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Menu();
                        }
                        break;
                }
            }
        }

        //Get JSON data from web services.
        public static void LoadData()
        {
            try
            {
                var json = new HttpClient().
                    GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/customers/").Result;

                Console.WriteLine(json);

                Customers = JsonConvert.DeserializeObject<List<Customer>>(json, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
                });

                const string format = "{0,-20} {1,-20} {2,-20} {3,-20} {4,-20} {5,-20}";
                Console.WriteLine(format, "Customer ID", "Customer Name", "Customer Address","City" ,"Postcode", "Accounts");
                foreach (var customer in Customers)
                {
                    //Console.WriteLine("Customer Name: " + customer.Name);
                    Console.WriteLine(format, customer.CustomerID, customer.Name, customer.Address, customer.City, customer.PostCode/*, customer.Accounts*/);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Something went wrong while loading the customer data.");
            }

        }

        public static void LoadLoginInfo()
        {
            try
            {
                var json = new HttpClient().
                    GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/logins/").
                    Result;

                //Console.WriteLine(json);

                LoginInfo = JsonConvert.DeserializeObject<List<Login>>(json);

                //const string format = "{0,-20} {1,-20} {2,-20}";
                //Console.WriteLine(format, "Login ID", "Customer ID", "Password Hash");
                //foreach (var login in LoginInfo)
                //{
                //    Console.WriteLine(format, login.LoginId, login.CustomerID, login.PasswordHash);
                //}

            }
            catch (Newtonsoft.Json.JsonSerializationException)
            {
                Console.WriteLine("There was an issue gettting Json data.");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong trying to load login info.");
            }

        }

        public static void Login()
        {
            bool idMatches = false;
            bool passwordMatches = false;
            int customerID = 0;

            while (idMatches == false)
            {
                Console.WriteLine("\n----- Customer Login -----");
                Console.WriteLine("Login ID: ");

                var loginInput = Console.ReadLine();   
                
                foreach (var login in BankSystemController.LoginInfo)
                {
                    if (login.LoginId == loginInput)
                    {
                        idMatches = true;
                        Console.WriteLine($"Customer ID: {login.CustomerID}");
                        customerID = login.CustomerID;
                    }                                     
                }
                if (idMatches == false)
                {
                    Console.WriteLine("No matches found.");
                }
                if (idMatches == true)
                {
                    BankSystemController.CustomerID = customerID;
                    while (passwordMatches == false)
                    {
                        Console.WriteLine("Login ID Valid. Enter your password: ");
                        var passwordInput = Console.ReadLine();
                    
                        foreach (var login in BankSystemController.LoginInfo)
                        {
                            if (customerID == login.CustomerID)
                            {
                                //Console.WriteLine($"CustomerID: {customerID}\nComparisonID: {login.CustomerID}");
                                if (PBKDF2.Verify(login.PasswordHash, passwordInput))
                                {
                                    Console.WriteLine($"Password is correct. You are now logged in as Customer: {customerID}");
                                    Username = GetCustomerName(customerID);                                    
                                    passwordMatches = true;                                   
                                }
                                else
                                {
                                    Console.WriteLine("Password Incorrect. Try again");
                                }
                            }
                        }
                    }

                }
            }
        }

        public static string GetCustomerName(int customerID)
        {
            string customerName = "";
            bool idMatches = false;
           
            Console.WriteLine("Looking for customer ID....");

            foreach(var customer in CustomerManager.Customers)
            {
                if(customer.CustomerID == customerID)
                {
                    Console.WriteLine("Matching customer ID found.");
                    customerName = customer.Name;
                    idMatches = true;
                }
            }
            
            if(idMatches == false)
            {
                Console.WriteLine("No matching customer IDs found.");
                return null;
            }
            else
            {
                return customerName;
            }            
        }

        public static void GetAndSaveCustomers()
        {
            if (CustomerManager.Customers.Any())
                return;

            using var client = new HttpClient();
            var json =
                client.GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/customers/").Result;
            CustomerManager.Customers = JsonConvert.DeserializeObject<List<Customer>>(json);

            foreach (var customer in CustomerManager.Customers)
            {
                //Replace null values with empty strings as db doesn't except null.
                if(customer.Address == null)
                {
                    customer.Address = "";
                }
                if (customer.City == null)
                {
                    customer.City = "";
                }
                if (customer.PostCode == null)
                {
                    customer.PostCode = "";
                }

                CustomerManager.InsertCustomer(customer);
            }
        }

        public static void GetAndSaveAccounts()
        {
            if (AccountManager.Accounts.Any())
                return;

            using var client = new HttpClient();
            var json =
                client.GetStringAsync("https://coreteaching01.csit.rmit.edu.au/~e87149/wdt/services/customers/").Result;
            var customers = JsonConvert.DeserializeObject<List<CustomerWithAccounts>>(json);
            var accounts = new List<Account>();

            //Get all the accounts out of the customer objects.
            foreach (var customer in customers)
            {
                accounts.AddRange(customer.Accounts);
            }

            //insert the account information into the database.
            foreach(var account in accounts)
            {
                AccountManager.InsertAccount(account);
            }
        }
    }   
}