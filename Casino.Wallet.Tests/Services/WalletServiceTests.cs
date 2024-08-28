using Casino.Wallet.Core.Services;
using Xunit;

namespace Casino.Wallet.Tests.Services
{
    public class WalletServiceTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalance()
        {
            // Arrange
            var walletService = new WalletService();

            // Act
            walletService.Deposit(10);

            // Assert
            Assert.Equal(10, walletService.GetBalance());
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance()
        {
            // Arrange
            var walletService = new WalletService();
            walletService.Deposit(10);

            // Act
            var result = walletService.Withdraw(5);

            // Assert
            Assert.True(result);
            Assert.Equal(5, walletService.GetBalance());
        }

        [Fact]
        public void Withdraw_InsufficientFunds_ShouldFail()
        {
            // Arrange
            var walletService = new WalletService();

            // Act
            var result = walletService.Withdraw(10);

            // Assert
            Assert.False(result);
            Assert.Equal(0, walletService.GetBalance());
        }
    }
}
