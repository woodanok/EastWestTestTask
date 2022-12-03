using EastWest.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.DAL
{
    public class EastWestDbContext : DbContext
    {
        public EastWestDbContext(DbContextOptions<EastWestDbContext> options) : base(options)
        { 
            SQLitePCL.Batteries.Init();
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
