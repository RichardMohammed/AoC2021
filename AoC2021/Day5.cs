using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace AoC2021
{
    public class Day5
    {
        public static async Task<List<List<CoOrdinate>>> GetValues(string[] sourceData = null)
        {
           var data = sourceData ?? await SourceDataHelper.GetDataFromUrl("/2021/day/5/input").ConfigureAwait(false);

           var values = data.Select(x => x.Replace(" ", "").Split(new string[] {"->"}, StringSplitOptions.None)).ToList();
           var coOrdinates = values.Select(c => new List<CoOrdinate>{
               new CoOrdinate
               {
                   Col = int.Parse(c[0].Split(',')[0]),
                   Row = int.Parse(c[0].Split(',')[1])
               },
               new CoOrdinate
               {
                   Col = int.Parse(c[1].Split(',')[0]),
                   Row = int.Parse(c[1].Split(',')[1])
               }
           }).ToList();

           return coOrdinates;
        }

        public static List<List<CoOrdinate>> GetFilteredValues(IEnumerable<List<CoOrdinate>> coOrdinates) =>
            coOrdinates.Where(c => c.First().Col == c.Last().Col || c.First().Row == c.Last().Row).ToList();

        public static async Task<int[,]> PlotMatrix(List<List<CoOrdinate>> data = null)
        {
            var allCoOrds = data ?? await GetValues().ConfigureAwait(false);
            var coOrds = GetFilteredValues(allCoOrds);
            var maxRows = coOrds.SelectMany(x => x.Select(c => c.Row)).Max() + 1;
            var maxCols = coOrds.SelectMany(x => x.Select(c => c.Col)).Max() + 1;

            var matrix = new int[maxRows, maxCols];

            foreach (var lineCoOrd in coOrds)
            {
                var from = lineCoOrd.First();
                var to = lineCoOrd.Last();

                if (from.Row == to.Row)
                {
                    // draw vertical line
                    if (from.Col < to.Col)
                    {
                        for (var i = from.Col; i <= to.Col; i++)
                        {
                            matrix[from.Row, i] += 1;
                        }
                    }
                    else
                    {
                        for (var i = from.Col; i >= to.Col; i--)
                        {
                            matrix[from.Row, i] += 1;
                        }
                    }
                }
                else
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
            }

            return matrix;
        }

        public static async Task<int> CalculateDangerousPoints(int[,] matrix = null)
        {
            var dangerousPoints = 0;
            matrix = matrix ?? await PlotMatrix().ConfigureAwait(false);

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
    }

    public class CoOrdinate
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}