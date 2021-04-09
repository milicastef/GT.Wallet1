using GT.Wallet.Data;
using GT.Wallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GT.Wallet.Tests
{
    public class FakePlayersRepository : IPlayersRepository
    {
        private readonly List<Player> _players;

        public FakePlayersRepository()
        {
            _players = new List<Player>
            {
                new(new Guid("73efa0dd-3a77-40c8-96bb-e6fdb4433413")),
                new(new Guid("58af6f10-134f-4886-87dc-9e0fe80bc70e")),
                new(new Guid("dfe6a616-20c0-4f74-9b2a-0b5dd358a07f"))
            };
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
