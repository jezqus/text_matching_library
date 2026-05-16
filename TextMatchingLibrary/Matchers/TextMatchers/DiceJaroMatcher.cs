using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class DiceJaroMatcher : BaseMatcher, IMatcher<string>
    {
        private readonly IMatcher<string> diceMatcher = new DiceMatcher();
        private readonly IMatcher<string> jaroWinklerMatcher = new JaroWinklerMatcher();

        public DiceJaroMatcher()
            :base() 
        { }

        public DiceJaroMatcher(INormalizer normalizer)
            :base(normalizer) { }

        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty() || second.IsNullOrEmpty()) return 0;

            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

            if (first.Length < 5 || second.Length < 5)
            {
                return this.jaroWinklerMatcher.Match(first, second);
            }
            else
            {
                var diceDistance = this.diceMatcher.Match(first, second);
                if (diceDistance > 0.7 && diceDistance < 0.9) //quite similar, but not the same add Jaro-Winkler distance to improve the score
                {
                    var jaroDistance = this.jaroWinklerMatcher.Match(first, second);

                    return (diceDistance * 0.6) + (jaroDistance * 0.4);
                }
                else
                {
                    return diceDistance;
                }
            }
        }
    }
}