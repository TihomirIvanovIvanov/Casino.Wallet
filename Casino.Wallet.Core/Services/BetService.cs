using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.Core.Services
{
    public class BetService : IBetService
    {
        private IWalletService walletService;

        public BetService(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        public void PlaceBet(decimal amount, int walletId, TransactionReason transactionReason)
        {
            this.walletService.Bet(amount, walletId, transactionReason);
        }

        public void PlaceWin(decimal amount, int walletId, TransactionReason transactionReason)
        {
            this.walletService.Win(amount, walletId, transactionReason);
        }
    }
}
