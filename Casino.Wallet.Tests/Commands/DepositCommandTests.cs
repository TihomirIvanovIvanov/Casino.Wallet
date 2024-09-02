using Casino.Wallet.App.Commands;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Moq;

namespace Casino.Wallet.Tests.Commands
{
    public class DepositCommandTests
    {
        private readonly DepositCommand depositCmd;
        private readonly Mock<IWalletService> walletServiceMock;

        private const decimal Amount = 10m;
        private const int WalletId = 1;

        public DepositCommandTests()
        {
            this.walletServiceMock = new Mock<IWalletService>();
            this.depositCmd = new DepositCommand(walletServiceMock.Object);
        }

        [Fact]
        public void DepositCommand_ShouldCallWalletServiceDepositMethodOnes()
        {
            // Act
            this.depositCmd.Execute(Amount);

            // Assert
            this.walletServiceMock.Verify(service =>
                service.Deposit(Amount, WalletId, TransactionReason.Deposit), Times.Once);
        }
    }
}
