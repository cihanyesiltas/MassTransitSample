using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.Contracts.Commands;
using MassTransitSample.ProductConsumer.Models.Requests;
using MassTransitSample.ProductConsumer.Services.Abstractions;

namespace MassTransitSample.ProductConsumer.HostedServices
{
    public class UpdateProductPriceConsumer : IConsumer<IUpdateProductPriceCommand>
    {
        private readonly IProductService _productService;

        public UpdateProductPriceConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<IUpdateProductPriceCommand> context)
        {
            var command = context.Message;

            var response = await _productService.UpdateProductPriceAsync(new UpdateProductPriceRequest
            {
                Price = command.Price,
                ProductId = command.ProductId
            });

            if (response != null)
            {
                Console.WriteLine($"UpdateProductPriceConsumer: ProductId: {response.ProductId} Price: {response.Price} updated");
            }
        }
    }
}