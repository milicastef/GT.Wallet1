using GT.Wallet.Data;
using GT.Wallet.Model.Enums;
using GT.Wallet.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GT.Wallet.Tests
{
    [TestFixture]
    public class WalletTests
    {
        private ITransactionService _transactionService;
        private IPlayersRepository _playersRepository;
        private Guid _testPlayerId;

        [SetUp]
        public void SetUp()
        {
            _playersRepository = new FakePlayersRepository();
            Guid.TryParse("73efa0dd-3a77-40c8-96bb-e6fdb4433413", out Guid guid);
            _testPlayerId = guid;
            _transactionService = new TransactionService(_playersRepository);
        }

        [Test]
        public async Task Credit_ShouldReturnAccepted()
        {
            //Arrange
            var player = _playersRepository.FindById(_testPlayerId);
            player.Register();
            var request = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Deposit);

            //Act
            var actual = await _transactionService.Process(request);

            //Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public async Task Deposit_NegativeAmount_ShouldReturnRejected()
        {
            var request = new TransactionRequest(_testPlayerId, Guid.NewGuid(), -50, TransactionType.Deposit);
            Assert.IsFalse(await _transactionService.Process(request));
        }


        [Test]
        public async Task ConsistentWallet_Test()
        {
            var player = _playersRepository.FindById(_testPlayerId);
            player.Register();

            var request1 = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Deposit);
            var request2 = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Deposit);
            var request3 = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Deposit);
            var request4 = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Stake);
            var request5 = new TransactionRequest(_testPlayerId, Guid.NewGuid(), 100, TransactionType.Stake);

            await Task.WhenAll(
                _transactionService.Process(request1),
                _transactionService.Process(request2),
                _transactionService.Process(request3),
                _transactionService.Process(request4),
                _transactionService.Process(request5));

            Assert.AreEqual(100, player.Wallet.Balance);
        }
    }
}
