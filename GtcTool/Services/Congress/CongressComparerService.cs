using GtcTool.Models;
using GtcTool.Models.Congress;
using System.Text;

namespace GtcTool.Services.Congress
{
    public class CongressComparerService
    {
        public static CongressResponseEqualityComparer Comparer { get; } = new CongressResponseEqualityComparer();

        public ComparisonAnalysis<CongressResponse> GetAnalysis(CongressResponse oldResponse, CongressResponse newResponse)
        {
            var areEqual = true;
            StringBuilder analysisResults = new StringBuilder();

            if (!Comparer.Equals(oldResponse, newResponse))
            {
                areEqual = false;
                analysisResults.Append(ComparerMessages.ThereIsNewInformation);

                if (!oldResponse.Packages.SequenceEqual(newResponse.Packages))
                {
                    analysisResults.Append(ComparerMessages.ThereAreDifferentPackages);
                }
                else
                {
                    analysisResults.Append(ComparerMessages.ThereAreNoDifferentPackages);
                }
            }
            else
            {
                analysisResults.Append(ComparerMessages.BothResponsesAreTheSame);
            }

            return new ComparisonAnalysis<CongressResponse>(oldResponse, newResponse, areEqual, analysisResults.ToString());
        }
    }
}
