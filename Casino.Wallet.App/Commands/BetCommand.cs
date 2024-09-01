using Casino.Wallet.Common;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;

namespace Casino.Wallet.App.Commands
{
    public class BetCommand : ICommand
    {
        private readonly IBetService betService;
        private readonly Data.Models.Wallet wallet;
        private readonly Random random;

        public BetCommand(IBetService betService, Player player)
        {
            this.betService = betService;
            this.wallet = new Data.Models.Wallet(player);
            this.random = new Random();
        }

        public void Execute(decimal betAmount)
        {
            if (this.wallet.GetBalance() <= 0)
            {
                Console.WriteLine(GlobalConstants.InsufficientFundsMsg + " Please, deposit!");
                return;
            }
            this.betService.PlaceBet(betAmount, 1, TransactionReason.Bet);

            int chanceToWin = this.random.Next(1, 101);
            decimal winAmount = 0m;

            if (chanceToWin <= GlobalConstants.LoseChance)
            {
                Console.WriteLine($"{GlobalConstants.NoLuckMsg} {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
            else if (chanceToWin <= GlobalConstants.LoseChance + GlobalConstants.DoubleChance)
            {
                winAmount = betAmount * 2;
                this.betService.PlaceWin(winAmount, 1, TransactionReason.Win);
                Console.WriteLine($"{GlobalConstants.CongratsMsg} ${winAmount:F2}! {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
            else
            {
                var multiplier = this.random.Next(GlobalConstants.MinBigWinMultiplier, GlobalConstants.MaxBigWinMultiplier);
                winAmount = betAmount * multiplier;
                this.betService.PlaceWin(winAmount, 1, TransactionReason.Win);
                Console.WriteLine($"{GlobalConstants.CongratsMsg} ${winAmount:F2}! {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
        }
    }
}
