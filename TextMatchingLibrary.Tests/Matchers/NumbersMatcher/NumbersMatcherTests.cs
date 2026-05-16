using Moq;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class NumbersMatcherTests
    {
        private NumbersMatcher numbersMatcher;

        [SetUp]
        public void Setup()
        {
            this.numbersMatcher = new NumbersMatcher();
        }

        [TestCase("frost", "", 0)]
        [TestCase("", "abc", 0)]
        [TestCase("forst", "frost", 1)]
        [TestCase("frost 123", "frst", 0)]
        [TestCase("frost 123", "frostXYZ 123", 1)]
        [TestCase("frost 123", "frostXYZ 123 12", 0.667)]
        [TestCase("frost 123 13", "frostXYZ 123 12", 0.5)]
        public void Match(string first, string second, double expected)
        {
            double result = this.numbersMatcher.Match(first, second);

            Assert.That(Math.Round(result, 3), Is.EqualTo(expected));
        }
    }
}
