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
    public string HotPursuit { get; set; }
    [XmlElement]
    public string RoadRage { get; set; }
    [XmlElement]
    public string StolenVehicle { get; set; }
    [XmlElement]
    public string StoreRobbery { get; set; }
    [XmlElement]
    public string StreetFight { get; set; }
    [XmlElement]
    public string WantedCriminalFound { get; set; }
}