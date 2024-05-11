namespace JapaneseCallouts.Xml;

internal static class XmlManager
{
    internal const string PACIFIC_BANK_HEIST_XML = @"PacificBankHeistConfig.xml";
    internal const string DRUNK_GUYS_XML = @"DrunkGuysConfig.xml";
    internal const string ROAD_RAGE_XML = @"RoadRageConfig.xml";
    internal const string STORE_ROBBERY_XML = @"StoreRobberyConfig.xml";
    internal const string BANK_HEIST_XML = @"BankHeistConfig.xml";
    internal const string CALLOUTS_SOUND_XML = @"CalloutsSoundConfig.xml";

    internal static PacificBankHeistConfig PacificBankHeistConfig { get; private set; }
    internal static DrunkGuysConfig DrunkGuysConfig { get; private set; }
    internal static RoadRageConfig RoadRageConfig { get; private set; }
    internal static StoreRobberyConfig StoreRobberyConfig { get; private set; }
    internal static BankHeistConfig BankHeistConfig { get; private set; }
    internal static CalloutsSoundConfig CalloutsSoundConfig { get; private set; }

    internal static void Initialize()
    {
        var pbhSerializer = new XmlSerializer(typeof(PacificBankHeistConfig));
        var gdSerializer = new XmlSerializer(typeof(DrunkGuysConfig));
        var rrSerializer = new XmlSerializer(typeof(RoadRageConfig));
        var srSerializer = new XmlSerializer(typeof(StoreRobberyConfig));
        var bhSerializer = new XmlSerializer(typeof(BankHeistConfig));
        var csSerializer = new XmlSerializer(typeof(CalloutsSoundConfig));

        PacificBankHeistConfig = LoadXml<PacificBankHeistConfig>(PACIFIC_BANK_HEIST_XML, pbhSerializer);
        DrunkGuysConfig = LoadXml<DrunkGuysConfig>(DRUNK_GUYS_XML, gdSerializer);
        RoadRageConfig = LoadXml<RoadRageConfig>(ROAD_RAGE_XML, rrSerializer);
        StoreRobberyConfig = LoadXml<StoreRobberyConfig>(STORE_ROBBERY_XML, srSerializer);
        BankHeistConfig = LoadXml<BankHeistConfig>(BANK_HEIST_XML, bhSerializer);
        CalloutsSoundConfig = LoadXml<CalloutsSoundConfig>(CALLOUTS_SOUND_XML, csSerializer);
    }

    private static T LoadXml<T>(string filename, XmlSerializer serializer)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = @$"{Main.PLUGIN_DIRECTORY}/Xml/{filename}";
            if (File.Exists(path))
            {
                using var stream = new FileStream(path, FileMode.Open);
                return (T)serializer.Deserialize(stream);
            }
            else
            {
                Logger.Warn($"The xml file named '{filename}' was not found. Check whether the filename is correct or the file exists in the correct directory.", filename);
                var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Resources.{filename}");
                using var sr = new StreamReader(stream);
                return (T)serializer.Deserialize(sr);
            }
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
            throw new Exception($"The xml file ({filename}) was not loaded.");
        }
    }
}