using Gtc.Models.FederalRegister;
using System.Text.Json;

namespace GtcTool.Services
{
    public class FederalRegisterService
    {
        public static async Task<string> GetResponseJsonAsync(HttpClient client)
        {
            try
            {
                //TODO split base url and endpoint, config
                using HttpResponseMessage msg = await client.GetAsync("https://www.federalregister.gov/api/v1/documents.json?conditions[publication_date][year]=2023&conditions[agencies][]=agriculture-department");
                msg.EnsureSuccessStatusCode();
                return await msg.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message {ex.Message}");
                return string.Empty;
            }
        }

        public static async Task<Response> GetResponseAsync(HttpClient client)
        {
            var str = await GetResponseJsonAsync(client);
            if (str == string.Empty)
            {
                return new Response();
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                };
                var response = JsonSerializer.Deserialize<Response>(str, options);
                return response ?? new Response();
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON is invalid");
                Console.WriteLine(ex);
                return new Response();
            }
        }
    }
}
