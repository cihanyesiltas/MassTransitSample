using MassTransitSample.Contracts.Commands;

namespace MassTransitSample.ProductApi.Models.Requests
{
    public class UpdateProductPriceRequest:IUpdateProductPriceCommand
    {
        public int ProductId { get; }
        public decimal Price { get; }
    }
}