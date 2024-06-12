namespace JapaneseCallouts.Helpers;

internal static class LanguageHelpers
{
    internal static string GetLangCode(ELanguages lang)
    {
        return lang switch
        {
            ELanguages.English => "en-US",
            ELanguages.Japanese => "ja-JP",
            _ => "Custom"
        };
    }
}