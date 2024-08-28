using Casino.Wallet.Core.Contracts;

namespace Casino.Wallet.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute(decimal amount)
        {
            Console.WriteLine("Thank you for playing! Hope to see you soon.");
            Environment.Exit(0);
        }
    }
}
