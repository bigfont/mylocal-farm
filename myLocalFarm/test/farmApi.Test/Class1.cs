using Xunit;

namespace farmApi.Test
{
    public class Class1
    {
        public Class1()
        {

        }

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
