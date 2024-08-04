namespace JapaneseCallouts.Callouts.BankHeist;

internal class Configurations
{
    [JsonProperty("robbers")]
    internal PedConfig[] Robbers { get; set; }
    [JsonProperty("weapons")]
    internal WeaponConfig[] Weapons { get; set; }
}