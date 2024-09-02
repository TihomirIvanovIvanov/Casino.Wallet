using Casino.Wallet.App.Commands;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Moq;

namespace Casino.Wallet.Tests.Commands
{
    public class WithdrawCommandTests
    {
        private readonly Mock<IWalletService> walletServiceMock;
        private readonly WithdrawCommand withdrawCmd;

        private const decimal Amount = 10m;
        private const int WalletId = 1;

        public WithdrawCommandTests()
        {
            this.walletServiceMock = new Mock<IWalletService>();
            this.withdrawCmd = new WithdrawCommand(this.walletServiceMock.Object);
        }

        [Fact]
        public void WithdrawCommand_ShouldCallWalletServiceWithdrawMethodOnes()
        {
            // Act
            this.withdrawCmd.Execute(Amount);

            // Assert
            this.walletServiceMock.Verify(service =>
                service.Withdraw(Amount, WalletId, TransactionReason.Withdraw), Times.Once);
        }
    }
}
