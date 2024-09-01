namespace Casino.Wallet.Data.Models
{
    public class Wallet
    {
        public Wallet(Player player)
        {
            this.Player = player;
        }

        public int Id { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public decimal Balance
        {
            get => this.Player.Balance;
            private set => this.Player.Balance = value;
        }

        public void UpdateBalance(decimal amount)
        {
            this.Player.Balance += amount;
            this.Balance = this.Player.Balance;
        }

        public decimal GetBalance() { return this.Player.Balance; }
    }
}
