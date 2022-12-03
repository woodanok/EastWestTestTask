using EastWest.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Utils.FileWorkers.Interfaces
{
    public interface IFileWorker
    {
        Task<List<OrderDTO>> Apply();
    }
}
