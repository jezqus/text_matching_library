using System.Globalization;
using System.Text;
using TextMatchingLibrary.Extensions;

namespace TextMatchingLibrary.Normalizers
{
    public class SimpleTokenizer : ITokenizer
    {
        public IEnumerable<string> ReadToken(string input)
        {
            StringBuilder token = new StringBuilder(16);

            foreach (char c in input)
            {
                if (!char.IsWhiteSpace(c))
                {
                    token.Append(c);
                }
                else if(token.Length > 0)
                {
                    yield return token.ToString();
                    token.Clear();
                }
            }
        }
    }
}
