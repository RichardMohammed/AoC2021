using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day5Test
    {
        private ITestOutputHelper _testOutputHelper;
        private readonly string[] _data = new []{ "0,9 -> 5,9", "8,0 -> 0,8", "9,4 -> 3,4", "2,2 -> 2,1", "7,0 -> 7,4", "6,4 -> 2,0", "0,9 -> 2,9", "3,4 -> 1,4", "0,0 -> 8,8", "5,5 -> 8,2"};

        public Day5Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetDataTest_ReturnsCoOrdinates()
        {
            var coOrds = await Day5.GetValues().ConfigureAwait(false);

            foreach (var item in coOrds)
            {
                _testOutputHelper.WriteLine($"{item.First().Row},{item.First().Col} {item.Last().Row},{item.Last().Col}");
            }
        }

        [Fact]
        public async Task PlotMatrixTest_ReturnsValues()
        {
            var testData = await Day5.GetValues(_data).ConfigureAwait(false);
            var matrix = await Day5.PlotMatrix(testData).ConfigureAwait(false);

            // assert rows x columns rather than X x Y
            Assert.Equal(2, matrix[9, 0]);

            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                var row = "";
                for (var c = 0; c < matrix.GetLength(1); c++)
                {
                    row += $"{matrix[r, c]} ";

                }
                _testOutputHelper.WriteLine(row);
            }
            _testOutputHelper.WriteLine( matrix[0, 9].ToString());

        }

        [Fact]
        public async Task CalculateDangerousPointsTest_ReturnsValues()
        {
            var testData = await Day5.GetValues(_data).ConfigureAwait(false);
            var matrix = await Day5.PlotMatrix(testData).ConfigureAwait(false);
            var numDangerousPoints = await Day5.CalculateDangerousPoints(matrix).ConfigureAwait(false);

            Assert.Equal(5, numDangerousPoints);
            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }

        [Fact]
        public async Task PlotReadDataMatrixTest_ReturnsValues()
        {
            var matrix = await Day5.PlotMatrix().ConfigureAwait(false);

            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                var row = "";
                for (var c = 0; c < matrix.GetLength(1); c++)
                {
                    row += $"{matrix[r, c]} ";

                }
                _testOutputHelper.WriteLine(row);
            }
            _testOutputHelper.WriteLine( matrix[0, 9].ToString());

        }

        [Fact]
        public async Task CalculateDangerousPointsFromRealDataTest_ReturnsValues()
        {
            var numDangerousPoints = await Day5.CalculateDangerousPoints().ConfigureAwait(false);

            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }
    }
}