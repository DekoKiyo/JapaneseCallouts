namespace JapaneseCallouts.Xml;

internal static class XmlManager
{
    private const string BANK_HEIST_XML = @"BankHeistConfig.xml";

    internal static BankHeistConfig BankHeist { get; private set; }

    internal static void Initialize()
    {
        var bhPath = @$"{Main.PLUGIN_DIRECTORY}/{BANK_HEIST_XML}";
        var bhSerializer = new XmlSerializer(typeof(BankHeistConfig));

        var assembly = Assembly.GetExecutingAssembly();

        if (File.Exists(bhPath))
        {
            using var stream = new FileStream(bhPath, FileMode.Open);
            BankHeist = (BankHeistConfig)bhSerializer.Deserialize(stream);
        }
        else
        {
            Logger.Warn($"The xml file named '{BANK_HEIST_XML}' was not found. Check whether the filename is correct or the file exists in the correct directory.", BANK_HEIST_XML);
            var stream = assembly.GetManifestResourceStream($"JapaneseCallouts.Resources.{BANK_HEIST_XML}");
            using var sr = new StreamReader(stream);
            BankHeist = (BankHeistConfig)bhSerializer.Deserialize(sr);

#if DEBUG
            bhSerializer.Serialize(Console.Out, BankHeist);
#endif
        }
    }
}