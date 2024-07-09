namespace Gtc.Models.FederalRegister;

public class Agency(string rawName, string name, int id, string url, string jsonUrl, int parentId, string slug)
{
    public string RawName { get; } = rawName;
    public string Name { get; } = name;
    public int Id { get;  } = id;
    public string Url { get; } = url;
    public string JsonUrl { get; } = jsonUrl;
    public int? ParentId { get; } = parentId;
    public string Slug { get; } = slug;

    public override string ToString()
    {
        return $"""
               
               ###Agency
               RawName: {RawName}
               Name: {Name}
               Id: {Id}
               Url: {Url}
               JsonUrl: {JsonUrl}
               ParentId: {ParentId}
               Slug: {Slug}
               """;
    }
}
