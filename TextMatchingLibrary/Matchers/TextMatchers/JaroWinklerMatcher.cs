using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class JaroWinklerMatcher : JaroMatcher
    {
        public JaroWinklerMatcher()
            :base() { }

        public JaroWinklerMatcher(INormalizer normalizer)
            :base(normalizer) { }

        public override double Match(string first, string second)
        {
            if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return 0;

            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

            var jaroDistance = base.Match(first, second);

            var acceptedLength = Math.Min(Math.Min(first.Length, second.Length), 4);
            var commonPrefixLength = 0;
            for(var i = 0; i < acceptedLength; i++)
            {
                if (first[i] == second[i]) commonPrefixLength++;
                else break;
            }

            return jaroDistance + 0.1 * commonPrefixLength * (1 - jaroDistance);
        }
    }
}