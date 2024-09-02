using Casino.Wallet.Core.Services;
using Casino.Wallet.Data.Models;
using Casino.Wallet.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Wallet.App
{
    public class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var serviceProvider = ServiceRegistration.ConfigureServices(); 
            var commandProcessor = serviceProvider.GetService<CommandProcessor>();
            var player = new Player { Id = 1, Balance = 0, Name = "Betty" };
            var walletService = new WalletService(serviceProvider.GetService<TransactionRepository>(), player);

            while (true)
            {
                Console.WriteLine("Please, submit action:");
                var action = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(action)) 
                    continue;

                var args = action.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var command = args[0].ToLower();

                decimal amount = 0;
                if (args.Length > 1 && !decimal.TryParse(args[1], out amount))
                {
                    Console.WriteLine("Please enter a valid command and amount.");
                    continue;
                }

                commandProcessor?.ProcessCommand(command, amount);
            }
        }
    }
}