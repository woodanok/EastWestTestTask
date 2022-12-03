using EastWest;
using EastWest.BLL.Interfaces;
using EastWest.Utils.ExternalWorkers.APIWorker.Interfaces;
using EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWestTestTask
{
    public class Main
    {
        private readonly IServiceCollection services;

        public Main(IServiceCollection services)
        {
            this.services = services;
        }

        public async void Entry()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var customerOneApiBridge = serviceProvider?.GetService<ICustomerOneApiBridge>();
            var orderService = serviceProvider?.GetService<IOrderService>();
            var eastWestFTPBridge = serviceProvider?.GetService<IEastWestFTPBridge>();

            var orderProccessing = new OrderProccessing(customerOneApiBridge, orderService, eastWestFTPBridge);
            await orderProccessing.Process();

        }
    }
}
