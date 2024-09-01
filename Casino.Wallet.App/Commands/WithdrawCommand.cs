using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.App.Commands
{
    public class WithdrawCommand : ICommand
    {
        private readonly IWalletService walletService;

        public WithdrawCommand(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        public void Execute(decimal amount)
        {
            this.walletService.Withdraw(amount, 1, TransactionReason.Withdraw);
        }
    }
}
