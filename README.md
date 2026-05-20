# TextMatchingLibrary

A library that implements popular text comparison algorithms and helpful utilities. It also includes a hybrid matcher that combines Dice and Jaro–Winkler (DiceJaro) and a CustomMatcher that allows arbitrary composition of other matchers using weights.

Available matchers

- ContainsMatcher — checks whether one string contains another and returns a length-based ratio (0..1).
- DiceMatcher — Dice coefficient based on character bigrams (short strings are padded with spaces).
- JaroMatcher — implementation of the Jaro similarity algorithm, accounting for matches and transpositions.
- JaroWinklerMatcher — Jaro–Winkler extension which boosts score for common prefixes.
- DiceJaroMatcher — heuristic hybrid: uses Jaro–Winkler for short strings; for longer strings uses Dice, and for moderate similarity mixes both.
- LongestSubstringMatcher — score based on the longest common substring normalized to 0..1.
- LengthMatcher — simple length-based similarity measure.
- NumbersMatcher — compares numeric tokens extracted from text (useful when numbers are significant).
- CustomMatcher — composes multiple IMatcher<string> instances with weights; the sum of weights must not exceed 1.

Technical notes

- Each matcher implements IMatcher<string> and exposes a method:

    double Match(string first, string second)

- Many matchers accept an optional INormalizer in the constructor to normalize input before comparison.
- CustomMatcher validates configuration and throws an ArgumentException if the configuration is empty or the sum of weights exceeds 1.

Examples

1) JaroMatcher

    using TextMatchingLibrary.Matchers;

    var jaro = new JaroMatcher();
    double score = jaro.Match("Martha", "Marhta");
    Console.WriteLine($"Jaro score: {score}");

2) CustomMatcher (combine Dice + Jaro)

    using TextMatchingLibrary.Matchers;

    var config = new List<CustomMatcherConfigurationEntry>
    {
        new CustomMatcherConfigurationEntry(new DiceMatcher(), 0.6),
        new CustomMatcherConfigurationEntry(new JaroMatcher(), 0.4)
    };

    var custom = new CustomMatcher(config);
    double combinedScore = custom.Match("Martha", "Marhta");
    Console.WriteLine($"Custom combined score: {combinedScore}");

Tests and example runner

See TextMatchingLibrary.Tests for unit tests that demonstrate expected behavior for individual matchers and TextMatchingLibrary.Tester for a simple example console runner.

Contributing

Contributions are welcome — please open an issue or a pull request with new matchers, normalizers, or tests.
