namespace Casino.Wallet.Core.Contracts
{
    public interface ICommand
    {
        void Execute(decimal amount);
    }
}
