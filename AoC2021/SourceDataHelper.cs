using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AoC2021
{
    public static class SourceDataHelper
    {
        public static async Task<string[]> GetDataFromUrl(string path)
        {
            string[] data;
            var baseAddress = new Uri("https://adventofcode.com");
            using (var handler = new HttpClientHandler {UseCookies = false})
            using (var client = new HttpClient(handler) {BaseAddress = baseAddress})
            {
                var message = new HttpRequestMessage(HttpMethod.Get, path);
                message.Headers.Add("Cookie", MyCookie.CookieValue);
                var result = await client.SendAsync(message).ConfigureAwait(false);
                result.EnsureSuccessStatusCode();

                var text = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                data = text.TrimEnd('\n').Split('\n');
            }

            return data;
        }
    }
}