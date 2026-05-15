using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

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
        [TestCase("frost", "frostXYZ", 0.769)]
        [TestCase("frostXYZ", "frost", 0.769)]
        [TestCase("abc", "abcdef", 0.667)]
        [TestCase("abcdef", "abc", 0.667)]
        [TestCase("abc", "xyzabcdef", 0.5)]
        [TestCase("a", "xyzabcdaefaa", 0.154)]
        public void Match(string first, string second, double expected)
        {
            double result = this.longestSubstringMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }

        [TestCase("AbcDef", "aBc", 0.667)]
        public void Match_WithNormalizer(string first, string second, double expected)
        {
            var mock = new Mock<INormalizer>();
            mock.Setup(normalizer => normalizer.Normalize(It.IsAny<string>())).Returns((string s) => s.ToLower());

            var matcher = new LongestSubstringMatcher(mock.Object);
            double result = matcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
