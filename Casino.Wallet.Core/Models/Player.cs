namespace Casino.Wallet.Core.Models
{
    public class Player
    {
        public Player()
        {
            this.Balance = default;
        }

        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
                return true;
            }
            return false;
        }

        public void ApplyGameResult(GameResult result)
        {
            this.Balance += result.NetChange;
        }
    }
}
