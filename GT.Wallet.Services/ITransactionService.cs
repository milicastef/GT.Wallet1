using System.Threading.Tasks;

namespace GT.Wallet.Services
{
    public interface ITransactionService
    {
        Task<bool> Process(TransactionRequest transactionRequest);
    }
}
