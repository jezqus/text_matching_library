using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class CustomMatcherConfigurationEntry
    {
        public CustomMatcherConfigurationEntry(IMatcher<string> matcher, double weight)
        {
            Matcher = matcher;
            Weight = weight;
        }

        public IMatcher<string> Matcher { get; set; }
        public double Weight { get; set; }
    }
}
