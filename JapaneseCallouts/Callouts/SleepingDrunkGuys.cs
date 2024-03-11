namespace JapaneseCallouts.Callouts;

internal class SleepingDrunkGuys : CalloutBase
{
    private const string DRUNK_ANIM_DICTIONARY = "missarmenian2";
    private const string DRUNK_ANIM_UP = "corpse_search_exit_ped";
    private const string DRUNK_ANIM_DOWN = "drunk_loop";

    private int index = 0;
    private bool arrived = false;

    private static readonly Model[] Models =
    [
        "a_f_y_business_01",
        "a_f_y_business_02",
        "a_m_y_business_01",
        "a_m_y_business_02",
        "a_m_y_golfer_01",
        "a_m_y_gencaspat_01",
        "cs_davenorton",
        "cs_bankman",
        "cs_fbisuit_01",
        "a_m_m_prolhost_01",
        "a_f_y_femaleagent",
        "a_f_y_hipster_02",
        "g_m_m_chicold_01",
        "a_m_m_business_01",
        "a_m_m_mexlabor_01",
        "cs_paper",
        "s_f_y_airhostess_01",
        "s_f_y_casino_01",
        "ig_orleans",
        "s_m_m_fiboffice_01",
        "s_m_m_highsec_02",
        "csb_screen_writer",
    ];
    private static readonly Vector3[][] Positions =
    [
        [
            new(190.3f, -990.8f, 30.0f),
            new(197.7f, -981.1f, 30.0f),
            new(188.9f, -978.9f, 30.0f),
            new(169.5f, -982.2f, 30.0f),
            new(156.1f, -975.6f, 30.0f),
            new(158.3f, -966.4f, 30.0f),
        ]
    ];

    internal override void Setup()
    {
        index = Main.MersenneTwister.Next(Positions.Length);
        CalloutMessage = CalloutsName.SleepingDrunkGuys;
        CalloutPosition = Positions[index][Main.MersenneTwister.Next(Positions[index].Length)];
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 100f);
        Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_ACCIDENT IN_OR_ON_POSITION UNITS_RESPOND_CODE_02", CalloutPosition);
    }

    internal override void OnDisplayed() { }

    internal override void Accepted()
    {
        foreach (var pos in Positions[index])
        {
            for (int i = 0; i < Main.MersenneTwister.Next(2) + 1; i++)
            {
                var ped = new Ped(Models[Main.MersenneTwister.Next(Models.Length)], pos.Around(15f), Main.MersenneTwister.Next(3600) / 10f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    KeepTasks = true,
                    Health = Main.MersenneTwister.Next(20) is < 1 ? 0 : 80
                };
                Natives.SET_PED_IS_DRUNK(ped, true);
                ped.Tasks.PlayAnimation(DRUNK_ANIM_DICTIONARY, Main.MersenneTwister.Next(2) is 0 ? DRUNK_ANIM_DOWN : DRUNK_ANIM_UP, 1f, AnimationFlags.Loop);
            }
        }
    }

    internal override void NotAccepted() { }

    internal override void Update()
    {
        if (!arrived && Vector3.Distance(Main.Player.Position, CalloutPosition) < 50f)
        {
            arrived = true;
            HudHelpers.DisplayHelp(CalloutsText.DrunkGuysHelp);
            End();
        }
    }
}