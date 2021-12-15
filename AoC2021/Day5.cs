using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class Day5
    {
        public static async Task<List<List<CoOrdinate>>> GetValues(string[] sourceData = null)
        {
           var data = sourceData ?? await SourceDataHelper.GetDataFromUrl(5).ConfigureAwait(false);

           var values = data.Select(x => x.Replace(" ", "").Split(new string[] {"->"}, StringSplitOptions.None)).ToList();
           var coOrdinates = values.Select(c => new List<CoOrdinate>{
               new CoOrdinate(int.Parse(c[0].Split(',')[1]), int.Parse(c[0].Split(',')[0])),
               new CoOrdinate(int.Parse(c[1].Split(',')[1]), int.Parse(c[1].Split(',')[0]))
           }).ToList();

           return coOrdinates;
        }

        public static readonly int[] HorizonTalAndVerticalAngles = {0, 90, 180};
        public static readonly int[] DiagonalAngles = { 45, 135};

        public static List<List<CoOrdinate>> GetLinesAt90And45Degrees(IEnumerable<List<CoOrdinate>> coOrdinates, IEnumerable<int> angles) =>
            coOrdinates.Where(c => angles.Contains(GetAngleOfLineBetweenTwoPoints(c.First(), c.Last()))).ToList();

        public static int[,] PlotMatrix(List<List<CoOrdinate>> horizontalAndVerticalCoOrds, List<List<CoOrdinate>> diagonalCoOrds = null)
        {
            var maxDiagRows = diagonalCoOrds?.SelectMany(x => x.Select(c => c.Row)).Max() + 1;
            var maxDiagCols  = diagonalCoOrds?.SelectMany(x => x.Select(c => c.Col)).Max() + 1;
            var maxRows = Math.Max(maxDiagRows??0,  horizontalAndVerticalCoOrds.SelectMany(x => x.Select(c => c.Row)).Max() + 1);
            var maxCols = Math.Max(maxDiagCols??0, horizontalAndVerticalCoOrds.SelectMany(x => x.Select(c => c.Col)).Max() + 1);

            var matrix = new int[maxRows, maxCols];

            foreach (var lineCoOrd in horizontalAndVerticalCoOrds)
            {
                var from = lineCoOrd.First();
                var to = lineCoOrd.Last();

                if (from.Row == to.Row)
                {
                    GetVerticalLineCoordinates(from, to, matrix);
                }
                else
                {
                    GetHorizontalLineCoOrdinates(from, to, matrix);
                }
            }

            if (diagonalCoOrds != null)
            {
                foreach (var lineCoOrd in diagonalCoOrds)
                {
                    var from = lineCoOrd.First();
                    var to = lineCoOrd.Last();

                    GetDiagonalLineCoOrdinates(from, to, matrix);
                }
            }

            return matrix;
        }

        private static void GetDiagonalLineCoOrdinates(CoOrdinate @from, CoOrdinate to, int[,] matrix)
        {
            var start = from.Col < to.Col ? from : to;
            var end = to.Col > from.Col ? to : from;

            var increment = 0;
            for (var c = start.Col; c <= end.Col; c++)
            {
                if (start.Row > end.Row)
                {
                    // draw line from left bottom to right top
                        matrix[start.Row - increment, c] += 1;
                }
                else
                {
                    // draw line from left top to right bottom
                        matrix[start.Row + increment, c] += 1;
                }

                increment++;
            }
        }

        private static void GetHorizontalLineCoOrdinates(CoOrdinate from, CoOrdinate to, int[,] matrix)
        {
            //draw horizontal line
            if (from.Row < to.Row)
            {
                for (var i = from.Row; i <= to.Row; i++)
                {
                    matrix[i, from.Col] += 1;
                }
            }
            else
            {
                for (var i = from.Row; i >= to.Row; i--)
                {
                    matrix[i, from.Col] += 1;
                }
            }
        }

        private static void GetVerticalLineCoordinates(CoOrdinate @from, CoOrdinate to, int[,] matrix)
        {
            // draw vertical line
            if (@from.Col < to.Col)
            {
                for (var i = @from.Col; i <= to.Col; i++)
                {
                    matrix[@from.Row, i] += 1;
                }
            }
            else
            {
                for (var i = @from.Col; i >= to.Col; i--)
                {
                    matrix[@from.Row, i] += 1;
                }
            }
        }

        public static int CalculateDangerousPoints(int[,] matrix)
        {
            var dangerousPoints = 0;

            var matrixRowCount = matrix.GetLength(0);
            var matrixColCount = matrix.GetLength(1);
            for (var row = 0; row < matrixRowCount; row++)
            {
                for (var col = 0; col < matrixColCount; col++)
                {
                    if (matrix[row, col] > 1)
                    {
                        dangerousPoints++;
                    }
                }
            }

            return dangerousPoints;
        }

        public static int GetAngleOfLineBetweenTwoPoints(CoOrdinate from, CoOrdinate to)
        {
            double xDiff = to.Col - from.Col;
            double yDiff = to.Row - from.Row;
            var angle = Convert.ToInt32(Math.Atan2(yDiff, xDiff) * (180 / Math.PI));

            return Math.Abs(angle);
        }
    }

    public class CoOrdinate
    {
        public CoOrdinate(int row, int col)
        {
            Row = row;
            Col = col;
        }
        public int Row { get; }
        public int Col { get; }
    }
}