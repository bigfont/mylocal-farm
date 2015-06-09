using Xunit;

namespace farmApi.Test
{
    /// <summary>
    /// For details see http://xunit.github.io/docs/getting-started-dnx.html
    /// See also https://github.com/aspnet/Testing/wiki/How-to-create-test-projects
    /// </summary>
    public class Demo
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, 2 + 2);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void SimpleTheory(int value)
        {
            Assert.Equal(3, value);
        }
    }
}
