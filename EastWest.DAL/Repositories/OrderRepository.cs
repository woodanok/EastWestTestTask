using EastWest.DAL.Entities;
using EastWest.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EastWestDbContext db;

        public OrderRepository(EastWestDbContext db)
        {
            this.db = db;
        }

        public async Task SaveOrderAsync(Order order)
        {
            db.Orders.Add(order);   
            await db.SaveChangesAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
            => await db.Orders.FirstOrDefaultAsync(s => s.Id == id);

        public async Task SaveOredersAsync(List<Order> orders)
        {
            db.Orders.AddRange(orders);
            await db.SaveChangesAsync();
        }
    }
}
