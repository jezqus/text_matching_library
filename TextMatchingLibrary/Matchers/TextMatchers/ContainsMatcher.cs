using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class ContainsMatcher : BaseMatcher, IMatcher<string>
    {
        public ContainsMatcher()
            :base() { }
        
        public ContainsMatcher(INormalizer normalizer)
            :base(normalizer) { }

        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty()) return 0;
            if (second.IsNullOrEmpty()) return 0;

            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

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
