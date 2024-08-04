namespace JapaneseCallouts.Callouts.WantedCriminalFound;

[CalloutInfo("[JPC] Wanted Criminal Found", CalloutProbability.High)]
internal class WantedCriminalFound : CalloutBase
{
    private Ped criminal;
    private Blip blip;
    private LHandle pursuit;
    private int blipTimer = 750;
    private int count = 0;
    private bool found = false;
    private readonly bool fight = Main.MT.Next(0, 3) is 0;
    private readonly RelationshipGroup suspectRG = new("SUSPECT");

    internal override void Setup()
    {
        CalloutMessage = Localization.GetString("WantedCriminalFound");
        CalloutPosition = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(500f, 1200f));
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 50f);
        Functions.PlayScannerAudioUsingPosition(XmlManager.CalloutsSoundConfig.WantedCriminalFound, CalloutPosition);

        OnCalloutsEnded += () =>
        {
            if (Game.LocalPlayer.Character.IsDead)
            {
                if (criminal is not null && criminal.IsValid() && criminal.Exists()) criminal.Delete();
            }
            else
            {
                if (criminal is not null && criminal.IsValid() && criminal.Exists()) criminal.Dismiss();
            }
            if (blip is not null && blip.IsValid() && blip.Exists()) blip.Delete();
            if (!Game.LocalPlayer.Character.IsDead)
            {
                Hud.DisplayNotification(Localization.GetString("CalloutCode4"), Localization.GetString("Dispatch"), Localization.GetString("WantedCriminalFound"));
            }
        };
    }

    internal override void Accepted()
    {
        Hud.DisplayNotification(Localization.GetString("WantedCriminalFoundDesc"), Localization.GetString("Dispatch"), Localization.GetString("WantedCriminalFound"));

        var weather = CalloutHelpers.GetWeatherType(IPTFunctions.GetWeatherType());
        var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.WantedCriminalFoundConfig.Criminals]);
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
                criminal.GiveWeapon([.. XmlManager.WantedCriminalFoundConfig.Weapons], true);
            }
            criminal.Tasks.Wander();
            blip = new(criminal.Position.Around(Main.MT.Next(100)), Main.MT.Next(75, 120))
            {
                Color = Color.Yellow,
                Alpha = 0.5f,
                IsRouteEnabled = true,
            };
        }

        Game.SetRelationshipBetweenRelationshipGroups(suspectRG, RelationshipGroup.Cop, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(suspectRG, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);
        Game.SetRelationshipBetweenRelationshipGroups(RelationshipGroup.Cop, Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
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

            Hud.DisplayNotification(Localization.GetString("GPSUpdate"), Localization.GetString("Dispatch"), Localization.GetString("WantedCriminalFound"));
            Hud.DisplayNotification(Localization.GetString("WantedCriminalData", criminal.IsMale ? Localization.GetString("Male") : Localization.GetString("Female")), Localization.GetString("Dispatch"), Localization.GetString("WantedCriminalFound"));
            Functions.PlayScannerAudioUsingPosition("SUSPECT_LAST_SEEN IN_OR_ON_POSITION", criminal.Position);
            count++;
        }
        if (!found && Vector3.Distance(Game.LocalPlayer.Character.Position, criminal.Position) < 30f)
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
        if (Game.LocalPlayer.Character.IsDead) End();
        if (criminal is not null && criminal.IsValid() && criminal.Exists() && EntityHelpers.IsAllPedDeadOrArrested([criminal])) End();
        if (KeyHelpers.IsKeysDown(Settings.EndCalloutsKey, Settings.EndCalloutsModifierKey)) End();
    }

    internal override void OnDisplayed() { }
    internal override void NotAccepted() { }
}