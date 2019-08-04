using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransitSample.Contracts.Commands;
using MassTransitSample.ProductApi.Infrastructure;
using MassTransitSample.ProductApi.Models;
using MassTransitSample.ProductApi.Models.Requests;
using MassTransitSample.ProductApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MassTransitSample.ProductApi.Controllers
{
    [Route("api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOptions<RabbitMqConnectionConfig> _options;
        
        public ProductController(ISendEndpointProvider sendEndpointProvider, IOptions<RabbitMqConnectionConfig> options)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _options = options;            
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductPriceRequest request)
        {
            var response = new BaseResponse<UpdateProductPriceResponse>
            {
                Result = new UpdateProductPriceResponse
                {
                    ProductId = request.ProductId,
                    Price = request.Price
                }
            };
            
            var bus = await GetSendEndpoint();

            await bus.Send<IUpdateProductPriceCommand>(request);

            return Ok(response);
        }

        private async Task<ISendEndpoint> GetSendEndpoint()
        {
            return await _sendEndpointProvider.GetSendEndpoint(
                new Uri($"{_options.Value.HostUrl}{_options.Value.ProductPriceCommandQueueName}"));
        }
    }
}