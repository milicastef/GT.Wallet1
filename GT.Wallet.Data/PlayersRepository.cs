using GT.Wallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GT.Wallet.Data
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly List<Player> _players;

        public PlayersRepository()
        {
            _players = new List<Player>();
        }

        public Player FindById(Guid guid)
        {
            return _players.FirstOrDefault(p => p.Guid == guid);
        }

        public Player Create(Guid playerId)
        {
            var player = FindById(playerId);
            if (player == null)
            {
                player = new Player(playerId);
                _players.Add(player);
            }

            return player;
        }
    }
}
