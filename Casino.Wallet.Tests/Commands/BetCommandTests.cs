using Casino.Wallet.App.Commands;
using Casino.Wallet.Common;
using Casino.Wallet.Common.Utilities;
using Casino.Wallet.Core.Contracts;
using Casino.Wallet.Data.Enums;
using Casino.Wallet.Data.Models;
using Moq;

namespace Casino.Wallet.Tests.Commands
{
    public class BetCommandTests
    {
        private readonly Mock<IBetService> betServiceMock;
        private readonly Player player;
        private readonly BetCommand betCmd;
        private readonly Mock<IRandomNumberGenerator> rnd;

        private readonly StringWriter stringWriter;
        private const decimal Amount = 10m;
        private const int WalletId = 1;

        public BetCommandTests()
        {
            this.betServiceMock = new Mock<IBetService>();
            this.rnd = new Mock<IRandomNumberGenerator>();
            this.player = new Player { Id = 1, Balance = 100m, Name = "Betty" };
            this.betCmd = new BetCommand(this.betServiceMock.Object, this.player, this.rnd.Object);

            this.stringWriter = new StringWriter();
            Console.SetOut(this.stringWriter);
        }

        [Fact]
        public void BetCommand_ShouldCallBetServicePlaceBetMethodWithBetReason()
        {
            // Act
            this.betCmd.Execute(Amount);

            // Assert
            this.betServiceMock.Verify(service =>
                service.PlaceBet(Amount, WalletId, TransactionReason.Bet), Times.Once);
        }

        [Fact]
        public void BetCommand_ShouldCallBetServicePlaceBetMethodAndPrintNoLuckMessage()
        {
            // Arrange
            this.rnd.Setup(rng => rng.NextInt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GlobalConstants.LoseChance);

            // Act
            this.betCmd.Execute(Amount);

            // Assert
            var expected = $"{GlobalConstants.NoLuckMsg} {GlobalConstants.CurrentBalanceMsg} ${this.player.Balance:F2}";
            var actual = this.stringWriter.ToString().Trim();
            this.betServiceMock.Verify(service =>
                service.PlaceBet(Amount, WalletId, TransactionReason.Bet), Times.Once);
        }

        [Fact]
        public void BetCommand_ShouldDoubleBetAmountWhenWinDouble()
        {
            // Arrange
            this.rnd.Setup(rng => rng.NextInt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GlobalConstants.LoseChance + 1);

            // Act
            this.betCmd.Execute(Amount);

            // Assert
            this.betServiceMock.Verify(service =>
                service.PlaceWin(Amount * 2, WalletId, TransactionReason.Win), Times.Once);
        }

        [Fact]
        public void BetCommand_ShouldWinWithMultiplierWhenHighWin()
        {
            // Arrange
            this.rnd.SetupSequence(rng => rng.NextInt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GlobalConstants.LoseChance + GlobalConstants.DoubleChance + 1)
                .Returns(5);

            // Act
            this.betCmd.Execute(Amount);

            // Assert
            this.betServiceMock.Verify(service =>
                service.PlaceWin(Amount * 5, WalletId, TransactionReason.Win), Times.Once);
        }
    }
}
