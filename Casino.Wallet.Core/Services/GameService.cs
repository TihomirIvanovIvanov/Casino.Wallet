using Casino.Wallet.Core.Models;
using Casino.Wallet.Common.Utilities;
using Casino.Wallet.Core.Contracts;

namespace Casino.Wallet.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IRandomNumberGenerator randomNumberGenerator;

        public GameService(IRandomNumberGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        public GameResult Play(decimal betAmount)
        {
            var rng = 90;//this.randomNumberGenerator.Generate(1, 100);

            decimal winAmount = default;

            if (rng <= 50)
            {
                // 50% chance to lose
                winAmount = 0;
            }
            else if (rng <= 90)
            {
                // 40% chance to win up to x2 the bet amount
                winAmount = betAmount * this.randomNumberGenerator.Generate(1, 2);
            }
            else
            {
                // 10% chance to win x2 <> x10 the bet amount
                winAmount = betAmount * 7;//this.randomNumberGenerator.Generate(2, 10);
            }

            return new GameResult(betAmount, winAmount);
        }
    }
}
