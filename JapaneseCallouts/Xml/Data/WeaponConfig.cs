namespace JapaneseCallouts.Xml.Data;

public class WeaponConfig : IChanceObject, IEntityObject
{
    [XmlAttribute("chance")]
    public int Chance { get; set; } = 100;
    [XmlElement("Model")]
    public string Model { get; set; } = string.Empty;
    [XmlArray("Components"), XmlArrayItem("Component")]
    public List<string> Components { get; set; } = [];
}