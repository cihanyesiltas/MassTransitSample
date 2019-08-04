using MassTransitSample.Contracts.Commands;

namespace MassTransitSample.ProductApi.Models
{
    public class UpdateProductPriceCommand : IUpdateProductPriceCommand
    {
        public int ProductId { get; }
        public decimal Price { get; }
    }
}