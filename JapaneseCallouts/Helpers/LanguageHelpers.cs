namespace JapaneseCallouts.Helpers;

internal static class LanguageHelpers
{
    internal static string GetLangCode(ELanguages lang)
    {
        return lang switch
        {
            ELanguages.Japanese => "ja-JP",
            _ => "en-US"
        };
    }
}