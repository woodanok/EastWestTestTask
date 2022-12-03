using Bogus;
using EastWest.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EastWest.Tests.FakeDatas
{
    public class OrderDTOFakeData
    {
        private readonly int seed;
        public OrderDTOFakeData(int seed)
        {
            this.seed = seed;
        }

        public OrderDTO Create
            => new Faker<OrderDTO>()
                    .UseSeed(seed)
                    .CustomInstantiator(f => new OrderDTO
                    {
                        Name = f.Random.Words(),
                        Price = f.Random.Decimal()
                    }).Generate();

        public List<OrderDTO> CreateCollection(int count)
        {
            var result = new List<OrderDTO>(count);

            for (int i = 0; i < count; i++)
            {
                result.Add(Create);
            }

            return result;
        }
    }
}
