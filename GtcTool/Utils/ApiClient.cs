using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GtcTool.Utils;

public class ApiClient(HttpClient client, IConfiguration config, ILogger logger)
{
    private readonly HttpClient _client = client ?? throw new ArgumentException(nameof(client));
    private readonly IConfiguration _config = config ?? throw new ArgumentException(nameof(config));
    private readonly ILogger _logger = logger ?? throw new ArgumentException(nameof(logger));

    public async Task<string> GetResponseJsonAsync(string configBaseUrlKey, string configQueryEndpointKey)
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

        try
        {
            var baseUri = new Uri(baseUrl);
            var uri = new Uri(baseUri, queryEndpoint);
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