using JapaneseCallouts.Callouts.BankHeist;
using JapaneseCallouts.Callouts.DrunkGuys;
using JapaneseCallouts.Callouts.HotPursuit;
using JapaneseCallouts.Callouts.PacificBankHeist;
using JapaneseCallouts.Callouts.RoadRage;
using JapaneseCallouts.Callouts.StoreRobbery;
using JapaneseCallouts.Callouts.StreetFight;
using JapaneseCallouts.Callouts.WantedCriminalFound;

namespace JapaneseCallouts.Configurations;

internal static class ConfigurationManager
{
    internal static readonly Dictionary<Type, (string path, bool isError)> REQUIRED_FILES_PATH = new()
    {
        { typeof(Callouts.BankHeist.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/BankHeist.json", false) },
        { typeof(Callouts.DrunkGuys.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/DrunkGuys.json", false) },
        { typeof(Callouts.HotPursuit.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/HotPursuit.json", false) },
        { typeof(Callouts.PacificBankHeist.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/PacificBankHeist.json", false) },
        { typeof(Callouts.RoadRage.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/RoadRage.json", false) },
        { typeof(Callouts.StoreRobbery.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/StoreRobbery.json", false) },
        { typeof(Callouts.StreetFight.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/StreetFight.json", false) },
        { typeof(Callouts.WantedCriminalFound.Configurations), ($"{Main.PLUGIN_DIRECTORY}/Json/WantedCriminalFound.json", false) },
    };

    internal static Dictionary<string, OutfitConfig> OutfitConfigurations = [];

    internal static void Initialize()
    {
        // outfits
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

        BankHeist.Configuration = ReadConfig<Callouts.BankHeist.Configurations>();
        DrunkGuys.Configuration = ReadConfig<Callouts.DrunkGuys.Configurations>();
        HotPursuit.Configuration = ReadConfig<Callouts.HotPursuit.Configurations>();
        PacificBankHeist.Configuration = ReadConfig<Callouts.PacificBankHeist.Configurations>();
        RoadRage.Configuration = ReadConfig<Callouts.RoadRage.Configurations>();
        StoreRobbery.Configuration = ReadConfig<Callouts.StoreRobbery.Configurations>();
        StreetFight.Configuration = ReadConfig<Callouts.StreetFight.Configurations>();
        WantedCriminalFound.Configuration = ReadConfig<Callouts.WantedCriminalFound.Configurations>();
    }

    private static T ReadConfig<T>()
    {
        var (path, _) = REQUIRED_FILES_PATH[typeof(T)];
        if (File.Exists(path))
        {
            using var stream = new StreamReader(path);
            return JsonSerializer.Deserialize<T>(stream.ReadToEnd());
        }
        else
        {
            var filename = Path.GetFileName(path);
            var assembly = Assembly.GetExecutingAssembly();
            Main.Logger.Warn($"The json file named '{filename}' was not found. Check whether the filename is correct or the file exists in the correct directory.", filename);
            var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Json.{filename}");
            using var sr = new StreamReader(stream, Encoding.UTF8);
            return JsonSerializer.Deserialize<T>(sr.ReadToEnd());
        }
    }

    internal static bool GetOutfit(PedConfig config, out OutfitConfig outfit)
    {
        if (OutfitConfigurations.ContainsKey(config.OutfitName))
        {
            outfit = OutfitConfigurations[config.OutfitName];
            return true;
        }
        else
        {
            outfit = null;
            return false;
        }
    }
}