namespace JapaneseCallouts.Xml.Callouts;

[XmlRoot("CalloutsSoundConfig")]
public class CalloutsSoundConfig
{
    [XmlElement]
    public string BankHeist { get; set; }
    [XmlElement]
    public string PacificBankHeist { get; set; }
    [XmlElement]
    public string DrunkGuys { get; set; }
    [XmlElement]
    public string RoadRage { get; set; }
    [XmlElement]
    public string StolenVehicle { get; set; }
    [XmlElement]
    public string StoreRobbery { get; set; }
}