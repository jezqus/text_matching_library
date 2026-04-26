namespace TextMatchingLibrary.Matchers
{
    public interface IMatcher<T>
    {
        double Match(T first, T second);
    }
}
