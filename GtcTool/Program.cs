using Gtc.Services;
using GtcTool.Services.Congress;
using GtcTool.Services.FederalRegister;
using GtcTool.Services.Storage;
using GtcTool.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

//Init log
using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = factory.CreateLogger("Gtc-tool");

//Init config
var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>();
var config = builder.Build();

//Init http client
var client = new HttpClient();
var apiClient = new ApiClient(client, config, logger);

//Init memory services
var memoryService = new MemoryStorageService();
var fileService = new FileStorageService(logger);

//Init services
var federalRegisterMemoryService = new FederalRegisterMemoryService(memoryService, apiClient, logger);
var congressService = new CongressService(apiClient, logger);
var congressFileService = new CongressFileService(fileService, apiClient, logger);
var congressComparerService = new CongressComparerService();
var congressAnalysisService = new CongressAnalysisService(congressService, congressFileService, congressComparerService, logger);

//Init menu
var menu = new MenuService(federalRegisterMemoryService, congressFileService, congressAnalysisService);
await menu.ShowMenu();
