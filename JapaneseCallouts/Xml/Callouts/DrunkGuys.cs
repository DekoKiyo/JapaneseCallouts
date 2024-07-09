namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("DrunkGuysConfig")]
public class DrunkGuysConfig
{
    [XmlArray("CalloutPositions"), XmlArrayItem("CalloutPosition")]
    public List<DrunkGuysPosition> DrunkGuysPositions { get; set; }
}

public class DrunkGuysPosition
{
    [XmlAttribute("x")]
    public float X { get; set; }
    [XmlAttribute("y")]
    public float Y { get; set; }
    [XmlAttribute("z")]
    public float Z { get; set; }
    [XmlAttribute("heading")]
    public float Heading { get; set; }
    [XmlArray("DrunkPositions"), XmlArrayItem("Position")]
    public List<Position> DrunkPos { get; set; }
}