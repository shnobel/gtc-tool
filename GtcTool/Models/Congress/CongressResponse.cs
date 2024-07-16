namespace GtcTool.Models.Congress;

record CongressResponse(int Count, string Message, string NextPage, 
    string PreviousPage, List<Package> Packages);