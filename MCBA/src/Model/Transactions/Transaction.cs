using MCBA.src.Model.Transactions;
using System;

namespace MBCA.src.Model.Transactions
{
    public class Transaction : AbstractTransaction
    {

        public override float Amount { get; set; }
        public override string Comment { get; set; }
        public override string TransactionType { get; set; }
        public override DateTime TransactionTimeUtc { get; set; }
        public override string FromAccount { get; set; }
        public override string ToAccount { get; set; }

        public Transaction(float amount, string comment, string transactionType, DateTime transactionTimeUtc, string fromAccount, string toAccount) :
            base(amount, comment, transactionType, transactionTimeUtc, fromAccount, toAccount)
        {
            this.Amount = amount;
            this.Comment = comment;
            this.TransactionType = transactionType;
            this.TransactionTimeUtc = transactionTimeUtc;
            this.FromAccount = fromAccount;
            this.ToAccount = toAccount;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}