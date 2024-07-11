using Gtc.Models.FederalRegister;

namespace Gtc.Services;

public class MockFederalRegisterService
{
    public static Response GetFederalRegisterResponse()
    {
        Agency a = new Agency("DEPARTMENT OF AGRICULTURE", "Agriculture Department", 12,
            "https://...", "https://...", 12, "agriculture-department");
        Document d = new Document("title", "type", "abstract",
            "asdp123kp", "url", "pdfUrl", "url", "date", "exrec");
        d.Agencies.Add(a);
        Response r = new Response(950363, "All Documents", 50,
            "https://www.federalregister.gov/api/v1/documents?format=json&page=2&per_page=20");
        r.Results.Add(d);

        return r;
    }
}

