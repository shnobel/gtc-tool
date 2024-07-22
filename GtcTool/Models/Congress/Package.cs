namespace GtcTool.Models.Congress;

public record Package(string PackageId, DateTime LastModified, string PackageLink, string DocClass, 
    string Title, string Congress, DateTime DateIssued)
{
    public override string ToString()
    {
        return $"""

                 ##Package
                 PackageId: {PackageId}
                 LastModified: {LastModified}
                 PackageLink: {PackageLink}
                 DocClass: {DocClass}
                 Title: {Title}
                 Congress: {Congress}
                 DateIssued: {DateIssued}
                 """;
    }
}

