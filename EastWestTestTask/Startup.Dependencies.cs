using EastWest.BLL.Interfaces;
using EastWest.BLL.Services;
using EastWest.DAL;
using EastWest.DAL.Interfaces;
using EastWest.Utils.ExternalWorkers.APIWorker.Bridges;
using EastWest.Utils.ExternalWorkers.APIWorker.Interfaces;
using EastWest.Utils.ExternalWorkers.FTPWorker.Bridges;
using EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWestTestTask
{
    public partial class Startup
    {
        private void ConfigureDependencies(IServiceCollection services, IConfigurationRoot configuration)
        {
            var databaseUrl = configuration["DATABASE_URL"];
            var contextOptionsBuilder = new DbContextOptionsBuilder<EastWestDbContext>();
            contextOptionsBuilder.UseSqlite(databaseUrl);

            services.AddScoped<IUnitOfWork>(s => new UnitOfWork(new EastWestDbContext(contextOptionsBuilder.Options)));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerOneApiBridge>(s => new CustomerOneApiBridge(configuration));
            services.AddScoped<IEastWestFTPBridge>(s => new EastWestFTPBridge(configuration));
        }
    }
}
