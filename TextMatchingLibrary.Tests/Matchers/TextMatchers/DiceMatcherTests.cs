
using TextMatchingLibrary.Matchers;

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
    }
}
