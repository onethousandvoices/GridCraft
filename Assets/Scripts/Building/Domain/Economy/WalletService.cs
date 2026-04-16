using System;

namespace GridCraft.Construction.Runtime
{
    public sealed class WalletService
    {
        public event Action Changed;

        public int Balance { get; private set; }

        public WalletService(int balance) => Balance = balance;

        public bool CanSpend(int amount) => Balance >= amount;

        public bool TrySpend(int amount)
        {
            if (!CanSpend(amount)) return false;

            Balance -= amount;
            Changed?.Invoke();
            return true;
        }
    }
}