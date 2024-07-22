using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GtcTool.Utils;

public class ApiClient
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public ApiClient(HttpClient client, IConfiguration config, ILogger logger)
    {
        _client = client ?? throw new ArgumentException(nameof(client));
        _config = config ?? throw new ArgumentException(nameof(config));
        _logger = logger ?? throw new ArgumentException(nameof(logger));
    }

    public async Task<string> GetResponseJsonAsync(string configBaseUrlKey, string configQueryEndpointKey, string? apiKey = null)
    {
        //TODO move to config reader extension
        var baseUrl = _config[configBaseUrlKey];
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new ArgumentException("Base URL is not configured properly.");
        }

        var queryEndpoint = _config[configQueryEndpointKey];
        if (string.IsNullOrEmpty(queryEndpoint))
        {
            throw new ArgumentException("Query Endpoint is not configured properly.");
        }
        
        var baseUri = new Uri(baseUrl);
        Uri uri;

        if (!string.IsNullOrEmpty(apiKey)) 
        {
            //TODO consider to improve this part later in case we extend services with different API
            var congressApiKey = _config.GetSection("Gtc");
            var value = _config.GetValue<string>(apiKey);
            queryEndpoint = queryEndpoint.Replace("{apiKey}", value);
        }

        uri = new Uri(baseUri, queryEndpoint);

        try
        {
            using HttpResponseMessage msg = await _client.GetAsync(uri);
            msg.EnsureSuccessStatusCode();
            return await msg.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching data from Federal Register API.");
            return string.Empty;
        }
    }
}