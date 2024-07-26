using Gtc.Models.FederalRegister;
using GtcTool.Services.Storage;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;

namespace GtcTool.Services.FederalRegister
{
    public class FederalRegisterMemoryService : FederalRegisterService
    {
        private const string Key = "Federal";
        private readonly MemoryStorageService _memoryService;

        public FederalRegisterMemoryService(MemoryStorageService memoryService, ApiClient client, ILogger logger)
            : base(client, logger)
        {
            _memoryService = memoryService;
        }

        public override async Task<Response> GetResponseAsync()
        {
            var cache = _memoryService.Get<Response>(Key);
            if (cache == null)
            {
                var response = await base.GetResponseAsync();
                _memoryService.Save(Key, response);
                cache = response;
            }

            return cache;
        }
    }
}
