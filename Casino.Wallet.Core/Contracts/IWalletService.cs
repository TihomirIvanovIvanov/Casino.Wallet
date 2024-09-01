using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.Core.Contracts
{
    public interface IWalletService
    {
        void Deposit(decimal amount, int walletId, TransactionReason transactionReason);

        void Withdraw(decimal amount, int walletId, TransactionReason transactionReason);

        void Bet(decimal amount, int walletId, TransactionReason transactionReason);

        void Win(decimal amount, int walletId, TransactionReason transactionReason);
    }
}
