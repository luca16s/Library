namespace Consumer;

using System.Net.Http.Headers;

public class Consumer<TResponse>
    where TResponse : notnull
{
    public HttpClient Client { get; set; } = new HttpClient();

    public async Task<TResponse?> GetItemAsync(
        string url,
        string requestUri,
        string mediaType = "application/json"
    )
    {
        Client.BaseAddress = new Uri(url);

        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        Client.DefaultRequestHeaders.Add("accept", "application/json;odata.metadata=minimal");

        HttpResponseMessage response = await Client.GetAsync(requestUri);

        return !response.IsSuccessStatusCode ?
            throw new Exception() :
            await response.Content.ReadAsAsync<TResponse>();
    }

    public async Task<IEnumerable<TResponse>> GetItemsAsync(
        string url = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.4390/dados"
    )
    {
        Client.BaseAddress = new Uri(url);
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        Client.DefaultRequestHeaders.Add("accept", "application/json;odata.metadata=minimal");

        HttpResponseMessage response = await Client.GetAsync("?formato=json");

        return !response.IsSuccessStatusCode ?
            throw new Exception() :
            await response.Content.ReadAsAsync<IEnumerable<TResponse>>();
    }
}
