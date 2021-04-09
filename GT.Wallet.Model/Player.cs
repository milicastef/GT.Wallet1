using System;

namespace GT.Wallet.Model
{
    public class Player
    {
        public Guid Guid { get; }
        public Wallet Wallet { get; private set; }

        private Player()
        {
            Guid = Guid.NewGuid();
        }

        public Player(Guid guid)
        {
            Guid = guid;
        }
        public bool Register()
        {
            if (Wallet != null)
            {
                return false;
            }

            Wallet = new Wallet();
            return true;
        }
    }
}
