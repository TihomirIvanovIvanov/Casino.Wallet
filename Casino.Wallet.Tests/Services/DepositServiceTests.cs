//using Casino.Wallet.Common;
//using Casino.Wallet.Core.Contracts;
//using Casino.Wallet.Core.Services;
//using Moq;

//namespace Casino.Wallet.Tests.Services
//{
//    public class DepositServiceTests
//    {
//        private readonly Mock<IPlayer> playerMock;
//        private readonly DepositService depositService;

//        private readonly StringWriter stringWriter;
//        private const decimal InitialBalance = 100m;
//        private const decimal Amount = 10m;

//        public DepositServiceTests()
//        {
//            this.playerMock = new Mock<IPlayer>();
//            this.depositService = new DepositService(this.playerMock.Object);
//            this.stringWriter = new StringWriter();
//        }

//        [Fact]
//        public void Deposit_WithNegativeAmount_ShouldNotProceed()
//        {
//            // Act
//            Console.SetOut(this.stringWriter);
//            this.depositService.Deposit(-Amount);

//            // Assert
//            var output = this.stringWriter.ToString().Trim();
//            Assert.Contains(GlobalConstants.NegativeDepositAmountMsg, output);
//        }

//        [Fact]
//        public void Deposit_WithZeroAmount_ShouldNotProceed()
//        {
//            // Act
//            Console.SetOut(this.stringWriter);
//            this.depositService.Deposit(0);

//            // Assert
//            var output = this.stringWriter.ToString().Trim();
//            Assert.Contains(GlobalConstants.NegativeDepositAmountMsg, output);
//        }

//        [Fact]
//        public void Deposit_PositiveAmount_ShouldUpdateBalanceAndPrintMessage()
//        {
//            // Arrange
//            decimal depositAmount = 50m;
//            decimal expectedBalance = InitialBalance + depositAmount;

//            this.playerMock.Setup(p => p.GetBalance()).Returns(InitialBalance);
//            this.playerMock.Setup(p => p.UpdateBalance(It.IsAny<decimal>())).Verifiable();
//            this.playerMock.Setup(p => p.GetBalance()).Returns(expectedBalance);

//            // Act
//            Console.SetOut(this.stringWriter);
//            this.depositService.Deposit(depositAmount);

//            // Assert
//            var expectedMessage = $"Your deposit of ${depositAmount:F2} was successful. {GlobalConstants.CurrentBalanceMsg} ${this.playerMock.Object.GetBalance():F2}";
//            var output = this.stringWriter.ToString().Trim();
//            Assert.Contains(expectedMessage, output);
//            this.playerMock.Verify(p => p.UpdateBalance(depositAmount), Times.Once);
//            this.playerMock.Verify(p => p.GetBalance(), Times.Between(1, 2, Moq.Range.Inclusive));
//        }
//    }
//}
