using Gtc.Models.FederalRegister;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GtcTool.Services
{
    public class FederalRegisterService(
        IConfiguration config,
        ILogger<FederalRegisterService> logger,
        HttpClient client)
    {
        private readonly IConfiguration _config = config ?? throw new ArgumentException(nameof(config));
        private readonly ILogger<FederalRegisterService> _logger = logger ?? throw new ArgumentException(nameof(logger));
        private readonly HttpClient _client = client ?? throw new ArgumentException(nameof(client));

        private async Task<string> GetResponseJsonAsync()
        {
            //TODO move to config reader extension
            var baseUrl = _config["FederalRegister:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentException("Base URL is not configured properly.");
            }
            
            var queryEndpoint = _config["FederalRegister:Documents"];
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

        public async Task<Response> GetResponseAsync()
        {
            var responseJson = await GetResponseJsonAsync();
            if (string.IsNullOrEmpty(responseJson))
            {
                return new Response();
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                };
                var response = JsonSerializer.Deserialize<Response>(responseJson, options);
                return response ?? new Response();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Invalid JSON response from Federal Register API.");
                return new Response();
            }
        }
    }
}
