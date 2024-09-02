using Casino.Wallet.App.Commands;
using Casino.Wallet.Common.Utilities;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Core.Services;
using Casino.Wallet.Data.Models;
using Casino.Wallet.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Wallet.App
{
    public class ServiceRegistration
    {
        public static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddScoped<Player>()
                .AddTransient<IRandomNumberGenerator, RandomNumberGenerator>()
                .AddTransient<TransactionRepository>()
                .AddTransient<IWalletService, WalletService>()
                .AddTransient<IBetService, BetService>()
                .AddTransient<CommandProcessor>()
                .AddTransient<DepositCommand>()
                .AddTransient<WithdrawCommand>()
                .AddTransient<BetCommand>()
                .AddTransient(provider => new CommandProcessor(
                    new Dictionary<string, ICommand>
                    {
                        { "deposit", provider.GetService<DepositCommand>() },
                        { "withdraw", provider.GetService<WithdrawCommand>() },
                        { "bet", provider.GetService<BetCommand>() },
                    }))
                .BuildServiceProvider();
        }
    }
}
