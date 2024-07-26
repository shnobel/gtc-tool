using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GtcTool.Services.Storage
{
    public class FileStorageService
    {
        private readonly ILogger _logger;

        public FileStorageService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task SaveToFileAsync<T>(string fileName, T responce)
        {
            using (var writer = File.CreateText(fileName))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(responce, options);
                await writer.WriteAsync(json);
            }
        }

        public async Task<T?> GetObjectFromFileAsync<T>(string fileName)
        {
            try
            {
                char[] buffer;
                using (var sr = new StreamReader(fileName))
                {
                    buffer = new char[(int)sr.BaseStream.Length];
                    await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
                }
                return JsonSerializer.Deserialize<T>(buffer);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "No such file");
                return default;
            }
        }
    }
}
