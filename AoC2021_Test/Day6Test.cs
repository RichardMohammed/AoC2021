using System.Collections.Generic;
using System.Linq;
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
        public void Day6Test_ReturnsCountOfFish_ForExampleData()
        {
            var count = Day6.CountLanternFish(_data, 18);
            Assert.Equal(26, count);
            _testOutputHelper.WriteLine(count.ToString());
        }
    }
}