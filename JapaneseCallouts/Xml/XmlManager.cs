namespace JapaneseCallouts.Xml;

internal static class XmlManager
{
    internal const string BANK_HEIST_XML = @"BankHeistConfig.xml";
    internal const string DRUNK_GUYS_XML = @"DrunkGuysConfig.xml";
    internal const string CALLOUTS_SOUND_XML = @"CalloutsSoundConfig.xml";

    internal static BankHeistConfig BankHeistConfig { get; private set; }
    internal static DrunkGuysConfig DrunkGuysConfig { get; private set; }
    internal static CalloutsSoundConfig CalloutsSoundConfig { get; private set; }

    internal static void Initialize()
    {
        var bhSerializer = new XmlSerializer(typeof(BankHeistConfig));
        var gdSerializer = new XmlSerializer(typeof(DrunkGuysConfig));
        var csSerializer = new XmlSerializer(typeof(CalloutsSoundConfig));

        BankHeistConfig = LoadXml<BankHeistConfig>(BANK_HEIST_XML, bhSerializer);
        DrunkGuysConfig = LoadXml<DrunkGuysConfig>(DRUNK_GUYS_XML, gdSerializer);
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