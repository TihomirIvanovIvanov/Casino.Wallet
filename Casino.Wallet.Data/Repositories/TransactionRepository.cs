using Casino.Wallet.Data.Models;

namespace Casino.Wallet.Data.Repositories
{
    public class TransactionRepository
    {
        private readonly Dictionary<int, Transaction> transactions;
        private int nextTransactionId;

        public TransactionRepository()
        {
            transactions = new Dictionary<int, Transaction>();
            nextTransactionId = 1;
        }

        public void CreateTransaction(Transaction transaction)
        {
            transaction.Id = nextTransactionId++;
            transactions.Add(transaction.Id, transaction);
        }

        public List<Transaction> GetTransactions()
        {
            return transactions.Values.ToList();
        }

        public List<Transaction> GetAllByWalletId(int walletId)
        {
            return transactions.Values.Where(t => t.WalletId == walletId).ToList();
        }

        public Transaction GetById(int id)
        {
            if (!transactions.TryGetValue(id, out Transaction? value))
            {
                throw new Exception($"Transaction with ID {id} does not exist.");
            }

            return value;
        }
    }
}
