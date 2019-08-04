namespace MassTransitSample.ProductConsumer.Infrastructure
{
    public class RabbitMqConnectionConfig
    {
        public string HostUrl { get; set; }
        public string ProductPriceCommandQueueName { get; set; }
    }
}