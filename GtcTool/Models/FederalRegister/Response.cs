namespace Gtc.Models.FederalRegister;

public class Response(int count, string description, int totalPages, string nextPageUrl)
{
    public int Count { get; } = count;
    public string Description { get; } = description;
    public int TotalPages { get;  } = totalPages;
    public string NextPageUrl { get; } = nextPageUrl;
    public List<Document> Results { get; } = new List<Document>();

    public override string ToString()
    {
        return $"""
                 #Response
                 Count: {Count}
                 Description: {Description}
                 TotalPages: {TotalPages}
                 NextPageUrl: {NextPageUrl}
                 Results: {Results.ListToString()}
                 """;
    }
}