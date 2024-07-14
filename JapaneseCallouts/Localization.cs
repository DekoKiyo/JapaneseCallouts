namespace JapaneseCallouts;

internal static class Localization
{
    private static readonly Dictionary<string, string> Translation = [];
    private const string NO_TRANSLATION = "[NO TRANSLATION]";
    internal static ELanguages Language { get; set; } = ELanguages.English;

    internal static void Initialize()
    {
        Main.Logger.Info($"Loading {Language.ToString()}", "Localization");
        Load(Language);
        Main.Logger.Info("Locale files has loaded.", "Localization");
    }

    private static void Load(ELanguages lang, bool isChange = false)
    {
        var langCode = LanguageHelpers.GetLangCode(lang);
        var data = LoadJson(langCode);
        foreach (var obj in data)
        {
            foreach (var locale in obj.Value)
            {
#if DEBUG
                Main.Logger.Info($"Loading Translation - Key: \"{locale.Key}\" Value: \"{locale.Value}\"", "Localization");
#endif
                if (!isChange && Translation.ContainsKey(locale.Key))
                {
                    Main.Logger.Warn($"The translation key is already exists! Key: {locale.Key}", "Localization");
                }
                Translation[locale.Key] = locale.Value;
            }
        }
        Language = lang;
    }

    internal static string GetString(string key)
    => Translation.ContainsKey(key) ? Translation[key] : NoTranslation(key);

    internal static string GetString(string key, params string[] vars)
    => Translation.ContainsKey(key) ? string.Format(Translation[key], vars) : NoTranslation(key);

    private static string NoTranslation(string key)
    {
        Main.Logger.Warn($"There is no translation. Key: \"{key}\" Language: \"{Language.ToString()}\"", "Localization");
        return NO_TRANSLATION;
    }

    [ConsoleCommand("Change Japanese Callouts' language.")]
    internal static void JPCChangeLanguage([ConsoleCommandParameter("Enter the language code that you want to use.", AutoCompleterType = typeof(ConsoleCommandParameterAutoCompleterEnum))] ELanguages lang)
    {
        if (Enum.GetNames(typeof(ELanguages)).Contains($"{lang}"))
        {
            Main.Logger.Info($"Loading {lang}", "Localization");
            Load(lang, true);
            Main.Logger.Info("Locale files has loaded.", "Localization");
        }
        else
        {
            throw new ArgumentException("lang");
        }
    }

    private static Dictionary<string, Dictionary<string, string>> LoadJson(string langCode)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = @$"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_LOCALIZATION_DIRECTORY}/{langCode}.json";
            if (File.Exists(path))
            {
                using var sr = new StreamReader(path);
                return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(sr.ReadToEnd());
            }
            else
            {
                Main.Logger.Warn($"The locale file, \"{langCode}.json\" was not found. Check whether the filename is correct or the file exists in the correct directory.", $"{langCode}.json");
                var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Localization.{langCode}.json");
                var byteArray = new byte[stream.Length];
                _ = stream.Read(byteArray, 0, (int)stream.Length);
                var json = Encoding.UTF8.GetString(byteArray);

                return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            }
        }
        catch (FileNotFoundException e)
        {
            Main.Logger.Error(e.ToString());
            throw new FileNotFoundException($"The locale file, \"{langCode}.json\" was not loaded.", $"{langCode}.json", e);
        }
        catch (Exception e)
        {
            throw new FileLoadException("The error was occurred while load the json file.", $"{langCode}.json", e);
        }
    }
}