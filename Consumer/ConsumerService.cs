namespace Consumer
{
    using Consumer.Model;
    using Consumer.Properties;

    using Core.Interfaces.Services;

    using System.Net.Http.Headers;

    public class ConsumerService<TResponse> : IConsumer<TResponse>
        where TResponse : notnull
    {
        private HttpClient Client { get; set; } = new();
        private string RequestUri { get; } = string.Empty;

        public ConsumerService(ConsumerModel consumerModel)
        {
            if (consumerModel is null)
                throw new ArgumentNullException(nameof(consumerModel));

            RequestUri = consumerModel.RequestUri;
            Client.BaseAddress = new Uri(consumerModel.BaseURL);
            Client.DefaultRequestHeaders.Add(Resources.ACCEPT_HEADER, consumerModel.AcceptHeader);
            Client.DefaultRequestHeaders.Add(Resources.USERAGENT_HEADER, consumerModel.UserAgent);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(consumerModel.MediaType));
        }

        private async Task<HttpResponseMessage> ConsumeAPI()
        {
            return await Client.GetAsync(RequestUri);
        }

        public async Task<TResponse> GetItemAsync()
        {
            HttpResponseMessage response = await ConsumeAPI();

            return !response.IsSuccessStatusCode ?
                throw new Exception(Resources.CONSUMER_ERROR) :
                await response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<IEnumerable<TResponse>> GetItemsAsync()
        {
            HttpResponseMessage response = await ConsumeAPI();

            return !response.IsSuccessStatusCode ?
                throw new Exception(Resources.CONSUMER_ERROR) :
                await response.Content.ReadAsAsync<IEnumerable<TResponse>>();
        }
    }
}