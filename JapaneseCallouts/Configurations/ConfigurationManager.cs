namespace JapaneseCallouts.Configurations;

internal static class ConfigurationManager
{
    internal static Dictionary<string, OutfitConfig> OutfitConfigurations = [];

    internal static void Initialize()
    {
        if (Directory.Exists($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_JSON_DIRECTORY}/Outfits"))
        {
            foreach (var filepath in Directory.EnumerateFiles($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_JSON_DIRECTORY}/Outfits", "*.json", SearchOption.TopDirectoryOnly))
            {
                using var stream = new StreamReader(filepath);
                var outfit = JsonSerializer.Deserialize<OutfitConfig>(stream.ReadToEnd());
                var filename = Path.GetFileNameWithoutExtension(filepath).ToUpper();
                OutfitConfigurations[filename] = outfit;
            }
        }
    }
}