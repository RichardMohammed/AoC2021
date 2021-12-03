using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day2Test
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Day2 _day2 = new Day2();


        public Day2Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Day2_GetPosition()
        {
            var position =   await _day2.GetPosition().ConfigureAwait(false);

            _testOutputHelper.WriteLine(position.ToString());
        }

        [Fact]
        public async Task Day2_GetPositionWithAim()
        {
            var position =   await _day2.GetPositionWithAim().ConfigureAwait(false);

            _testOutputHelper.WriteLine(position.ToString());
        }
    }
}