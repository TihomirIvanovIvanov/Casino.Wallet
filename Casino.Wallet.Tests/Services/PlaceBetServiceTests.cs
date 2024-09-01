//using Casino.Wallet.Common;
//using Casino.Wallet.Common.Utilities;
//using Casino.Wallet.Core.Models;
//using Casino.Wallet.Core.Services;
//using Moq;

//namespace Casino.Wallet.Tests.Services
//{
//    public class PlaceBetServiceTests
//    {
//        private readonly Player player;
//        private readonly Mock<IRandomNumberGenerator> rnd;
//        private readonly PlaceBetService placeBetService;

//        private const decimal InitialBalance = 100m;
//        private const decimal Amount = 10m;

//        public PlaceBetServiceTests()
//        {
//            this.player = new Player();
//            this.rnd = new Mock<IRandomNumberGenerator>();
//            this.placeBetService = new PlaceBetService(this.player, this.rnd.Object);
//        }

//        [Fact]
//        public void PlaceBet_ShouldRemoveBetAmountFromBalanceWhenLose()
//        {
//            // Arrange
//            this.player.UpdateBalance(InitialBalance);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.Equal(InitialBalance - Amount, this.player.GetBalance());
//        }

//        [Fact]
//        public void PlaceBet_InvalidBetAmount_ShouldReturnErrorMessage()
//        {
//            // Arrange
//            decimal invalidBetAmount = 15m;

//            // Act
//            var result = this.placeBetService.PlaceBet(invalidBetAmount);

//            // Assert
//            Assert.Equal($"{GlobalConstants.InvalidBetMsg} ${GlobalConstants.MinBet:F2} and ${GlobalConstants.MaxBet:F2}.", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_InsufficientFunds_ShouldReturnErrorMessage()
//        {
//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.Equal($"{GlobalConstants.InsufficientFundsMsg} {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_LessThanTheBetAmount_ShouldReturnErrorMessage()
//        {
//            // Arrange
//            this.player.UpdateBalance(5m);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.Equal($"{GlobalConstants.InsufficientFundsMsg} {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_ShouldReturnGameResult()
//        {
//            // Act
//            player.UpdateBalance(InitialBalance);
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal($"{GlobalConstants.NoLuckMsg} {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_WinAmountShouldBeValid()
//        {
//            // Arrange
//            this.player.UpdateBalance(InitialBalance);
//            this.rnd.Setup(rng => rng.GenerateNextInt(It.IsAny<int>(), It.IsAny<int>())).Returns(51);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.True(result.WinAmount >= 0 && result.WinAmount <= Amount * 10);
//            Assert.Equal($"{GlobalConstants.CongratsMsg} ${(Amount * 2):F2}! {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_ShouldDoubleBetAmountWhenWinDouble()
//        {
//            // Arrange
//            this.player.UpdateBalance(InitialBalance);
//            this.rnd.Setup(r => r.GenerateNextInt(It.IsAny<int>(), It.IsAny<int>()))
//                    .Returns(GlobalConstants.LoseChance + 1);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.Equal(InitialBalance - Amount + (Amount * 2), this.player.GetBalance());
//            Assert.Equal($"{GlobalConstants.CongratsMsg} ${(Amount * 2):F2}! {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);
//        }

//        [Fact]
//        public void PlaceBet_ShouldWinWithMultiplierWhenHighWin()
//        {
//            // Arrange
//            this.player.UpdateBalance(InitialBalance);

//            this.rnd.SetupSequence(r => r.GenerateNextInt(It.IsAny<int>(), It.IsAny<int>()))
//                    .Returns(GlobalConstants.LoseChance + GlobalConstants.DoubleChance + 1) 
//                    .Returns(5);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.Equal(InitialBalance - Amount + (Amount * 5), this.player.GetBalance());
//            Assert.Equal($"{GlobalConstants.CongratsMsg} ${(Amount * 5):F2}! {GlobalConstants.CurrentBalanceMsg} ${this.player.GetBalance():F2}", result.Message);

//        }

//        [Fact]
//        public void PlaceBet_ExactBalance_ShouldAllowBet()
//        {
//            // Arrange
//            this.player.UpdateBalance(Amount);

//            // Act
//            var result = this.placeBetService.PlaceBet(Amount);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(0m, this.player.GetBalance());
//        }
//    }
//}
