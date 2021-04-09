using GT.Wallet.Model.Enums;
using System;

namespace GT.Wallet.Services
{
    public class TransactionRequest
    {
        public TransactionRequest(Guid playerId, Guid transactionId, decimal amount, TransactionType type)
        {
            PlayerId = playerId;
            TransactionId = transactionId;
            Amount = amount;
            TransactionType = type;
        }

        public Guid TransactionId { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
