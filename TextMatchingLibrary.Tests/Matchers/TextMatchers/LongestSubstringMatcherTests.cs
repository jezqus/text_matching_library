using TextMatchingLibrary.Matchers;

namespace TextMatchingLibrary.Tests
{
    public class LongestSubstringMatcherTests
    {
        private LongestSubstringMatcher longestSubstringMatcher;

        [SetUp]
        public void Setup()
        {
            this.longestSubstringMatcher = new LongestSubstringMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0.4)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.625)]
        [TestCase("frostXYZ", "frost", 0.625)]
        [TestCase("abc", "abcdef", 0.5)]
        [TestCase("abcdef", "abc", 0.5)]
        [TestCase("abc", "xyzabcdef", 0.333)]
        [TestCase("a", "xyzabcdaefaa", 0.083)]
        public void Match(string first, string second, double expected)
        {
            double result = this.longestSubstringMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
