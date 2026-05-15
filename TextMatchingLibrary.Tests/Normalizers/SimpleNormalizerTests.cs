using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Tests
{
    public class SimpleNormalizerTests
    {
        private SimpleNormalizer simpleTextNormalizer;

        [SetUp]
        public void Setup()
        {
            this.simpleTextNormalizer = new SimpleNormalizer();
        }

        [TestCase("frost", "frost")]
        [TestCase("", "")]
        [TestCase("Frost", "frost")]
        [TestCase("The Enemy", "the enemy")]
        [TestCase("Łukasz", "lukasz")]
        [TestCase("źdŹbłO", "zdzblo")]
        [TestCase("DeadLine", "deadline")]
        [TestCase("ąęŖôƓ", "aerog")]
        [TestCase("CosюTam", "cosyutam")]
        [TestCase("Herß", "herss")]
        [TestCase("KoloNaukoweψ", "kolonaukoweps")]
        [TestCase("Hey!Wow%Nice,break.", "hey wow nice break")]
        [TestCase("Herß-Klin'enn", "herss-klin enn")]
        [TestCase("!Halt! What about  that??.", "halt what about that")]
        public void Normalize(string intput, string output)
        {
            string result = this.simpleTextNormalizer.Normalize(intput);

            Assert.That(result, Is.EqualTo(output));
        }
    }
}
