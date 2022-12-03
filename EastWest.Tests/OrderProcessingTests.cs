using EastWest.BLL.Interfaces;
using EastWest.DAL;
using EastWest.Infrastructure.DTOs;
using EastWest.Tests.FakeDatas;
using EastWest.Utils.ExternalWorkers.APIWorker;
using EastWest.Utils.ExternalWorkers.APIWorker.Interfaces;
using EastWest.Utils.ExternalWorkers.FTPWorker.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Tests
{
    [TestFixture]
    public class OrderProcessingTests
    {
        private ICustomerOneApiBridge customerOneApiBridge;
        private IOrderService orderService;
        private IEastWestFTPBridge eastWestFTPBridge;

        [SetUp]
        public void SetUp()
        {

            this.customerOneApiBridge = Substitute.For<ICustomerOneApiBridge>();
            this.orderService = Substitute.For<IOrderService>();
            this.eastWestFTPBridge = Substitute.For<IEastWestFTPBridge>();
        }

        [Test]
        public void GetOrdersFromApi_IfOrdersHaveNotAny_ReturnZero()
        {
            // Arrange
            var param = new Dictionary<string, string>(1);
            param.Add("date", DateTime.Now.ToString());

            var orderProcessing = new OrderProccessing(customerOneApiBridge, orderService, eastWestFTPBridge);

            // Act
            var result = orderProcessing.GetOrdersFromApi();

            // Assert
            Assert.AreEqual(result.Result, 0);
        }

        [Test]
        public void GetOrdersFromApi_IfOrdersHaveAny_ReturnOne()
        {
            // Arrange
            var orderDTO = new OrderDTOFakeData(1).Create;
            var param = new Dictionary<string, string>(1);
            param.Add("date", DateTime.Now.ToString());

            customerOneApiBridge.GetAsync<OrderDTO>("/some/url", ApiConstants.json, "token", param).ReturnsForAnyArgs(orderDTO);
            var orderProcessing = new OrderProccessing(customerOneApiBridge, orderService, eastWestFTPBridge);

            // Act
            var result = orderProcessing.GetOrdersFromApi();

            // Assert
            Assert.Multiple(() =>
            {
                customerOneApiBridge.ReceivedWithAnyArgs(1).GetAsync<OrderDTO>("/some/url", ApiConstants.json, "token", param);
                result.Result.Should().Be(1);
            });
        }
    }
}
