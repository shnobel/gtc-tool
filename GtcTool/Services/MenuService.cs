using Gtc.Models;
using GtcTool.Services;

namespace Gtc.Services;

class MenuService
{
    private readonly FederalRegisterService _federalRegisterService;
    private readonly CongressService _congressService;
    public MenuService(FederalRegisterService federalRegisterService, CongressService congressService)
    {
        _federalRegisterService = federalRegisterService;
        _congressService = congressService;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the GTC Consulting Tool!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Federal Register");
            Console.WriteLine("2. Congress");
            Console.WriteLine("3. Exit");
            Console.Write("Selection: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var selection))
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("You chose option 1.");
                        var responseFederal = await _federalRegisterService.GetResponseAsync();
                        Console.WriteLine(responseFederal);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("You chose option 2.");
                        var responseCongress = await _congressService.GetCongressResponseWithPackagesAsync();
                        Console.WriteLine(responseCongress.response);
                        if (responseCongress.packages.Count > 0)
                        {
                            Console.WriteLine("There are specific documents for your request.");
                            Console.WriteLine(responseCongress.packages.ListToString());
                        }
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("You chose option 3.");
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                Console.ReadLine();
            }
        }
    }
}