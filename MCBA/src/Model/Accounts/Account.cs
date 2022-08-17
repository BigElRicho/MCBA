using MBCA.src.Model.Transactions;
using System.Collections.Generic;

namespace MCBA.src.Model.Accounts
{
    public class Account : AbstractAccount
    {
        public override int AccountNumber { get; set; }
        public override string AccountType { get; set; }
        public override int CustomerID { get; set; }
        //public override List<Transaction> Transactions { get; set; }

        public Account(int accountNumber, string accountType, int customerID /*List<Transaction> transactions*/) : base(accountNumber, accountType, customerID)
        {
            this.AccountNumber = accountNumber;
            this.AccountType = accountType;
            this.CustomerID = customerID;
            //this.Transactions = transactions;
        }
    }
}