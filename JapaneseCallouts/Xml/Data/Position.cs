namespace JapaneseCallouts.Xml.Data;

public class PositionBase
{
    [XmlAttribute("x")]
    public float X { get; set; }
    [XmlAttribute("y")]
    public float Y { get; set; }
    [XmlAttribute("z")]
    public float Z { get; set; }
}

public class Position : PositionBase
{
    [XmlAttribute("heading")]
    public float Heading { get; set; }
}