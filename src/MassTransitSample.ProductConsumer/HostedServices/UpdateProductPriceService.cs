using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.ProductConsumer.Infrastructure;
using MassTransitSample.ProductConsumer.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MassTransitSample.ProductConsumer.HostedServices
{
    public class UpdateProductPriceService: BackgroundService
    {
        private readonly IBusControl _bus;

        public UpdateProductPriceService(IProductService productService, IOptions<RabbitMqConnectionConfig> options)
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(options.Value.HostUrl), h => { });

                cfg.ReceiveEndpoint(host, options.Value.ProductPriceCommandQueueName, e =>
                {
                    e.Consumer<UpdateProductPriceConsumer>(() => new UpdateProductPriceConsumer(productService));
                });
            });

            _bus.Start();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(base.StopAsync(cancellationToken), _bus.StopAsync(cancellationToken));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _bus.StartAsync(stoppingToken);
        }
    }
}