using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day1Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day1Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Day1_GetIncreases()
        {
            var increases = await Day1.GetIncreases().ConfigureAwait(false);

            _testOutputHelper.WriteLine(increases.ToString());
            // assert to refactor
            Assert.Equal(1342, increases);
        }

        [Fact]
        public async Task Day1_GetSlidingWindow()
        {
            var countWindowIncreases = await Day1.GetSlidingWindowIncreases().ConfigureAwait(false);

            _testOutputHelper.WriteLine(countWindowIncreases.ToString());
            Assert.Equal(1378, countWindowIncreases);
        }
    }
}