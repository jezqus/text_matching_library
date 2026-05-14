
using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class DiceMatcherTests
    {
        private DiceMatcher diceMatcher;

        [SetUp]
        public void Setup()
        {
            this.diceMatcher = new DiceMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0.25)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.727)]
        [TestCase("frostXYZ", "frost", 0.727)]
        [TestCase("abc", "abcdef", 0.571)]
        [TestCase("abcdef", "abc", 0.571)]
        [TestCase("abc", "xyzabcdef", 0.400)]
        [TestCase("a", "xyzabcdaefaa", 0.133)]
        public void Match(string first, string second, double expected)
        {
            double result = this.diceMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }

        [TestCase("AbcDef", "aBc", 0.571)]
        public void Match_WithNormalizer(string first, string second, double expected)
        {
            var mock = new Mock<INormalizer>();
            mock.Setup(normalizer => normalizer.Normalize(It.IsAny<string>())).Returns((string s) => s.ToLower());

            var matcher = new DiceMatcher(mock.Object);
            double result = matcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
