using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class Day3
    {
        private static async Task<List<char[]>> GetValues()
        {
            var data = await SourceDataHelper.GetDataFromUrl("/2021/day/3/input").ConfigureAwait(false);
            var diagnostics = data.Select(x => x.ToCharArray()).ToList();

            return diagnostics;
        }

        public static async Task<int> GetPowerConsumption()
        {
            var data = await GetValues().ConfigureAwait(false);
            var gamma = string.Empty;
            var epsilon = string.Empty;
            var arrSize = data.First().Length;
            var toCompare = data.Count / 2;

            for (var i = 0; i < arrSize; i++)
            {
                var numOnes = data.Count(x => x[i] == '1');

                gamma += numOnes >= toCompare ? "1" : "0";
                epsilon += numOnes >= toCompare ? "0" : "1";
            }

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        public static async Task<int> GetLifeSupportRating()
        {
            var data = await GetValues().ConfigureAwait(false);
            var oxygenData = new List<char[]>(data);
            var co2Data = new List<char[]>(data);
            var arrSize = data.First().Length;

            for (var i = 0; i < arrSize; i++)
            {
                if (oxygenData.Count > 1)
                {
                    var oxygenCount1 = oxygenData.Count(x => x[i] == '1');
                    var oxygenCount0 = oxygenData.Count - oxygenCount1;

                    oxygenData = oxygenCount1 >= oxygenCount0
                        ? oxygenData.Where(x => x[i] == '1').ToList()
                        : oxygenData.Where(x => x[i] == '0').ToList();
                }

                if (co2Data.Count <= 1) continue;
                {
                    var co2Count1 = co2Data.Count(x => x[i] == '1');
                    var co2Count0 = co2Data.Count - co2Count1;

                    var chooseO = co2Data.Where(x => x[i] == '0').ToList();
                    var chooseOne = co2Data.Where(x => x[i] == '1').ToList();
                    co2Data = co2Count0 <= co2Count1
                        ? chooseO.Count == 0 ? chooseOne : chooseO
                        : chooseOne.Count == 0 ? chooseO : chooseOne;
                }
            }

            var oxygen = new string(oxygenData.First());
            var co2 = new string(co2Data.First());

            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }
    }
}