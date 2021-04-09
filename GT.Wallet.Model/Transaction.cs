using System;
using TransactionType = GT.Wallet.Model.Enums.TransactionType;

namespace GT.Wallet.Model
{

    public class Transaction
    {
        public Transaction(Guid transactionId, Wallet wallet, decimal amount, TransactionType type)
        {
            Guid = transactionId;
            Amount = amount;
            Type = type;
            Wallet = wallet;
        }

        public Guid Guid { get; }
        public TransactionType Type { get; }
        public decimal Amount { get; }
        public Wallet Wallet { get; }
        public bool? Accepted { get; private set; }

        public void Execute()
        {
            lock (Wallet)
            {
                Wallet.Execute(this);
            }
        }

        public void Accept()
        {
            Accepted = true;
        }
        public void Reject()
        {
            Accepted = false;
        }
    }
}
