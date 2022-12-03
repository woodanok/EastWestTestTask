using EastWest.BLL.Interfaces;
using EastWest.Infrastructure.DTOs;
using EastWest.Utils.ExternalWorkers.APIWorker;
using EastWest.Utils.ExternalWorkers.APIWorker.Interfaces;
using EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces;
using EastWest.Utils.FileWorkers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EastWest
{
    public class OrderProccessing
    {
        private readonly ICustomerOneApiBridge customerOneApiBridge;
        private readonly IOrderService orderService;
        private readonly IEastWestFTPBridge eastWestFTPBridge;

        public OrderProccessing(ICustomerOneApiBridge customerOneApiBridge, IOrderService orderService, IEastWestFTPBridge eastWestFTPBridge)
        {
            this.customerOneApiBridge = customerOneApiBridge;
            this.orderService = orderService;
            this.eastWestFTPBridge = eastWestFTPBridge;
        }


        /// <summary>
        /// работу с разными источниками данных, я бы выделил в отдельные проекты, здесь для удобства они собраны в Utils
        /// </summary>
        /// <returns></returns>
        public async Task Process()
        {
            // можно/нужно прикрутить обработку ошибок
            while (true)
            {
                // здесь скорее всего я бы распараллелил 
                var apiCount = await GetOrdersFromApi();
                Log.Information($"From Api - {apiCount}");

                var ftpCount = await GetOrdertsFromFTP();
                Log.Information($"From ftp - {ftpCount}");

                var emailCount = await GetOrdersFromEmail();
                Log.Information($"From email - {emailCount}");

                var tgCount = await GetOrdersFromTG();
                Log.Information($"From tg - {tgCount}");

                // Thread.Sleep(60000); 1 minute - такая зарежка или handler с нужными требованиями
            }
        }

        public async Task<int> GetOrdersFromApi()
        {
            // сначала будет запрос есть ли файл и его формат
            // скорее всего придётся реализовывать handler для проверки формата файла и его десирилизацию

            // здесь предполагаем что это get запрос и данные в json
            var param = new Dictionary<string, string>(1);
            param.Add("date", DateTime.Now.ToString());

            var orderDTO = await customerOneApiBridge.GetAsync<OrderDTO>("/some/url", ApiConstants.json, "token", param);
            
            if(orderDTO is not null)
            {
                await orderService.SaveOrderAsync(orderDTO);
                return 1; // или если вернули список то отдали count
            }

            return 0; 
        }

        public async Task<int> GetOrdertsFromFTP()
        {
            var count = 0;
            var paths = await eastWestFTPBridge.GetFilesInfoFromDirectoryAsync("some path");
            foreach(var path in paths)
            {
                var localPath = "some local path";
                await eastWestFTPBridge.DownLoad(path, localPath);

                var readFileStrategy = new FileWorkerResolver().Resolve(path);
                var ordersDTO = await readFileStrategy.Apply();

                if (ordersDTO.Any()) 
                {
                    await orderService.SaveOrdersAsync(ordersDTO);
                    count += ordersDTO.Count;
                }

                await eastWestFTPBridge.DeleteFile(path);
            }

            return count;
        }

        public async Task<int> GetOrdersFromEmail() => 0;

        public async Task<int> GetOrdersFromTG() => 0;
    }
}
