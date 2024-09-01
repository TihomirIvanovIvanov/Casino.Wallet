using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.App.Commands
{
    public class DepositCommand : ICommand
    {
        private readonly IWalletService walletService;

        public DepositCommand(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        public void Execute(decimal amount)
        {
            this.walletService.Deposit(amount, 1, TransactionReason.Deposit);
        }
    }
}
