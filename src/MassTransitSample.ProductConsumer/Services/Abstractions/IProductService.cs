using System.Threading.Tasks;
using MassTransitSample.ProductConsumer.Models.Requests;
using MassTransitSample.ProductConsumer.Models.Responses;

namespace MassTransitSample.ProductConsumer.Services.Abstractions
{
    public interface IProductService
    {
        Task<UpdateProductPriceResponse> UpdateProductPriceAsync(UpdateProductPriceRequest request);
    }
}