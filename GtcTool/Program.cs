using Microsoft.Extensions.Configuration;
using GtcTool.Services;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;
using Gtc.Services;

//Init log
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = factory.CreateLogger("Gtc-tool");

//Init config
var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>();
var config = builder.Build();

//Init http client
var client = new HttpClient();
var apiClient = new ApiClient(client, config, logger);

//Init services
var federalRegisterService = new FederalRegisterService(apiClient, logger);
var congressService = new CongressService(apiClient, logger);

//Init menu
var menu = new MenuService(federalRegisterService, congressService);
await menu.ShowMenu();
