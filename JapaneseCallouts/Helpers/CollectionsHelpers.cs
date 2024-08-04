namespace JapaneseCallouts.Helpers;

internal static class CollectionsHelpers
{
    internal static List<T> Shuffle<T>(this T[] collection)
    {
        var list = new List<T>(collection);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Main.MT.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
        return list;
    }
}