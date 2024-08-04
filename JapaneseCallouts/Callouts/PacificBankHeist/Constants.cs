namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal partial class PacificBankHeist
{
    // Blips sprites
    internal const BlipSprite SPRITE = BlipSprite.CriminalCarsteal;
    internal const BlipSprite RIOT_BLIP = BlipSprite.PolicePatrol;
    internal const BlipSprite COMMANDER_BLIP = BlipSprite.Friend;

    // Positions arrays
    internal static readonly Vector3 BankLocation = new(250.9f, 219.0f, 106.2f);
    internal static readonly Vector3 OutsideBankVault = new(257.2f, 225.2f, 101.8f);
    internal static readonly Vector3[] PacificBankInsideChecks = [new(235.9f, 220.6f, 106.2f), new(238.3f, 214.8f, 106.2f), new(261.0f, 208.1f, 106.2f), new(235.2f, 217.1f, 106.2f)];
    internal static readonly Vector3[] BankDoorPositions = [new(231.5f, 215.2f, 106.2f), new(259.1f, 202.7f, 106.2f)];

    // These doors will be unlocked to allow the player entering the bank
    internal static readonly (Vector3 pos, uint hash)[] Doors =
    [
        // 東側入口
        (new(258.2093f, 204.119f, 106.4328f), 110411286),
        (new(260.6518f, 203.2292f, 106.4328f), 110411286),
        // 西側入口
        (new(232.6054f, 214.1584f, 106.4049f), 110411286),
        (new(231.5075f, 216.5148f, 106.4049f), 110411286),
        // 東側入口第二
        (new(259.0951f, 212.8039f, 106.4328f), 110411286),
        (new(259.985f, 215.2464f, 106.4328f), 110411286),
        // 窓口横
        (new(256.3116f, 220.6579f, 106.4296f), 4072696575),
        // 地下室入口
        (new(262.1981f, 222.5188f, 106.4296f), 746855201),
        // 金庫ドア
        (new(255.2283f, 223.976f, 102.3932f), 961976194),
        // 金庫中扉
        (new(251.8576f, 221.0655f, 101.8324f), 2786611474),
        (new(261.3004f, 214.5051f, 101.8324f), 2786611474),
        // 2階
        (new(266.3624f, 217.5697f, 110.4328f), 1956494919),
        (new(262.5366f, 215.0576f, 110.4328f), 964838196),
        (new(260.8579f, 210.4453f, 110.4328f), 964838196),
        (new(256.6172f, 206.1522f, 110.4328f), 1956494919),
        // 階段
        (new(236.5488f, 228.3147f, 110.4328f), 1956494919),
        (new(237.7704f, 227.87f, 106.426f), 1956494919),
    ];

    // SWAT Animations
    internal const string SWAT_ANIMATION_DICTIONARY = "cover@weapon@rpg";
    internal const string SWAT_ANIMATION_LEFT = "blindfire_low_l_corner_exit";
    internal const string SWAT_ANIMATION_RIGHT_LOOKING = "blindfire_low_r_corner_exit";
    internal const string SWAT_ANIMATION_RIGHT = "blindfire_low_r_centre_exit";
    internal const string HOSTAGE_ANIMATION_DICTIONARY = "random@arrests";
    internal const string HOSTAGE_ANIMATION_KNEELING = "kneeling_arrest_idle";

    // Models
    internal static readonly Model BarrierModel = "PROP_BARRIER_WORK05";
    internal static readonly Model PhoneModel = "PROP_POLICE_PHONE";

    // Relationship groups
    internal static readonly RelationshipGroup PoliceRG = new("POLICE");
    internal static readonly RelationshipGroup RobbersRG = new("ROBBERS");
    internal static readonly RelationshipGroup SneakRobbersRG = new("SNEAK_ROBBERS");
    internal static readonly RelationshipGroup HostageRG = new("HOSTAGE");
}