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

        public static int CountLanternFish(List<int> data, int days)
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
    }
}