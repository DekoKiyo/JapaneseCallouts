namespace JapaneseCallouts;

internal static class Localization
{
    private static readonly Dictionary<string, Dictionary<string, string>> Translation = [];
    private const string NO_TRANSLATION = "[NO TRANSLATION]";
    private static string CurrentLanguage = "English";
    private static readonly string[] AvailableLanguages = ["Custom", "English", "Japanese"];

    static Localization()
    {
        Logger.Info("Loading ", "Localization");
        Load(CurrentLanguage);
        Logger.Info("Locale files has loaded", "Localization");
    }

    internal static string Language
    {
        get => CurrentLanguage;
        set
        {
            if (AvailableLanguages.Contains(value))
            {
                CurrentLanguage = value;
            }
            else
            {
                Logger.Warn("The language was not found in the list of available languages. The language will be set to English.");
                CurrentLanguage = "English";
            }
        }
    }

    private static void Load(string lang)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Localization.{lang}.json");
        var byteArray = new byte[stream.Length];
        _ = stream.Read(byteArray, 0, (int)stream.Length);
        var json = Encoding.UTF8.GetString(byteArray);

        var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

        foreach (var obj in data)
        {
            foreach (var locale in obj.Value)
            {
                Logger.Info($"Loading Translation - Key: {locale.Key} Value: {locale.Value}", "Localization");
                Translation[lang][locale.Key] = locale.Value;
            }
        }
    }

    internal static string GetString(string key)
    {
        if (Translation[CurrentLanguage].ContainsKey(key))
        {
            return Translation[CurrentLanguage][key];
        }
        else
        {
            Logger.Warn($"There is no translation. Key: {key} Language: {CurrentLanguage}", "Localization");
            return NO_TRANSLATION;
        }
    }

    internal static string GetString(string key, params string[] vars)
    {
        if (Translation[CurrentLanguage].ContainsKey(key))
        {
            return string.Format(Translation[CurrentLanguage][key], vars);
        }
        else
        {
            Logger.Warn($"There is no translation. Key: {key} Language: {CurrentLanguage}", "Localization");
            return NO_TRANSLATION;
        }
    }
}