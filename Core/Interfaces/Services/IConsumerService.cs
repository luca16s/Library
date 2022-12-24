namespace Core.Interfaces.Services
{
    public interface IConsumerService<TResponse>
        where TResponse : notnull
    {
        Task<TResponse> GetItemAsync();

        Task<IEnumerable<TResponse>> GetItemsAsync();
    }
}
