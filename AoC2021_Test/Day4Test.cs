using System;
using System.Threading.Tasks;
using AoC2021;
using Xunit;
using Xunit.Abstractions;

namespace AoC2021_Test
{
    public class Day4Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day4Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Day4_GetLosingTotal()
        {
            var total = await Day4.CalculateTotal(Day4.GetLosingBoard).ConfigureAwait(false);
            _testOutputHelper.WriteLine(total.ToString());
            Assert.Equal(26936, total);
        }


        [Fact]
        public async Task Day4_GetWinningTotal()
        {
            var total = await Day4.CalculateTotal(Day4.GetWinningBoard).ConfigureAwait(false);
            _testOutputHelper.WriteLine(total.ToString());

            Assert.Equal(39902, total);
        }

        [Fact]
        public async Task Day4_GetLosingBoard()
        {
            var (number, board) = await Day4.GetLosingBoard().ConfigureAwait(false);
            _testOutputHelper.WriteLine(number.ToString());

            foreach (var row in board)
            {
                _testOutputHelper.WriteLine(string.Join(" ", row));
            }

        }

        [Fact]
        public async Task Day4_GetWinningBoard()
        {
            var board = await Day4.GetWinningBoard().ConfigureAwait(false);
            _testOutputHelper.WriteLine(board.ToString());
        }

        [Fact]
        public async Task Day4_GetValues()
        {
            var (numbers, boards) = await Day4.GetValues().ConfigureAwait(false);
            _testOutputHelper.WriteLine(boards.ToString());
        }
    }
}