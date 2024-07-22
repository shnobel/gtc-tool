using Gtc.Models;

namespace GtcTool.Models.Congress;

public record CongressResponse(int Count = 0, string? Message = "", string NextPage = "",
    string? PreviousPage = "", List<Package> Packages = null)
{
    public override string ToString()
    {
        return $"""
                 #CongressResponse
                 Count: {Count}
                 Message: {Message}
                 NextPage: {NextPage}
                 PreviousPage: {PreviousPage}
                 Packages: {Packages?.ListToString()}
                 """;
    }
}