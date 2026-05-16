using Castle.Components.DictionaryAdapter.Xml;
using System.Reflection.PortableExecutable;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class SimpleTokenizerTests
    {
        private SimpleTokenizer simpleTokenizer;

        [SetUp]
        public void Setup()
        {
            this.simpleTokenizer = new SimpleTokenizer();
        }

        [TestCase("frost", new string[] { "frost" })]
        [TestCase("frost with dry machine", new string[] { "frost", "with", "dry", "machine" })]
        [TestCase("frost with  machine", new string[] { "frost", "with", "machine" })]
        [TestCase("frost 123 13", new string[] { "frost", "123", "13" })]
        public void Normalize(string intput, string[] expected)
        {
            string[] result = this.simpleTokenizer.ReadToken(intput).ToArray();
            
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
