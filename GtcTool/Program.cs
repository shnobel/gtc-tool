using Microsoft.Extensions.Configuration;
using GtcTool.Services;
using Microsoft.Extensions.Logging;

//Init log
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
var federalRegisterLogger = factory.CreateLogger<FederalRegisterService>();

//Init config
var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
var config = builder.Build();

//Init http client
var client = new HttpClient();

//Init services
var federalRegisterService = new FederalRegisterService(config, federalRegisterLogger, client);

try
{
    var response = await federalRegisterService.GetResponseAsync();
    Console.WriteLine(response);
}
catch (Exception)
{
    Console.WriteLine("There was an error calling the Federal Register API.");
}
