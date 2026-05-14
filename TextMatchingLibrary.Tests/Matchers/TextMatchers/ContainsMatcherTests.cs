using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class ContainsMatcherTests
    {
        private ContainsMatcher containsMatcher;

        [SetUp]
        public void Setup()
        {
            this.containsMatcher = new ContainsMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.625)]
        [TestCase("frostXYZ", "frost", 0.625)]
        [TestCase("abc", "abcdef", 0.5)]
        [TestCase("abcdef", "abc", 0.5)]
        public void Match(string first, string second, double expected)
        {
            double result = this.containsMatcher.Match(first, second);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("AbcDef", "aBc", 0.5)]
        public void Match_WithNormalizer(string first, string second, double expected)
        {
            var mock = new Mock<INormalizer>();
            mock.Setup(normalizer => normalizer.Normalize(It.IsAny<string>())).Returns((string s) => s.ToLower());

            var matcher = new ContainsMatcher(mock.Object);
            double result = matcher.Match(first, second);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
