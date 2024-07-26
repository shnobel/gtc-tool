using Gtc.Models.FederalRegister;
using System.Text.Json;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;

namespace GtcTool.Services.FederalRegister
{
    public class FederalRegisterService
    {
        private const string ConfigBaseUrlKey = "FederalRegister:BaseUrl";
        private const string ConfigQueryEndpointKey = "FederalRegister:Documents";

        private readonly ApiClient _client;
        private readonly ILogger _logger;
        public FederalRegisterService(ApiClient client, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _client = client ?? throw new ArgumentException(nameof(client));
        }

        public virtual async Task<Response> GetResponseAsync()
        {
            var responseJson = await _client.GetResponseJsonAsync(ConfigBaseUrlKey, ConfigQueryEndpointKey);
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