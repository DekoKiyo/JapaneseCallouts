namespace JapaneseCallouts;

internal enum Languages
{
    English,
    French,
    German,
    Italian,
    Spanish,
    BrazilianPortuguese,
    Polish,
    Russian,
    Korean,
    TraditionalChinese,
    Japanese,
    MexicanSpanish,
    SimplifiedChinese
}

internal static class Localization
{
    private static readonly Dictionary<Languages, Dictionary<string, string>> Translation = [];
    private const string NO_TRANSLATION = "[NO TRANSLATION]";

    internal static void Initialize()
    {
        Logger.Info("Loading ", "Localization");
        foreach (Languages lang in Enum.GetValues(typeof(Languages)))
        {
            Load(lang);
        }
        Logger.Info("Locale files has loaded", "Localization");
    }

    private static void Load(Languages Language)
    {
        var lang = Helpers.ConvertToTwoCode(Language);
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Resources.{lang}.json");
        var byteArray = new byte[stream.Length];
        _ = stream.Read(byteArray, 0, (int)stream.Length);
        var json = Encoding.UTF8.GetString(byteArray);

        var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

        foreach (var obj in data)
        {
            foreach (var locale in obj.Value)
            {
                Logger.Info($"Loading Translation - Key: {locale.Key} Value: {locale.Value}", "Localization");
                Translation[Language][locale.Key] = locale.Value;
            }
        }
    }

    internal static string GetString(string key)
    {
        if (Translation[Main.CurrentLanguage].ContainsKey(key))
        {
            return Translation[Main.CurrentLanguage][key];
        }
        else
        {
            Logger.Warn($"There is no translation. Key: {key} Language: {Main.CurrentLanguage}", "Localization");
            return NO_TRANSLATION;
        }
    }

    internal static string GetString(string key, params string[] vars)
    {
        if (Translation[Main.CurrentLanguage].ContainsKey(key))
        {
            return string.Format(Translation[Main.CurrentLanguage][key], vars);
        }
        else
        {
            Logger.Warn($"There is no translation. Key: {key} Language: {Main.CurrentLanguage}", "Localization");
            return NO_TRANSLATION;
        }
    }
}