using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day3Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day3Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Day3_GetPower()
        {
            var power =   await Day3.GetPowerConsumption().ConfigureAwait(false);

            _testOutputHelper.WriteLine(power.ToString());
            //Add asserts to help refactor
            Assert.Equal(2954600, power);
        }

        [Fact]
        public async Task Day3_LifeSupportRating()
        {
            var lifeSupport =   await Day3.GetLifeSupportRating().ConfigureAwait(false);

            _testOutputHelper.WriteLine(lifeSupport.ToString());
            //Add asserts to help refactor
            Assert.Equal(1662846, lifeSupport);
        }
    }
}