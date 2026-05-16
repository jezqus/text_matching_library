using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class NumbersMatcher : BaseMatcher, IMatcher<string>
    {
        private readonly ITokenizer? tokenizer;
        public NumbersMatcher(ITokenizer? tokenizer = null)
            : base()
        {
            this.tokenizer = tokenizer ?? new SimpleTokenizer();
        }

        public NumbersMatcher(INormalizer normalizer, ITokenizer? tokenizer = null) 
            : base(normalizer)
        {
            this.tokenizer = tokenizer ?? new SimpleTokenizer();
        }

        public double Match(string first, string second)
        {
            if (first.IsNullOrEmpty()) return 0;
            if (second.IsNullOrEmpty()) return 0;

            if (this.normalizer is not null)
            {
                first = this.normalizer.Normalize(first);
                second = this.normalizer.Normalize(second);
            }

            List<string> firstNumbers = GetAllNumbersFromText(first);
            List<string> secondNumbers = GetAllNumbersFromText(second);

            if (firstNumbers.Count == 0 && secondNumbers.Count == 0) return 1;

            int matches = 0;
            foreach (var number in firstNumbers)
            {
                if (secondNumbers.Contains(number))
                {
                    matches++;
                }
            }

            return (double)(2 * matches) / (double)(firstNumbers.Count + secondNumbers.Count);
        }

        private List<string> GetAllNumbersFromText(string text)
        {
            List<string> numbers = new List<string>();
            foreach (var word in tokenizer.ReadToken(text))
            {
                if (IsNumber(word))
                {
                    numbers.Add(word);
                }
            }
            return numbers;
        }

        private bool IsNumber(string word)
        {
            foreach (var c in word)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
