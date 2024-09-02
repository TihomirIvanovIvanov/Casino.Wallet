using Casino.Wallet.Data.Models;

namespace Casino.Wallet.Data.Repositories
{
    public class TransactionRepository
    {
        private readonly Dictionary<int, Transaction> transactions;
        private int nextTransactionId;

        public TransactionRepository()
        {
            this.transactions = new Dictionary<int, Transaction>();
            this.nextTransactionId = 1;
        }

        public void CreateTransaction(Transaction transaction)
        {
            transaction.Id = nextTransactionId++;
            this.transactions.Add(transaction.Id, transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return this.transactions.Values.ToList();
        }

        public List<Transaction> GetAllByWalletId(int walletId)
        {
            return this.transactions.Values.Where(t => t.WalletId == walletId).ToList();
        }

        public Transaction GetById(int id)
        {
            if (!this.transactions.TryGetValue(id, out Transaction? value))
            {
                throw new Exception($"Transaction with ID {id} does not exist.");
            }

            return value;
        }
    }
}
