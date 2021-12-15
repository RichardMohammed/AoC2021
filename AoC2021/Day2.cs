using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Day2
    {
        private async Task<List<Tuple<string, int>>> GetValues()
        {
            var data = await SourceDataHelper.GetDataFromUrl(2).ConfigureAwait(false);
            var directions = data.Select(x => new Tuple<string, int>(x.Split(' ')[0], Convert.ToInt32(x.Split(' ')[1]))).ToList();

            return directions;
        }

        public async Task<int> GetPosition()
        {
            var values = await GetValues().ConfigureAwait(false);
            var horizontal = 0;
            var depth = 0;

            foreach (var (direction, moveBy) in values)
            {
                switch (direction)
                {
                    case "forward":
                        horizontal += moveBy;
                        break;
                    case "up":
                        depth -= moveBy;
                        break;
                    default:
                        depth += moveBy;
                        break;
                }
            }

            return horizontal * depth;
        }

        public async Task<int> GetPositionWithAim()
        {
            var values = await GetValues().ConfigureAwait(false);
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var (direction, moveBy) in values)
            {
                switch (direction)
                {
                    case "forward":
                        horizontal += moveBy;
                        depth += aim * moveBy;
                        break;
                    case "up":
                        aim -= moveBy;
                        break;
                    default:
                        aim += moveBy;
                        break;
                }
            }

            return horizontal * depth;
        }
    }
}