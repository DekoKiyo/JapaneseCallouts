namespace JapaneseCallouts.Helpers;

internal static class LanguageHelpers
{
    internal static string GetLangCode(ELanguages lang)
    {
        return lang switch
        {
            ELanguages.Japanese => "ja-JP",
            ELanguages.Russian => "ru-RU",
            ELanguages.Italian => "it-IT",
            ELanguages.Czech => "cs-CZ",
            ELanguages.German => "de-DE",
            _ => "en-US"
        };
    }
}