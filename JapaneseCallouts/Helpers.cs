namespace JapaneseCallouts;

internal static class Helpers
{
    internal static string ConvertToTwoCode(Languages lang)
    {
        return lang switch
        {
            Languages.English => "en-US",
            Languages.French => "fr-FR",
            Languages.German => "de-DE",
            Languages.Italian => "it-IT",
            Languages.Spanish => "es-ES",
            Languages.BrazilianPortuguese => "pt-BR",
            Languages.Polish => "pl-PL",
            Languages.Russian => "ru-RU",
            Languages.Korean => "ko-KR",
            Languages.TraditionalChinese => "zn-TW",
            Languages.Japanese => "ja-JP",
            Languages.MexicanSpanish => "es-MX",
            Languages.SimplifiedChinese => "zn-CN",
            _ => "en-US"
        };
    }
}