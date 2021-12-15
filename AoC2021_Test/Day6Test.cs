using System.Collections.Generic;
using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day6Test
    {
        private readonly List<int> _data = new List<int> {3, 4, 3, 1, 2};
        private readonly ITestOutputHelper _testOutputHelper;

        public Day6Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetTestData_ReturnsArrayOfInt()
        {
            var data = await Day6.GetValues().ConfigureAwait(false);

            Assert.IsType<List<int>>(data);
        }

        [Theory]
        [InlineData(18, 26)]
        [InlineData(80, 5934)]
        public void Day6Test_ReturnsCountOfFish_ForExampleData(int days, int expected)
        {
            var count = Day6.CountLanternFish(_data, days);
            Assert.Equal(expected, count);
            _testOutputHelper.WriteLine(count.ToString());
        }

        [Theory]
        [InlineData(4, 9)]
        [InlineData(5, 10)]
        [InlineData(6, 10)]
        [InlineData(10, 12)]
        [InlineData(256, 26984457539)]
        public void Day6Test_ReturnsCountOfFish_ForLarge_ExampleData(int days, long expected)
        {
            var count = Day6.PerformantCount(_data, days);
            Assert.Equal(expected, count);
            _testOutputHelper.WriteLine(count.ToString());
        }

        [Theory]
        [InlineData(80, 386536)]
        public async Task Day6Test_ReturnsCountOfFish_ForRealTestData(int days, long expectedCount)
        {
            var data = await Day6.GetValues().ConfigureAwait(false);
            var count = Day6.CountLanternFish(data, days);

            _testOutputHelper.WriteLine(count.ToString());
            Assert.Equal(expectedCount, count);
        }

        [Theory]
        [InlineData(256, 1732821262171)]
        public async Task Day6Test_ReturnsCountOfFish_ForLarge_RealTestData(int days, long expectedCount)
        {
            var data = await Day6.GetValues().ConfigureAwait(false);
            var count = Day6.PerformantCount(data, days);

            _testOutputHelper.WriteLine(count.ToString());
            Assert.Equal(expectedCount, count);
        }
    }
}