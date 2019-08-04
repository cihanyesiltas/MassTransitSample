using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.ProductConsumer.Services.Abstractions;
using Microsoft.Extensions.Hosting;

namespace MassTransitSample.ProductConsumer.HostedServices
{
    public class UpdateProductPriceService: BackgroundService
    {
        private readonly IBusControl _bus;
        private readonly IProductService _productService;

        public UpdateProductPriceService(IProductService productService)
        {
            _productService = productService;
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h => { });

                cfg.ReceiveEndpoint(host, "UpdateProductPrice", e =>
                {
                    e.Consumer<UpdateProductPriceConsumer>(() => new UpdateProductPriceConsumer(_productService));
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