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

using Microsoft.AspNetCore.Authentication.JwtBearer;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

public class Consumer<TResponse>(
    HttpClient client
) : IConsumer<TResponse>
    where TResponse : notnull
{
    private const string ACCEPT = "accept";
    private const string USER_AGENT = "User-Agent";
    private const string JSON = "application/json";
    private const string ACCEPT_CONTENT = "application/json;odata.metadata=minimal";

    private static StringContent GetContent<TContent>(
        TContent value
    ) => new(
        JsonConvert.SerializeObject(value),
        Encoding.UTF8,
        "application/json"
    );

    private static async Task<TResponse?> GetResultAsync(
        HttpResponseMessage response
    ) => HttpStatusCode.OK == response?.StatusCode
        ? JsonConvert.DeserializeObject<TResponse>(
            await response.Content.ReadAsStringAsync(),
            new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }
        ) : throw new ParseResponseNotSucced(
            $"{response?.ReasonPhrase} \n{response?.RequestMessage}",
            typeof(TResponse).Name
        );

    private async Task<TResponse?> ConsumeAsync(
        string token,
        ApplicationInfo appInfo,
        HttpRequestMessage request
    )
    {
        ArgumentNullException.ThrowIfNull(appInfo);

        request.Headers.Accept.Clear();
        request.Headers.Add(ACCEPT, ACCEPT_CONTENT);
        request.Headers.Add(USER_AGENT, appInfo.Name);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON));

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

        HttpResponseMessage response = await client.SendAsync(request);

        return response?.IsSuccessStatusCode != true
            ? throw new ConnectionNotSucced(request.RequestUri?.OriginalString ?? string.Empty)
            : await GetResultAsync(response);
    }

    public async Task<TResponse?> GetAsync(
        string url,
        string requestUri,
        string token,
        int requestTimeout,
        ApplicationInfo appInfo
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        return await ConsumeAsync(
            token: token,
            appInfo: appInfo,
            request: new(HttpMethod.Get, url + requestUri)
        );
    }

    public async Task<TResponse?> PostAsync<TContent>(
        string url,
        string requestUri,
        string token,
        TContent content,
        int? requestTimeout,
        ApplicationInfo appInfo
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        ArgumentException.ThrowIfNullOrWhiteSpace(requestUri);

        return await ConsumeAsync(
            token: token,
            appInfo: appInfo,
            request: new(HttpMethod.Post, url + requestUri) { Content = GetContent(content) }
        );
    }
}
