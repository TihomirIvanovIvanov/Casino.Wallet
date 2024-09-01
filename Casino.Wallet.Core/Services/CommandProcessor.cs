using Casino.Wallet.Common;
using Casino.Wallet.Core.Contracts;

namespace Casino.Wallet.Core.Services
{
    public class CommandProcessor
    {
        private readonly Dictionary<string, ICommand> commands;

        public CommandProcessor(Dictionary<string, ICommand> commands)
        {
            this.commands = commands;
        }

        public void ProcessCommand(string command, decimal amount)
        {
            if (commands.TryGetValue(command.ToLower(), out var cmd))
            {
                cmd.Execute(amount);
            }
            else
            {
                if (command == "exit")
                {
                    Console.WriteLine(GlobalConstants.GoodbyeMsg);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine(GlobalConstants.UnknownCmd);
                }
            }
        }
    }
}
