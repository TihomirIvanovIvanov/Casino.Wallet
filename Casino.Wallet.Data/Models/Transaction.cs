using Casino.Wallet.Data.Enums;

namespace Casino.Wallet.Data.Models
{
    public class Transaction
    {
        public Transaction(decimal amount, int walletId, TransactionType type, TransactionReason reason)
        {
            this.Amount = amount;
            this.WalletId = walletId;
            this.Type = type;
            this.Reason = reason;
            this.Timestamp = DateTime.Now;
        }

        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int WalletId { get; set; }

        public TransactionType Type { get; set; }

        public TransactionReason Reason { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
