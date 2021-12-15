using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class Day6
    {
        public static async Task<List<int>> GetValues()
        {
            var data = await SourceDataHelper.GetDataFromUrl(6).ConfigureAwait(false);

            var values = data.First().Split(',').Select(int.Parse).ToList();

            return values;
        }

        public static long CountLanternFish(List<int> data, int days)
        {
            for (var day = 0; day < days; day++)
            {
                var babies = new List<int>();

                for (var i = 0; i < data.Count; i++)
                {
                    if (data[i] > 0)
                    {
                        data[i] -= 1;
                    }
                    else
                    {
                        data[i] = 6;
                        babies.Add(8);
                    }
                }

                data.AddRange(babies);
            }

            return data.Count;
        }

        public static long PerformantCount(List<int> data, int days)
        {
            var totalFish = (long)data.Count;
            var fishTimers = new Dictionary<int, long>
            {
                {0, data.Count(c => c == 0)},
                {1, data.Count(c => c == 1)},
                {2, data.Count(c => c == 2)},
                {3, data.Count(c => c == 3)},
                {4, data.Count(c => c == 4)},
                {5, data.Count(c => c == 5)},
                {6, data.Count(c => c == 6)},
                {7, 0},
                {8, 0}
            };

            for (var day = 0; day < days; day++)
            {
                var zeroCounts = fishTimers[0];
                for (var internalTimer = 0; internalTimer < 8; internalTimer++)
                {
                    fishTimers[internalTimer] = fishTimers[internalTimer + 1];
                }


                fishTimers[6] += zeroCounts;
                fishTimers[8] = zeroCounts;
                totalFish += zeroCounts;
            }

            return totalFish;
        }
    }
}