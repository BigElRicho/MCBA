using System;
using MCBA.src.Model.Accounts;
using System.Collections.Generic;

namespace MBCA.src.Model.Customer
{
    public abstract class AbstractCustomer
    {
        abstract public int CustomerID { get; set; }
        abstract public string Name { get; set; }
        abstract public string Address { get; set; }
        abstract public string City { get; set; }
        abstract public string PostCode { get; set; }

        public AbstractCustomer(int customerID, string name, string address, string city, string postCode)
        {
            this.CustomerID = customerID;
            this.Name = name;
            this.Address = address;
            this.City = city;
            this.PostCode = postCode;
        }
    }
}