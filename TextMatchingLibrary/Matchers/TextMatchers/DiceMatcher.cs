using TextMatchingLibrary.Extensions;

namespace TextMatchingLibrary.Matchers
{
    public class DiceMatcher : IMatcher<string>
    {
        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return 0;
            if (first.Equals(second, StringComparison.InvariantCultureIgnoreCase)) return 1;
            if (first.Length < 2 && second.Contains(first)) return (double)first.Length / (double)second.Length;
            if (second.Length < 2 && first.Contains(second)) return (double)second.Length / (double)first.Length;

            Dictionary<string, int> firstBiGramms = new Dictionary<string, int>();

            for (int i = 0; i < first.Length - 1; i++)
            {
                string bigramm = first.Substring(i, 2);

                if (!firstBiGramms.ContainsKey(bigramm)) firstBiGramms.Add(bigramm, 1);
                else firstBiGramms[bigramm]++;
            }

            int matches = 0;
            for (int i = 0; i < second.Length - 1; i++)
            {
                string bigramm = second.Substring(i, 2);

                if (firstBiGramms.ContainsKey(bigramm) && firstBiGramms[bigramm] > 0) 
                {
                    matches++;
                    firstBiGramms[bigramm]--;
                }
            }

            return (double)(matches * 2) / (double)(first.Length + second.Length - 2);
        }
    }
}