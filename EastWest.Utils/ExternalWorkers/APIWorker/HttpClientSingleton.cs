using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.APIWorker
{
    internal class HttpClientSingleton
    {
        private static HttpClient? instanse;

        public static HttpClient GetInstanse(string? sheme = null, string? token = null, string contentType = "application/json")
        {
            if (instanse is null)
            {
                instanse = new HttpClient();

                instanse.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            }
            if (!string.IsNullOrEmpty(token)) instanse.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(sheme, token);
            return instanse;
        }

        public static void Dispose()
        {
            instanse?.Dispose();
        }
    }
}
