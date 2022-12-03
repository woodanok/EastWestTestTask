using EastWest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task SaveOredersAsync(List<Order> orders);
    }
}
