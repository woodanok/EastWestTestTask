using EastWest.DAL.Interfaces;
using EastWest.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public EastWestDbContext db;

        public UnitOfWork(EastWestDbContext db)
        {
            this.db = db;
        }

        private IOrderRepository? orderRepository;
        public IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public async Task<int> SaveAsync() =>
            await db.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
