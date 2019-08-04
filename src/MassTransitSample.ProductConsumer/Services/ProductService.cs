using System.Threading.Tasks;
using MassTransitSample.ProductConsumer.Models.Requests;
using MassTransitSample.ProductConsumer.Models.Responses;
using MassTransitSample.ProductConsumer.Services.Abstractions;

namespace MassTransitSample.ProductConsumer.Services
{
    public class ProductService:IProductService
    {
        public async Task<UpdateProductPriceResponse> UpdateProductPriceAsync(UpdateProductPriceRequest request)
        {
            return await Task.FromResult(new UpdateProductPriceResponse
            {
                Price = request.Price,
                ProductId = request.ProductId
            });
        }
    }
}