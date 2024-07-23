namespace JapaneseCallouts.Xml.Data;

public class VehicleConfig : IChanceObject, IEntityObject
{
    [XmlAttribute("chance")]
    public int Chance { get; set; } = 100;
    [XmlAttribute("livery")]
    public int Livery { get; set; } = -1;
    [XmlAttribute("color_r")]
    public int ColorR { get; set; } = -1;
    [XmlAttribute("color_g")]
    public int ColorG { get; set; } = -1;
    [XmlAttribute("color_b")]
    public int ColorB { get; set; } = -1;
    [XmlText()]
    public string Model { get; set; } = string.Empty;
}