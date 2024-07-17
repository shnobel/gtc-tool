using Microsoft.Extensions.Configuration;
using GtcTool.Services;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;

//Init log
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = factory.CreateLogger("Gtc-tool");

//Init config
var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
var config = builder.Build();

//Init http client
var client = new HttpClient();
var apiClient = new ApiClient(client, config, logger);

//Init services
var federalRegisterService = new FederalRegisterService(apiClient, logger);

try
{
    var response = await federalRegisterService.GetResponseAsync();
    Console.WriteLine(response);
}
catch (Exception)
{
    Console.WriteLine("There was an error calling the Federal Register API.");
}