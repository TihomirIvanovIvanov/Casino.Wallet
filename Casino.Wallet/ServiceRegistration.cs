using Casino.Wallet.Commands;
using Casino.Wallet.Common.Utilities;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Wallet
{
    public class ServiceRegistration
    {
        public ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<IWalletService, WalletService>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IRandomNumberGenerator, RandomNumberGenerator>()
                .AddTransient<CommandProcessor>()
                .AddTransient<DepositCommand>()
                .AddTransient<WithdrawCommand>()
                .AddTransient<BetCommand>()
                .AddTransient<ExitCommand>()
                .AddTransient(provider => new CommandProcessor(
                    new Dictionary<string, ICommand>
                    {
                        { "deposit", provider.GetService<DepositCommand>() },
                        { "withdraw", provider.GetService<WithdrawCommand>() },
                        { "bet", provider.GetService<BetCommand>() },
                        { "exit", provider.GetService<ExitCommand>() }
                    }))
                .BuildServiceProvider();
        }
    }
}
