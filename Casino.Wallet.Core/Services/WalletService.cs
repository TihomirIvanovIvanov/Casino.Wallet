using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Core.Models;

namespace Casino.Wallet.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly Player wallet;

        public WalletService()
        {
            this.wallet = new Player();
        }

        public void Deposit(decimal amount)
        {
            this.wallet.Deposit(amount);
        }

        public bool Withdraw(decimal amount)
        {
            return this.wallet.Withdraw(amount);
        }

        public void ApplyGameResult(GameResult result)
        {
            this.wallet.ApplyGameResult(result);
        }

        public decimal GetBalance()
        {
            return this.wallet.Balance;
        }
    }
}
