namespace TextMatchingLibrary.Normalizers
{
    public interface ITokenizer
    {
        IEnumerable<string> ReadToken(string input);
    }
}
