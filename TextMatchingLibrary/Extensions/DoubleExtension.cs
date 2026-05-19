namespace TextMatchingLibrary.Extensions
{
    public static class DoubleExtension
    {
        public static bool IsSimilar(this double result, double threshold)
        {
            return result >= threshold;
        }
    }
}
