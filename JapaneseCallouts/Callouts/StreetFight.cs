namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Street Fight", CalloutProbability.Medium)]
internal class StreetFight : CalloutBase
{
    private Ped suspect1, suspect2;
    private Blip area, susB1, susB2;
    private LHandle pursuit;
    private bool started = false;
    private bool isPursuitBegan = false;
    private readonly RelationshipGroup suspect1RG = new("suspect1");
    private readonly RelationshipGroup suspect2RG = new("suspect2");

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("StreetFight");
        CalloutPosition = World.GetNextPositionOnStreet(Main.Player.Position.Around(50f, 150f));
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 30f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.StreetFight, CalloutPosition);

        Game.SetRelationshipBetweenRelationshipGroups(suspect1RG, suspect2RG, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(suspect2RG, suspect1RG, Relationship.Hate);

        var data1 = CalloutHelpers.Select([.. XmlManager.StreetFightConfig.Suspects]);
        suspect1 = new(data1.Model, new(CalloutPosition.X, CalloutPosition.Y, (float)World.GetGroundZ(CalloutPosition, true, true)), 0f)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
            KeepTasks = true,
            RelationshipGroup = suspect1RG,
        };
        if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists())
        {
            suspect1.SetOutfit(data1);
            suspect1.Tasks.FightAgainstClosestHatedTarget(500f);
        }

        var data2 = CalloutHelpers.Select([.. XmlManager.StreetFightConfig.Suspects]);
        suspect2 = new(data2.Model, new(CalloutPosition.X, CalloutPosition.Y, (float)World.GetGroundZ(CalloutPosition, true, true)), 0f)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
            KeepTasks = true,
            RelationshipGroup = suspect2RG,
        };
        if (suspect2 is not null && suspect2.IsValid() && suspect2.Exists())
        {
            suspect2.SetOutfit(data2);
            suspect2.Tasks.FightAgainstClosestHatedTarget(500f);
        }

        OnCalloutsEnded += () =>
        {
            if (area is not null && area.IsValid() && area.Exists()) area.Delete();
            if (susB1 is not null && susB1.IsValid() && susB1.Exists()) susB1.Delete();
            if (susB2 is not null && susB2.IsValid() && susB2.Exists()) susB2.Delete();
            if (Main.Player.IsDead)
            {
                if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists()) suspect1.Delete();
                if (suspect2 is not null && suspect2.IsValid() && suspect2.Exists()) suspect2.Delete();
            }
            else
            {
                if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists()) suspect1.Dismiss();
                if (suspect2 is not null && suspect2.IsValid() && suspect2.Exists()) suspect2.Dismiss();
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("StreetFight"));
            }
        };
    }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("StreetFightDesc"), Localization.GetString("Dispatch"), Localization.GetString("StreetFight"));
        area = new(CalloutPosition.Around(10f, 20f), 40f)
        {
            Color = Color.Yellow,
            Alpha = 0.5f,
            IsRouteEnabled = true,
        };
    }

    internal override void Update()
    {
        if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists() &&
            suspect2 is not null && suspect2.IsValid() && suspect2.Exists() &&
            (Main.Player.DistanceTo(suspect1) < 80f || Main.Player.DistanceTo(suspect2) < 80f) && !started)
        {
            if (area is not null && area.IsValid() && area.Exists()) area.Delete();

            if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists())
            {
                susB1 = new(suspect1)
                {
                    IsFriendly = false,
                    Scale = 0.75f,
                };
            }
            if (suspect2 is not null && suspect2.IsValid() && suspect2.Exists())
            {
                susB2 = new(suspect2)
                {
                    IsFriendly = false,
                    Scale = 0.75f,
                };
            }
            started = true;
        }

        if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists() &&
            suspect2 is not null && suspect2.IsValid() && suspect2.Exists() &&
            !isPursuitBegan)
        {
            if (suspect1.DistanceTo(Main.Player.Position) < 10f ||
                suspect2.DistanceTo(Main.Player.Position) < 10f)
            {
                pursuit = Functions.CreatePursuit();
                if (pursuit is not null)
                {
                    if (suspect1.IsAlive) Functions.AddPedToPursuit(pursuit, suspect1);
                    if (suspect2.IsAlive) Functions.AddPedToPursuit(pursuit, suspect2);
                    Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                    isPursuitBegan = true;
                }
            }
        }

        if (Main.Player.IsDead) End();
        if (EntityHelpers.IsAllPedDeadOrArrested([suspect1, suspect2])) End();
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }

    internal override void NotAccepted()
    {
        if (suspect1 is not null && suspect1.IsValid() && suspect1.Exists()) suspect1.Dismiss();
        if (suspect2 is not null && suspect2.IsValid() && suspect2.Exists()) suspect2.Dismiss();
        if (area is not null && area.IsValid() && area.Exists()) area.Delete();
        if (susB1 is not null && susB1.IsValid() && susB1.Exists()) susB1.Delete();
        if (susB2 is not null && susB2.IsValid() && susB2.Exists()) susB2.Delete();
        if (pursuit is not null) Functions.ForceEndPursuit(pursuit);
    }

    internal override void OnDisplayed() { }
}