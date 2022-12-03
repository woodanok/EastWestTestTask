using EastWest.Infrastructure.DTOs;
using EastWest.Utils.FileWorkers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.FileWorkers
{
    public class XmlFileWorkerStrategy : IFileWorker
    {
        public Task<List<OrderDTO>> Apply()
        {
            throw new NotImplementedException();
        }
    }
}
