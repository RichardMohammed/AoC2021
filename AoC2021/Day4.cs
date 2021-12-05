using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class Day4
    {
        const int BoardSize = 5;
        public static async Task<(int[], List<int[][]>)> GetValues()
        {
            var data = await SourceDataHelper.GetDataFromUrl("/2021/day/4/input").ConfigureAwait(false);
            var numbersToCall = Array.ConvertAll(data.First().Split(','), int.Parse);
            var numbersToPlay = data.Skip(1).Where(x => x != string.Empty).ToList();

            var boards = numbersToPlay.Select((s, i) => new { Value = s, Index = i })
                .GroupBy(x => x.Index / BoardSize)
                .Select(grp => grp.Select(x => Array.ConvertAll(x.Value.Trim(' ').Replace("  ", " ").Split(' '), int.Parse)).ToArray())
                .ToList();

            return (numbersToCall, boards);
        }

        public static async Task<int> CalculateTotal(Func<Task<(int, int[][])>> getBoard)
        {
            var (lastNumberCalled, board) = await getBoard().ConfigureAwait(false);

            var remainingNumbers = board.Select(x => x.Where(r => r != -1).Sum()).Sum();

            return remainingNumbers * lastNumberCalled;
        }

        public static async Task<(int, int[][])> GetWinningBoard()
        {
            var (numbers, boards) = await GetValues().ConfigureAwait(false);

            for (var i = 0; i < numbers.Length; i++)
            {
                foreach (var board in boards)
                {
                    foreach (var row in board)
                    {
                        for (var r = 0; r < row.Length; r ++)
                        {
                            if (row[r] == numbers[i])
                            {
                                row[r] = -1;
                            }
                        }
                    }

                    if (i > 4)
                    {
                        var rowHasBingo = board.Any(x => x.All( y => y == -1));
                        var colHasBingo = false;

                        for (var index = 0; index < 5; index++)
                        {
                            var col = board.Select(x => x[index]);
                            colHasBingo = col.All(c => c == -1);
                            if (colHasBingo)
                            {
                                break;
                            }
                        }

                        if (colHasBingo || rowHasBingo)
                            return (numbers[i], board);
                    }
                }
            }

            return (0, null);
        }

        public static async Task<(int, int[][])> GetLosingBoard()
        {
            var (numbers, boards) = await GetValues().ConfigureAwait(false);
            var winningBoards = new List<int[][]>();

            foreach (var numberCalled in numbers)
            {
                for (var boardIndex = 0; boardIndex < boards.Count; boardIndex++)
                {
                    var board = boards[boardIndex];
                    MarkNumberOnBoard(ref board, numberCalled);

                    var rowHasBingo = board.Any(x => x.All(y => y == -1));
                    var colHasBingo = CheckColumnHasBingo(board);

                    if (!colHasBingo && !rowHasBingo) continue;
                    if (!winningBoards.Contains(board))
                    {
                        winningBoards.Add(board);
                    }

                    if (winningBoards.Count == boards.Count)
                    {
                        return (numberCalled, board);
                    }
                }
            }

            return (0, null);
        }

        private static bool CheckColumnHasBingo(int[][] board)
        {
            for (var index = 0; index < BoardSize; index++)
            {
                var index1 = index;
                var col = board.Select(x => x[index1]);
                var colHasBingo = col.All(x => x == -1);
                if (colHasBingo)
                {
                    return true;
                }
            }

            return false;
        }

        private static void MarkNumberOnBoard(ref int[][] board, int numberCalled)
        {
            foreach (var row in board)
            {
                for (var cellIndex = 0; cellIndex < row.Length; cellIndex++)
                {
                    if (row[cellIndex] == numberCalled)
                    {
                        row[cellIndex] = -1;
                    }
                }
            }
        }
    }
}