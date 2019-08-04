namespace MassTransitSample.ProductApi.Infrastructure
{
    public class RabbitMqConnectionConfig
    {
        public string HostUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProductPriceCommandQueueName { get; set; }
    }
}