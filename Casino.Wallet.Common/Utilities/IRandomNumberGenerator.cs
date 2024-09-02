namespace Casino.Wallet.Common.Utilities
{
    public interface IRandomNumberGenerator
    {
        int NextInt(int min, int max);
    }
}
