using EastWest.Utils.ExternalWorkers.APIWorker.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.APIWorker.Bridges
{
    /// <summary>
    /// В этом классе можно распилить метода, для уменьшения количества параметров
    /// </summary>
    public class CustomerOneApiBridge : ApiGeneric, ICustomerOneApiBridge
    {
        private readonly string baseUrl = string.Empty;
        private readonly static string sheme = "Bearer";
        public CustomerOneApiBridge(IConfigurationRoot configuration)
        {
            baseUrl = configuration["CUSTOMER_ONE_API"];
        }

        public async Task<T> GetAsync<T>(string method, string? contentType = null, string? token = null, Dictionary<string, string>? getParams = null)
        {
            return await GetAsync<T>(method, baseUrl, contentType, sheme, token, getParams);
        }
    }
}
