using Casino.Wallet.Core.Models;
using Casino.Wallet.Core.Services;
using Moq;
using Xunit;
using Casino.Wallet.Common.Utilities;

namespace Casino.Wallet.Tests.Services
{
    public class GameServiceTests
    {
        [Fact]
        public void Play_ShouldReturnGameResult()
        {
            // Arrange
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            randomNumberGeneratorMock.Setup(rng => rng.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(50); // Mocking a specific behavior

            var gameService = new GameService(randomNumberGeneratorMock.Object);
            var betAmount = 5m;

            // Act
            var result = gameService.Play(betAmount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(betAmount, result.BetAmount);
        }

        [Fact]
        public void Play_WinAmountShouldBeValid()
        {
            // Arrange
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            randomNumberGeneratorMock.Setup(rng => rng.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(50); // Mocking a specific behavior

            var gameService = new GameService(randomNumberGeneratorMock.Object);
            var betAmount = 5m;

            // Act
            var result = gameService.Play(betAmount);

            // Assert
            // Since the random number generator is mocked to always return 50,
            // adjust the expected win amount range as needed for your assertions.
            Assert.True(result.WinAmount >= 0 && result.WinAmount <= betAmount * 10);
        }
    }
}
