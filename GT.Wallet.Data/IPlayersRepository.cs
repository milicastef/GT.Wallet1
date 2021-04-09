using GT.Wallet.Model;
using System;

namespace GT.Wallet.Data
{
    public interface IPlayersRepository
    {
        Player FindById(Guid guid);
        Player Create(Guid playerId);
    }
}
