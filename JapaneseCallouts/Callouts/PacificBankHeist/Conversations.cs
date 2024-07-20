namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal class PBHConversations
{
    internal PBHConversations() { }

    internal (string, string)[] IntroConversation;
    internal readonly Dictionary<string, Keys> IntroSelection = new()
    {
        [Localization.GetString("IntroSelection1")] = Keys.D1,
        [Localization.GetString("IntroSelection2")] = Keys.D2,
    };
    internal readonly (string, string)[] DiscussConversation =
    [
        (Settings.OfficerName, Localization.GetString("Discuss1")),
        (Localization.GetString("Commander"), Localization.GetString("Discuss2")),
        (Settings.OfficerName, Localization.GetString("Discuss3")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm1")),
    ];
    internal readonly (string, string)[] FightConversation =
    [
        (Settings.OfficerName, Localization.GetString("Fight1")),
        (Localization.GetString("Commander"), Localization.GetString("Fight2")),
        (Localization.GetString("Commander"), Localization.GetString("Fight3")),
        (Settings.OfficerName, Localization.GetString("Fight4")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm1")),
    ];
    internal readonly Dictionary<string, Keys> AlarmSelection = new()
    {
        [Localization.GetString("AlarmSelection1")] = Keys.D1,
        [Localization.GetString("AlarmSelection2")] = Keys.D2,
    };
    internal readonly (string, string)[] AlarmOffConversation =
    [
        (Localization.GetString("Commander"), Localization.GetString("Alarm2")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm3")),
    ];
    internal readonly (string, string)[] AlarmOnConversation =
    [
        (Localization.GetString("Commander"), Localization.GetString("Alarm4")),
        (Localization.GetString("Commander"), Localization.GetString("Alarm5")),
    ];
    internal readonly (string, string)[] NegotiationIntroConversation =
    [
        (Localization.GetString("Robber"), Localization.GetString("Phone1")),
        (Settings.OfficerName, Localization.GetString("Phone2")),
        (Localization.GetString("Robber"), Localization.GetString("Phone3")),
    ];
    internal readonly Dictionary<string, Keys> NegotiationIntroSelection = new()
    {
        [Localization.GetString("NegotiationSelection1")] = Keys.D1,
        [Localization.GetString("NegotiationSelection2")] = Keys.D2,
        [Localization.GetString("NegotiationSelection3")] = Keys.D3,
    };
    internal readonly (string, string)[] Negotiation1Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation12")),
    ];
    internal readonly Dictionary<string, Keys> Negotiation1Selection = new()
    {
        [Localization.GetString("NegotiationSelection4")] = Keys.D1,
        [Localization.GetString("NegotiationSelection2")] = Keys.D2,
        [Localization.GetString("NegotiationSelection5")] = Keys.D3,
    };
    internal readonly (string, string)[] Negotiation11Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation111")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation112")),
    ];
    internal readonly Dictionary<string, Keys> Negotiation11Selection = new()
    {
        [Localization.GetString("NegotiationSelection6")] = Keys.D1,
        [Localization.GetString("NegotiationSelection7")] = Keys.D2,
        [Localization.GetString("NegotiationSelection8")] = Keys.D3,
    };
    internal readonly (string, string)[] Negotiation13Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation131")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation132")),
    ];
    internal readonly Dictionary<string, Keys> Negotiation13Selection = new()
    {
        [Localization.GetString("NegotiationSelection9")] = Keys.D1,
        [Localization.GetString("NegotiationSelection6")] = Keys.D2,
        [Localization.GetString("NegotiationSelection10")] = Keys.D3,
    };
    internal readonly (string, string)[] Negotiation131Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1311")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1312")),
    ];
    internal readonly (string, string)[] Negotiation133Conversation1 =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1332")),
    ];
    internal readonly (string, string)[] Negotiation133Conversation2 =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1312")),
    ];
    internal readonly (string, string)[] Negotiation111Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1111", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1112")),
    ];
    internal readonly (string, string)[] Negotiation112Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1121")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1122")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1123")),
    ];
    internal readonly (string, string)[] Negotiation113Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation1131")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1132")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1133")),
        (Settings.OfficerName, Localization.GetString("Negotiation1134")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1135")),
    ];
    internal readonly (string, string)[] RequestConversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection2")),
        (Localization.GetString("Robber"), Localization.GetString("Request1")),
        (Localization.GetString("Robber"), Localization.GetString("Request2")),
    ];
    internal readonly Dictionary<string, Keys> RequestSelection = new()
    {
        [Localization.GetString("RequestSelection1")] = Keys.D1,
        [Localization.GetString("RequestSelection2")] = Keys.D2,
        [Localization.GetString("RequestSelection3")] = Keys.D3,
    };
    internal readonly (string, string)[] Request1Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection1")),
        (Localization.GetString("Robber"), Localization.GetString("Request11")),
        (Settings.OfficerName, Localization.GetString("Request12")),
        (Localization.GetString("Robber"), Localization.GetString("Request13")),
    ];
    internal readonly (string, string)[] Request2Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection2")),
        (Localization.GetString("Robber"), Localization.GetString("Request21")),
        (Localization.GetString("Robber"), Localization.GetString("Request22")),
    ];
    internal readonly (string, string)[] Request3Conversation =
    [
        (Settings.OfficerName, Localization.GetString("RequestSelection3")),
        (Localization.GetString("Robber"), Localization.GetString("Request31")),
        (Localization.GetString("Robber"), Localization.GetString("Request32")),
        (Localization.GetString("Robber"), Localization.GetString("Request33")),
    ];
    internal readonly Dictionary<string, Keys> Request2Selection = new()
    {
        [Localization.GetString("RequestSelection4")] = Keys.D1,
        [Localization.GetString("RequestSelection5")] = Keys.D2,
        [Localization.GetString("RequestSelection6")] = Keys.D3,
    };
    internal readonly (string, string)[] Request21Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Request211")),
        (Localization.GetString("Robber"), Localization.GetString("Request212")),
    ];
    internal readonly (string, string)[] Request22Conversation1 =
    [
        (Settings.OfficerName, Localization.GetString("Request221", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation1112")),
    ];
    internal readonly (string, string)[] Request22Conversation2 =
    [
        (Settings.OfficerName, Localization.GetString("Request221", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Request222")),
        (Settings.OfficerName, Localization.GetString("Request223")),
    ];
    internal readonly (string, string)[] Request22Conversation3 =
    [
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request224")),
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request225")),
        (Localization.GetString("Robber"), Localization.GetString("Request226", XmlManager.PacificBankHeistConfig.WifeName)),
        (Localization.GetString("Robber"), Localization.GetString("Request227")),
        (Localization.GetString("Robber"), Localization.GetString("Request228", XmlManager.PacificBankHeistConfig.WifeName)),
    ];
    internal readonly (string, string)[] Request22Conversation4 =
    [
        (Settings.OfficerName, Localization.GetString("Request229", XmlManager.PacificBankHeistConfig.WifeName)),
        (XmlManager.PacificBankHeistConfig.WifeName, Localization.GetString("Request220")),
    ];
    internal readonly (string, string)[] Negotiation3Conversation =
    [
    (Settings.OfficerName, Localization.GetString("NegotiationSelection3")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation31")),
    ];
    internal readonly Dictionary<string, Keys> Negotiation3Selection = new()
    {
        [Localization.GetString("NegotiationSelection1")] = Keys.D1,
        [Localization.GetString("NegotiationSelection11")] = Keys.D2,
        [Localization.GetString("NegotiationSelection12")] = Keys.D3,
        [Localization.GetString("NegotiationSelection10")] = Keys.D4,
    };
    internal readonly (string, string)[] Negotiation31Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation311")),
    ];
    internal readonly (string, string)[] Negotiation32Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection11")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation321")),
    ];
    internal readonly (string, string)[] Negotiation33Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation331")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation332")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation333")),
    ];
    internal readonly (string, string)[] Negotiation34Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection10")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3212")),
    ];
    internal readonly Dictionary<string, Keys> Negotiation32Selection = new()
    {
        [Localization.GetString("NegotiationSelection10")] = Keys.D1,
        [Localization.GetString("NegotiationSelection14")] = Keys.D2,
        [Localization.GetString("NegotiationSelection15")] = Keys.D3,
    };
    internal readonly (string, string)[] Negotiation321Conversation =
    [
        (Settings.OfficerName, Localization.GetString("Negotiation3211")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3212")),
    ];
    internal readonly (string, string)[] Negotiation322Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection14")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3221")),
    ];
    internal readonly (string, string)[] Negotiation323Conversation =
    [
        (Settings.OfficerName, Localization.GetString("NegotiationSelection15")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3231")),
        (Localization.GetString("Robber"), Localization.GetString("Negotiation3232")),
    ];
    internal readonly (string, string)[] AfterSurrendered =
    [
        (Localization.GetString("Commander"), Localization.GetString("Surrender1")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender2")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender3")),
        (Localization.GetString("Commander"), Localization.GetString("Surrender4")),
        (Settings.OfficerName, Localization.GetString("Surrender5")),
    ];
    internal readonly (string, string)[] Final =
    [
        (Localization.GetString("Commander"), Localization.GetString("Final1")),
        (Localization.GetString("Commander"), Localization.GetString("Final2")),
        (Settings.OfficerName, Localization.GetString("Final3")),
    ];
}