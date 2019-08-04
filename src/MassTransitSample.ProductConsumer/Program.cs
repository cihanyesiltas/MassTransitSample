using System.IO;
using System.Threading.Tasks;
using MassTransitSample.ProductConsumer.HostedServices;
using MassTransitSample.ProductConsumer.Infrastructure;
using MassTransitSample.ProductConsumer.Services;
using MassTransitSample.ProductConsumer.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace MassTransitSample.ProductConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = confBuilder.Build();
            var rabbitMqConnectionSection = configuration.GetSection("RabbitMqConnection");

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IProductService, ProductService>();
                    services.Configure<RabbitMqConnectionConfig>(config =>
                    {
                        config.HostUrl = rabbitMqConnectionSection.GetValue<string>("HostUrl");
                        config.ProductPriceCommandQueueName = rabbitMqConnectionSection.GetValue<string>("ProductPriceCommandQueueName");
                    });
                    services.AddHostedService<UpdateProductPriceService>();
                });
            await builder.RunConsoleAsync();
        }
    }
}