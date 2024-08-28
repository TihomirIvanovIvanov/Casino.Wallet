using Casino.Wallet.Core.Contracts;
using System;

namespace Casino.Wallet.Commands
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
            this.walletService.Deposit(amount);
            Console.WriteLine($"Your deposit of ${amount:F2} was successful. Your current balance is: ${this.walletService.GetBalance():F2}");
        }
    }
}
