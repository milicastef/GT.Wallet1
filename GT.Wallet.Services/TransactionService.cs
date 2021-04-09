using GT.Wallet.Data;
using GT.Wallet.Model;
using GT.Wallet.Model.Enums;
using System.Threading.Tasks;

namespace GT.Wallet.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IPlayersRepository _playersRepository;

        public TransactionService(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }

        public async Task<bool> Process(TransactionRequest transactionRequest)
        {
            var player = _playersRepository.FindById(transactionRequest.PlayerId);
            if (player?.Wallet == null)
            {
                return false;
            }

            var transaction = new Transaction(transactionRequest.TransactionId, player.Wallet,
                transactionRequest.Amount, transactionRequest.TransactionType);
            transaction.Execute();
            return transaction.Accepted != null && transaction.Accepted.Value;
        }
    }
}
