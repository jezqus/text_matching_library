using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Normalizers;

namespace TextMatchingLibrary.Matchers
{
    public class BaseMatcher
    {
        protected INormalizer? normalizer;

        protected BaseMatcher()
        {

        }

        protected BaseMatcher(INormalizer normalizer)
            : this()
        {
            this.normalizer = normalizer;
        }
    }
}