using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.APIWorker
{
    public class ApiGeneric
    {
        public static async Task<T> GetAsync<T>(string method, string baseUrl, string? contentType = null, string? sheme = null, string? token = null, Dictionary<string, string>? getParams = null)
        {
            var get = getParams is null ? null : $"?{string.Join("&", getParams.Select(s => $"{s.Key}={s.Value}"))}";

            HttpResponseMessage response = await HttpClientSingleton.GetInstanse(sheme, token, contentType).GetAsync(baseUrl + method + get);
            HttpContent responseContent = response.Content;
            var responceString = await responseContent.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<T>(responceString);

            return repositories;
        }
    }
}
