// See https://aka.ms/new-console-template for more information

using Gtc.Services;

var response = FederalRegisterService.GetFederalRegisterResponse();
Console.WriteLine(response);
