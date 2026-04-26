using TextMatchingLibrary.Extensions;

namespace TextMatchingLibrary.Matchers
{
    public class ContainsMatcher : IMatcher<string>
    {
        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty()) return 0;
            if (second.IsNullOrEmpty()) return 0;

            if (first.Equals(second, StringComparison.InvariantCultureIgnoreCase))
            {
                return 1;
            }else
            {
                if (first.Contains(second)) return (double)second.Length / (double)first.Length;
                
                if (second.Contains(first)) return (double)first.Length / (double)second.Length;
            }

            return 0;
        }
    }
}
