
using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class DiceJaroMatcherTests
    {
        private DiceJaroMatcher diceJaroMatcher;

        [SetUp]
        public void Setup()
        {
            this.diceJaroMatcher = new DiceJaroMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 0.25)]
        [TestCase("Ala ma kota", "Ala ma koto", 0.9)]
        [TestCase("frost", "frost", 1)]
        [TestCase("frost", "frostXYZ", 0.806)]
        [TestCase("frostXYZ", "frost", 0.806)]
        [TestCase("abc", "abcdef", 0.883)]
        [TestCase("abcdef", "abc", 0.883)]
        [TestCase("abc", "xyzabcdef", 0.778)]
        [TestCase("a", "xyzabcdaefaa", 0.694)]
        public void Match(string first, string second, double expected)
        {
            double result = this.diceJaroMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
