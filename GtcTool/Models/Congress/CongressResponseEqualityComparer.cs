using System.Diagnostics.CodeAnalysis;

namespace GtcTool.Models.Congress
{
    public sealed class CongressResponseEqualityComparer : EqualityComparer<CongressResponse>
    {
        public override bool Equals(CongressResponse? x, CongressResponse? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            return x.Count == y.Count
                && x.Message == y.Message
                && x.NextPage == y.NextPage
                && x.PreviousPage == y.PreviousPage
                && x.Packages.SequenceEqual(y.Packages);
        }

        public override int GetHashCode([DisallowNull] CongressResponse obj)
        {
            if (obj == null) return 0;

            int hash = 17;
            hash = hash * 23 + obj.Count.GetHashCode();
            hash = hash * 23 + (obj.Message?.GetHashCode(StringComparison.Ordinal) ?? 0);
            hash = hash * 23 + (obj.NextPage?.GetHashCode(StringComparison.Ordinal) ?? 0);
            hash = hash * 23 + (obj.PreviousPage?.GetHashCode(StringComparison.Ordinal) ?? 0);
            hash = hash * 23 + obj.Packages.Aggregate(0, (acc, package) => acc ^ package.GetHashCode());
            return hash;
        }
    }
}
