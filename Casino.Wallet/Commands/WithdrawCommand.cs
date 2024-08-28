using Casino.Wallet.Core.Contracts;
using System;

namespace Casino.Wallet.Commands
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
            if (this.walletService.Withdraw(amount))
            {
                Console.WriteLine($"Your withdrawal of ${amount:F2} was successful. Your current balance is: ${this.walletService.GetBalance():F2}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
    }
}
