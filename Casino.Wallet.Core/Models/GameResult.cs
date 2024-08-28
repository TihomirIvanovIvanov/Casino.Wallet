namespace Casino.Wallet.Core.Models
{
    public class GameResult
    {
        public decimal BetAmount { get; }

        public decimal WinAmount { get; }

        public decimal NetChange => WinAmount - BetAmount;

        public GameResult(decimal betAmount, decimal winAmount)
        {
            this.BetAmount = betAmount;
            this.WinAmount = winAmount;
        }
    }
}
