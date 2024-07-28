using GtcTool.Services.Congress;
using GtcTool.Services.FederalRegister;

namespace Gtc.Services;

class MenuService
{ //
    private readonly FederalRegisterService FederalRegisterService;
    private readonly CongressService CongressService;
    private readonly CongressAnalysisService CongressAnalysisService;
    public MenuService(FederalRegisterService federalRegisterService, CongressService congressService,
    CongressAnalysisService congressAnalysisService)
    { //
        FederalRegisterService = federalRegisterService;
        CongressService = congressService;
        CongressAnalysisService = congressAnalysisService;
    }
    public async Task ShowMenu()
    { //
        while (true)
        { //
            Console.Clear();
            Console.WriteLine("Welcome to the GTC Consulting Tool!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Federal Register");
            Console.WriteLine("2. Congress");
            Console.WriteLine("3. Compare Congress cached data with API data");
            Console.WriteLine("4. Exit");
            Console.Write("Selection: ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out var selection))
            { //
                switch (selection)
                { //
                    case 1:
                        Console.WriteLine("You chose option 1.");
                        var responseFederal = await FederalRegisterService.GetResponseAsync();
                        Console.WriteLine(responseFederal);
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("You chose option 2.");

                        var responseCongress = await CongressService.GetCongressResponseWithPackagesAsync();
                        Console.WriteLine(responseCongress.response);
                        if (responseCongress.packages.Count > 0)
                        { //
                            Console.WriteLine("There are specific documents for your request.");
                            Console.WriteLine(responseCongress.packages);
                        }
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("You chose option 3.");
                        await CongressAnalysisService.CompareCongressDataAsync();
                        break;
                    case 4:
                        Console.WriteLine("You chose option 4.");
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
            else
            { //
                Console.WriteLine("Invalid selection.");
                Console.ReadLine();
            }
        }
    }
}