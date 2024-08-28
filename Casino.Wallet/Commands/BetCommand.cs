using Casino.Wallet.Core.Contracts;
using System;

namespace Casino.Wallet.Commands
{
    public class BetCommand : ICommand
    {
        private readonly IWalletService walletService;
        private readonly IGameService gameService;

        public BetCommand(IWalletService walletService, IGameService gameService)
        {
            this.walletService = walletService;
            this.gameService = gameService;
        }

        public void Execute(decimal amount)
        {
            var result = this.gameService.Play(amount);
            this.walletService.ApplyGameResult(result);

            if (result.WinAmount > 0)
            {
                Console.WriteLine($"Congrats - you won ${result.WinAmount:F2}! Your current balance is: ${this.walletService.GetBalance():F2}");
            }
            else
            {
                Console.WriteLine($"No luck this time! Your current balance is: ${this.walletService.GetBalance():F2}");
            }
        }
    }
}
