using GT.Wallet.Data;
using GT.Wallet.Model.Enums;
using GT.Wallet.Services;
using GT.Wallet.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;


namespace GT.Wallet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITransactionService _transactionService;
        public WalletsController(IPlayersRepository playersRepository, ITransactionService transactionService)
        {
            _playersRepository = playersRepository;
            _transactionService = transactionService;
        }


        [HttpGet]
        [Route("{playerId}/balance")]
        public ActionResult<decimal> GetBalance(Guid playerId)
        {
            var player = _playersRepository.FindById(playerId);
            return player?.Wallet != null ? Ok(player.Wallet.Balance) : NotFound();
        }

        [HttpPut]
        [Route("credit")]
        public ActionResult Credit(CreditRequest request)
        {
            var accepted = _transactionService.Process(new TransactionRequest(request.PlayerId, request.TransactionId,
                request.Amount, TransactionType.Deposit));
            return Ok(accepted);
        }

        [HttpPost]
        [Route("{playerId}/register")]
        public ActionResult Register(Guid playerId)
        {
            var player = _playersRepository.Create(playerId);
            return player.Register() ? Ok() : BadRequest("Wallet already created.");
        }
    }
}
