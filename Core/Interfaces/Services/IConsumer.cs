namespace Core.Interfaces.Services
{
    public interface IConsumer<TResponse>
        where TResponse : notnull
    {
        Task<TResponse> GetItemAsync();

        Task<IEnumerable<TResponse>> GetItemsAsync();
    }
}
