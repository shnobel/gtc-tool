using GtcTool.Models.Congress;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GtcTool.Services.Congress;

public class CongressService
{
    private const string ConfigBaseUrlKey = "Congress:BaseUrl";
    private const string ConfigQueryEndpointKey = "Congress:Collections";
    private const string ConfigSecretApiKey = "Gtc:ApiKey";

    private readonly ApiClient _client;
    private readonly ILogger _logger;

    public CongressService(ApiClient client, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _client = client ?? throw new ArgumentException(nameof(client));
    }

    public virtual async Task<CongressResponse?> GetCongressResponseAsync()
    {
        var responseJson = await _client.GetResponseJsonAsync(ConfigBaseUrlKey, ConfigQueryEndpointKey, ConfigSecretApiKey);
        if (string.IsNullOrEmpty(responseJson))
        {
            return new CongressResponse();
        }

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var response = JsonSerializer.Deserialize<CongressResponse>(responseJson, options);
            return response ?? new CongressResponse();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Invalid JSON response from Federal Register API.");
            return new CongressResponse();
        }
    }

    public async Task<(CongressResponse? response, List<Package> packages)> GetCongressResponseWithPackagesAsync()
    {
        var congressResponse = await GetCongressResponseAsync();
        var packages = GetSpecificPackages(congressResponse);

        return (congressResponse, packages);
    }

    private List<Package> GetSpecificPackages(CongressResponse response)
    {
        var listOfInterestWords = new[] { "agric", "water", "plant", "marketing", "hearing", "activities", "minerals" };
        return response.Packages
            .Where(package => listOfInterestWords.Any(word => package.Title.ToLower().Contains(word))).ToList();
    }
}