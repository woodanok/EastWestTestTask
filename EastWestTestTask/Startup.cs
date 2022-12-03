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
        public Startup()
        {
            ConfigureServices();
        }

        public void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            ConfigureDependencies(services, configuration);

            new Main(services).Entry();
        }
    }
}
