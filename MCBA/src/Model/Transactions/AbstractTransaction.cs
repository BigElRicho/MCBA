using System;

namespace MCBA.src.Model.Transactions
{
    public abstract class AbstractTransaction
    {
        public abstract float Amount { get; set; }
        public abstract string Comment { get; set; }
        public abstract string TransactionType { get; set; }
        public abstract DateTime TransactionTimeUtc { get; set; }
        public abstract string FromAccount { get; set; }
        public abstract string ToAccount { get; set; }

        public AbstractTransaction(float amount, string comment, string transactionType, DateTime transactionTimeUtc, string fromAccount, string toAccount)
        {
            this.Amount = amount;
            this.Comment = comment;
            this.TransactionType = transactionType;
            this.TransactionTimeUtc = transactionTimeUtc;
            this.FromAccount = fromAccount;
            this.ToAccount = toAccount;
        }

    }
}