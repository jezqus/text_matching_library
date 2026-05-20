
using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class CustomMatcherTests
    {
        private CustomMatcher customMatcher;

        [SetUp]
        public void Setup()
        {
            var matcherMock = new Mock<IMatcher<string>>();
            matcherMock.Setup(matcher => matcher.Match(It.IsAny<string>(), It.IsAny<string>())).Returns((string x, string y) => (double)Math.Min(x.Length, y.Length)/ (double)Math.Max(x.Length, y.Length));

            var simpleMatcherMock = new Mock<IMatcher<string>>();
            simpleMatcherMock.Setup(matcher => matcher.Match(It.IsAny<string>(), It.IsAny<string>())).Returns(0.5);

            this.customMatcher = new CustomMatcher(new List<CustomMatcherConfigurationEntry>()
            {
                new CustomMatcherConfigurationEntry(matcherMock.Object, 0.5),
                new CustomMatcherConfigurationEntry(simpleMatcherMock.Object, 0.5),
            });
        }

        [TestCase("frost", "", 0.25)]
        [TestCase("", "abc", 0.25)]
        [TestCase("forst", "frost", 0.75)]
        [TestCase("frost", "frostXYZ", 0.562)]
        [TestCase("abc", "abcdef", 0.5)]
        public void Match_MultiMatchers(string first, string second, double expected)
        {
            double result = this.customMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.625)]
        [TestCase("abc", "abcdef", 0.5)]
        public void Match_SingleMatcher(string first, string second, double expected)
        {
            var matcherMock = new Mock<IMatcher<string>>();
            matcherMock.Setup(matcher => matcher.Match(It.IsAny<string>(), It.IsAny<string>())).Returns((string x, string y) => (double)Math.Min(x.Length, y.Length) / (double)Math.Max(x.Length, y.Length));

            this.customMatcher = new CustomMatcher(new List<CustomMatcherConfigurationEntry>()
            {
                new CustomMatcherConfigurationEntry(matcherMock.Object, 1)
            });
        }

        [Test]
        public void Constructor_NoConfiguraion()
        {
            Assert.Throws<ArgumentException>(() => new CustomMatcher(null));
        }
    }
}
