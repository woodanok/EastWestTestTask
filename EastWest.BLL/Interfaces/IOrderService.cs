using EastWest.DAL.Entities;
using EastWest.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.BLL.Interfaces
{
    public interface IOrderService
    {
        Task SaveOrderAsync(OrderDTO orderDTO);
        Task<OrderDTO> GetOrderByIdAsync(int id);
        Task SaveOrdersAsync(List<OrderDTO> orderDTO);
    }
}
