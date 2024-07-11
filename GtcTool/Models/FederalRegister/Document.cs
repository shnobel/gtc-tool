namespace Gtc.Models.FederalRegister;

public class Document(string title, string type, string @abstract, string documentNumber,
    string htmlUrl, string pdfUrl, string publicInspectionPdfUrl, string publicationDate, string excerpts)
{
    public string Title { get; } = title;
    public string Type { get; } = type;
    public string Abstract { get; } = @abstract;
    public string DocumentNumber { get; } = documentNumber;
    public string HtmlUrl { get; } = htmlUrl;
    public string PdfUrl { get; } = pdfUrl;
    public string PublicInspectionPdfUrl { get; } = publicInspectionPdfUrl;
    public string PublicationDate { get; } = publicationDate;
    public List<Agency> Agencies { get; set; } = new List<Agency>();
    public string Excerpts { get; } = excerpts;

    public override string ToString()
    {
        return $"""
                
                ##Document
                Title: {Title}
                Type: {Type}
                Abstract: {Abstract}
                DocumentNumber: {DocumentNumber}
                HtmlUrl: {HtmlUrl}
                PdfUrl: {PdfUrl}
                PublicInspectionPdfUrl: {PublicInspectionPdfUrl}
                PublicationDate: {PublicationDate}
                Agencies: {Agencies.ListToString()}
                Excerpts: {Excerpts}
                """
            ;
    }
}