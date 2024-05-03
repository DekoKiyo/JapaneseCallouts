namespace JapaneseCallouts;

internal static class Localization
{
    private static readonly Dictionary<string, string> Translation = [];
    private const string NO_TRANSLATION = "[NO TRANSLATION]";
    internal static ELanguages CurrentLanguage = ELanguages.English;

    internal static void Initialize()
    {
        Logger.Info($"Loading {CurrentLanguage}", "Localization");
        Load(CurrentLanguage);
        Logger.Info("Locale files has loaded.", "Localization");
    }

    private static void Load(ELanguages lang, bool isChange = false)
    {
        var langCode = LanguageHelpers.GetLangCode(lang);
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Localization.{langCode}.json");
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
                if (!isChange && Translation.ContainsKey(locale.Key))
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
    internal static void ChangeJPCLanguage([ConsoleCommandParameter(AutoCompleterType = typeof(ConsoleCommandParameterAutoCompleterEnum), Description = "Enter the language code that you want to use.")] ELanguages lang)
    {
        if (Enum.GetNames(typeof(ELanguages)).Contains($"{lang}"))
        {
            Logger.Info($"Loading {lang}", "Localization");
            Load(lang, true);
            Logger.Info("Locale files has loaded.", "Localization");
        }
        else
        {
            throw new ArgumentException("lang");
        }
    }
}