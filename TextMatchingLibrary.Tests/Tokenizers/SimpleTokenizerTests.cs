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
        public void Normalize(string intput, string[] output)
        {
            int index = 0;
            foreach (string token in this.simpleTokenizer.ReadToken(intput))
            {
                Assert.That(token, Is.EqualTo(output[index++]));
            }
        }
    }
}
