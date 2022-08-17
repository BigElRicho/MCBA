using System;
using System.Collections.Generic;
using MBCA.src.Model.Transactions;


namespace MCBA.src.Model.Accounts
{
    public abstract class AbstractAccount
    {
        abstract public int AccountNumber { get; set; }
        abstract public string AccountType { get; set; }
        abstract public int CustomerID { get; set; }

        //abstract public List<Transaction> Transactions { get; set; }

        public AbstractAccount(int accountNumber, String accountType, int customerID)
        {
            this.AccountNumber = accountNumber;
            this.AccountType = accountType;
            this.CustomerID = customerID;
        }


    }
}