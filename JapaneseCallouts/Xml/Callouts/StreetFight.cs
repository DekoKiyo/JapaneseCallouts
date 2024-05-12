namespace JapaneseCallouts.Xml.Callouts;

public class StreetFightConfig
{
    [XmlArray, XmlArrayItem("Ped")]
    public List<PedConfig> Suspects { get; set; }
}