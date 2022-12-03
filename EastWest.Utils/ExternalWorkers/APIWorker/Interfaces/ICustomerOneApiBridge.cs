using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.ExternalWorkers.APIWorker.Interfaces
{
    public interface ICustomerOneApiBridge
    {
        Task<T> GetAsync<T>(string method, string? contentType = null, string? token = null, Dictionary<string, string>? getParams = null);
    }
}
