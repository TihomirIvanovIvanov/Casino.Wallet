//using Casino.Wallet.Common;
//using Casino.Wallet.Core.Contracts;
//using Casino.Wallet.Core.Services;
//using Moq;

//namespace Casino.Wallet.Tests.Services
//{
//    public class WithdrawServiceTests
//    {
//        private readonly Mock<IPlayer> player;
//        private readonly WithdrawService withdrawService;

//        private readonly StringWriter stringWriter;
//        private const decimal InitialBalance = 100m;
//        private const decimal Amount = 10m;

//        public WithdrawServiceTests()
//        {
//            this.player = new Mock<IPlayer>();
//            this.withdrawService = new WithdrawService(this.player.Object);
//            this.stringWriter = new StringWriter();
//        }

//        [Fact]
//        public void Withdraw_WithNegativeAmount_ShouldNotProceed()
//        {
//            // Act
//            Console.SetOut(this.stringWriter);
//            this.withdrawService.Withdraw(-Amount);

//            // Assert
//            var output = this.stringWriter.ToString().Trim();
//            Assert.Contains(GlobalConstants.NegativeWithdrawAmountMsg, output);
//        }

//        [Fact]
//        public void Withdraw_WithZeroAmount_ShouldNotProceed()
//        {
//            // Act
//            Console.SetOut(this.stringWriter);
//            this.withdrawService.Withdraw(0m);

//            // Assert
//            var output = this.stringWriter.ToString().Trim();
//            Assert.Contains(GlobalConstants.NegativeWithdrawAmountMsg, output);
//        }

//        [Fact]
//        public void Withdraw_AmountExceedsBalance_ShouldDenyWithdrawal()
//        {
//            // Arrange
//            this.player.Setup(p => p.GetBalance()).Returns(InitialBalance);

//            // Act
//            Console.SetOut(stringWriter);
//            this.withdrawService.Withdraw(150m);

//            // Assert
//            var output = stringWriter.ToString().Trim();
//            Assert.Contains($"{GlobalConstants.InsufficientFundsMsg} {GlobalConstants.CurrentBalanceMsg} ${this.player.Object.GetBalance():F2}", output);
//        }

//        [Fact]
//        public void Withdraw_AmountIsWithinBalance_ShouldAllowWithdrawal()
//        {
//            // Arrange
//            decimal withdrawalAmount = 50m;
//            this.player.Setup(p => p.GetBalance()).Returns(InitialBalance);
//            this.player.Setup(p => p.UpdateBalance(It.IsAny<decimal>())).Verifiable();

//            // Act
//            Console.SetOut(stringWriter);
//            this.withdrawService.Withdraw(withdrawalAmount);

//            // Assert
//            var output = stringWriter.ToString().Trim();
//            Assert.Contains($"Your withdrawal of ${withdrawalAmount:F2} was successful! {GlobalConstants.CurrentBalanceMsg} ${this.player.Object.GetBalance():F2}", output);
//            this.player.Verify(p => p.UpdateBalance(-withdrawalAmount), Times.Once);
//            this.player.Verify(p => p.GetBalance(), Times.Between(2, 3, Moq.Range.Inclusive));
//        }
//    }
//}
