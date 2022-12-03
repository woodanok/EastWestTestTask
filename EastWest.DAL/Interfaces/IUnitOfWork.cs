using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; }
        public Task<int> SaveAsync();
    }
}
