using System.Globalization;
using System.Text;
using TextMatchingLibrary.Extensions;

namespace TextMatchingLibrary.Normalizers
{
    public class SimpleNormalizer : INormalizer
    {
        /// <summary>
        /// For Polish German and "simple Greek" and "simple Cyrylic"
        /// </summary>
        static readonly Dictionary<char, string> mostCommonProblematicCharacters = new()
        {
            // Polish
            ['ł'] = "l",
            ['Ł'] = "L",

            // German
            ['ß'] = "ss",

            // Greek (subset)
            ['α'] = "a", ['β'] = "b", ['γ'] = "g", ['δ'] = "d", ['ε'] = "e",
            ['ζ'] = "z", ['η'] = "i", ['θ'] = "th", ['ι'] = "i", ['κ'] = "k",
            ['λ'] = "l", ['μ'] = "m", ['ν'] = "n", ['ξ'] = "x", ['ο'] = "o",
            ['π'] = "p", ['ρ'] = "r", ['σ'] = "s", ['ς'] = "s", ['ω'] = "o",
            ['τ'] = "t", ['υ'] = "y", ['φ'] = "f", ['χ'] = "ch", ['ψ'] = "ps", ['ɠ'] = "g",

            //Cyrylic
            ['А']="A", ['а']="a", ['Б']="B", ['б']="b", ['В']="V",
            ['в']="v", ['Г']="G", ['г']="g", ['Д']="D", ['д']="d",
            ['Е']="E", ['е']="e", ['Ё']="E", ['ё']="e", ['Ж']="Zh",
            ['ж']="zh", ['З']="Z", ['з']="z", ['И']="I", ['и']="i",
            ['Й']="Y", ['й']="y", ['К']="K", ['к']="k", ['Л']="L",
            ['л']="l", ['М']="M", ['м']="m", ['Н']="N", ['н']="n",
            ['О']="O", ['о']="o", ['П']="P", ['п']="p", ['Р']="R",
            ['р']="r", ['С']="S", ['с']="s", ['Т']="T", ['т']="t",
            ['У']="U", ['у']="u", ['Ф']="F", ['ф']="f", ['Х']="Kh",
            ['х']="kh", ['Ц']="Ts", ['ц']="ts", ['Ч']="Ch", ['ч']="ch",
            ['Ш']="Sh", ['ш']="sh", ['Щ']="Shch", ['щ']="shch", ['Ъ']="",
            ['ъ']="", ['Ы']="Y", ['ы']="y", ['Ь']="", ['ь']="",
            ['Э']="E", ['э']="e", ['Ю']="Yu", ['ю']="yu", ['Я']="Ya", ['я'] = "ya"
        };

        public string Normalize(string input)
        {
            if (!input.IsNullOrEmpty())
            {
                var normalizedInput = input.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder(normalizedInput.Length);

                bool wasPreviousCharWhitespace = false;
                foreach (var c in normalizedInput)
                {
                    UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory == UnicodeCategory.OpenPunctuation ||
                        unicodeCategory == UnicodeCategory.ClosePunctuation ||
                        unicodeCategory == UnicodeCategory.InitialQuotePunctuation ||
                        unicodeCategory == UnicodeCategory.FinalQuotePunctuation ||
                        unicodeCategory == UnicodeCategory.OtherPunctuation)
                    {
                        if (!wasPreviousCharWhitespace)
                        {
                            sb.Append(' ');
                        }

                        wasPreviousCharWhitespace = true;
                    }
                    else if (unicodeCategory == UnicodeCategory.SpaceSeparator)
                    {
                        if (!wasPreviousCharWhitespace)
                        {
                            sb.Append(' ');
                        }

                        wasPreviousCharWhitespace = true;
                    }
                    else if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        char cToAppend = Char.IsUpper(c) ? char.ToLower(c) : c;

                        sb.Append(mostCommonProblematicCharacters.ContainsKey(cToAppend) ? mostCommonProblematicCharacters[cToAppend] : cToAppend);

                        wasPreviousCharWhitespace = false;
                    }
                }
                
                return sb.ToString().Trim();
            }

            return input;
        }
    }
}
