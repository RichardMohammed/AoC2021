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

            var coOrds = Day5.GetLinesAt90And45Degrees(testData, Day5.HorizonTalAndVerticalAngles);

            var matrix = Day5.PlotMatrix(coOrds);

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
            var coOrds = Day5.GetLinesAt90And45Degrees(testData, Day5.HorizonTalAndVerticalAngles);
            var matrix = Day5.PlotMatrix(coOrds);
            var numDangerousPoints = Day5.CalculateDangerousPoints(matrix);

            Assert.Equal(5, numDangerousPoints);
            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }

        [Fact]
        public async Task PlotRealDataMatrixTest_ReturnsValues()
        {
            var realData = await Day5.GetValues().ConfigureAwait(false);
            var coOrds = Day5.GetLinesAt90And45Degrees(realData, Day5.HorizonTalAndVerticalAngles);
            var matrix = Day5.PlotMatrix(coOrds);

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
            var testData = await Day5.GetValues().ConfigureAwait(false);
            var coOrds = Day5.GetLinesAt90And45Degrees(testData, Day5.HorizonTalAndVerticalAngles);
            var matrix = Day5.PlotMatrix(coOrds);
            var numDangerousPoints = Day5.CalculateDangerousPoints(matrix);

            Assert.Equal(7644, numDangerousPoints);
            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }

        [Theory]
        [InlineData(0, 9, 5, 9, 90)]
        [InlineData(5, 9, 0, 9, 90)]
        [InlineData(0, 9, 0, 7, 180)]
        [InlineData(1, 1, 3, 3, 45)]
        [InlineData(3, 3, 1, 1, 135)]
        [InlineData(9, 7, 7, 7, 90)]
        public void CalculateAngleTest(int x1, int y1, int x2, int y2, int degree)
        {
            var angle = Day5.GetAngleOfLineBetweenTwoPoints(new CoOrdinate(x1, y1), new CoOrdinate(x2, y2));

            _testOutputHelper.WriteLine($"{x1}, {y1} > {x2}, {y2} = {degree}");

            Assert.Equal(degree, angle);
        }

        [Fact]
        public async Task Part2CalculateDangerousPointsFor90And45DegreesLinesTest_UsingTestData_ReturnsValues()
        {
            var testData = await Day5.GetValues(_data).ConfigureAwait(false);
            var hAndVCoOrdinates = Day5.GetLinesAt90And45Degrees(testData, Day5.HorizonTalAndVerticalAngles);
            var diagonalCoOrdinates = Day5.GetLinesAt90And45Degrees(testData, Day5.DiagonalAngles);
            var matrix = Day5.PlotMatrix(hAndVCoOrdinates, diagonalCoOrdinates);

            for (var r = 0; r < matrix.GetLength(0); r++)
            {
                var row = "";
                for (var c = 0; c < matrix.GetLength(1); c++)
                {
                    row += $"{matrix[r, c]} ";

                }
                _testOutputHelper.WriteLine(row);
            }

            var numDangerousPoints = Day5.CalculateDangerousPoints(matrix);

            Assert.Equal(12, numDangerousPoints);
            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }

        [Fact]
        public async Task Part2CalculateDangerousPointsFor90And45DegreesLinesTest_ReturnsValues()
        {
            var testData = await Day5.GetValues().ConfigureAwait(false);
            var hAndVCoOrdinates = Day5.GetLinesAt90And45Degrees(testData, Day5.HorizonTalAndVerticalAngles);
            var diagonalCoOrdinates = Day5.GetLinesAt90And45Degrees(testData, Day5.DiagonalAngles);
            var matrix = Day5.PlotMatrix(hAndVCoOrdinates, diagonalCoOrdinates);

            var numDangerousPoints = Day5.CalculateDangerousPoints(matrix);

            Assert.Equal(18627, numDangerousPoints);
            _testOutputHelper.WriteLine(numDangerousPoints.ToString());
        }
    }
}