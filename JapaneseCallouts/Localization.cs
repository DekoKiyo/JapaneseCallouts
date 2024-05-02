namespace JapaneseCallouts;

internal static class Localization
{
    private static readonly Dictionary<string, string> Translation = [];
    private const string NO_TRANSLATION = "[NO TRANSLATION]";
    private static string CurrentLanguage = "en-US";
    private static readonly LanguageCode[] AvailableLanguages =
    [
        new() { KeyCode = "en-US", Description = "English" },
        new() { KeyCode = "ja-JP", Description = "Japanese" }
    ];

    internal static void Initialize()
    {
        Logger.Info($"Loading {CurrentLanguage}", "Localization");
        Load(CurrentLanguage);
        Logger.Info("Locale files has loaded.", "Localization");
    }

    internal static string Language
    {
        get => CurrentLanguage;
        set
        {
            if (AvailableLanguages.Length is 0)
            {
                Logger.Warn("The language was not found in the list of available languages. The language will be set to English.");
                CurrentLanguage = "en-US";
            }
            else
            {
                foreach (var avl in AvailableLanguages)
                {
                    if (avl.KeyCode == value)
                    {
                        CurrentLanguage = value;
                        return;
                    }
                }

                Logger.Warn("The given language key is not supported now. The language will be set to English.");
                CurrentLanguage = "en-US";
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
#if DEBUG
                Logger.Info($"Loading Translation - Key: \"{locale.Key}\" Value: \"{locale.Value}\"", "Localization");
#endif
                if (Translation.ContainsKey(locale.Key))
                {
                    Logger.Warn($"The translation key is already exists! Key: {locale.Key}", "Localization");
                }
                Translation[locale.Key] = locale.Value;
            }
        }
        CurrentLanguage = lang;
    }

    internal static string GetString(string key)
    => Translation.ContainsKey(key) ? Translation[key] : NoTranslation(key);

    internal static string GetString(string key, params string[] vars)
    => Translation.ContainsKey(key) ? string.Format(Translation[key], vars) : NoTranslation(key);

    private static string NoTranslation(string key)
    {
        Logger.Warn($"There is no translation. Key: \"{key}\" Language: \"{CurrentLanguage}\"", "Localization");
        return NO_TRANSLATION;
    }

    [ConsoleCommand("Change Japanese Callouts' language.")]
    internal static void ChangeJPCLanguage([ConsoleCommandParameter("Enter the language key that you want to change")] string lang)
    {
        CurrentLanguage = lang;
        Logger.Info($"Loading {CurrentLanguage}", "Localization");
        Load(CurrentLanguage);
        Logger.Info("Locale files has loaded.", "Localization");
    }

    internal struct LanguageCode
    {
        internal string KeyCode;
        internal string Description;
    }
}