namespace MassTransitSample.ProductApi.Models.Requests
{
    public class UpdateProductPriceRequest
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}