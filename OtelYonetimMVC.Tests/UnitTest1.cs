using Xunit;

namespace OtelYonetimMVC.Tests
{
    public class BasicTests
    {
        [Fact]
        public void Simple_Test_Should_Pass()
        {
            // Arrange
            int a = 5;
            int b = 5;

            // Act
            int result = a + b;

            // Assert
            Assert.Equal(10, result);
        }
    }
}
