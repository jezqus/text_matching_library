// See https://aka.ms/new-console-template for more information

using TextMatchingLibrary.Extensions;
using TextMatchingLibrary.Matchers;
using TextMatchingLibrary.Normalizers;

Console.WriteLine("Testing ...");

List<(string first, string second)> entries = new List<(string first, string second)>()
{
    ("Ala ma kota","Kot ma ale"),
    ("Ala ma kota","Ala am koto"),
    ("Znaczny wzrost infracji","Brak wzrostu inflacji"),
    ("System przetwarza dane wejsciowe w czasie rzeczywistym", "System analizuje dane wejsciowe w czasie rzeczywistym"),
    ("Uzytkownik moze zmienić ustawienia aplikacji w panelu", "Uzytkownik ma mozliwość modyfikacji ustawień aplikacji w panelu"),
    ("Algorytm optymalizuje wyniki na podstawie wcześniejszych danych","Algorytm ulepsza wyniki na podstawie wcześniejszych informacji"),
    ("W przypadku braku odpowiedzi serwera zadanie zostaje ponowione automatycznie", "Jesli serwer nie odpowie, system sam ponawia wyslanie zapytania"),
    ("Model wykorzystuje wczesniejsze obserwacje do przewidywania przyszlych wartosci", "Na podstawie historycznych danych model estymuje wartosci, które moga wystąpic pozniej"),
    ("23 Kamal Eddin Salah St. Garden City", "23 Kamal El Din Hussein St, Cairo")
};

INormalizer normalizer = new SimpleNormalizer();
List<IMatcher<string>> matchers = new List<IMatcher<string>>()
{
    new LongestSubstringMatcher(normalizer),
    new JaroMatcher(normalizer),
    new DiceMatcher(normalizer),
    new JaroWinklerMatcher(normalizer),
    new DiceJaroMatcher(normalizer)
};

foreach (var entry in entries)
{
    double threshold = 0.85;

    Console.Write($"{entry.first.Substring(0, 8)}... vs {entry.second.Substring(0, 8)} ... ");
    foreach (var matcher in matchers)
    {
        Console.Write($"{matcher.GetType().Name}: {Math.Round(matcher.Match(entry.first, entry.second), 3)}({Math.Round(matcher.Match(entry.first, entry.second), 3).IsSimilar(threshold)}), ");
    }

    Console.WriteLine();
}