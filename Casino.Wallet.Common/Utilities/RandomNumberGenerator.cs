namespace Casino.Wallet.Common.Utilities
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random random = new Random();

        public int Generate(int min, int max)
        {
            // max + 1 because max is exlusive and never gonna be reacht
            return this.random.Next(min, max + 1);
        }
    }
}
