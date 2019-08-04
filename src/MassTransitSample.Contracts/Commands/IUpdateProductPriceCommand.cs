namespace MassTransitSample.Contracts.Commands
{
    public interface IUpdateProductPriceCommand
    {
        int ProductId { get; }
        decimal Price {get;}
    }
}