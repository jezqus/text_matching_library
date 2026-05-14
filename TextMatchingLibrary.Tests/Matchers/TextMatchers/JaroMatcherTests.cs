
using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class JaroMatcherTests
    {
        private JaroMatcher jaroMatcher;

        [SetUp]
        public void Setup()
        {
            this.jaroMatcher = new JaroMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0.933)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.875)]
        [TestCase("frostXYZ", "frost", 0.875)]
        [TestCase("abc", "abcdef", 0.833)]
        [TestCase("abcdef", "abc", 0.833)]
        [TestCase("abc", "xyzabcdef", 0.778)]
        [TestCase("a", "xyzabcdaefaa", 0.694)]
        public void Match(string first, string second, double expected)
        {
            double result = this.jaroMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }

        [TestCase("AbcDef", "aBc", 0.833)]
        public void Match_WithNormalizer(string first, string second, double expected)
        {
            var mock = new Mock<INormalizer>();
            mock.Setup(normalizer => normalizer.Normalize(It.IsAny<string>())).Returns((string s) => s.ToLower());

            var matcher = new JaroMatcher(mock.Object);
            double result = matcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
