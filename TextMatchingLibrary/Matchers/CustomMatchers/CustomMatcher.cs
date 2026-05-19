using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class CustomMatcher : BaseMatcher, IMatcher<string>
    {
        private readonly List<CustomMatcherConfigurationEntry> _matcherConfigurationEntries;

        public CustomMatcher(List<CustomMatcherConfigurationEntry> matcherConfigurationEntries, INormalizer normalizer = null) 
            :base(normalizer)
        { 
            this._matcherConfigurationEntries = matcherConfigurationEntries;

            ValidateConfiguration();
        }

        public double Match(string first, string second)
        {
            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

            return this._matcherConfigurationEntries.Sum(entry => entry.Matcher.Match(first, second) * entry.Weight);
        }

        private void ValidateConfiguration()
        {
            if (this._matcherConfigurationEntries == null || this._matcherConfigurationEntries.Count == 0)
            {
                throw new ArgumentException("Matcher configuration entries cannot be null or empty.");
            }

            if (this._matcherConfigurationEntries.Sum(entry => entry.Weight) > 1)
            {
                throw new ArgumentException("The sum of weights in matcher configuration entries cannot exceed 1.");
            }
        }
    }
}
