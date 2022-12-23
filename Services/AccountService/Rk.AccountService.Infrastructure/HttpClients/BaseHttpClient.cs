using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using DateOnlyTimeOnly.AspNet.Converters;
using Microsoft.Extensions.Logging;
using Rk.Messages.Common.Json;

namespace Rk.AccountService.Infrastructure.HttpClients;

public abstract class BaseHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    
    protected BaseHttpClient(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    protected async Task<T?> GetResponse<T>(HttpRequestMessage request, JsonSerializerOptions? jsonOption = null)
    {
        var response = await _httpClient.SendAsync(request);
        await Log(request, response);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<T>(responseContent, jsonOption ?? GetJsonOption());
        return result;
    }

    protected async Task<HttpResponseMessage> Send(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request);
        await Log(request, response);
        return response.EnsureSuccessStatusCode();
    }
    
    protected async Task<Stream?> GetResponseStream(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request);
        await Log(request, response);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStreamAsync();
        return responseContent;
    }


    protected static JsonSerializerOptions GetJsonOption(
        JsonNamingPolicy? namingPolicy = null,
        JsonIgnoreCondition jsonCondition = JsonIgnoreCondition.WhenWritingNull, 
        JavaScriptEncoder? javaScriptEncoder = null, 
        IEnumerable<JsonConverter>? converters = null)
    {
        var option = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = jsonCondition,
            PropertyNameCaseInsensitive = true,
            Encoder = javaScriptEncoder ?? JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            PropertyNamingPolicy = namingPolicy ?? JsonNamingPolicy.CamelCase
        };

        option.Converters.Add(new DateOnlyJsonConverter());
        option.Converters.Add(new TimeOnlyJsonConverter());

        if (converters is null) return option;
        foreach (var jsonConverter in converters) option.Converters.Add(jsonConverter);
        return option;
    }

    private async Task Log(HttpRequestMessage request, HttpResponseMessage response)
    {
        _logger.LogDebug("Запрос: {@Method}  {@Content}  {@Header}  {@Uri}", 
            request.Method, request.Content, request.Headers, request.RequestUri);
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogDebug("Ответ: {@Content}  {@Header}  {@Status}  {@Reason}  {@TrailingHeaders}", 
            content, response.Headers, response.StatusCode, response.ReasonPhrase, response.TrailingHeaders);
    }
}