using Casino.Wallet.Common;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;
using Casino.Wallet.Data.Repositories;

namespace Casino.Wallet.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly TransactionRepository transactionRepository;
        private readonly Data.Models.Wallet wallet;

        public WalletService(TransactionRepository transactionRepository, Player player)
        {
            this.transactionRepository = transactionRepository;
            this.wallet = new Data.Models.Wallet(player);
        }

        public void Deposit(decimal amount, int walletId, TransactionReason transactionReason)
        {
            if (amount <= 0)
            {
                Console.WriteLine(GlobalConstants.NegativeDepositAmountMsg);
                return;
            }
            var transaction = new Transaction(amount, walletId, TransactionType.Deposit, transactionReason);
            this.transactionRepository.CreateTransaction(transaction);
            this.wallet.UpdateBalance(amount);
            if (((TransactionReason)transactionReason).Equals(TransactionReason.Deposit) && amount > 0)
            {
                Console.WriteLine($"Your deposit of ${amount:F2} was successful. {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
        }

        public void Withdraw(decimal amount, int walletId, TransactionReason transactionReason)
        {
            if (amount <= 0)
            {
                Console.WriteLine(GlobalConstants.NegativeWithdrawAmountMsg);
                return;
            }
            if (this.wallet.Player.Balance >= amount)
            {
                var transaction = new Transaction(amount, walletId, TransactionType.Withdraw, transactionReason);
                this.transactionRepository.CreateTransaction(transaction);
                this.wallet.UpdateBalance(-amount);
                if (((TransactionReason)transactionReason).Equals(TransactionReason.Withdraw) && amount > 0)
                {
                    Console.WriteLine($"Your withdrawal of ${amount:F2} was successful! {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
                }
            }
            else
            {
                Console.WriteLine($"{GlobalConstants.InsufficientFundsMsg} {GlobalConstants.CurrentBalanceMsg} ${this.wallet.GetBalance():F2}");
            }
        }

        public void Bet(decimal amount, int walletId, TransactionReason transactionReason)
        {
            this.Withdraw(amount, walletId, transactionReason);
        }

        public void Win(decimal amount, int walletId, TransactionReason transactionReason)
        {
            this.Deposit(amount, walletId, transactionReason);
        }
    }
}
