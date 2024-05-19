namespace JapaneseCallouts.Callouts;

[CalloutInfo("[JPC] Wanted Criminal Found", CalloutProbability.High)]
internal class WantedCriminalFound : CalloutBase
{
    private Ped criminal;
    private Blip blip;
    private LHandle pursuit;
    private int blipTimer = 750;
    private int count = 0;
    private bool found = false;
    private readonly bool fight = Main.MersenneTwister.Next(0, 3) is 0;
    private readonly RelationshipGroup suspectRG = new("SUSPECT");

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("WantedCriminalFound");
        CalloutPosition = World.GetNextPositionOnStreet(Main.Player.Position.Around(500f, 1200f));
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.WantedCriminalFound, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Main.Player.IsDead)
            {
                if (criminal is not null && criminal.IsValid() && criminal.Exists()) criminal.Delete();
            }
            else
            {
                if (criminal is not null && criminal.IsValid() && criminal.Exists()) criminal.Dismiss();
            }
            if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
            if (!Main.Player.IsDead)
            {
                HudHelpers.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("WantedCriminalFound"));
            }
        };
    }

    internal override void Accepted()
    {
        HudHelpers.DisplayNotification(Localization.GetString("WantedCriminalFoundDesc"));

        var data = CalloutHelpers.Select([.. XmlManager.WantedCriminalFoundConfig.Criminals]);
        criminal = new(data.Model, CalloutPosition, 0f)
        {
            IsPersistent = true,
            BlockPermanentEvents = true,
        };
        if (criminal is not null && criminal.IsValid() && criminal.Exists())
        {
            criminal.SetOutfit(data);
            Functions.GetPersonaForPed(criminal).Wanted = true;
            if (fight)
            {
                criminal.GiveWeapon([.. XmlManager.WantedCriminalFoundConfig.Weapons]);
            }
            criminal.Tasks.Wander();
            blip = new(criminal.Position.Around(Main.MersenneTwister.Next(100)), Main.MersenneTwister.Next(75, 120))
            {
                Color = Color.Yellow,
                Alpha = 0.5f,
                IsRouteEnabled = true,
            };

            Game.SetRelationshipBetweenRelationshipGroups(suspectRG, RelationshipGroup.Cop, Relationship.Hate);
            Game.SetRelationshipBetweenRelationshipGroups(suspectRG, Main.Player.RelationshipGroup, Relationship.Hate);
            Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Main.Player.RelationshipGroup, Relationship.Respect);
        }
    }

    internal override void Update()
    {
        if (!found && !IPTFunctions.IsGamePaused()) blipTimer--;
        if (blipTimer < 0 && !found)
        {
            blipTimer = 1800;
            blip.IsRouteEnabled = false;
            blip.Position = criminal.Position;
            blip.IsRouteEnabled = true;

            HudHelpers.DisplayNotification(Localization.GetString("GPSUpdate"));
            HudHelpers.DisplayNotification(Localization.GetString("WantedCriminalData", criminal.IsMale ? Localization.GetString("Male") : Localization.GetString("Female")));
            Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", criminal.Position);
            count++;
        }
        if (!found && Vector3.Distance(Main.Player.Position, criminal.Position) < 30f)
        {
            found = true;
            if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
            Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.WantedCriminalFound, criminal.Position);
            if (criminal is not null && criminal.IsValid() && criminal.Exists())
            {
                criminal.Tasks.Clear();
                pursuit = Functions.CreatePursuit();
                if (pursuit is not null)
                {
                    Functions.AddPedToPursuit(pursuit, criminal);
                    Functions.SetPursuitIsActiveForPlayer(pursuit, true);
                    if (fight)
                    {
                        criminal.Tasks.FightAgainstClosestHatedTarget(100f);
                    }
                }
            }
        }
        if (count > 15) End();
        if (Main.Player.IsDead) End();
        if (criminal is not null && criminal.IsValid() && criminal.Exists() && EntityHelpers.IsAllPedDeadOrArrested([criminal])) End();
    }

    internal override void OnDisplayed() { }
    internal override void NotAccepted() { }
}