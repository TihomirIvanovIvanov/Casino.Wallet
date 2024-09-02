using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;
using Casino.Wallet.Data.Repositories;

namespace Casino.Wallet.Tests.Repositories
{
    public class TransactionRepositoryTests
    {
        private readonly TransactionRepository transactionRepo;

        private const decimal Amount = 10m;
        private const int WalletId = 1;
        public TransactionRepositoryTests()
        {
            this.transactionRepo = new TransactionRepository();
        }

        [Fact]
        public void CreateTransaction_ShouldStoreTransactionCorrectly()
        {
            // Arrange
            var transactionReason = TransactionReason.Deposit;
            var transactionType = TransactionType.Deposit;

            // Act
            var transaction = new Transaction(Amount, WalletId, transactionType, transactionReason);
            this.transactionRepo.CreateTransaction(transaction);

            // Assert
            var transactions = this.transactionRepo.GetTransactions();
            Assert.Single(transactions);
            Assert.Equal(1, transactions[0].Id);
            Assert.Equal(Amount, transactions[0].Amount);
            Assert.Equal(WalletId, transactions[0].WalletId);
            Assert.Equal(transactionType, transactions[0].Type);
            Assert.Equal(transactionReason, transactions[0].Reason);
        }

        [Fact]
        public void CreateTransaction_ShouldAutoIncrementTransactionId()
        {
            // Arrange
            var transactionReason = TransactionReason.Deposit;
            var transactionType = TransactionType.Deposit;

            // Act
            var transaction1 = new Transaction(Amount, WalletId, transactionType, transactionReason);
            var transaction2 = new Transaction(Amount, WalletId, transactionType, transactionReason);
            this.transactionRepo.CreateTransaction(transaction1);
            this.transactionRepo.CreateTransaction(transaction2);

            // Assert
            var transactions = this.transactionRepo.GetTransactions();
            Assert.Equal(2, transactions.Count);
            Assert.Equal(1, transactions[0].Id);
            Assert.Equal(2, transactions[1].Id);
        }

        [Fact]
        public void GetTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var depositReason = TransactionReason.Deposit;
            var withdrawReason = TransactionReason.Withdraw;
            var depositType = TransactionType.Deposit;
            var withdrawType = TransactionType.Withdraw;

            var depositTransaction = new Transaction(Amount, WalletId, depositType, depositReason);
            var withdrawTransaction = new Transaction(Amount, WalletId, withdrawType, withdrawReason);

            // Act
            this.transactionRepo.CreateTransaction(depositTransaction);
            this.transactionRepo.CreateTransaction(withdrawTransaction);

            // Assert
            var transactions = this.transactionRepo.GetTransactions();
            Assert.Equal(2, transactions.Count);

            Assert.Equal(Amount, transactions[0].Amount);
            Assert.Equal(WalletId, transactions[0].WalletId);
            Assert.Equal(depositType, transactions[0].Type);
            Assert.Equal(depositReason, transactions[0].Reason);
            Assert.Equal(1, transactions[0].Id);

            Assert.Equal(Amount, transactions[1].Amount);
            Assert.Equal(WalletId, transactions[1].WalletId);
            Assert.Equal(withdrawType, transactions[1].Type);
            Assert.Equal(withdrawReason, transactions[1].Reason);
            Assert.Equal(2, transactions[1].Id);
        }

        [Fact]
        public void GetAllByWalletId_ShouldReturnAllTransactionsForGivenWalletId()
        {
            // Arrange
            var transaction1 = new Transaction(Amount, WalletId, TransactionType.Deposit, TransactionReason.Deposit);
            var transaction2 = new Transaction(Amount, WalletId, TransactionType.Withdraw, TransactionReason.Bet);
            var transaction3 = new Transaction(Amount, WalletId + 1, TransactionType.Deposit, TransactionReason.Win);

            this.transactionRepo.CreateTransaction(transaction1);
            this.transactionRepo.CreateTransaction(transaction2);
            this.transactionRepo.CreateTransaction(transaction3);

            // Act
            var result = this.transactionRepo.GetAllByWalletId(WalletId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(transaction1, result);
            Assert.Contains(transaction2, result);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectTransactionWhenExists()
        {
            // Arrange
            var transaction = new Transaction(Amount, WalletId, TransactionType.Deposit, TransactionReason.Deposit);
            this.transactionRepo.CreateTransaction(transaction);

            // Act
            var result = this.transactionRepo.GetById(transaction.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Equal(transaction.Amount, result.Amount);
            Assert.Equal(transaction.WalletId, result.WalletId);
            Assert.Equal(transaction.Type, result.Type);
            Assert.Equal(transaction.Reason, result.Reason);
        }

        [Fact]
        public void GetById_ShouldThrowExceptionWhenTransactionDoesNotExist()
        {
            // Arrange
            int nonExistentId = 999;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() =>
                this.transactionRepo.GetById(nonExistentId));
            Assert.Equal($"Transaction with ID {nonExistentId} does not exist.", exception.Message);
        }
    }
}
