namespace JapaneseCallouts.Callouts.BankHeist;

internal class Configurations : IConfig
{
    [JsonPropertyName("robbers")]
    internal PedConfig[] Robbers { get; set; }
    [JsonPropertyName("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
}