namespace MassTransitSample.ProductApi.Models
{
    public class BaseResponse<T>
    {
        public T Result { get; set; }
    }
}