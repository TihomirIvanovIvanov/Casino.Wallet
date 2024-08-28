using System;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Wallet
{
    public class Program
    {
        public static void Main()
        {
            var serviceProvider = new ServiceRegistration().ConfigureServices(); 
            var commandProcessor = serviceProvider.GetService<CommandProcessor>();

            while (true)
            {
                Console.Write("Please, submit action: ");
                var action = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(action)) 
                    continue;

                var parts = action.Split(' ');
                var command = parts[0].ToLower();

                decimal amount = 0;
                if (parts.Length > 1 && !decimal.TryParse(parts[1], out amount))
                {
                    Console.WriteLine("Invalid amount. Please enter a valid command and amount.");
                    continue;
                }

                commandProcessor.ProcessCommand(command, amount);
            }
        }
    }
}