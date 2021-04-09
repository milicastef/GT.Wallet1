using GT.Wallet.Model.Enums;
using System;
using System.Collections.Concurrent;

namespace GT.Wallet.Model
{
    public class Wallet
    {
        private decimal _balance;
        private readonly ConcurrentDictionary<Guid, Transaction> _transactionsAudit = new();
        public decimal Balance => _balance;

        internal Wallet()
        {
            _balance = Decimal.Zero;
        }

        public void Execute(Transaction transaction)
        {
            if (IsExistingTransaction(transaction))
            {
                return;
            }

            try
            {
                Process(transaction);
                transaction.Accept();
            }
            catch (Exception)
            {
                transaction.Reject();
            }
            finally
            {
                _transactionsAudit.TryAdd(transaction.Guid, transaction);
            }
        }

        private void Process(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Deposit:
                    {
                        Deposit(transaction);
                        break;
                    }
                case TransactionType.Stake:
                    {
                        Stake(transaction);
                        break;
                    }
                case TransactionType.Win:
                    {
                        Win(transaction);
                        break;
                    }
            }
        }

        private bool IsExistingTransaction(Transaction transaction)
        {
            return _transactionsAudit.TryGetValue(transaction.Guid, out _);
        }

        private void Deposit(Transaction transaction)
        {
            AddMoney(transaction.Amount);
        }

        private void Stake(Transaction transaction)
        {
            if (transaction.Amount <= 0)
            {
                throw new InvalidOperationException("Transaction amount is negative");
            }

            if (transaction.Amount > Balance)
            {
                throw new InvalidOperationException("Not enough money");
            }

            _balance -= transaction.Amount;
        }

        private void Win(Transaction transaction)
        {
            AddMoney(transaction.Amount);
        }
        private void AddMoney(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Amount must be positive");
            }

            _balance += amount;
        }
    }
}
