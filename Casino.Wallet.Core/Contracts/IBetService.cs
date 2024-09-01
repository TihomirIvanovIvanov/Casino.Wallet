using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.Core.Contracts
{
    public interface IBetService
    {
        void PlaceBet(decimal amount, int walletId, TransactionReason transactionReason);

        void PlaceWin(decimal amount, int walletId, TransactionReason transactionReason);
    }
}
