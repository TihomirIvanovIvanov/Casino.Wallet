namespace Casino.Wallet.Common.Utilities
{
    public interface IRandomNumberGenerator
    {
        int Generate(int min, int max);
    }
}
