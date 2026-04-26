using TextMatchingLibrary.Extensions;

namespace TextMatchingLibrary.Matchers
{
    public class JaroMatcher : IMatcher<string>
    {
        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return 0;
            
            int maxLength = Math.Max(first.Length, second.Length);
            int maxDistance = (int)Math.Floor((double)maxLength / 2) - 1;

            int[] matchesInFirst = new int[first.Length];
            int[] matchesInSecond = new int[second.Length];
            int matches = 0;
            for (int i = 0; i < first.Length; i++)
            {
                for (int j = Math.Max(0, i - maxDistance); j < Math.Min(second.Length, i + maxDistance + 1); j++)
                {
                    if (first[i] == second[j] && matchesInSecond[j] == 0)
                    {
                        matchesInFirst[i] = 1;
                        matchesInSecond[j] = 1;
                        matches++;
                        break;
                    }
                }
            }

            int transpositions = 0;
            int matchesInSecondCursor = 0;
            for (int i = 0; i < matchesInFirst.Length; i++)
            {
                if (matchesInFirst[i] == 1)
                {
                    while (matchesInSecond[matchesInSecondCursor] == 0) matchesInSecondCursor++;

                    if (first[i] != second[matchesInSecondCursor++])
                        transpositions++;
                }
            }

            return (((double)matches / (double)first.Length) + ((double)matches / (double)second.Length) + ((double)(matches - (transpositions/2)) / (double)matches)) / 3;
        }
    }
}