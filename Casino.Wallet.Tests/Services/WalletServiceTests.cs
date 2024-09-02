using Casino.Wallet.Common;
using Casino.Wallet.Core.Services;
using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;
using Casino.Wallet.Data.Repositories;
using Moq;

namespace Casino.Wallet.Tests.Services
{
    public class WalletServiceTests
    {
        private readonly Mock<TransactionRepository> transactionRepoMock;
        private readonly WalletService walletService;
        private readonly Player player;
        private readonly StringWriter stringWriter;

        public WalletServiceTests()
        {
            this.transactionRepoMock = new Mock<TransactionRepository>();
            this.player = new Player { Id = 1, Balance = 100m, Name = "Betty" };
            this.walletService = new WalletService(transactionRepoMock.Object, player);
            this.stringWriter = new StringWriter();
            Console.SetOut(this.stringWriter);
        }

        [Fact]
        public void Deposit_WithNegativeAmount_ShouldNotProceed()
        {
            // Act
            this.walletService.Deposit(-10m, 1, TransactionReason.Deposit);

            // Assert
            var actual = this.stringWriter.ToString().Trim();
            Assert.Contains(GlobalConstants.NegativeDepositAmountMsg, actual);
        }

        [Fact]
        public void Deposit_WithZeroAmount_ShouldNotProceed()
        {
            // Act
            this.walletService.Deposit(0m, 1, TransactionReason.Deposit);

            // Assert
            var actual = this.stringWriter.ToString().Trim();
            Assert.Contains(GlobalConstants.NegativeDepositAmountMsg, actual);
        }

        [Fact]
        public void Deposit_PositiveAmount_ShouldUpdateBalanceAndPrintMessage()
        {
            // Arrange
            decimal depositAmount = 50m;

            // Act
            this.walletService.Deposit(depositAmount, 1, TransactionReason.Deposit);

            // Assert
            var expected = $"Your deposit of ${depositAmount:F2} was successful. {GlobalConstants.CurrentBalanceMsg} ${this.player.Balance:F2}";
            var actual = this.stringWriter.ToString().Trim();
            Assert.Contains(expected, actual);
        }

        [Fact]
        public void Deposit_ShouldNotChangeBalance_WhenAmountIsNegative()
        {
            // Act
            this.walletService.Deposit(-10m, 1, TransactionReason.Deposit);

            // Assert
            Assert.Equal(100m, this.player.Balance);
        }

        [Fact]
        public void Withdraw_ShouldDecreasePlayerBalance_WhenAmountIsValid()
        {
            // Act
            this.walletService.Withdraw(30m, 1, TransactionReason.Withdraw);

            // Assert
            Assert.Equal(70m, this.player.Balance);
        }

        [Fact]
        public void Withdraw_ShouldNotChangeBalance_WhenAmountIsMoreThanBalance()
        {
            // Act
            this.walletService.Withdraw(150m, 1, TransactionReason.Withdraw);

            // Assert
            Assert.Equal(100m, this.player.Balance);
        }

        [Fact]
        public void Withdraw_WithNegativeAmount_ShouldNotProceed()
        {
            // Act
            this.walletService.Withdraw(-10m, 1, TransactionReason.Withdraw);

            // Assert
            var actual = this.stringWriter.ToString().Trim();
            Assert.Contains(GlobalConstants.NegativeWithdrawAmountMsg, actual);
        }

        [Fact]
        public void Withdraw_WithZeroAmount_ShouldNotProceed()
        {
            // Act
            this.walletService.Withdraw(0m, 1, TransactionReason.Withdraw);

            // Assert
            var actual = this.stringWriter.ToString().Trim();
            Assert.Contains(GlobalConstants.NegativeWithdrawAmountMsg, actual);
        }

        [Fact]
        public void Withdraw_AmountIsWithinBalance_ShouldAllowWithdrawal()
        {
            // Arrange
            decimal withdrawalAmount = 50m;

            // Act
            this.walletService.Withdraw(withdrawalAmount, 1, TransactionReason.Withdraw);

            // Assert
            var expected = $"Your withdrawal of ${withdrawalAmount:F2} was successful! {GlobalConstants.CurrentBalanceMsg} ${this.player.Balance:F2}";
            var actual = this.stringWriter.ToString().Trim();
            Assert.Equal(50m, this.player.Balance);
            Assert.Contains(expected, actual);
        }

        [Fact]
        public void Bet_ShouldDecreaseBalance_WhenBetAmountIsValid()
        {
            // Act
            this.walletService.Bet(20m, 1, TransactionReason.Bet);

            // Assert
            Assert.Equal(80m, this.player.Balance);
        }

        [Fact]
        public void Win_ShouldIncreaseBalance_WhenWinAmountIsValid()
        {
            // Act
            this.walletService.Win(100m, 1, TransactionReason.Win);

            // Assert
            Assert.Equal(200m, this.player.Balance);
        }
    }
}
