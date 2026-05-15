using System.Text;
using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class LongestSubstringMatcher : BaseMatcher, IMatcher<string>
    {
        public LongestSubstringMatcher()
            :base() { }

        public LongestSubstringMatcher(INormalizer normalizer)
            :base(normalizer) { }

        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return 0;

            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

            int maxWordLength = Math.Max(first.Length, second.Length);

            int[,] values = new int[first.Length + 1, second.Length + 1];
            int maxLength = 0;
            for (int i = 1; i < first.Length + 1; i++)
            {
                for (int j = 1; j < second.Length + 1; j++)
                {
                    if (first[i - 1] == second[j - 1])
                    {
                        values[i, j] = values[i - 1, j - 1] + 1;
                        maxLength = Math.Max(maxLength, values[i, j]);
                    }
                }
            }

            return (double)(maxLength*2) / (double)(first.Length + second.Length);
        }
    }
}
