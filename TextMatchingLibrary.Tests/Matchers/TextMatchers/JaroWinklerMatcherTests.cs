
using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class JaroWinklerMatcherTests
    {
        private JaroWinklerMatcher jaroWinklerMatcher;

        [SetUp]
        public void Setup()
        {
            this.jaroWinklerMatcher = new JaroWinklerMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0.94)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.925)]
        [TestCase("frostXYZ", "frost", 0.925)]
        [TestCase("abc", "abcdef", 0.883)]
        [TestCase("abcdef", "abc", 0.883)]
        [TestCase("abc", "xyzabcdef", 0.778)]
        [TestCase("a", "xyzabcdaefaa", 0.694)]
        public void Match(string first, string second, double expected)
        {
            double result = this.jaroWinklerMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }

        [TestCase("AbcDef", "aBc", 0.883)]
        public void Match_WithNormalizer(string first, string second, double expected)
        {
            var mock = new Mock<INormalizer>();
            mock.Setup(normalizer => normalizer.Normalize(It.IsAny<string>())).Returns((string s) => s.ToLower());

            var matcher = new JaroWinklerMatcher(mock.Object);
            double result = matcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
