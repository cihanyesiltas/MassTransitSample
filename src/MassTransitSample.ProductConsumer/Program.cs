using System.Threading.Tasks;
using MassTransitSample.ProductConsumer.HostedServices;
using MassTransitSample.ProductConsumer.Services;
using MassTransitSample.ProductConsumer.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MassTransitSample.ProductConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<UpdateProductPriceService>();
                    services.AddTransient<IProductService, ProductService>();
                });

            await builder.RunConsoleAsync();
        }
    }
}