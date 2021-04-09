using System;

namespace GT.Wallet.WebApi.Models
{
    public class CreditRequest
    {
        public Guid TransactionId { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
    }
}
