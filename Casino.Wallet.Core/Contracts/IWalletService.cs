using Casino.Wallet.Core.Models;

namespace Casino.Wallet.Core.Contracts
{
    public interface IWalletService
    {
        void Deposit(decimal amount);

        bool Withdraw(decimal amount);

        void ApplyGameResult(GameResult result);

        decimal GetBalance();
    }
}
