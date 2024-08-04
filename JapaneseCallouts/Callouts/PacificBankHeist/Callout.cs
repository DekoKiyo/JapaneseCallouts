/*
    This callout is based Bank Heist in [Assorted Callouts](https://github.com/Albo1125/Assorted-Callouts) made by [Albo1125](https://github.com/Albo1125).
    The source code is licensed under the GPL-3.0.

    Edited by @DekoKiyo

    Remade on July, 2024
*/

namespace JapaneseCallouts.Callouts.PacificBankHeist;

[CalloutInfo("[JPC] Pacific Bank Heist", CalloutProbability.High)]
internal class PacificBankHeist : CalloutBase<Configurations>
{
    private static GameFiber CopFightingAIGameFiber;
    private static GameFiber RobbersFightingAIGameFiber;

    internal readonly List<Entity> CalloutEntities = [];
    internal readonly PBHConversations conversations = new();
    internal readonly Variables variables = new();
    internal readonly PBHNegotiations negotiations = new();

    internal override void Setup()
    {
        CalloutPosition = Constants.BankLocation;
        CalloutMessage = Localization.GetString("PacificBankHeist");
        NoLastRadio = true;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 40f);
        Functions.PlayScannerAudioUsingPosition(Settings.Instance.PacificBankHeistRadioSound, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("PacificBankHeistDesc"));

        conversations.IntroConversation =
        [
            (Settings.Instance.OfficerName, Localization.GetString("Intro1")),
            (Localization.GetString("Commander"), Localization.GetString("Intro2")),
            (Settings.Instance.OfficerName, Localization.GetString("Intro3")),
            (Localization.GetString("Commander"), Localization.GetString("Intro4", variables.HostageCount.ToString())),
            (Localization.GetString("Commander"), Localization.GetString("Intro5")),
            (Localization.GetString("Commander"), Localization.GetString("Intro6")),
            (Localization.GetString("Commander"), Localization.GetString("Intro7")),
            (Localization.GetString("Commander"), Localization.GetString("Intro8")),
        ];

        OnCalloutsEnded += () =>
        {
            if (variables.BankBlip is not null && variables.BankBlip.IsValid() && variables.BankBlip.Exists())
            {
                variables.BankBlip.Delete();
            }
            if (variables.SideDoorBlip is not null && variables.SideDoorBlip.IsValid() && variables.SideDoorBlip.Exists())
            {
                variables.SideDoorBlip.Delete();
            }
            if (variables.CommanderBlip is not null && variables.CommanderBlip.IsValid() && variables.CommanderBlip.Exists())
            {
                variables.CommanderBlip.Delete();
            }
            if (variables.Commander is not null && variables.Commander.IsValid() && variables.Commander.Exists())
            {
                variables.Commander.Dismiss();
            }
            if (variables.Wife is not null && variables.Wife.IsValid() && variables.Wife.Exists())
            {
                variables.Wife.Delete();
            }
            if (variables.WifeDriver is not null && variables.WifeDriver.IsValid() && variables.WifeDriver.Exists())
            {
                variables.WifeDriver.Delete();
            }
            if (variables.WifeCar is not null && variables.WifeCar.IsValid() && variables.WifeCar.Exists())
            {
                variables.WifeCar.Delete();
            }
            if (variables.MobilePhone is not null && variables.MobilePhone.IsValid() && variables.MobilePhone.Exists())
            {
                variables.MobilePhone.Delete();
            }

            foreach (var e in variables.AllPoliceVehicles)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in variables.AllRiot)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in variables.AllAmbulance)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in variables.AllOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllStandingOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllAimingOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.OfficersArresting)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllSWATUnits)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllSneakRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllVaultRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllBarriersEntities)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in variables.FightingSneakRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in variables.AllHostages)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Delete();
                }
            }
            foreach (var e in variables.RiotBlips)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var b in variables.EnemyBlips)
            {
                b?.Dismiss();
            }
        };
    }

    internal override void Accepted()
    {
        // setup alarm
        variables.BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{Main.ALARM_SOUND_FILE_NAME}");
        variables.BankAlarm.Load();

        Hud.DisplayNotification(Localization.GetString("BankHeistWarning"), Main.PLUGIN_NAME, Localization.GetString("BankHeist"));

        if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
        {
            CalloutEntities.Add(Game.LocalPlayer.Character.CurrentVehicle);
        }
        Hud.DisplayNotification(Localization.GetString("PacificBankHeistDesc"), Localization.GetString("Dispatch"), Localization.GetString("BankHeist"));

        variables.DiedHostagesTB = new(Localization.GetString("DiedHostages"), $"{(variables.TotalHostagesCount - variables.AliveHostagesCount).ToString()}")
        {
            Highlight = HudColor.Green.GetColor(),
        };
        variables.RescuedHostagesTB = new(Localization.GetString("RescuedHostages"), $"{variables.SafeHostagesCount.ToString()}/{variables.TotalHostagesCount.ToString()}")
        {
            Highlight = HudColor.Blue.GetColor(),
        };
        variables.TBPool.Add(variables.DiedHostagesTB);
        variables.TBPool.Add(variables.RescuedHostagesTB);
        CalloutHandler();
    }

    private void CalloutHandler()
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                variables.BankBlip = new(CalloutPosition)
                {
                    Sprite = Constants.SPRITE,
                    Color = Color.Yellow,
                    RouteColor = Color.Yellow,
                    IsRouteEnabled = true,
                };
                variables.SideDoorBlip = new(new Vector3(258.3f, 200.4f, 104.9f))
                {
                    Sprite = Constants.SPRITE,
                    Color = Color.Yellow,
                };

                GameFiber.StartNew(() =>
                {
                    GameFiber.Wait(4800);
                    Hud.DisplayNotification(Localization.GetString("BankHeistCopyThat"));
                    Functions.PlayScannerAudio("JP_COPY_THAT_MOVING_RIGHT_NOW REPORT_RESPONSE_COPY JP_PROCEED_WITH_CAUTION");
                    GameFiber.Wait(3400);
                    Hud.DisplayNotification(Localization.GetString("BankHeistRoger"));
                });

                // Loading models
                GameFiber.Yield();
                Constants.BarrierModel.Load();
                Constants.PhoneModel.Load();

                if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
                {
                    CalloutEntities.Add(Game.LocalPlayer.Character.CurrentVehicle);
                    var passengers = Game.LocalPlayer.Character.CurrentVehicle.Passengers;
                    if (passengers.Length > 0)
                    {
                        foreach (var passenger in passengers)
                        {
                            CalloutEntities.Add(passenger);
                        }
                    }
                }
                GameFiber.WaitUntil(() => Vector3.Distance(Game.LocalPlayer.Character.Position, CalloutPosition) <= 200f);

                // get the weather for select the peds
                var weather = CalloutHelpers.GetWeatherType(IPTFunctions.GetWeatherType());

                variables.SpeedZoneId = World.AddSpeedZone(CalloutPosition, 200f, 25f);

                PBHFunctions.ClearUnrelatedEntities(this);
                PBHFunctions.PlaceBarriers(this);
                PBHFunctions.SpawnVehicles(this);
                PBHFunctions.SpawnOfficers(this, weather);
                PBHFunctions.SpawnNegotiationRobbers(this, weather);
                PBHFunctions.SpawnSneakyRobbers(this, weather);
                PBHFunctions.SpawnHostages(this, weather);
                PBHFunctions.SpawnEMS(this, weather);

                Game.FrameRender += TimerBarsProcess;

                GameFiber.StartNew(() => PBHFunctions.MakeNearbyPedsFlee(this));
                GameFiber.StartNew(() => PBHFunctions.SneakyRobbersAI(this));
                GameFiber.StartNew(() => PBHFunctions.HandleHostages(this));
                GameFiber.StartNew(() => PBHFunctions.HandleOpenBackRiotVan(this));
                GameFiber.StartNew(() => PBHFunctions.HandleCalloutAudio(this));
                GameFiber.StartNew(() => PBHFunctions.UnlockDoorsAlways(this));

                if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.Exists() && Game.LocalPlayer.Character.IsAlive)
                {
                    Game.LocalPlayer.Character.CanAttackFriendlies = false;
                    Game.LocalPlayer.Character.IsInvincible = false;

                    Natives.SET_PLAYER_WEAPON_DEFENSE_MODIFIER(Game.LocalPlayer, 0.45f);
                    Natives.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.LocalPlayer, 0.92f);
                    Natives.SET_AI_MELEE_WEAPON_DAMAGE_MODIFIER(1f);
                }

                var fightingPrepared = false;
                var evaluatedWithCommander = false;

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    // When player has just arrived
                    if (variables.Status is EPacificBankHeistStatus.Init)
                    {
                        if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                        {
                            if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                            {
                                if (variables.Commander is not null && variables.Commander.IsValid() && variables.Commander.IsAlive)
                                {
                                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, variables.Commander.Position) < 4f)
                                    {
                                        Hud.DisplayNotification(Localization.GetString("BankHeistWarning"));
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                        if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                        {
                                            variables.Status = EPacificBankHeistStatus.TalkingToCommander;
                                            negotiations.DetermineInitialDialogue(this);
                                        }
                                    }
                                    else
                                    {
                                        Hud.DisplayHelp(Localization.GetString("TalkToCommander", $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }

                    // Is fighting is initialized
                    if (!fightingPrepared)
                    {
                        if (variables.Status is EPacificBankHeistStatus.FightingWithRobbers)
                        {
                            PBHFunctions.SpawnAssaultRobbers(this, weather);

                            if (Main.MT.Next(10) is < 3)
                            {
                                PBHFunctions.SpawnVaultRobbers(this, weather);
                            }

                            foreach (var cop in variables.AllOfficers)
                            {
                                cop.RelationshipGroup = Constants.PoliceRG;
                            }
                            foreach (var cop in variables.AllSWATUnits)
                            {
                                cop.RelationshipGroup = Constants.PoliceRG;
                            }
                            foreach (var robber in variables.AllRobbers)
                            {
                                var blip = new BlipPlus(robber, HudColor.Enemy.GetColor(), BlipSprite.Enemy);
                                variables.EnemyBlips.Add(blip);
                                robber.RelationshipGroup = Constants.RobbersRG;
                            }
                            foreach (var robber in variables.AllSneakRobbers)
                            {
                                if (robber is not null)
                                {
                                    var blip = new BlipPlus(robber, HudColor.Enemy.GetColor(), BlipSprite.Enemy);
                                    variables.EnemyBlips.Add(blip);
                                    robber.RelationshipGroup = Constants.SneakRobbersRG;
                                }
                            }
                            foreach (var hostage in variables.AllHostages)
                            {
                                var blip = new BlipPlus(hostage, HudColor.Michael.GetColor(), BlipSprite.Friend);
                                variables.EnemyBlips.Add(blip);
                                hostage.RelationshipGroup = Constants.HostageRG;
                            }

                            PBHFunctions.SetRelationships();

                            CopFightingAIGameFiber = GameFiber.StartNew(() => PBHFunctions.CopFightingAI(this));
                            RobbersFightingAIGameFiber = GameFiber.StartNew(() => PBHFunctions.RobbersFightingAI(this));
                            GameFiber.StartNew(() => PBHFunctions.CheckForRobbersOutside(this));

                            fightingPrepared = true;
                        }
                    }

                    // If player talks to cpt wells during fight
                    if (variables.Status is EPacificBankHeistStatus.FightingWithRobbers)
                    {
                        if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                        {
                            if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                            {
                                if (variables.Commander is not null && variables.Commander.IsValid() && variables.Commander.Exists())
                                {
                                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, variables.Commander.Position) < 3f)
                                    {
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                        if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                        {
                                            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("StillFighting"))]);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Make everyone fight if player enters bank
                    if (variables.Status is not EPacificBankHeistStatus.FightingWithRobbers and not EPacificBankHeistStatus.Surrendering)
                    {
                        foreach (var check in Constants.PacificBankInsideChecks)
                        {
                            if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                            {
                                if (Vector3.Distance(check, Game.LocalPlayer.Character.Position) < 2.3f)
                                {
                                    variables.Status = EPacificBankHeistStatus.FightingWithRobbers;
                                }
                            }
                        }
                    }

                    // If all hostages rescued break
                    if (variables.SafeHostagesCount == variables.AliveHostagesCount) break;
                    // If surrendered
                    if (variables.SurrenderComplete) break;

                    if (KeyHelpers.IsKeysDown(Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey))
                    {
                        PBHFunctions.SwitchSWATFollowing(this);
                    }

                    if (variables.IsSWATFollowing)
                    {
                        if (Game.LocalPlayer.Character.IsShooting)
                        {
                            variables.IsSWATFollowing = false;
                            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                        }
                    }
                }

                // When surrendered
                if (variables.SurrenderComplete) PBHFunctions.CopFightingAI(this);

                PBHFunctions.SetRelationships();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    if (variables.SafeHostagesCount == variables.AliveHostagesCount)
                    {
                        GameFiber.Wait(3000);
                        break;
                    }

                    if (KeyHelpers.IsKeysDown(Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey))
                    {
                        PBHFunctions.SwitchSWATFollowing(this);
                    }

                    if (variables.IsSWATFollowing)
                    {
                        if (Game.LocalPlayer.Character.IsShooting)
                        {
                            variables.IsSWATFollowing = false;
                            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                        }
                    }

                    if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                    {
                        if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                        {
                            if (variables.Commander is not null && variables.Commander.IsValid() && variables.Commander.Exists())
                            {
                                if (Vector3.Distance(Game.LocalPlayer.Character.Position, variables.Commander.Position) < 4f)
                                {
                                    KeyHelpers.DisplayKeyHelp("PressToTalk", [Localization.GetString("Commander")], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                    if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                    {
                                        if (!variables.TalkedToCommander2nd)
                                        {
                                            Conversations.Talk(conversations.AfterSurrendered);
                                            variables.TalkedToCommander2nd = true;
                                            variables.Status = EPacificBankHeistStatus.FightingWithRobbers;
                                            KeyHelpers.DisplayKeyHelp("SWATFollowing", Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey);
                                        }
                                        else
                                        {
                                            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("StillFighting"))]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!variables.TalkedToCommander2nd)
                                    {
                                        Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }
                }

                // The end
                variables.Status = EPacificBankHeistStatus.Last;
                variables.CurrentAlarmState = AlarmState.None;

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    if (!evaluatedWithCommander)
                    {
                        if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Game.LocalPlayer.Character.Position, variables.Commander.Position) < 4f)
                            {
                                KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                {
                                    evaluatedWithCommander = true;
                                    Conversations.Talk(conversations.Final);
                                    GameFiber.Wait(5000);
                                    PBHFunctions.DetermineResults(this);
                                    GameFiber.Wait(2500);
                                    break;
                                }
                            }
                            else
                            {
                                Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{Constants.COMMANDER_BLIP.GetIconToken()}~"));
                            }
                        }
                    }
                }

                variables.IsCalloutFinished = true;
                End();
            }
            catch (Exception e)
            {
                Main.Logger.Error("The exception is occurred in the process of callout.", nameof(PacificBankHeist));
                Main.Logger.Error(e.ToString());
            }
        }, $"[{nameof(PacificBankHeist)}] Callout Main Process");
    }

    private void TimerBarsProcess(object sender, GraphicsEventArgs e)
    {
        if (variables.Status is EPacificBankHeistStatus.FightingWithRobbers || (variables.SurrenderComplete && variables.Status is EPacificBankHeistStatus.TalkingToCommander2nd))
        {
            if (variables.TBPool is not null)
            {
                variables.RescuedHostagesTB.Text = $"{variables.SafeHostagesCount.ToString()}/{variables.TotalHostagesCount.ToString()}";
                variables.DiedHostagesTB.Text = $"{(variables.TotalHostagesCount - variables.AliveHostagesCount).ToString()}";
                variables.DiedHostagesTB.Highlight = variables.TotalHostagesCount - variables.AliveHostagesCount is > 0 ? HudColor.Red.GetColor() : HudColor.Green.GetColor();
                variables.TBPool.Draw();
            }
        }
        if (!IsCalloutRunning || variables.Status is EPacificBankHeistStatus.Last)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
    }

    internal override void Update() { }

    internal override void OnDisplayed() { }
    internal override void NotAccepted() { }
}