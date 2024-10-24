// -----------------------------------------------------------------------
// <copyright file="Consumer.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Consumer;

using Core.Consumer.Exceptions;
using Core.CrossCutting;
using Core.Interfaces;

using Newtonsoft.Json;

using System.Net.Http.Headers;

public class Consumer<TResponse> : IConsumer<TResponse> where TResponse : notnull
{
    private const string ACCEPT = "accept";
    private const string USER_AGENT = "User-Agent";
    private const string JSON = "application/json";
    private const string ACCEPT_CONTENT = "application/json;odata.metadata=minimal";

    private static HttpClient Client { get; } = new();

    public async Task<TResponse> Consume(
        string url,
        string requestUri,
        ApplicationInfo appInfo
    )
    {
        ArgumentNullException.ThrowIfNull(appInfo);
        ArgumentException.ThrowIfNullOrWhiteSpace(url);

        Client.BaseAddress = new Uri(url);
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add(ACCEPT, ACCEPT_CONTENT);
        Client.DefaultRequestHeaders.Add(USER_AGENT, appInfo.Name);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON));

        HttpResponseMessage response = await Client.GetAsync(requestUri);

        if (response?.IsSuccessStatusCode != true)
            throw new ConnectionNotSucced(url);

        TResponse? result = JsonConvert.DeserializeObject<TResponse>(
            await response.Content.ReadAsStringAsync()
        );

        return result is not null ? result :
            throw new ParseResponseNotSucced(url, typeof(TResponse).Name);
    }
}
