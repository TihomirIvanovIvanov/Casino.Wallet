using Casino.Wallet.Common;
using Casino.Wallet.Common.Utilities;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;

namespace Casino.Wallet.App.Commands
{
    public class BetCommand : ICommand
    {
        private readonly IBetService betService;
        private readonly Data.Models.Wallet wallet;
        private readonly IRandomNumberGenerator rnd;

        public BetCommand(IBetService betService, Player player, IRandomNumberGenerator randomNumberGenerator)
        {
            this.betService = betService;
            this.wallet = new Data.Models.Wallet(player);
            this.rnd = randomNumberGenerator;
        }

        public void Execute(decimal betAmount)
        {
            if (this.wallet.GetBalance() <= 0)
            {
                Console.WriteLine(GlobalConstants.InsufficientFundsMsg + " Please, deposit!");
                return;
            }
            if (this.wallet.GetBalance() < betAmount)
            {
                Console.WriteLine(GlobalConstants.InsufficientFundsMsg + $" {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
                return;
            }
            if (betAmount < GlobalConstants.MinBet || betAmount > GlobalConstants.MaxBet)
            {
                Console.WriteLine($"Invalid bet amount. Please bet between ${GlobalConstants.MinBet:F2} and ${GlobalConstants.MaxBet:F2}.");
                return;
            }
            this.betService.PlaceBet(betAmount, 1, TransactionReason.Bet);

            int chanceToWin = this.rnd.NextInt(1, 101);

            if (chanceToWin <= GlobalConstants.LoseChance)
            {
                Console.WriteLine($"{GlobalConstants.NoLuckMsg} {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
            else if (chanceToWin <= GlobalConstants.LoseChance + GlobalConstants.DoubleChance)
            {
                this.wallet.WinAmount = betAmount * 2;
                this.betService.PlaceWin(this.wallet.WinAmount, 1, TransactionReason.Win);
                Console.WriteLine($"{GlobalConstants.CongratsMsg} ${this.wallet.WinAmount:F2}! {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
            else
            {
                var multiplier = this.rnd.NextInt(GlobalConstants.MinBigWinMultiplier, GlobalConstants.MaxBigWinMultiplier);
                this.wallet.WinAmount = betAmount * multiplier;
                this.betService.PlaceWin(this.wallet.WinAmount, 1, TransactionReason.Win);
                Console.WriteLine($"{GlobalConstants.CongratsMsg} ${this.wallet.WinAmount:F2}! {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
        }
    }
}
