using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace AoC2021
{
    public class Day1
    {
        private async Task<int[]> GetValues()
        {
            var data = await SourceDataHelper.GetDataFromUrl("/2021/day/1/input").ConfigureAwait(false);
            var numbers = Array.ConvertAll(data, int.Parse);

            return numbers;
        }

        public async Task<int> GetIncreases()
        {
            var count = 0;
                var numbers = await GetValues().ConfigureAwait(false);
                for (var x = 1; x < numbers.Length; x++)
                {
                    count = numbers[x] > numbers[x - 1] ? count + 1 : count;
                }

                return count;
        }

        public async Task<int> GetSlidingWindowIncreases()
        {
            var count = 0;
            var numbers = await GetValues().ConfigureAwait(false);
            for (var x = 0; x < numbers.Length - 3; x++)
            {
                var g1 = numbers.Skip(x).Take(3).Sum();
                var g2 = numbers.Skip(x + 1).Take(3).Sum();

                count = g2 > g1 ? count + 1 : count;
            }

            return count;
        }
    }
}