namespace GtcTool.Models
{
    public class ComparisonAnalysis<T>
    {
        public ComparisonAnalysis(T response1, T response2, bool areEqual, string analysisResults)
        {
            Response1 = response1;
            Response2 = response2;
            AreEqual = areEqual;
            AnalysisResults = analysisResults;
        }
        public T Response1 { get; set; }
        public T Response2 { get; set; }
        public bool AreEqual { get; set; }
        public string AnalysisResults { get; set; }
    }
}
