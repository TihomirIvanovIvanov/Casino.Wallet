using Casino.Wallet.Core.Models;

namespace Casino.Wallet.Core.Contracts
{
    public interface IGameService
    {
        GameResult Play(decimal betAmount);
    }
}
