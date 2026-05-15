using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class LengthMatcherTests
    {
        private LengthMatcher lengthMatcher;

        [SetUp]
        public void Setup()
        {
            this.lengthMatcher = new LengthMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 1)]
        [TestCase("frost", "frst", 0.889)]
        [TestCase("frost", "frostXYZ", 0.769)]
        [TestCase("frostXYZ", "frost", 0.769)]
        [TestCase("abc", "abcdef", 0.667)]
        [TestCase("abcdef", "abc", 0.667)]
        public void Match(string first, string second, double expected)
        {
            double result = this.lengthMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
