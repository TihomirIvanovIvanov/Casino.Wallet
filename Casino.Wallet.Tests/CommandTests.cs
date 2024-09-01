//using Casino.Wallet.Commands;
//using Casino.Wallet.Common;
//using Casino.Wallet.Core.Contracts;
//using Casino.Wallet.Core.Models;
//using Moq;

//namespace Casino.Wallet.Tests
//{
//    public class CommandTests
//    {
//        private readonly Mock<IDepositService> depositService;
//        private readonly Mock<IWithdrawService> withdrawService;
//        private readonly Mock<IPlaceBetService> placeBetService;

//        private readonly StringWriter stringWriter;
//        private const decimal Amount = 10m;

//        public CommandTests()
//        {
//            this.depositService = new Mock<IDepositService>();
//            this.withdrawService = new Mock<IWithdrawService>();
//            this.placeBetService = new Mock<IPlaceBetService>();

//            this.stringWriter = new StringWriter();
//        }

//        [Fact]
//        public void DepositCommand_ShouldCallDepositMethod()
//        {
//            // Arrange
//            var depositCommand = new DepositCommand(this.depositService.Object);

//            // Act
//            depositCommand.Execute(Amount);

//            // Assert
//            this.depositService.Verify(service => service.Deposit(Amount), Times.Once);
//        }

//        [Fact]
//        public void WithdrawCommand_ShouldCallWithdrawMethod()
//        {
//            // Arrange
//            var withdrawCommand = new WithdrawCommand(this.withdrawService.Object);

//            // Act
//            withdrawCommand.Execute(Amount);

//            // Assert
//            this.withdrawService.Verify(service => service.Withdraw(Amount), Times.Once);
//        }

//        [Fact]
//        public void BetCommand_ShouldCallPlaceBetAndPrintMessage()
//        {
//            // Arrange
//            var expectedMessage = $"{GlobalConstants.CongratsMsg} $20! {GlobalConstants.CurrentBalanceMsg} $30";
//            var gameResult = new GameResult
//            {
//                WinAmount = 20m,
//                Message = expectedMessage
//            };

//            this.placeBetService.Setup(service => service.PlaceBet(Amount))
//                                .Returns(gameResult);

//            var betCommand = new BetCommand(this.placeBetService.Object);

//            // Act
//            Console.SetOut(this.stringWriter);
//            betCommand.Execute(Amount);

//            // Assert
//            this.placeBetService.Verify(service => service.PlaceBet(Amount), Times.Once);
//            var consoleOutput = this.stringWriter.ToString().Trim();
//            Assert.Equal(expectedMessage, consoleOutput);
//        }
//    }
//}