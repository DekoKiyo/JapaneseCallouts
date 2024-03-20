/*
    This callout is based Bank Heist in [Assorted Callouts](https://github.com/Albo1125/Assorted-Callouts) made by [Albo1125](https://github.com/Albo1125).
    The source code is licensed under the GPL-3.0.
*/

namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Bank Heist", CalloutProbability.Low)]
internal class BankHeist : CalloutBase
{
    private enum AlarmState
    {
        Alarm,
        None
    }

    private enum Negotiation
    {
        Surrender,
        Fight,
    }

    private const string ALARM_SOUND_FILE_NAME = "BankHeistAlarm.wav";

    internal static SoundPlayer BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{ALARM_SOUND_FILE_NAME}");
    private const ulong _DOOR_CONTROL = 0x9b12f9a24fabedb0;

    private Vector3 BankLocation = new(250.9f, 219.0f, 106.2f);
    private Vector3 OutsideBankVault = new(257.2f, 225.2f, 101.8f);
    private readonly Vector3[] PacificBankInsideChecks = [new(235.9f, 220.6f, 106.2f), new(238.3f, 214.8f, 106.2f), new(261.0f, 208.1f, 106.2f), new(235.2f, 217.1f, 106.2f)];

    // Timer Bars
    internal static TimerBarPool TBPool = [];
    internal static TextTimerBar RescuedHostagesTB;
    internal static TextTimerBar DiedHostagesTB;

    #region Positions
    // Vehicles
    private readonly (Vector3 pos, float heading)[] PoliceCruiserPositions =
    [
        (new(271.3f, 180.6f, 104.3f), 69.4f),
        (new(258.9f, 185.2f, 104.4f), 69.8f),
        (new(246.8f, 189.8f, 104.8f), 68.1f),
        (new(236.9f, 193.9f, 104.9f), -110.7f),
        (new(227.7f, 197.9f, 105.0f), 64.5f),
        (new(220.6f, 217.9f, 105.1f), -18.9f),
        (new(223.9f, 226.8f, 105.1f), -20.0f),
        (new(231.3f, 173.6f, 104.9f), -110.2f)
    ];
    private readonly (Vector3 pos, float heading)[] PoliceTransportPositions =
    [
        (new(273.9f, 191.7f, 104.6f), 54.3f),
        (new(219.0f, 175.1f, 105.2f), -111.1f)
    ];
    private readonly (Vector3 pos, float heading)[] RiotPositions =
    [
        (new(220.2f, 209.2f, 105.1f), 23.2f),
        (new(265.4f, 192.2f, 104.4f), -139.9f)
    ];
    private readonly (Vector3 pos, float heading)[] AmbulancePositions =
    [
        (new(263.2f, 158.9f, 104.2f), -110.3f),
        (new(254.0f, 162.2f, 104.4f), -109.2f)
    ];
    private readonly (Vector3 pos, float heading)[] FiretruckPositions =
    [
        (new(239.6f, 170.7f, 105.1f), -109.9f)
    ];
    // Barrier
    private readonly (Vector3 pos, float heading)[] BarrierPositions =
    [
        (new(266.5f, 182.4f, 103.6f), -20.1f),
        (new(263.4f, 183.6f, 103.7f), -20.1f),
        (new(254.3f, 186.8f, 103.9f), -20.1f),
        (new(251.2f, 187.9f, 103.9f), -20.1f),
        (new(241.6f, 192.0f, 104.1f), -21.5f),
        (new(232.2f, 195.8f, 104.2f), -22.2f),
        (new(223.8f, 200.1f, 104.4f), -33.5f),
        (new(220.9f, 203.0f, 104.4f), -53.1f),
        (new(222.2f, 222.2f, 104.4f), -110.2f),
        (new(227.7f, 228.2f, 104.5f), 159.8f),
        (new(230.5f, 227.2f, 104.5f), 159.8f)
    ];
    // Officers
    private readonly (Vector3 pos, float heading)[] AimingOfficerPositions =
    [
        (new(219.8f, 220.3f, 105.5f), -120.1f),
        (new(218.5f, 216.5f, 105.5f), -105.7f),
        (new(226.1f, 197.0f, 105.4f), -19.9f),
        (new(234.4f, 193.3f, 105.2f), -21.4f),
        (new(247.8f, 187.9f, 105.0f), -29.9f),
        (new(257.3f, 184.2f, 104.9f), 2.6f),
        (new(260.5f, 183.1f, 104.8f), -12.2f)
    ];
    private readonly (Vector3 pos, float heading)[] StandingOfficerPositions =
    [
        (new(215.2f, 177.1f, 105.3f), 70.4f),
        (new(218.6f, 207.0f, 105.4f), 113.0f)
    ];
    // Robbers
    private readonly (Vector3 pos, float heading)[] NormalRobbersPositions =
    [
        (new(262.5f, 211.9f, 106.2f), 161.0f),
        (new(257.3f, 214.9f, 106.2f), 160.5f),
        (new(235.4f, 217.3f, 106.2f), 116.3f),
        (new(237.9f, 213.6f, 106.2f), 69.5f),
        (new(243.7f, 212.4f, 110.2f), -20.6f),
        (new(266.6f, 220.1f, 110.2f), 69.9f),
        (new(254.7f, 228.1f, 106.2f), 161.5f),
        (new(268.2f, 223.0f, 103.4f), 159.4f),
        (new(246.7f, 218.7f, 106.2f), 169.6f),
        (new(251.0f, 208.6f, 106.2f), -21.0f),
    ];
    private readonly (Vector3 pos, float heading)[] RobbersNegotiationPositions =
    [
        (new(235.3f, 216.8f, 106.2f), 115.5f),
        (new(243.0f, 222.6f, 106.2f), 70.8f),
        (new(256.1f, 216.8f, 106.2f), -109.0f),
        (new(254.0f, 213.1f, 106.2f), -109.1f),
        (new(257.6f, 222.5f, 106.2f), 159.9f),
        (new(264.0f, 224.6f, 101.6f), -110.5f),
        (new(237.0f, 218.3f, 110.2f), -66.1f),
    ];
    private readonly (Vector3 pos, float heading, bool isRight)[] RobbersSneakPosition =
    [
        (new(255.1f, 222.0f, 106.2f), -18.7f, false),
        (new(263.1f, 215.4f, 110.2f), -175.0f, false),
        (new(265.2f, 222.3f, 101.6f), 125.6f, false),
        (new(257.0f, 205.4f, 110.2f), -24.9f, true),
        (new(235.8f, 228.0f, 110.2f), -122.6f, true),
        (new(238.9f, 228.3f, 106.2f), 34.7f, true),
        (new(245.7f, 214.3f, 106.2f), 69.8f, true),
    ];
    private readonly (Vector3 pos, float heading)[] RobbersInVaultPositions =
    [
        (new(255.9f, 217.0f, 101.6f), 69.9f),
        (new(258.5f, 216.0f, 101.6f), 70.8f),
        (new(251.3f, 218.9f, 101.6f), -19.1f),
    ];
    private readonly (Vector3 pos, float heading)[] RobbersSurrenderingPositions =
    [
        (new(229.2f, 207.9f, 105.4f), 160.1f),
        (new(233.8f, 206.2f, 105.3f), 160.1f),
        (new(238.2f, 204.6f, 105.3f), 160.1f),
        (new(242.8f, 202.9f, 105.2f), 160.1f),
        (new(246.8f, 201.4f, 105.1f), 160.1f),
        (new(251.5f, 199.7f, 105.0f), 160.1f),
        (new(257.1f, 197.7f, 104.9f), 160.1f),
    ];
    // EMS
    private readonly (Vector3 pos, float heading)[] FirefighterPositions =
    [
        (new(244.9f, 168.3f, 104.9f), -24.0f)
    ];
    private readonly (Vector3 pos, float heading)[] ParamedicPositions =
    [
        (new(250.8f, 165.5f, 104.7f), -21.2f),
        (new(259.8f, 162.4f, 104.6f), -20.5f)
    ];
    // SWAT
    private readonly (Vector3 pos, float heading)[] LeftSittingSWATPositions =
    [
        (new(231.6f, 211.8f, 105.4f), 84.4f),
        (new(232.0f, 211.0f, 105.4f), 93.8f),
        (new(232.9f, 210.4f, 105.4f), 129.7f),
        (new(260.7f, 200.2f, 104.9f), 133.2f),
        (new(261.5f, 199.9f, 104.9f), 118.9f),
        (new(262.3f, 199.6f, 104.9f), 127.6f)
    ];
    private readonly (Vector3 pos, float heading)[] RightSittingSWATPositions =
    [
        (new(228.7f, 218.0f, 105.5f), 150.2f),
        (new(228.9f, 219.2f, 105.5f), 125.3f),
        (new(255.7f, 202.1f, 105.0f), -128.7f),
        (new(254.8f, 202.5f, 105.0f), -145.8f)
    ];
    private readonly (Vector3 pos, float heading)[] RightLookingSWATPositions =
    [
        (new(229.1f, 217.1f, 105.5f), 171.3f),
        (new(256.6f, 201.8f, 105.0f), -138.0f)
    ];
    // Bank Door
    private readonly Vector3[] BankDoorPositions =
    [
        new Vector3(231.5f, 215.2f, 106.2f),
        new Vector3(259.1f, 202.7f, 106.2f)
    ];
    // Captain
    private readonly (Vector3 pos, float heading) CaptainPosition = (new(271.9f, 190.8f, 104.7f), 138.2f);
    // Wife
    private readonly (Vector3 pos, float heading) WifePosition = (new(178.6f, 120.6f, 95.6f), -17.2f);
    private readonly Vector3 WifeCarDestination = new(241.7f, 178.9f, 104.7f);
    // Hostage
    private readonly List<Vector3> HostagePositions =
    [
        new(242.8f, 228.3f, 106.2f),
        new(248.3f, 229.7f, 106.2f),
        new(241.4f, 221.5f, 106.2f),
        new(245.6f, 216.6f, 106.2f),
        new(253.6f, 218.9f, 106.2f),
        new(267.2f, 223.0f, 110.2f),
        new(262.3f, 224.8f, 101.6f),
        new(262.6f, 208.2f, 110.2f),
        new(250.6f, 209.1f, 110.2f),
        new(243.3f, 211.7f, 110.2f),
        new(236.1f, 216.3f, 110.2f),
        new(255.0f, 224.7f, 106.2f)
    ];
    private readonly (Vector3 pos, float heading) HostageSafePosition = (new(248.6f, 169.1f, 104.9f), 158.3f);
    #endregion
    #region Conversations
    // Conversation
    private readonly (string, string)[] IntroConversation =
    [
        (Settings.OfficerName, BankHeistConversation.Intro1),
        (CalloutsText.Commander, BankHeistConversation.Intro2),
        (Settings.OfficerName, BankHeistConversation.Intro3),
        (CalloutsText.Commander, BankHeistConversation.Intro4),
        (CalloutsText.Commander, BankHeistConversation.Intro5),
        (CalloutsText.Commander, BankHeistConversation.Intro6),
        (CalloutsText.Commander, BankHeistConversation.Intro7),
        (CalloutsText.Commander, BankHeistConversation.Intro8),
    ];
    private readonly Dictionary<string, Keys> IntroSelection = new()
    {
        [BankHeistConversation.IntroSelection1] = Keys.D1,
        [BankHeistConversation.IntroSelection2] = Keys.D2,
    };
    private readonly (string, string)[] DiscussConversation =
    [
        (Settings.OfficerName, BankHeistConversation.Discuss1),
        (CalloutsText.Commander, BankHeistConversation.Discuss2),
        (Settings.OfficerName, BankHeistConversation.Discuss3),
        (CalloutsText.Commander, BankHeistConversation.Alarm1),
    ];
    private readonly (string, string)[] FightConversation =
    [
        (Settings.OfficerName, BankHeistConversation.Fight1),
        (CalloutsText.Commander, BankHeistConversation.Fight2),
        (CalloutsText.Commander, BankHeistConversation.Fight3),
        (Settings.OfficerName, BankHeistConversation.Fight4),
        (CalloutsText.Commander, BankHeistConversation.Alarm1),
    ];
    private readonly Dictionary<string, Keys> AlarmSelection = new()
    {
        [BankHeistConversation.AlarmSelection1] = Keys.D1,
        [BankHeistConversation.AlarmSelection2] = Keys.D2,
    };
    private readonly (string, string)[] AlarmOffConversation =
    [
        (CalloutsText.Commander, BankHeistConversation.Alarm2),
        (CalloutsText.Commander, BankHeistConversation.Alarm3),
    ];
    private readonly (string, string)[] AlarmOnConversation =
    [
        (CalloutsText.Commander, BankHeistConversation.Alarm4),
        (CalloutsText.Commander, BankHeistConversation.Alarm5),
    ];
    private readonly (string, string)[] NegotiationIntroConversation =
    [
        (CalloutsText.Robber, BankHeistConversation.Phone1),
        (Settings.OfficerName, BankHeistConversation.Phone2),
        (CalloutsText.Robber, BankHeistConversation.Phone3),
    ];
    private readonly Dictionary<string, Keys> NegotiationIntroSelection = new()
    {
        [BankHeistConversation.NegotiationSelection1] = Keys.D1,
        [BankHeistConversation.NegotiationSelection2] = Keys.D2,
        [BankHeistConversation.NegotiationSelection3] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation1Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation11),
        (CalloutsText.Robber, BankHeistConversation.Negotiation12),
    ];
    private readonly Dictionary<string, Keys> Negotiation1Selection = new()
    {
        [BankHeistConversation.NegotiationSelection4] = Keys.D1,
        [BankHeistConversation.NegotiationSelection2] = Keys.D2,
        [BankHeistConversation.NegotiationSelection5] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation11Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation111),
        (CalloutsText.Robber, BankHeistConversation.Negotiation112),
    ];
    private readonly Dictionary<string, Keys> Negotiation11Selection = new()
    {
        [BankHeistConversation.NegotiationSelection6] = Keys.D1,
        [BankHeistConversation.NegotiationSelection7] = Keys.D2,
        [BankHeistConversation.NegotiationSelection8] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation13Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation131),
        (CalloutsText.Robber, BankHeistConversation.Negotiation132),
    ];
    private readonly Dictionary<string, Keys> Negotiation13Selection = new()
    {
        [BankHeistConversation.NegotiationSelection9] = Keys.D1,
        [BankHeistConversation.NegotiationSelection6] = Keys.D2,
        [BankHeistConversation.NegotiationSelection10] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation131Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation1311),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1312),
    ];
    private readonly (string, string)[] Negotiation133Conversation1 =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation1331),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1332),
    ];
    private readonly (string, string)[] Negotiation133Conversation2 =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation1331),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1312),
    ];
    private readonly (string, string)[] Negotiation111Conversation =
    [
        (Settings.OfficerName, string.Format(BankHeistConversation.Negotiation1111, Settings.WifeName)),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1112),
    ];
    private readonly (string, string)[] Negotiation112Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation1121),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1122),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1123),
    ];
    private readonly (string, string)[] Negotiation113Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation1131),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1132),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1133),
        (Settings.OfficerName, BankHeistConversation.Negotiation1134),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1135),
    ];
    private readonly (string, string)[] RequestConversation =
    [
        (CalloutsText.Robber, BankHeistConversation.Request1),
        (CalloutsText.Robber, BankHeistConversation.Request2),
    ];
    private readonly Dictionary<string, Keys> RequestSelection = new()
    {
        [BankHeistConversation.RequestSelection1] = Keys.D1,
        [BankHeistConversation.RequestSelection2] = Keys.D2,
        [BankHeistConversation.RequestSelection3] = Keys.D3,
    };
    private readonly (string, string)[] Request1Conversation =
    [
        (CalloutsText.Robber, BankHeistConversation.Request11),
        (Settings.OfficerName, BankHeistConversation.Request12),
        (CalloutsText.Robber, BankHeistConversation.Request13),
    ];
    private readonly (string, string)[] Request2Conversation =
    [
        (CalloutsText.Robber, BankHeistConversation.Request21),
        (CalloutsText.Robber, BankHeistConversation.Request22),
    ];
    private readonly (string, string)[] Request3Conversation =
    [
        (CalloutsText.Robber, BankHeistConversation.Request31),
        (CalloutsText.Robber, BankHeistConversation.Request32),
        (CalloutsText.Robber, BankHeistConversation.Request33),
    ];
    private readonly Dictionary<string, Keys> Request2Selection = new()
    {
        [BankHeistConversation.RequestSelection4] = Keys.D1,
        [BankHeistConversation.RequestSelection5] = Keys.D2,
        [BankHeistConversation.RequestSelection6] = Keys.D3,
    };
    private readonly (string, string)[] Request21Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Request211),
        (CalloutsText.Robber, BankHeistConversation.Request212),
    ];
    private readonly (string, string)[] Request22Conversation1 =
    [
        (Settings.OfficerName, string.Format(BankHeistConversation.Request221, Settings.WifeName)),
        (CalloutsText.Robber, BankHeistConversation.Negotiation1112),
    ];
    private readonly (string, string)[] Request22Conversation2 =
    [
        (Settings.OfficerName, string.Format(BankHeistConversation.Request221, Settings.WifeName)),
        (CalloutsText.Robber, BankHeistConversation.Request222),
        (Settings.OfficerName, BankHeistConversation.Request223),
    ];
    private readonly (string, string)[] Request22Conversation3 =
    [
        (Settings.WifeName, BankHeistConversation.Request224),
        (Settings.WifeName, BankHeistConversation.Request225),
        (CalloutsText.Robber, string.Format(BankHeistConversation.Request226, Settings.WifeName)),
        (CalloutsText.Robber, BankHeistConversation.Request227),
        (CalloutsText.Robber, string.Format(BankHeistConversation.Request228, Settings.WifeName)),
    ];
    private readonly (string, string)[] Request22Conversation4 =
    [
        (Settings.OfficerName, string.Format(BankHeistConversation.Request229, Settings.WifeName)),
        (Settings.WifeName, BankHeistConversation.Request220),
    ];
    private readonly (string, string)[] Negotiation3Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.NegotiationSelection3),
        (CalloutsText.Robber, BankHeistConversation.Negotiation31)
    ];
    private readonly Dictionary<string, Keys> Negotiation3Selection = new()
    {
        [BankHeistConversation.NegotiationSelection1] = Keys.D1,
        [BankHeistConversation.NegotiationSelection11] = Keys.D2,
        [BankHeistConversation.NegotiationSelection12] = Keys.D3,
        [BankHeistConversation.NegotiationSelection10] = Keys.D4,
    };
    private readonly (string, string)[] Negotiation31Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation11),
        (CalloutsText.Robber, BankHeistConversation.Negotiation311),
    ];
    private readonly (string, string)[] Negotiation32Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.NegotiationSelection11),
        (CalloutsText.Robber, BankHeistConversation.Negotiation321),
    ];
    private readonly (string, string)[] Negotiation33Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation331),
        (CalloutsText.Robber, BankHeistConversation.Negotiation332),
        (CalloutsText.Robber, BankHeistConversation.Negotiation333),
    ];
    private readonly (string, string)[] Negotiation34Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.NegotiationSelection10),
        (CalloutsText.Robber, BankHeistConversation.Negotiation3212),
    ];
    private readonly Dictionary<string, Keys> Negotiation32Selection = new()
    {
        [BankHeistConversation.NegotiationSelection10] = Keys.D1,
        [BankHeistConversation.NegotiationSelection14] = Keys.D2,
        [BankHeistConversation.NegotiationSelection15] = Keys.D3,
    };
    private readonly (string, string)[] Negotiation321Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.Negotiation3211),
        (CalloutsText.Robber, BankHeistConversation.Negotiation3212),
    ];
    private readonly (string, string)[] Negotiation322Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.NegotiationSelection14),
        (CalloutsText.Robber, BankHeistConversation.Negotiation3221),
    ];
    private readonly (string, string)[] Negotiation323Conversation =
    [
        (Settings.OfficerName, BankHeistConversation.NegotiationSelection15),
        (CalloutsText.Robber, BankHeistConversation.Negotiation3231),
        (CalloutsText.Robber, BankHeistConversation.Negotiation3232),
    ];
    private readonly (string, string)[] AfterSurrendered =
    [
        (CalloutsText.Commander, BankHeistConversation.Surrender1),
        (CalloutsText.Commander, BankHeistConversation.Surrender2),
        (CalloutsText.Commander, BankHeistConversation.Surrender3),
        (CalloutsText.Commander, BankHeistConversation.Surrender4),
        (Settings.OfficerName, BankHeistConversation.Surrender5),
    ];
    private readonly (string, string)[] Final =
    [
        (CalloutsText.Commander, BankHeistConversation.Final1),
        (CalloutsText.Commander, BankHeistConversation.Final2),
        (CalloutsText.Commander, BankHeistConversation.Final3),
        (Settings.OfficerName, BankHeistConversation.Final4),
    ];
    #endregion

    // Vehicle Model
    private readonly Model[] PoliceCruiserModels = ["POLICE", "POLICE2", "POLICE3", "POLICE4"];
    private readonly Model[] PoliceTransportModels = ["POLICET"];
    private readonly Model[] RiotModels = ["RIOT"];
    private readonly Model[] AmbulanceModels = ["AMBULANCE"];
    private readonly Model[] FiretruckModels = ["FIRETRUK"];
    private readonly Model BarrierModel = "PROP_BARRIER_WORK05";
    private readonly Model InvisibleWallModel = "P_ICE_BOX_01_S";
    private readonly Model[] CopModels = ["S_M_Y_COP_01", "S_F_Y_COP_01"];
    private readonly Model SwatModel = "S_M_Y_SWAT_01";
    private readonly Model ParamedicModel = "S_M_M_PARAMEDIC_01";
    private readonly Model FirefighterModel = "S_M_Y_FIREMAN_01";
    private readonly Model CaptainModel = "IG_FBISUIT_01";
    private readonly Model WifeModel = "IG_FBISUIT_01";
    private readonly Model RobbersModel = "MP_G_M_PROS_01";
    private readonly Model PhoneModel = "PROP_POLICE_PHONE";
    private readonly Model[] HostageModels = ["A_M_M_BUSINESS_01", "A_M_Y_BUSINESS_01", "A_M_Y_BUSINESS_02", "A_M_Y_BUSINESS_03", "A_F_Y_BUSINESS_01", "A_F_Y_FEMALEAGENT"];
    private readonly WeaponHash[] OfficerWeapons = [WeaponHash.Pistol];
    private readonly WeaponHash[] SWATWeapons = [WeaponHash.CarbineRifle, WeaponHash.AssaultRifle];
    private readonly WeaponHash[] RobbersWeapons = [WeaponHash.SawnOffShotgun, WeaponHash.AssaultRifle, WeaponHash.PumpShotgun, WeaponHash.Pistol, WeaponHash.AdvancedRifle, WeaponHash.AssaultSMG];
    private readonly WeaponHash[] Grenades = [WeaponHash.Grenade, WeaponHash.SmokeGrenade];
    private readonly WeaponHash[] RobbersSneakyWeapons = [WeaponHash.Pistol50, WeaponHash.Knife, WeaponHash.AssaultSMG, WeaponHash.Crowbar, WeaponHash.Hammer, WeaponHash.AssaultRifle];

    private const string SWAT_ANIMATION_DICTIONARY = "cover@weapon@rpg";
    private const string SWAT_ANIMATION_LEFT = "blindfire_low_l_corner_exit";
    private const string SWAT_ANIMATION_RIGHT_LOOKING = "blindfire_low_r_corner_exit";
    private const string SWAT_ANIMATION_RIGHT = "blindfire_low_r_centre_exit";
    private const string HOSTAGE_ANIMATION_DICTIONARY = "random@arrests";
    private const string HOSTAGE_ANIMATION_KNEELING = "kneeling_arrest_idle";

    private readonly List<Vehicle> AllPoliceVehicles = [];
    private readonly List<Vehicle> AllRiot = [];
    private readonly List<Vehicle> AllAmbulance = [];
    private readonly List<Vehicle> AllFiretruck = [];
    private readonly List<Ped> AllOfficers = [];
    private readonly List<Ped> AllStandingOfficers = [];
    private readonly List<Ped> AllAimingOfficers = [];
    private readonly List<Ped> OfficersArresting = [];
    private readonly List<Ped> OfficersTargetsToShoot = [];
    private readonly List<Ped> AllSWATUnits = [];
    private readonly List<Ped> AllEMS = [];
    private readonly List<Ped> AllRobbers = [];
    private readonly List<Ped> AllSneakRobbers = [];
    private readonly List<Ped> AllVaultRobbers = [];
    private readonly List<RObject> AllBarriers = [];
    private readonly List<Ped> AllBarrierPeds = [];
    private readonly List<RObject> AllInvisibleWalls = [];
    private readonly List<Ped> FightingSneakRobbers = [];
    private readonly List<Entity> CalloutEntities = [];
    private readonly List<Ped> RescuedHostages = [];
    private readonly List<Ped> SafeHostages = [];
    private readonly List<Ped> AllHostages = [];
    private readonly List<Ped> SpawnedHostages = [];
    private int AliveHostagesCount = 0;
    private int SafeHostagesCount = 0;
    private int TotalHostagesCount = 0;

    private readonly RelationshipGroup RobbersRG = new("ROBBERS");
    private readonly RelationshipGroup SneakRobbersRG = new("SNEAK_ROBBERS");
    private readonly RelationshipGroup HostageRG = new("HOSTAGE");
    private Blip BankBlip;
    private Blip SideDoorBlip;
    private Ped Commander;
    private Ped Wife;
    private Ped WifeDriver;
    private Vehicle WifeCar;
    private Blip CommanderBlip;
    private RObject MobilePhone;

    private static int DiedRobbersCount = 0;

    private AlarmState CurrentAlarmState = AlarmState.None;
    private Negotiation NegotiationResult = Negotiation.Fight;
    private bool IsAlarmPlaying = true;
    private bool AlarmStateChanged = true;
    private bool TalkedToCommander = false;
    private bool IsFighting = false;
    private bool SurrenderComplete = false;
    private bool IsSurrendering = false;
    private bool FightingPrepared = false;
    private bool IsSWATFollowing = false;
    private bool TalkedToWells2nd = false;
    private bool DoneFighting = false;
    private bool EvaluatedWithWells = false;
    private bool RescuingHostage = false;
    private bool HandlingRespawn = false;
    private bool IsCalloutFinished = false;

    internal override void Setup()
    {
        CalloutPosition = BankLocation;
        CalloutMessage = CalloutsName.BankHeist;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 30f);
        Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_BANK_ROBBERY IN_OR_ON_POSITION UNITS_RESPOND_CODE_99", CalloutPosition);

        if (Main.IsCalloutInterfaceAPIExist)
        {
            CalloutInterfaceAPIFunctions.SendMessage(this, CalloutsDescription.BankHeist);
        }

        OnCalloutsEnded += () =>
        {
            BankAlarm.Stop();
            BankAlarm.Dispose();
            BankAlarm = null;
            Main.Player.IsPositionFrozen = false;
            Game.LocalPlayer.HasControl = true;
            // Main.Player.CanAttackFriendlies = false;
            NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 1f);
            NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 1f);
            NativeFunction.Natives.RESET_AI_WEAPON_DAMAGE_MODIFIER();
            NativeFunction.Natives.RESET_AI_MELEE_WEAPON_DAMAGE_MODIFIER();
            if (SideDoorBlip is not null && SideDoorBlip.IsValid() && SideDoorBlip.Exists()) SideDoorBlip.Delete();
            ToggleMobilePhone(Main.Player, false);
            if (IsCalloutFinished)
            {
                HudHelpers.DisplayNotification(General.CalloutCode4, General.Dispatch, CalloutsName.BankHeist);

                if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists()) BankBlip.Delete();
                if (Commander is not null && Commander.IsValid() && Commander.Exists()) Commander.Dismiss();
                if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Dismiss();
                if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Dismiss();
                if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Dismiss();
                if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists()) CommanderBlip.Delete();
                if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
                foreach (var e in AllPoliceVehicles)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        var driver = e.HasDriver ? e.Driver : e.CreateRandomDriver();
                        if (driver is not null && driver.IsValid() && driver.Exists())
                        {
                            driver.Tasks.CruiseWithVehicle(e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                            driver.Dismiss();
                        }
                        e.Dismiss();
                    }
                }
                foreach (var e in AllAmbulance)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        var driver = e.HasDriver ? e.Driver : e.CreateRandomDriver();
                        if (driver is not null && driver.IsValid() && driver.Exists())
                        {
                            driver.Tasks.CruiseWithVehicle(e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                            driver.Dismiss();
                        }
                        e.Dismiss();
                    }
                }
                foreach (var e in AllFiretruck)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        var driver = e.HasDriver ? e.Driver : e.CreateRandomDriver();
                        if (driver is not null && driver.IsValid() && driver.Exists())
                        {
                            driver.Tasks.CruiseWithVehicle(e, 14f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                            driver.Dismiss();
                        }
                        e.Dismiss();
                    }
                }
                foreach (var e in AllHostages) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllSWATUnits) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllEMS) if (e is not null && e.IsValid() && e.Exists()) e.Dismiss();
                foreach (var e in AllRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }
                foreach (var e in AllSneakRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }
                foreach (var e in AllVaultRobbers)
                {
                    if (e is not null && e.IsValid() && e.Exists())
                    {
                        if (e.IsAlive) e.Delete();
                        else e.Dismiss();
                    }
                }
                foreach (var e in AllBarriers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllBarrierPeds) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllInvisibleWalls) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
            }
            else
            {
                HudHelpers.DisplayNotification(General.SomethingWrong);

                if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists()) BankBlip.Delete();
                if (Commander is not null && Commander.IsValid() && Commander.Exists()) Commander.Delete();
                if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Delete();
                if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Delete();
                if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Delete();
                if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists()) CommanderBlip.Delete();
                if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
                foreach (var e in AllPoliceVehicles) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllAmbulance) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllFiretruck) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllHostages) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllOfficers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllSWATUnits) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllEMS) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllSneakRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllVaultRobbers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllBarriers) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllBarrierPeds) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
                foreach (var e in AllInvisibleWalls) if (e is not null && e.IsValid() && e.Exists()) e.Delete();
            }
        };
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(CalloutsText.BankHeistWarning);
        BankAlarm.Load();
        if (Main.Player.IsInAnyVehicle(false))
        {
            CalloutEntities.Add(Main.Player.CurrentVehicle);
        }
        HudHelpers.DisplayNotification(CalloutsDescription.BankHeist);

        DiedHostagesTB = new(CalloutsText.DiedHostages, $"{TotalHostagesCount - AliveHostagesCount}")
        {
            Highlight = HudColor.Green.GetColor()
        };
        RescuedHostagesTB = new(CalloutsText.RescuedHostages, $"{SafeHostagesCount}/{TotalHostagesCount}")
        {
            Highlight = HudColor.Blue.GetColor()
        };
        TBPool.Add(DiedHostagesTB);
        TBPool.Add(RescuedHostagesTB);
        CalloutHandler();
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!HandlingRespawn)
        {
            if (Main.Player.IsDead)
            {
                HandleCustomRespawn();
            }
        }
        if (!IsCalloutRunning || DoneFighting)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
    }

    private void HandleCustomRespawn()
    {
        HandlingRespawn = true;
        IsSWATFollowing = false;
        var OldState = CurrentAlarmState;
        CurrentAlarmState = AlarmState.None;
        AlarmStateChanged = true;
        GameFiber.StartNew(() =>
        {
            while (true)
            {
                GameFiber.Yield();
                if (Game.IsScreenFadedOut) break;
            }
            GameFiber.Sleep(1000);
            while (true)
            {
                GameFiber.Yield();
                if (Main.Player.IsValid() && Main.Player.Exists())
                {
                    if (Main.Player.IsAlive) break;
                }
            }
            Game.LocalPlayer.HasControl = false;
            Game.FadeScreenOut(1, true);
            Main.Player.WarpIntoVehicle(AllAmbulance[0], 2);

            Game.FadeScreenIn(2500, true);
            Game.LocalPlayer.HasControl = true;
            CurrentAlarmState = OldState;
            AlarmStateChanged = true;
            Main.Player.WarpIntoVehicle(AllAmbulance[0], 2);
            GameFiber.Yield();
            if (Main.Player.IsInVehicle(AllAmbulance[0], false))
            {
                Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
            }
            while (true)
            {
                GameFiber.Yield();
                if (Main.Player.IsValid())
                {
                    if (Vector3.Distance(Main.Player.Position, AllAmbulance[0].Position) < 70f) break;
                    if (Main.Player.IsAlive)
                    {
                        HudHelpers.DisplayHelp(string.Format(CalloutsText.SpawnAmbulance, Settings.EnterRiotVanModifierKey is Keys.None ? $"~{Settings.EnterRiotVanKey.GetInstructionalId()}~" : $"~{Settings.EnterRiotVanModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.EnterRiotVanKey.GetInstructionalId()}~"));
                        if (KeyHelpers.IsKeysDown(Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey))
                        {
                            Main.Player.WarpIntoVehicle(AllAmbulance[0], 2);
                            Game.HideHelp();
                            GameFiber.Sleep(1000);
                        }
                    }
                }
            }
            HandlingRespawn = false;
        });
    }

    private void CalloutHandler()
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                BankBlip = new(CalloutPosition)
                {
                    Color = Color.Yellow,
                    RouteColor = Color.Yellow,
                    IsRouteEnabled = true
                };
                SideDoorBlip = new(new Vector3(258.3f, 200.4f, 104.9f))
                {
                    Color = Color.Yellow,
                };
                GameFiber.StartNew(() =>
                {
                    GameFiber.Wait(4800);
                    HudHelpers.DisplayNotification(CalloutsText.BankHeistCopyThat);
                    Functions.PlayScannerAudio("JP_COPY_THAT_MOVING_RIGHT_NOW REPORT_RESPONSE_COPY JP_PROCEED_WITH_CAUTION");
                    GameFiber.Wait(3400);
                    HudHelpers.DisplayNotification(CalloutsText.BankHeistRoger);
                });
                LoadModels();
                while (Vector3.Distance(Main.Player.Position, CalloutPosition) > 350f)
                {
                    GameFiber.Yield();
                    GameFiber.Wait(1000);
                }
                if (Main.Player.IsInAnyVehicle(false))
                {
                    CalloutEntities.Add(Main.Player.CurrentVehicle);
                    var passengers = Main.Player.CurrentVehicle.Passengers;
                    if (passengers.Length > 0)
                    {
                        foreach (var passenger in passengers)
                        {
                            CalloutEntities.Add(passenger);
                        }
                    }
                }
                GameFiber.Yield();
                CreateSpeedZone();
                GameFiber.Yield();
                ClearUnrelatedEntities();
                GameFiber.Yield();
                SpawnVehicles();
                GameFiber.Yield();
                PlaceBarrier();
                GameFiber.Yield();
                SpawnOfficers();
                GameFiber.Yield();
                SpawnNegotiationRobbers();
                GameFiber.Yield();
                SpawnEMS();
                GameFiber.Yield();
                SpawnHostages();
                GameFiber.Yield();
                MakeNearbyPedsFlee();

                SneakyRobbersAI();
                HandleHostages();
                HandleOpenBackRiotVan();
                HandleCalloutAudio();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    Main.Player.CanAttackFriendlies = false;
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RobbersRG, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Respect);
                    Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RelationshipGroup.Cop, Relationship.Respect);
                    Game.SetRelationshipBetweenRelationshipGroups(HostageRG, Main.Player.RelationshipGroup, Relationship.Respect);
                    Game.SetRelationshipBetweenRelationshipGroups(SneakRobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                    Main.Player.IsInvincible = false;
                    NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                    NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.92f);
                    NativeFunction.Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                    CallByHash<uint>(_DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);

                    // When player has just arrived
                    if (!TalkedToCommander && !IsFighting)
                    {
                        if (!Main.Player.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                            {
                                HudHelpers.DisplayNotification(CalloutsText.BankHeistWarning);
                                HudHelpers.DisplayHelp(string.Format(General.PressToTalkWith, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~", CalloutsText.Commander));
                                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                {
                                    TalkedToCommander = true;
                                    DetermineInitialDialogue();
                                }
                            }
                            else
                            {
                                HudHelpers.DisplayHelp(CalloutsText.TalkToCommander);
                            }
                        }
                    }

                    // Is fighting is initialized
                    if (!FightingPrepared)
                    {
                        if (IsFighting)
                        {
                            SpawnAssaultRobbers();

                            if (Main.MersenneTwister.Next(10) is < 3)
                            {
                                SpawnVaultRobbers();
                            }

                            CopFightingAI();
                            RobbersFightingAI();

                            CheckForRobbersOutside();

                            FightingPrepared = true;
                        }
                    }

                    // If player talks to cpt wells during fight
                    if (IsFighting)
                    {
                        if (!Main.Player.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Main.Player.Position, Commander.Position) < 3f)
                            {
                                HudHelpers.DisplayHelp(string.Format(General.PressToTalkWith, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~", CalloutsText.Commander));
                                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                {
                                    Conversations.Talk([(CalloutsText.Commander, BankHeistConversation.StillFighting)]);
                                }
                            }
                        }
                    }

                    // Make everyone fight if player enters bank
                    if (!IsFighting && !IsSurrendering)
                    {
                        foreach (var check in PacificBankInsideChecks)
                        {
                            if (Vector3.Distance(check, Main.Player.Position) < 2.3f)
                            {
                                IsFighting = true;
                            }
                        }
                    }

                    // If all hostages rescued break
                    if (SafeHostagesCount == AliveHostagesCount) break;

                    // If surrendered
                    if (SurrenderComplete) break;
                    if (KeyHelpers.IsKeysDown(Settings.SWATFollowKey, Settings.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }
                    if (IsSWATFollowing)
                    {
                        if (Main.Player.IsShooting)
                        {
                            IsSWATFollowing = false;
                            HudHelpers.DisplayHelp(CalloutsText.SWATIsNotFollowing);
                            Logger.Info("Follow off - shooting", "Bank Heist");
                        }
                    }
                }

                // When surrendered
                if (SurrenderComplete) CopFightingAI();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    // Constants
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, RobbersRG, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, RelationshipGroup.Cop, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RobbersRG, Relationship.Hate);
                    Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Companion);
                    Game.SetRelationshipBetweenRelationshipGroups(Main.Player.RelationshipGroup, RelationshipGroup.Cop, Relationship.Companion);
                    Game.SetRelationshipBetweenRelationshipGroups(HostageRG, Main.Player.RelationshipGroup, Relationship.Companion);
                    Game.SetRelationshipBetweenRelationshipGroups(SneakRobbersRG, Main.Player.RelationshipGroup, Relationship.Hate);
                    Main.Player.IsInvincible = false;
                    NativeFunction.Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                    NativeFunction.Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.93f);
                    NativeFunction.Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                    CallByHash<uint>(_DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);
                    // If all hostages rescued
                    if (SafeHostagesCount == AliveHostagesCount)
                    {
                        GameFiber.Wait(3000);
                        break;
                    }
                    if (KeyHelpers.IsKeysDown(Settings.SWATFollowKey, Settings.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }
                    if (IsSWATFollowing)
                    {
                        if (Main.Player.IsShooting)
                        {
                            IsSWATFollowing = false;
                            HudHelpers.DisplayHelp(CalloutsText.SWATIsNotFollowing);
                            Logger.Info("Follow off - shooting", "Bank Heist");
                        }
                    }

                    if (!Main.Player.IsInAnyVehicle(false))
                    {
                        if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                        {
                            HudHelpers.DisplayHelp(string.Format(CalloutsText.TalkTo, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~", CalloutsText.Commander));
                            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                            {
                                if (!TalkedToWells2nd)
                                {
                                    Conversations.Talk(AfterSurrendered);
                                    TalkedToWells2nd = true;
                                    IsFighting = true;
                                    HudHelpers.DisplayHelp(string.Format(CalloutsText.SWATFollowing, Settings.SWATFollowModifierKey is Keys.None ? $"~{Settings.SWATFollowKey.GetInstructionalId()}~" : $"~{Settings.SWATFollowModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SWATFollowKey.GetInstructionalId()}~"));
                                }
                                else
                                {
                                    Conversations.Talk([(CalloutsText.Commander, BankHeistConversation.StillFighting)]);
                                }
                            }
                        }
                        else
                        {
                            if (!TalkedToWells2nd)
                            {
                                HudHelpers.DisplayHelp(string.Format(CalloutsText.TalkTo, CalloutsText.Commander));
                            }
                        }
                    }
                }

                // The end
                IsSWATFollowing = false;
                DoneFighting = true;
                CurrentAlarmState = AlarmState.None;
                AlarmStateChanged = true;
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    CallByHash<uint>(_DOOR_CONTROL, 4072696575, 256.3116f, 220.6579f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 746855201, 262.1981f, 222.5188f, 106.4296f, false, 0f, 0f, 0f);
                    CallByHash<uint>(_DOOR_CONTROL, 110411286, 258.2022f, 204.1005f, 106.4049f, false, 0f, 0f, 0f);
                    if (!EvaluatedWithWells)
                    {
                        if (!Main.Player.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Main.Player.Position, Commander.Position) < 4f)
                            {
                                HudHelpers.DisplayHelp(string.Format(CalloutsText.TalkTo, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~", CalloutsText.Commander));
                                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
                                {
                                    TalkedToCommander = true;
                                    Conversations.Talk(Final);
                                    GameFiber.Wait(5000);
                                    DetermineResults();
                                    EvaluatedWithWells = true;
                                    GameFiber.Wait(2500);
                                    break;
                                }
                            }
                            else
                            {
                                HudHelpers.DisplayHelp(string.Format(CalloutsText.TalkTo, CalloutsText.Commander));
                            }
                        }
                    }
                }
                Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH JP_WE_ARE_CODE JP_FOUR JP_NO_FURTHER_UNITS_REQUIRED");
                IsCalloutFinished = true;
                End();
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
                End();
            }
        });
    }

    private void LoadModels()
    {
        foreach (var model in PoliceCruiserModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in PoliceTransportModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in RiotModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in AmbulanceModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in FiretruckModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in CopModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        foreach (var model in HostageModels)
        {
            GameFiber.Yield();
            model.Load();
        }
        GameFiber.Yield();
        BarrierModel.Load();
        InvisibleWallModel.Load();
        SwatModel.Load();
        ParamedicModel.Load();
        FirefighterModel.Load();
        CaptainModel.Load();
        WifeModel.Load();
        RobbersModel.Load();
        PhoneModel.Load();
        GameFiber.Yield();
    }

    private void DetermineResults()
    {
        foreach (var robber in AllRobbers)
        {
            if (robber is not null && robber.IsValid() && robber.Exists())
            {
                if (robber.IsDead)
                {
                    DiedRobbersCount++;
                }
            }
        }
        HudHelpers.DisplayNotification(string.Format(CalloutsText.BankHeistReportText, SafeHostagesCount, TotalHostagesCount - AliveHostagesCount, DiedRobbersCount), CalloutsText.BankHeistReportTitle, TotalHostagesCount - AliveHostagesCount is < 3 ? CalloutsText.BankHeistReportSubtitle : "", "mphud", "mp_player_ready");
        if (TotalHostagesCount == AliveHostagesCount)
        {
            var bigMessage = new BigMessageThread(true);
            bigMessage.MessageInstance.ShowColoredShard(CalloutsText.Congratulations, CalloutsText.NoDiedHostage, HudColor.Yellow, HudColor.MenuGrey);
        }
    }

    private void SwitchSWATFollowing()
    {
        IsSWATFollowing = !IsSWATFollowing;
        if (IsSWATFollowing)
        {
            HudHelpers.DisplayHelp(CalloutsText.SWATIsFollowing);
        }
        else
        {
            HudHelpers.DisplayHelp(CalloutsText.SWATIsNotFollowing);
        }
    }

    private void CheckForRobbersOutside()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                if (IsFighting)
                {
                    foreach (var loc in BankDoorPositions)
                    {
                        foreach (var ped in World.GetEntities(loc, 1.6f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
                        {
                            if (ped is not null && ped.IsValid() && ped.Exists() && ped.IsAlive)
                            {
                                if (Vector3.Distance(ped.Position, loc) < 1.5f)
                                {
                                    if (AllRobbers.Contains(ped))
                                    {
                                        if (!OfficersTargetsToShoot.Contains(ped))
                                        {
                                            OfficersTargetsToShoot.Add(ped);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
    }

    private GameFiber CopFightingAIGameFiber;
    private void CopFightingAI()
    {
        CopFightingAIGameFiber = GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (IsFighting)
                    {
                        if (OfficersTargetsToShoot.Count > 0)
                        {
                            if (OfficersTargetsToShoot[0] is not null && OfficersTargetsToShoot[0].IsValid() && OfficersTargetsToShoot[0].Exists())
                            {
                                if (OfficersTargetsToShoot[0].IsAlive)
                                {
                                    foreach (var cop in OfficersTargetsToShoot)
                                    {
                                        cop.Tasks.FightAgainst(OfficersTargetsToShoot[0]);
                                    }
                                }
                                else
                                {
                                    OfficersTargetsToShoot.RemoveAt(0);
                                }
                            }
                            else
                            {
                                OfficersTargetsToShoot.RemoveAt(0);
                            }
                        }
                        else
                        {
                            CopsReturnToLocation();
                        }
                    }
                    if (IsFighting || IsSWATFollowing)
                    {
                        foreach (var cop in AllSWATUnits)
                        {
                            GameFiber.Yield();
                            if (cop is not null && cop.IsValid() && cop.Exists())
                            {
                                if (!IsSWATFollowing)
                                {
                                    NativeFunction.Natives.REGISTER_HATED_TARGETS_AROUND_PED(cop, 60f);
                                    cop.Tasks.FightAgainstClosestHatedTarget(60f);
                                }
                                else
                                {
                                    cop.Tasks.FollowNavigationMeshToPosition(Main.Player.Position, Main.Player.Heading, 1.6f, Math.Abs(Main.Player.Position.Z - cop.Position.Z) > 1f ? 1f : 4f);
                                }
                            }
                        }
                        GameFiber.Sleep(4000);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private Entity entityPlayerAimingAt;
    private void RobbersFightingAI()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (IsFighting)
                    {
                        foreach (var robber in AllRobbers)
                        {
                            GameFiber.Yield();
                            if (robber is not null && robber.IsValid() && robber.Exists())
                            {
                                var distance = Vector3.Distance(robber.Position, PacificBankInsideChecks[0]) < Vector3.Distance(robber.Position, PacificBankInsideChecks[1]) ? Vector3.Distance(robber.Position, PacificBankInsideChecks[0]) : Vector3.Distance(robber.Position, PacificBankInsideChecks[1]);
                                if (distance < 16.5f) distance = 16.5f;
                                else if (distance > 21f) distance = 21f;
                                NativeFunction.Natives.REGISTER_HATED_TARGETS_AROUND_PED(robber, distance);
                                robber.Tasks?.FightAgainstClosestHatedTarget(distance);
                                // Rage.Native.NativeFunction.CallByName<uint>("TASK_GUARD_CURRENT_POSITION", robber, 10.0f, 10.0f, true);
                                try
                                {
                                    unsafe
                                    {
                                        uint entityHandle;
                                        NativeFunction.Natives.x2975C866E6713290(Game.LocalPlayer, new IntPtr(&entityHandle)); // Stores the entity the player is aiming at in the uint provided in the second parameter.
                                        entityPlayerAimingAt = World.GetEntityByHandle<Entity>(entityHandle);
                                    }
                                }
                                catch // (Exception e)
                                {
                                    // Logger.Error(e.ToString());
                                }

                                if (AllRobbers.Contains(entityPlayerAimingAt))
                                {
                                    var pedAimingAt = (Ped)entityPlayerAimingAt;
                                    pedAimingAt.Tasks.FightAgainst(Main.Player);
                                }
                            }
                        }
                        GameFiber.Sleep(3000);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void CopsReturnToLocation()
    {
        for (int i = 0; i < AllStandingOfficers.Count; i++)
        {
            if (AllStandingOfficers[i] is not null && AllStandingOfficers[i].IsValid() && AllStandingOfficers[i].Exists())
            {
                if (AllStandingOfficers[i].IsAlive)
                {
                    if (Vector3.Distance(AllStandingOfficers[i].Position, StandingOfficerPositions[i].pos) > 0.5f)
                    {
                        AllStandingOfficers[i].BlockPermanentEvents = true;
                        AllStandingOfficers[i].Tasks.FollowNavigationMeshToPosition(StandingOfficerPositions[i].pos, StandingOfficerPositions[i].heading, 2f);
                    }
                }
            }
        }
        for (int i = 0; i < AllAimingOfficers.Count; i++)
        {
            if (AllAimingOfficers is not null && AllAimingOfficers[i].IsValid() && AllAimingOfficers[i].Exists())
            {
                if (AllAimingOfficers[i].IsAlive)
                {
                    if (Vector3.Distance(AllAimingOfficers[i].Position, AimingOfficerPositions[i].pos) > 0.5f)
                    {
                        AllAimingOfficers[i].BlockPermanentEvents = true;
                        AllAimingOfficers[i].Tasks.FollowNavigationMeshToPosition(AimingOfficerPositions[i].pos, AimingOfficerPositions[i].heading, 2f);
                    }
                    else
                    {
                        var aimPoint = Vector3.Distance(AllAimingOfficers[i].Position, BankDoorPositions[0]) < Vector3.Distance(AllAimingOfficers[i].Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
                        NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(AllAimingOfficers[i], aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);
                    }
                }
            }
        }
    }

    private void DetermineInitialDialogue()
    {
        Conversations.Talk(IntroConversation);
        var intro = Conversations.DisplayAnswers(IntroSelection);
        // Try to discuss
        if (intro is 0)
        {
            Conversations.Talk(DiscussConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4000);
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                HudHelpers.DisplayHelp(string.Format(CalloutsText.CallBankRobbers, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~"));
                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey)) break;
            }
            Game.HideHelp();
            NegotiationIntro();
        }
        // Fighting
        else if (intro is 1)
        {
            Conversations.Talk(FightConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4500);
            PrepareFighting();
        }
    }

    private void SwitchAlarmQuestion()
    {
        int alarm = Conversations.DisplayAnswers(AlarmSelection);
        if (alarm is 0)
        {
            Conversations.Talk(AlarmOffConversation);
            BankAlarm.Stop();
            CurrentAlarmState = AlarmState.None;
            Conversations.Talk([(CalloutsText.Commander, BankHeistConversation.Alarm5)]);
        }
        else if (alarm is 1)
        {
            CurrentAlarmState = AlarmState.Alarm;
            Conversations.Talk(AlarmOnConversation);
        }
        HudHelpers.DisplayHelp(string.Format(CalloutsText.AlarmSwitchKey, Settings.ToggleBankHeistAlarmSoundModifierKey is Keys.None ? $"~{Settings.ToggleBankHeistAlarmSoundKey.GetInstructionalId()}~" : $"~{Settings.ToggleBankHeistAlarmSoundModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.ToggleBankHeistAlarmSoundKey.GetInstructionalId()}~"));
    }

    private void NegotiationIntro()
    {
        ToggleMobilePhone(Main.Player, true);
        Conversations.PlayPhoneCallingSound(2);
        GameFiber.Wait(5800);
        NegotiationResult = Negotiation.Fight;
        Conversations.Talk(NegotiationIntroConversation);
        var intro = Conversations.DisplayAnswers(NegotiationIntroSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Negotiation1Conversation);
                var ng1 = Conversations.DisplayAnswers(Negotiation1Selection);
                switch (ng1)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation11Conversation);
                        var ng11 = Conversations.DisplayAnswers(Negotiation11Selection);
                        switch (ng11)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation111Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation112Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation113Conversation);
                                NegotiationResult = Negotiation.Surrender;
                                break;
                        }
                        break;
                    case 1:
                        Request();
                        break;
                    case 2:
                        Conversations.Talk(Negotiation13Conversation);
                        var ng13 = Conversations.DisplayAnswers(Negotiation13Selection);
                        switch (ng13)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation131Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation111Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                            case 2:
                                var isSucceed = Main.MersenneTwister.Next(2) is 0;
                                if (isSucceed)
                                {
                                    Conversations.Talk(Negotiation133Conversation1);
                                    NegotiationResult = Negotiation.Surrender;
                                }
                                else
                                {
                                    Conversations.Talk(Negotiation133Conversation2);
                                    NegotiationResult = Negotiation.Fight;
                                }
                                break;
                        }
                        break;
                }
                break;
            case 1:
                Request();
                break;
            case 2:
                Conversations.Talk(Negotiation3Conversation);
                var ng3 = Conversations.DisplayAnswers(Negotiation3Selection);
                switch (ng3)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation31Conversation);
                        NegotiationResult = Negotiation.Fight;
                        break;
                    case 1:
                        Conversations.Talk(Negotiation32Conversation);
                        var ng32 = Conversations.DisplayAnswers(Negotiation32Selection);
                        switch (ng32)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation321Conversation);
                                NegotiationResult = Negotiation.Surrender;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation322Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation323Conversation);
                                NegotiationResult = Negotiation.Fight;
                                break;
                        }
                        break;
                    case 2:
                        Conversations.Talk(Negotiation33Conversation);
                        NegotiationResult = Negotiation.Fight;
                        break;
                    case 3:
                        Conversations.Talk(Negotiation34Conversation);
                        NegotiationResult = Negotiation.Surrender;
                        break;
                }
                break;
        }

        if (!Wife.Exists())
        {
            Conversations.PlayPhoneBusySound(1);
        }
        ToggleMobilePhone(Main.Player, false);

        GameFiber.Wait(2000);
        if (NegotiationResult is Negotiation.Surrender)
        {
            NegotiationRobbersSurrender();
            return;
        }
        else
        {
            PrepareFighting();
        }
    }

    private void PrepareFighting()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            HudHelpers.DisplayHelp(string.Format(CalloutsText.BankHeistMoveIn, Settings.SpeakWithThePersonModifierKey is Keys.None ? $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~" : $"~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~"));
            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
            {
                Conversations.Talk([(Settings.OfficerName, BankHeistConversation.MoveIn)]);
                HudHelpers.DisplayHelp(string.Format(CalloutsText.SWATFollowing, Settings.SWATFollowModifierKey is Keys.None ? $"~{Settings.SWATFollowKey.GetInstructionalId()}~" : $"~{Settings.SWATFollowModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.SWATFollowKey.GetInstructionalId()}~"));
                IsFighting = true;
                break;
            }
        }
    }

    private void Request()
    {
        Conversations.Talk(RequestConversation);
        int intro = Conversations.DisplayAnswers(RequestSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Request1Conversation);
                NegotiationResult = Negotiation.Fight;
                break;
            case 1:
                Conversations.Talk(Request2Conversation);
                int req2 = Conversations.DisplayAnswers(Request2Selection);
                switch (req2)
                {
                    default:
                    case 0:
                        Conversations.Talk(Request21Conversation);
                        NegotiationResult = Negotiation.Fight;
                        break;
                    case 1:
                        var isSucceed = Main.MersenneTwister.Next(2) is 1;
                        if (isSucceed)
                        {
                            Conversations.Talk(Request22Conversation2);
                            GetWife();
                            ToggleMobilePhone(Main.Player, false);
                            ToggleMobilePhone(Wife, true);
                            Conversations.Talk(Request22Conversation3);
                            Conversations.PlayPhoneBusySound(1);
                            ToggleMobilePhone(Wife, false);
                            GameFiber.Wait(1500);

                            Conversations.Talk(Request22Conversation4);
                            Wife.Tasks.FollowNavigationMeshToPosition(WifeCar.GetOffsetPosition(Vector3.RelativeRight * 2f), WifeCar.Heading, 1.9f).WaitForCompletion(15000);
                            Wife.Tasks.EnterVehicle(WifeCar, 5000, 0).WaitForCompletion();
                            GameFiber.StartNew(() =>
                            {
                                WifeDriver.Tasks.CruiseWithVehicle(WifeCar, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds).WaitForCompletion();
                                if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Delete();
                                if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Delete();
                                if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Delete();
                            });
                            NegotiationResult = Negotiation.Surrender;
                        }
                        else
                        {
                            Conversations.Talk(Request22Conversation1);
                            NegotiationResult = Negotiation.Fight;
                        }
                        break;
                    case 2:
                        Conversations.Talk([(Settings.OfficerName, BankHeistConversation.Request231)]);
                        NegotiationResult = Negotiation.Fight;
                        break;
                }
                break;
            case 2:
                Conversations.Talk(Request3Conversation);
                NegotiationResult = Negotiation.Fight;
                break;
        }
    }

    private void NegotiationRobbersSurrender()
    {
        SurrenderComplete = false;
        IsSurrendering = true;
        GameFiber.StartNew(() =>
        {
            try
            {
                HudHelpers.DisplayNotification($"~b~{CalloutsText.Commander}~s~: {BankHeistConversation.SeemSurrender}");
                GameFiber.Wait(5000);
                HudHelpers.DisplayHelp(CalloutsText.SurrenderHelp, 80000);
                bool AllRobbersAtLocation = false;
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();
                    AllRobbers[i].Tasks.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 1f, AnimationFlags.UpperBodyOnly | AnimationFlags.StayInEndFrame | AnimationFlags.SecondaryTask);
                    AllRobbers[i].Tasks.FollowNavigationMeshToPosition(RobbersSurrenderingPositions[i].pos, RobbersSurrenderingPositions[i].heading, 1.45f);
                    NativeFunction.Natives.SET_PED_CAN_RAGDOLL(AllRobbers[i], false);
                }
                int count = 0;
                while (!AllRobbersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < AllRobbers.Count; i++)
                        {
                            AllRobbers[i].Position = RobbersSurrenderingPositions[i].pos;
                            AllRobbers[i].Heading = RobbersSurrenderingPositions[i].heading;
                        }
                        break;
                    }
                    for (int i = 0; i < AllRobbers.Count; i++)
                    {
                        GameFiber.Yield();
                        if (Vector3.Distance(AllRobbers[i].Position, RobbersSurrenderingPositions[i].pos) < 0.8f)
                        {
                            AllRobbersAtLocation = true;
                        }
                        else
                        {
                            AllRobbersAtLocation = false;
                            break;
                        }
                    }
                    for (int i = 0; i < AllSWATUnits.Count; i++)
                    {
                        GameFiber.Wait(100);
                        var robber = AllRobbers[Main.MersenneTwister.Next(AllRobbers.Count)];
                        NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(AllSWATUnits[i], robber.Position.X, robber.Position.Y, robber.Position.Z, -1, false, false);
                    }
                }
                GameFiber.Wait(1000);
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();

                    AllRobbers[i].Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                    NativeFunction.Natives.SET_PED_DROPS_WEAPON(AllRobbers[i]);
                    if (AllOfficers.Count >= i + 1)
                    {
                        OfficersArresting.Add(AllOfficers[i]);
                        AllOfficers[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[i].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[i].Heading, 1.55f);
                        NativeFunction.Natives.SET_PED_CAN_RAGDOLL(AllOfficers[i], false);
                    }
                }
                GameFiber.Wait(1000);

                bool AllArrestingOfficersAtLocation = false;
                count = 0;
                while (!AllArrestingOfficersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < OfficersArresting.Count; i++)
                        {
                            OfficersArresting[i].Position = AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f);
                            OfficersArresting[i].Heading = AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < OfficersArresting.Count; i++)
                    {
                        if (Vector3.Distance(OfficersArresting[i].Position, AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f)) < 0.8f)
                        {
                            AllArrestingOfficersAtLocation = true;
                        }
                        else
                        {
                            OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].Heading, 1.55f).WaitForCompletion(500);
                            AllArrestingOfficersAtLocation = false;
                            break;
                        }
                    }
                }
                foreach (var swat in AllSWATUnits)
                {
                    swat.Tasks.Clear();
                }
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    AllRobbers[i].Tasks.PlayAnimation("mp_arresting", "idle", 8f, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask | AnimationFlags.Loop);
                    AllRobbers[i].Tasks.FollowNavigationMeshToPosition(AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), AllPoliceVehicles[i].Heading, 1.58f);
                    OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), AllPoliceVehicles[i].Heading, 1.55f);
                }
                GameFiber.Wait(5000);
                SurrenderComplete = true;
                GameFiber.Wait(12000);
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    AllRobbers[i].BlockPermanentEvents = true;
                    AllRobbers[i].Tasks.EnterVehicle(AllPoliceVehicles[i], 11000, 1);
                    OfficersArresting[i].BlockPermanentEvents = true;
                    OfficersArresting[i].Tasks.EnterVehicle(AllPoliceVehicles[i], 11000, -1);
                }
                GameFiber.Wait(11100);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        });
    }

    private void SpawnVehicles()
    {
        foreach (var (pos, heading) in PoliceCruiserPositions)
        {
            var vehicle = new Vehicle(PoliceCruiserModels[Main.MersenneTwister.Next(PoliceCruiserModels.Length)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in PoliceTransportPositions)
        {
            var vehicle = new Vehicle(PoliceTransportModels[Main.MersenneTwister.Next(PoliceTransportModels.Length)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in RiotPositions)
        {
            var vehicle = new Vehicle(RiotModels[Main.MersenneTwister.Next(RiotModels.Length)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllPoliceVehicles.Add(vehicle);
            AllRiot.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in AmbulancePositions)
        {
            var vehicle = new Vehicle(AmbulanceModels[Main.MersenneTwister.Next(AmbulanceModels.Length)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllAmbulance.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
        foreach (var (pos, heading) in FiretruckPositions)
        {
            var vehicle = new Vehicle(FiretruckModels[Main.MersenneTwister.Next(FiretruckModels.Length)], pos, heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            AllFiretruck.Add(vehicle);
            CalloutEntities.Add(vehicle);
        }
    }

    private void PlaceBarrier()
    {
        foreach (var (pos, heading) in BarrierPositions)
        {
            var barrier = new RObject(BarrierModel, pos, heading)
            {
                IsPositionFrozen = true,
                IsPersistent = true
            };
            var invisibleWall = new RObject(InvisibleWallModel, barrier.Position, heading)
            {
                IsVisible = false,
                IsPersistent = true
            };
            var barrierPed = new Ped(invisibleWall.Position)
            {
                IsVisible = false,
                IsPositionFrozen = true,
                BlockPermanentEvents = true,
                IsPersistent = true
            };
            AllBarriers.Add(barrier);
            AllInvisibleWalls.Add(invisibleWall);
            AllBarrierPeds.Add(barrierPed);
        }
    }

    private void SpawnOfficers()
    {
        foreach (var (pos, heading) in LeftSittingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 250,
                Armor = 400,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);

            AllSWATUnits.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in RightLookingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 250,
                Armor = 400,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT_LOOKING, 1f, AnimationFlags.StayInEndFrame);
            AllSWATUnits.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in RightSittingSWATPositions)
        {
            var swat = new Ped(SwatModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(swat);
            Functions.SetCopAsBusy(swat, true);
            swat.Inventory.GiveNewWeapon(SWATWeapons[Main.MersenneTwister.Next(SWATWeapons.Length)], 10000, true);
            swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 1f, AnimationFlags.StayInEndFrame);
            AllSWATUnits.Add(swat);
            CalloutEntities.Add(swat);
        }
        foreach (var (pos, heading) in AimingOfficerPositions)
        {
            var officer = new Ped(CopModels[Main.MersenneTwister.Next(CopModels.Length)], pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(officer);
            Functions.SetCopAsBusy(officer, true);
            officer.Inventory.GiveNewWeapon(OfficerWeapons[Main.MersenneTwister.Next(OfficerWeapons.Length)], 10000, true);
            var aimPoint = Vector3.Distance(officer.Position, BankDoorPositions[0]) < Vector3.Distance(officer.Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
            NativeFunction.Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);
            AllOfficers.Add(officer);
            AllAimingOfficers.Add(officer);
            CalloutEntities.Add(officer);
        }
        foreach (var (pos, heading) in StandingOfficerPositions)
        {
            var officer = new Ped(CopModels[Main.MersenneTwister.Next(CopModels.Length)], pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = RelationshipGroup.Cop,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                Health = 200,
                Armor = 200,
            };
            Functions.SetPedAsCop(officer);
            Functions.SetCopAsBusy(officer, true);
            officer.Inventory.GiveNewWeapon(OfficerWeapons[Main.MersenneTwister.Next(OfficerWeapons.Length)], 10000, true);
            AllOfficers.Add(officer);
            AllStandingOfficers.Add(officer);
            CalloutEntities.Add(officer);
        }
        Commander = new Ped(CaptainModel, CaptainPosition.pos, CaptainPosition.heading)
        {
            BlockPermanentEvents = true,
            IsPersistent = true,
            IsInvincible = true,
            RelationshipGroup = RelationshipGroup.Cop
        };
        Functions.SetPedCantBeArrestedByPlayer(Commander, true);

        CommanderBlip = Commander.AttachBlip();
        CommanderBlip.Color = Color.Green;
        CalloutEntities.Add(Commander);
    }

    private void SpawnEMS()
    {
        foreach (var (pos, heading) in ParamedicPositions)
        {
            var paramedic = new Ped(ParamedicModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                Health = 200
            };
            AllEMS.Add(paramedic);
            CalloutEntities.Add(paramedic);
        }
        foreach (var (pos, heading) in FirefighterPositions)
        {
            var firefighter = new Ped(FirefighterModel, pos, heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                Armor = 200
            };
            AllEMS.Add(firefighter);
            CalloutEntities.Add(firefighter);
        }
    }

    private void SpawnHostages()
    {
        for (int i = 0; i < 8; i++)
        {
            var index = Main.MersenneTwister.Next(HostagePositions.Count);
            var hostage = new Ped(new Model(HostageModels[Main.MersenneTwister.Next(HostageModels.Length)]), HostagePositions[index], Main.MersenneTwister.Next(0, 360))
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                RelationshipGroup = HostageRG,
                CanAttackFriendlies = false,
                Armor = 0,
                Health = 100,
            };
            NativeFunction.Natives.SET_PED_CAN_RAGDOLL(hostage, false);
            AllHostages.Add(hostage);
            SpawnedHostages.Add(hostage);
            CalloutEntities.Add(hostage);
            hostage.Tasks.PlayAnimation(HOSTAGE_ANIMATION_DICTIONARY, HOSTAGE_ANIMATION_KNEELING, 1f, AnimationFlags.Loop);
            GameFiber.Yield();
            HostagePositions.RemoveAt(index);
            AliveHostagesCount++;
            TotalHostagesCount++;
        }
    }

    private void SpawnAssaultRobbers()
    {
        for (int i = 0; i < NormalRobbersPositions.Length; i++)
        {
            var ped = new Ped(RobbersModel, NormalRobbersPositions[i].pos, NormalRobbersPositions[i].heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                Armor = Main.MersenneTwister.Next(500, 750),
                Health = Main.MersenneTwister.Next(180, 250),
                RelationshipGroup = RobbersRG,
            };
            if (Main.MersenneTwister.Next(2) is 0)
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 2, 1, 0);
            }
            else
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 1, 1, 0);
            }
            Functions.SetPedCantBeArrestedByPlayer(ped, true);
            ped.Inventory.GiveNewWeapon(RobbersWeapons[Main.MersenneTwister.Next(RobbersWeapons.Length)], 10000, true);
            ped.Inventory.GiveNewWeapon(Grenades[Main.MersenneTwister.Next(Grenades.Length)], 4, false);
            NativeFunction.Natives.SetPedCombatAbility(ped, 3);
            AllRobbers.Add(ped);
            CalloutEntities.Add(ped);
        }
    }

    private void SpawnVaultRobbers()
    {
        for (int i = 0; i < RobbersInVaultPositions.Length; i++)
        {
            var ped = new Ped(RobbersModel, RobbersInVaultPositions[i].pos, RobbersInVaultPositions[i].heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                Armor = Main.MersenneTwister.Next(500, 750),
                Health = Main.MersenneTwister.Next(180, 250),
                RelationshipGroup = RobbersRG,
            };
            if (Main.MersenneTwister.Next(2) is 0)
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 2, 1, 0);
            }
            else
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 1, 1, 0);
            }
            Functions.SetPedCantBeArrestedByPlayer(ped, true);
            ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_ASSAULTSMG"), 10000, true);
            NativeFunction.Natives.SetPedCombatAbility(ped, 3);
            AllVaultRobbers.Add(ped);
            CalloutEntities.Add(ped);
        }
        HandleVaultRobbers();
    }

    private void SpawnNegotiationRobbers()
    {
        for (int i = 0; i < RobbersNegotiationPositions.Length; i++)
        {
            var ped = new Ped(RobbersModel, RobbersNegotiationPositions[i].pos, RobbersNegotiationPositions[i].heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                Armor = Main.MersenneTwister.Next(300, 500),
                Health = Main.MersenneTwister.Next(200, 230),
                RelationshipGroup = RobbersRG,
            };
            if (Main.MersenneTwister.Next(2) is 0)
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 2, 1, 0);
            }
            else
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 1, 1, 0);
            }
            Functions.SetPedCantBeArrestedByPlayer(ped, true);

            ped.Inventory.GiveNewWeapon(RobbersWeapons[Main.MersenneTwister.Next(RobbersWeapons.Length)], 10000, true);
            NativeFunction.Natives.SetPedCombatAbility(ped, 3);
            AllRobbers.Add(ped);
            CalloutEntities.Add(ped);
        }
    }

    private void SpawnSneakyRobbers()
    {
        for (int i = 0; i < RobbersSneakPosition.Length; i++)
        {
            if (Main.MersenneTwister.Next(5) is >= 2)
            {
                var ped = new Ped(RobbersModel, RobbersSneakPosition[i].pos, RobbersSneakPosition[i].heading)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    CanAttackFriendlies = false,
                    Armor = Main.MersenneTwister.Next(300, 500),
                    Health = Main.MersenneTwister.Next(200, 230),
                    RelationshipGroup = SneakRobbersRG
                };
                if (Main.MersenneTwister.Next(2) is 0)
                {
                    NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 2, 1, 0);
                }
                else
                {
                    NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 9, 1, 1, 0);
                }
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.Inventory.GiveNewWeapon(RobbersSneakyWeapons[Main.MersenneTwister.Next(RobbersSneakyWeapons.Length)], 10000, true);
                ped.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, RobbersSneakPosition[i].isRight ? SWAT_ANIMATION_RIGHT : SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);
                AllSneakRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
            else
            {
                AllSneakRobbers.Add(null);
            }
        }
    }

    private void HandleVaultRobbers()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                try
                {
                    if (Vector3.Distance(Main.Player.Position, OutsideBankVault) < 4f)
                    {
                        GameFiber.Wait(2000);

                        AllVaultRobbers[2].Tasks.FollowNavigationMeshToPosition(OutsideBankVault, AllVaultRobbers[2].Heading, 2f).WaitForCompletion(500);
                        World.SpawnExplosion(new Vector3(252.2609f, 225.3824f, 101.6835f), 2, 0.2f, true, false, 0.6f);
                        CurrentAlarmState = AlarmState.Alarm;
                        AlarmStateChanged = true;
                        GameFiber.Wait(900);
                        foreach (var robber in AllVaultRobbers)
                        {
                            robber.Tasks.FightAgainstClosestHatedTarget(23f);

                        }
                        GameFiber.Wait(3000);
                        foreach (Ped robber in AllVaultRobbers)
                        {
                            AllRobbers.Add(robber);
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void HandleCalloutAudio()
    {
        GameFiber.StartNew(() =>
        {
            CurrentAlarmState = AlarmState.None;
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (!HandlingRespawn)
                    {
                        if (IsAlarmPlaying)
                        {
                            if (Vector3.Distance(Main.Player.Position, CalloutPosition) > 70f)
                            {
                                IsAlarmPlaying = false;
                                CurrentAlarmState = AlarmState.None;
                                BankBlip.IsRouteEnabled = true;
                                AlarmStateChanged = true;
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(Main.Player.Position, CalloutPosition) < 55f)
                            {
                                IsAlarmPlaying = true;
                                CurrentAlarmState = AlarmState.Alarm;
                                BankBlip.IsRouteEnabled = false;
                                AlarmStateChanged = true;
                            }
                        }

                        if (KeyHelpers.IsKeysDown(Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey))
                        {
                            CurrentAlarmState = CurrentAlarmState is AlarmState.None ? AlarmState.Alarm : AlarmState.None;
                            AlarmStateChanged = true;
                        }

                        if (AlarmStateChanged)
                        {
                            switch (CurrentAlarmState)
                            {
                                case AlarmState.Alarm:
                                    BankAlarm.PlayLooping();
                                    break;
                                default:
                                case AlarmState.None:
                                    BankAlarm.Stop();
                                    break;
                            }
                            AlarmStateChanged = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        }, "");
    }

    private void MakeNearbyPedsFlee()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();

                foreach (var ped in World.GetEntities(CalloutPosition, 120f, GetEntitiesFlags.ConsiderAllPeds | GetEntitiesFlags.ExcludePlayerPed | GetEntitiesFlags.ExcludePoliceOfficers).Cast<Ped>())
                {
                    GameFiber.Yield();
                    if (CalloutEntities.Contains(ped)) continue;
                    if (ped is not null && ped.IsValid() && ped.Exists())
                    {
                        if (ped != Main.Player)
                        {
                            if (!ped.CreatedByTheCallingPlugin)
                            {
                                if (Vector3.Distance(ped.Position, CalloutPosition) < 74f)
                                {
                                    if (ped.IsInAnyVehicle(false))
                                    {
                                        if (ped.CurrentVehicle is not null)
                                        {
                                            ped.Tasks.PerformDrivingManeuver(VehicleManeuver.Wait);
                                        }
                                    }
                                    else
                                    {
                                        CallByName<uint>("TASK_SMART_FLEE_COORD", ped, CalloutPosition.X, CalloutPosition.Y, CalloutPosition.Z, 75f, 6000, true, true);
                                    }
                                }
                                if (Vector3.Distance(ped.Position, CalloutPosition) < 65f)
                                {
                                    if (ped.IsInAnyVehicle(false))
                                    {
                                        if (ped.CurrentVehicle.Exists())
                                        {
                                            ped.CurrentVehicle.Delete();
                                        }
                                    }
                                    if (ped.Exists())
                                    {
                                        ped.Delete();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
    }

    private void SneakyRobbersAI()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    foreach (var robber in AllSneakRobbers)
                    {
                        if (robber is not null && robber.IsValid() && robber.Exists())
                        {
                            if (robber.IsAlive)
                            {
                                if (!FightingSneakRobbers.Contains(robber))
                                {
                                    if (Vector3.Distance(robber.Position, RobbersSneakPosition[AllSneakRobbers.IndexOf(robber)].pos) > 0.7f)
                                    {
                                        robber.Tasks.FollowNavigationMeshToPosition(RobbersSneakPosition[AllSneakRobbers.IndexOf(robber)].pos, RobbersSneakPosition[AllSneakRobbers.IndexOf(robber)].heading, 2f).WaitForCompletion(300);
                                    }
                                    else
                                    {
                                        if (RobbersSneakPosition[AllSneakRobbers.IndexOf(robber)].isRight)
                                        {
                                            if (!NativeFunction.Natives.IS_ENTITY_PLAYING_ANIM<bool>(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 3))
                                            {
                                                robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                            }
                                        }
                                        else
                                        {
                                            if (!NativeFunction.Natives.IS_ENTITY_PLAYING_ANIM<bool>(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 3))
                                            {
                                                robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                            }
                                        }
                                    }
                                    var nearestPeds = robber.GetNearbyPeds(3);
                                    if (nearestPeds.Length > 0)
                                    {
                                        foreach (var nearestPed in nearestPeds)
                                        {
                                            if (nearestPed is not null && nearestPed.IsValid() && nearestPed.Exists())
                                            {
                                                if (nearestPed.IsAlive)
                                                {
                                                    if (nearestPed.RelationshipGroup == Main.Player.RelationshipGroup || nearestPed.RelationshipGroup == RelationshipGroup.Cop)
                                                    {
                                                        if (Vector3.Distance(nearestPed.Position, robber.Position) < 3.9f)
                                                        {
                                                            if (Math.Abs(nearestPed.Position.Z - robber.Position.Z) < 0.9f)
                                                            {
                                                                SneakyRobberFight(robber, nearestPed);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private Entity entityPlayerAimingAtSneakyRobber = null;
    private void SneakyRobberFight(Ped sneak, Ped nearestPed)
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                FightingSneakRobbers.Add(sneak);
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!nearestPed.Exists()) break;
                    if (!sneak.Exists() || !sneak.IsAlive) break;
                    if (!nearestPed.IsAlive) break;
                    if (Vector3.Distance(nearestPed.Position, sneak.Position) > 5.1f) break;
                    else if (Vector3.Distance(nearestPed.Position, sneak.Position) < 1.70f) break;
                    try
                    {
                        unsafe
                        {
                            uint entityHandle;
                            NativeFunction.Natives.x2975C866E6713290(Game.LocalPlayer, new IntPtr(&entityHandle)); // Stores the entity the player is aiming at in the uint provided in the second parameter.
                            entityPlayerAimingAtSneakyRobber = World.GetEntityByHandle<Rage.Entity>(entityHandle);
                        }
                    }
                    catch // (Exception e)
                    {
                        // Logger.Error(e.ToString());
                    }
                    if (entityPlayerAimingAtSneakyRobber == sneak) break;
                    if (RescuingHostage) break;
                }
                if (sneak is not null && sneak.IsValid() && sneak.Exists())
                {
                    sneak.Tasks.FightAgainstClosestHatedTarget(15f);
                    sneak.RelationshipGroup = RobbersRG;
                }
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!sneak.Exists()) break;
                    if (!nearestPed.Exists()) break;
                    NativeFunction.Natives.STOP_CURRENT_PLAYING_AMBIENT_SPEECH(sneak);
                    if (nearestPed.IsDead)
                    {
                        foreach (var hostage in SpawnedHostages)
                        {
                            if (Math.Abs(hostage.Position.Z - sneak.Position.Z) < 0.6f)
                            {
                                if (Vector3.Distance(hostage.Position, sneak.Position) < 14f)
                                {
                                    int waitCount = 0;
                                    while (hostage.IsAlive)
                                    {
                                        GameFiber.Yield();
                                        waitCount++;
                                        if (waitCount > 450)
                                        {
                                            hostage.Kill();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    if (sneak.IsDead) break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            finally
            {
                FightingSneakRobbers.Remove(sneak);
            }
        });
    }

    private void HandleHostages()
    {
        Game.FrameRender += TimerBarsProcess;
        GameFiber.StartNew(() =>
        {
            int waitCountForceAttack = 0;
            int enterAmbulanceCount = 0;
            int deleteSafeHostageCount = 0;
            int subtitleCount = 0;
            Ped closeHostage = null;
            while (IsCalloutRunning)
            {
                try
                {
                    waitCountForceAttack++;
                    enterAmbulanceCount++;

                    GameFiber.Yield();
                    if (waitCountForceAttack > 250)
                    {
                        waitCountForceAttack = 0;
                    }
                    if (enterAmbulanceCount > 101)
                    {
                        enterAmbulanceCount = 101;
                    }
                    foreach (Ped hostage in SpawnedHostages)
                    {
                        GameFiber.Yield();
                        if (hostage.Exists())
                        {
                            if (hostage.IsAlive)
                            {
                                if (Functions.IsPedGettingArrested(hostage) || Functions.IsPedArrested(hostage))
                                {
                                    SpawnedHostages[SpawnedHostages.IndexOf(hostage)] = hostage.ClonePed();
                                }
                                hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                                if (!Main.Player.IsShooting)
                                {
                                    if (Vector3.Distance(hostage.Position, Main.Player.Position) < 1.45f)
                                    {
                                        if (KeyHelpers.IsKeysDownRightNow(Settings.HostageRescueKey, Settings.HostageRescueModifierKey))
                                        {
                                            var direction = hostage.Position - Main.Player.Position;
                                            direction.Normalize();
                                            RescuingHostage = true;
                                            Main.Player.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(direction)).WaitForCompletion(1200);
                                            hostage.RelationshipGroup = "COP";
                                            Conversations.Talk([(Settings.OfficerName, BankHeistConversation.RescueHostage)]);
                                            Main.Player.Tasks.PlayAnimation("random@rescue_hostage", "bystander_helping_girl_loop", 1.5f, AnimationFlags.None).WaitForCompletion(3000);

                                            if (hostage.IsAlive)
                                            {
                                                hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_get_up", 0.9f, AnimationFlags.None).WaitForCompletion(6000);
                                                Main.Player.Tasks.ClearImmediately();
                                                if (hostage.IsAlive)
                                                {
                                                    hostage.Tasks.FollowNavigationMeshToPosition(HostageSafePosition.pos, HostageSafePosition.heading, 1.55f);
                                                    RescuedHostages.Add(hostage);
                                                    SpawnedHostages.Remove(hostage);
                                                }
                                                else
                                                {
                                                    Main.Player.Tasks.ClearImmediately();
                                                }
                                            }
                                            else
                                            {
                                                Main.Player.Tasks.ClearImmediately();
                                            }
                                            RescuingHostage = false;
                                        }
                                        else
                                        {
                                            subtitleCount++;
                                            closeHostage = hostage;
                                            if (subtitleCount > 10)
                                            {
                                                HudHelpers.DisplayHelp(string.Format(CalloutsText.BankHeistReleaseHostage, Settings.HostageRescueModifierKey is Keys.None ? $"~{Settings.HostageRescueKey.GetInstructionalId()}~" : $"~{Settings.HostageRescueModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.HostageRescueKey.GetInstructionalId()}~"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (hostage == closeHostage)
                                        {
                                            subtitleCount = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SpawnedHostages.Remove(hostage);
                                AliveHostagesCount--;
                            }
                        }
                        else
                        {
                            SpawnedHostages.Remove(hostage);
                            AliveHostagesCount--;
                        }
                    }
                    foreach (var rescued in RescuedHostages)
                    {
                        if (rescued.Exists() && rescued.IsAlive)
                        {
                            if (SpawnedHostages.Contains(rescued))
                            {
                                SpawnedHostages.Remove(rescued);
                            }
                            if (Vector3.Distance(rescued.Position, HostageSafePosition.pos) < 3f)
                            {
                                SafeHostages.Add(rescued);
                                SafeHostagesCount++;
                            }
                            if (Functions.IsPedGettingArrested(rescued) || Functions.IsPedArrested(rescued))
                            {
                                RescuedHostages[RescuedHostages.IndexOf(rescued)] = rescued.ClonePed();
                            }
                            rescued.Tasks.FollowNavigationMeshToPosition(HostageSafePosition.pos, HostageSafePosition.heading, 1.55f).WaitForCompletion(200);

                            if (waitCountForceAttack > 150)
                            {
                                var nearest = rescued.GetNearbyPeds(2)[0];
                                if (nearest == Main.Player)
                                {
                                    nearest = rescued.GetNearbyPeds(2)[1];
                                }
                                if (AllRobbers.Contains(nearest))
                                {
                                    nearest.Tasks.FightAgainst(rescued);
                                    waitCountForceAttack = 0;
                                }
                            }
                        }
                        else
                        {
                            RescuedHostages.Remove(rescued);
                            AliveHostagesCount--;
                        }
                    }
                    foreach (var safe in SafeHostages)
                    {
                        if (safe.Exists())
                        {
                            if (RescuedHostages.Contains(safe))
                            {
                                RescuedHostages.Remove(safe);
                            }
                            safe.IsInvincible = true;
                            if (!safe.IsInAnyVehicle(true))
                            {
                                if (enterAmbulanceCount > 100)
                                {
                                    if (AllAmbulance[1].IsSeatFree(2))
                                    {
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 2);
                                    }
                                    else if (AllAmbulance[1].IsSeatFree(1))
                                    {
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 1);
                                    }
                                    else
                                    {
                                        AllAmbulance[1].GetPedOnSeat(2).Delete();
                                        safe.Tasks.EnterVehicle(AllAmbulance[1], 2);
                                    }
                                    enterAmbulanceCount = 0;
                                }
                            }
                            else
                            {
                                deleteSafeHostageCount++;
                                if (deleteSafeHostageCount > 50)
                                {
                                    if (Vector3.Distance(Main.Player.Position, safe.Position) > 22f)
                                    {
                                        if (safe.IsInAnyVehicle(false))
                                        {
                                            safe.Delete();
                                            deleteSafeHostageCount = 0;
                                            NativeFunction.Natives.SET_VEHICLE_DOORS_SHUT(AllAmbulance[1], true);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            SafeHostages.Remove(safe);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        });
    }

    private void TimerBarsProcess(object sender, GraphicsEventArgs e)
    {
        if (IsFighting || (SurrenderComplete && TalkedToWells2nd))
        {
            if (TBPool is not null)
            {
                RescuedHostagesTB.Text = $"{SafeHostagesCount}/{TotalHostagesCount}";
                DiedHostagesTB.Text = $"{TotalHostagesCount - AliveHostagesCount}";
                DiedHostagesTB.Highlight = TotalHostagesCount - AliveHostagesCount is > 0 ? HudColor.Red.GetColor() : HudColor.Green.GetColor();
                TBPool.Draw();
            }
        }
        if (!IsCalloutRunning || DoneFighting)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
    }

    private void HandleOpenBackRiotVan()
    {
        GameFiber.StartNew(() =>
        {
            int CoolDown = 0;
            while (IsCalloutRunning)
            {
                try
                {
                    GameFiber.Yield();
                    if (CoolDown > 0) CoolDown--;
                    if (HandlingRespawn) CoolDown = 0;

                    if (AllRiot[0] is not null && AllRiot[0].IsValid() && AllRiot[0].Exists())
                    {
                        if (Vector3.Distance(AllRiot[0].GetOffsetPosition(Vector3.RelativeBack * 4f), Main.Player.Position) < 2f)
                        {
                            if (KeyHelpers.IsKeysDownRightNow(Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey))
                            {
                                if (CoolDown > 0)
                                {
                                    HudHelpers.DisplayNotification(CalloutsText.GearRunOut);
                                }
                                else
                                {
                                    CoolDown = 10000;
                                    Main.Player.Tasks.EnterVehicle(AllRiot[0], 1).WaitForCompletion();
                                    Main.Player.Armor = 100;
                                    Main.Player.Health = Main.Player.MaxHealth;
                                    Main.Player.Inventory.GiveNewWeapon(WeaponHash.CarbineRifle, 150, true);
                                    Main.Player.Inventory.GiveNewWeapon(Grenades[1], 3, false);
                                    NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "PURCHASE", "HUD_LIQUOR_STORE_SOUNDSET", 1);
                                    Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
                                }
                            }
                            else
                            {
                                if (CoolDown is 0)
                                {
                                    HudHelpers.DisplayHelp(string.Format(CalloutsText.EnterRiot, Settings.EnterRiotVanModifierKey is Keys.None ? $"~{Settings.EnterRiotVanKey.GetInstructionalId()}~" : $"~{Settings.EnterRiotVanModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.EnterRiotVanKey.GetInstructionalId()}~"));
                                }
                            }
                        }
                    }

                    if (AllRiot[1] is not null && AllRiot[1].IsValid() && AllRiot[1].Exists())
                    {
                        if (Vector3.Distance(AllRiot[1].GetOffsetPosition(Vector3.RelativeBack * 4f), Main.Player.Position) < 2f)
                        {
                            if (KeyHelpers.IsKeysDownRightNow(Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey))
                            {
                                if (CoolDown > 0)
                                {
                                    HudHelpers.DisplayNotification(CalloutsText.GearRunOut);
                                }
                                else
                                {
                                    CoolDown = 50000;
                                    Main.Player.Tasks.EnterVehicle(AllRiot[1], 1).WaitForCompletion();
                                    Main.Player.Armor = 100;
                                    Main.Player.Health = Main.Player.MaxHealth;
                                    Main.Player.Inventory.GiveNewWeapon(WeaponHash.CarbineRifle, 150, true);
                                    Main.Player.Inventory.GiveNewWeapon(Grenades[1], 3, false);
                                    NativeFunction.Natives.PLAY_SOUND_FRONTEND(-1, "PURCHASE", "HUD_LIQUOR_STORE_SOUNDSET", 1);
                                    Main.Player.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
                                }
                            }
                            else
                            {
                                if (CoolDown is 0)
                                {
                                    HudHelpers.DisplayHelp(string.Format(CalloutsText.EnterRiot, Settings.EnterRiotVanModifierKey is Keys.None ? $"~{Settings.EnterRiotVanKey.GetInstructionalId()}~" : $"~{Settings.EnterRiotVanModifierKey.GetInstructionalId()}~ ~+~ ~{Settings.EnterRiotVanKey.GetInstructionalId()}~"));
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.ToString());
                }
            }
        });
    }

    private void ToggleMobilePhone(Ped ped, bool toggle)
    {
        if (toggle)
        {
            NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(ped, false);
            ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_UNARMED"), -1, true);
            MobilePhone = new(PhoneModel, new(0, 0, 0));
            int boneIndex = NativeFunction.Natives.GET_PED_BONE_INDEX<int>(ped, (int)PedBoneId.RightPhHand);
            NativeFunction.Natives.ATTACH_ENTITY_TO_ENTITY(MobilePhone, ped, boneIndex, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, 1);
            ped.Tasks.PlayAnimation("cellphone@", "cellphone_call_listen_base", 1.3f, AnimationFlags.Loop | AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
        }
        else
        {
            NativeFunction.Natives.SET_PED_CAN_SWITCH_WEAPON(ped, true);
            ped.Tasks.Clear();
            if (GameFiber.CanSleepNow)
            {
                GameFiber.Wait(800);
            }
            if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
        }
    }

    private void GetWife()
    {
        Main.Player.IsPositionFrozen = true;
        WifeCar = new(PoliceCruiserModels[Main.MersenneTwister.Next(PoliceCruiserModels.Length)], WifePosition.pos, Wife.Heading)
        {
            IsPersistent = true,
            IsSirenOn = true
        };
        WifeDriver = WifeCar.CreateRandomDriver();
        WifeDriver.IsPersistent = true;
        WifeDriver.BlockPermanentEvents = true;
        Wife = new Ped(WifeModel, WifePosition.pos, Wife.Heading)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
        };
        Wife.WarpIntoVehicle(WifeCar, 1);
        CalloutEntities.Add(Wife);
        CalloutEntities.Add(WifeDriver);
        CalloutEntities.Add(WifeCar);

        WifeDriver.Tasks.DriveToPosition(WifeCarDestination, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
        while (true)
        {
            GameFiber.Yield();
            if (Vector3.Distance(WifeCar.Position, WifeCarDestination) < 6f) break;
        }
        Wife.Tasks.LeaveVehicle(LeaveVehicleFlags.None);
        Wife.Tasks.FollowNavigationMeshToPosition(Main.Player.GetOffsetPosition(Vector3.RelativeRight * 1.5f), Main.Player.Heading, 1.9f).WaitForCompletion(60000);
        Main.Player.IsPositionFrozen = false;
    }

    private void CreateSpeedZone()
    {
        GameFiber.StartNew(() =>
        {
            while (IsCalloutRunning)
            {
                GameFiber.Yield();

                foreach (var vehicle in World.GetEntities(CalloutPosition, 75f, GetEntitiesFlags.ConsiderGroundVehicles | GetEntitiesFlags.ExcludePoliceCars | GetEntitiesFlags.ExcludeFiretrucks | GetEntitiesFlags.ExcludeAmbulances).Cast<Vehicle>())
                {
                    GameFiber.Yield();
                    if (CalloutEntities.Contains(vehicle)) continue;
                    if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
                    {
                        if (vehicle != Main.Player.CurrentVehicle)
                        {
                            if (!vehicle.CreatedByTheCallingPlugin)
                            {
                                if (!CalloutEntities.Contains(vehicle))
                                {
                                    if (vehicle.Velocity.Length() > 0f)
                                    {
                                        var velocity = vehicle.Velocity;
                                        velocity.Normalize();
                                        velocity *= 0f;
                                        vehicle.Velocity = velocity;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
    }

    private void ClearUnrelatedEntities()
    {
        foreach (var ped in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
        {
            GameFiber.Yield();
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped != Main.Player)
                {
                    if (!ped.CreatedByTheCallingPlugin)
                    {
                        if (!CalloutEntities.Contains(ped))
                        {
                            if (Vector3.Distance(ped.Position, CalloutPosition) < 50f)
                            {
                                ped.Delete();
                            }
                        }
                    }
                }
            }
        }
        foreach (var vehicle in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderGroundVehicles).Cast<Vehicle>())
        {
            GameFiber.Yield();
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                if (vehicle != Main.Player.CurrentVehicle)
                {
                    if (!vehicle.CreatedByTheCallingPlugin)
                    {
                        if (!CalloutEntities.Contains(vehicle))
                        {
                            if (Vector3.Distance(vehicle.Position, CalloutPosition) < 50f)
                            {
                                vehicle.Delete();
                            }
                        }
                    }
                }
            }
        }
    }
}