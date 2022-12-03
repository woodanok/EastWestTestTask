using EastWest.BLL.Interfaces;
using EastWest.DAL.Entities;
using EastWest.DAL.Interfaces;
using EastWest.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task SaveOrderAsync(OrderDTO orderDTO)
        {
            var order = ConvertToOrder(orderDTO);
            await unitOfWork.OrderRepository.SaveOrderAsync(order);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
            return ConvertToOrderDTO(order);
        }

        private Order ConvertToOrder(OrderDTO order)
        {
            // для больших объектов здесь можно внедрить маппер, если по скорости не сильно бьёт
            return new Order
            {
                Name = order.Name,
                Price = order.Price,
            };
        }

        private OrderDTO ConvertToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                Name = order.Name,
                Price = order.Price,
            };
        }

        public async Task SaveOrdersAsync(List<OrderDTO> orderDTO)
        {
            var orders = ConvertToOrder(orderDTO);
            await unitOfWork.OrderRepository.SaveOredersAsync(orders);
        }

        private List<Order> ConvertToOrder(List<OrderDTO> ordersDTO)
        {
            // для больших объектов здесь можно внедрить маппер, если по скорости не сильно бьёт
            var orders = new List<Order>(ordersDTO.Count);
            foreach(var orderDTO in ordersDTO)
                orders.Add(ConvertToOrder(orderDTO));

            return orders;
        }
    }
}
