namespace Casino.Wallet.Common.Utilities
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random random = new Random();

        public int NextInt(int min, int max)
        {
            return this.random.Next(min, max);
        }
    }
}
