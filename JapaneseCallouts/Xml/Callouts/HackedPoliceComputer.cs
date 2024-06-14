namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("HackedPoliceComputerConfig")]
public class HackedPoliceComputerConfig
{
    [XmlArray, XmlArrayItem("Computer")]
    public List<Computer> Computers { get; set; }
}

public class Computer
{
    [XmlAttribute("x")]
    public float X { get; set; }
    [XmlAttribute("y")]
    public float Y { get; set; }
    [XmlAttribute("z")]
    public float Z { get; set; }
}