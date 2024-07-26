using GtcTool.Models.Congress;
using GtcTool.Services.Storage;
using GtcTool.Utils;
using Microsoft.Extensions.Logging;

namespace GtcTool.Services.Congress
{
    public class CongressFileService : CongressService
    {
        private readonly FileStorageService _fileStorageService;
        private const string FileName = "congress-response.json";

        public CongressFileService(FileStorageService fileStorageService, ApiClient client, ILogger logger)
            : base(client, logger)
        {
            _fileStorageService = new FileStorageService(logger);
        }

        public override async Task<CongressResponse?> GetCongressResponseAsync()
        {
            var data = await _fileStorageService.GetObjectFromFileAsync<CongressResponse>(FileName);
            if (data == null)
            {
                var response = await base.GetCongressResponseAsync();
                await _fileStorageService.SaveToFileAsync(FileName, response);
                data = response;
            }

            return data;
        }
    }
}
