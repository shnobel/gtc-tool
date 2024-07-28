using Microsoft.Extensions.Logging;

namespace GtcTool.Services.Congress
{
    public class CongressAnalysisService
    {
        private readonly CongressService _congressService;
        private readonly CongressFileService _congressFileService;
        private readonly CongressComparerService _congressComparerService;
        private readonly ILogger _logger;

        public CongressAnalysisService(CongressService congressService, CongressFileService congressFileService, CongressComparerService congressComparerService, ILogger logger)
        {
            _congressService = congressService;
            _congressFileService = congressFileService;
            _congressComparerService = congressComparerService;
            _logger = logger;
        }

        public async Task CompareCongressDataAsync()
        {
            try
            {
                var fileResponse = await _congressFileService.GetCongressResponseAsync();
                var response = await _congressService.GetCongressResponseAsync();
                var result = _congressComparerService.GetAnalysis(fileResponse, response);
                Console.WriteLine(result.AnalysisResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error appeared during congress data analysis");
            }
        }
    }
}
