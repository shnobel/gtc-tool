using GtcTool.Services;

var client = new HttpClient();
try
{
    var response = await FederalRegisterService.GetResponseAsync(client);
    Console.WriteLine(response);
}
catch (Exception)
{
    Console.WriteLine("There was an error calling the Federal Register API.");
}
