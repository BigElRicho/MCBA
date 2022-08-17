using MBCA.src.Model.Customer;
using MCBA.src.Model.Accounts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MCBA.src.Model.Customer
{
    public class Customer : AbstractCustomer
    {
        public override int CustomerID { get; set; }
        public override string Name { get; set; }
        public override string Address { get; set; }
        public override string City { get; set; }
        public override string PostCode { get; set; }

        public Customer(int customerID, string name, string address, string city, string postCode) : base(customerID, name, address, city, postCode)
        {
            this.CustomerID = customerID;
            this.Name = name;
            this.Address = address;
            this.City = city;
            this.PostCode = postCode;
        }
    }
}