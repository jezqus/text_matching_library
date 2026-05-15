using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class LengthMatcher : IMatcher<string>
    {
        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty()) return 0;
            if (second.IsNullOrEmpty()) return 0;

            return (double)(2 * Math.Min(first.Length, second.Length)) / (double)(first.Length + second.Length);
        }
    }
}
