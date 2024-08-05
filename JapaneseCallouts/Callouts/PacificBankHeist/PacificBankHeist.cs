/*
    This callout is based Bank Heist in [Assorted Callouts](https://github.com/Albo1125/Assorted-Callouts) made by [Albo1125](https://github.com/Albo1125).
    The source code is licensed under the GPL-3.0.

    Edited by @DekoKiyo

    Remade on July, 2024
*/

namespace JapaneseCallouts.Callouts.PacificBankHeist;

[CalloutInfo("[JPC] Pacific Bank Heist", CalloutProbability.High)]
internal partial class PacificBankHeist : CalloutBase<Configurations>
{
    private static GameFiber CopFightingAIGameFiber;
    private static GameFiber RobbersFightingAIGameFiber;

    internal readonly List<Entity> CalloutEntities = [];

    internal override void Setup()
    {
        CalloutPosition = BankLocation;
        CalloutMessage = Localization.GetString("PacificBankHeist");
        NoLastRadio = true;
        ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 40f);
        Functions.PlayScannerAudioUsingPosition(Settings.Instance.PacificBankHeistRadioSound, CalloutPosition);
        CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("PacificBankHeistDesc"));

        IntroConversation =
        [
            (Settings.Instance.OfficerName, Localization.GetString("Intro1")),
            (Localization.GetString("Commander"), Localization.GetString("Intro2")),
            (Settings.Instance.OfficerName, Localization.GetString("Intro3")),
            (Localization.GetString("Commander"), Localization.GetString("Intro4", HostageCount.ToString())),
            (Localization.GetString("Commander"), Localization.GetString("Intro5")),
            (Localization.GetString("Commander"), Localization.GetString("Intro6")),
            (Localization.GetString("Commander"), Localization.GetString("Intro7")),
            (Localization.GetString("Commander"), Localization.GetString("Intro8")),
        ];

        OnCalloutsEnded += () =>
        {
            if (BankBlip is not null && BankBlip.IsValid() && BankBlip.Exists())
            {
                BankBlip.Delete();
            }
            if (SideDoorBlip is not null && SideDoorBlip.IsValid() && SideDoorBlip.Exists())
            {
                SideDoorBlip.Delete();
            }
            if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists())
            {
                CommanderBlip.Delete();
            }
            if (Commander is not null && Commander.IsValid() && Commander.Exists())
            {
                Commander.Dismiss();
            }
            if (Wife is not null && Wife.IsValid() && Wife.Exists())
            {
                Wife.Delete();
            }
            if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists())
            {
                WifeDriver.Delete();
            }
            if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists())
            {
                WifeCar.Delete();
            }
            if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists())
            {
                MobilePhone.Delete();
            }

            foreach (var e in AllPoliceVehicles)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in AllRiot)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in AllAmbulance)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in AllOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllStandingOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllAimingOfficers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in OfficersArresting)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllSWATUnits)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllSneakRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllVaultRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllBarriersEntities)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var e in FightingSneakRobbers)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Dismiss();
                }
            }
            foreach (var e in AllHostages)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Tasks.Clear();
                    e.Delete();
                }
            }
            foreach (var e in RiotBlips)
            {
                if (e is not null && e.IsValid() && e.Exists())
                {
                    e.Delete();
                }
            }
            foreach (var b in EnemyBlips)
            {
                b?.Dismiss();
            }
        };
    }

    internal override void Accepted()
    {
        // setup alarm
        BankAlarm = new($"{Main.PLUGIN_DIRECTORY}/{Main.PLUGIN_AUDIO_DIRECTORY}/{Main.ALARM_SOUND_FILE_NAME}");
        BankAlarm.Load();

        Hud.DisplayNotification(Localization.GetString("BankHeistWarning"), Main.PLUGIN_NAME, Localization.GetString("BankHeist"));

        if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
        {
            CalloutEntities.Add(Game.LocalPlayer.Character.CurrentVehicle);
        }
        Hud.DisplayNotification(Localization.GetString("PacificBankHeistDesc"), Localization.GetString("Dispatch"), Localization.GetString("BankHeist"));

        DiedHostagesTB = new(Localization.GetString("DiedHostages"), $"{(TotalHostagesCount - AliveHostagesCount).ToString()}")
        {
            Highlight = HudColor.Green.GetColor(),
        };
        RescuedHostagesTB = new(Localization.GetString("RescuedHostages"), $"{SafeHostagesCount.ToString()}/{TotalHostagesCount.ToString()}")
        {
            Highlight = HudColor.Blue.GetColor(),
        };
        TBPool.Add(DiedHostagesTB);
        TBPool.Add(RescuedHostagesTB);
        CalloutHandler();
    }

    private void CalloutHandler()
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                BankBlip = new(CalloutPosition)
                {
                    Sprite = SPRITE,
                    Color = Color.Yellow,
                    RouteColor = Color.Yellow,
                    IsRouteEnabled = true,
                };
                SideDoorBlip = new(new Vector3(258.3f, 200.4f, 104.9f))
                {
                    Sprite = SPRITE,
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
                BarrierModel.Load();
                PhoneModel.Load();

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

                SpeedZoneId = World.AddSpeedZone(CalloutPosition, 200f, 25f);

                ClearUnrelatedEntities();
                PlaceBarriers();
                SpawnVehicles();
                SpawnOfficers(weather);
                SpawnNegotiationRobbers(weather);
                SpawnSneakyRobbers(weather);
                SpawnHostages(weather);
                SpawnEMS(weather);

                Game.FrameRender += TimerBarsProcess;

                GameFiber.StartNew(MakeNearbyPedsFlee);
                GameFiber.StartNew(SneakyRobbersAI);
                GameFiber.StartNew(HandleHostages);
                GameFiber.StartNew(HandleOpenBackRiotVan);
                GameFiber.StartNew(HandleCalloutAudio);
                GameFiber.StartNew(UnlockDoorsAlways);

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
                    if (Status is EPacificBankHeistStatus.Init)
                    {
                        if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                        {
                            if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                            {
                                if (Commander is not null && Commander.IsValid() && Commander.IsAlive)
                                {
                                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, Commander.Position) < 4f)
                                    {
                                        Hud.DisplayNotification(Localization.GetString("BankHeistWarning"));
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                        if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                        {
                                            Status = EPacificBankHeistStatus.TalkingToCommander;
                                            DetermineInitialDialogue();
                                        }
                                    }
                                    else
                                    {
                                        Hud.DisplayHelp(Localization.GetString("TalkToCommander", $"~{COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }

                    // Is fighting is initialized
                    if (!fightingPrepared)
                    {
                        if (Status is EPacificBankHeistStatus.FightingWithRobbers)
                        {
                            SpawnAssaultRobbers(weather);

                            if (Main.MT.Next(10) is < 3)
                            {
                                SpawnVaultRobbers(weather);
                            }

                            foreach (var cop in AllOfficers)
                            {
                                cop.RelationshipGroup = PoliceRG;
                            }
                            foreach (var cop in AllSWATUnits)
                            {
                                cop.RelationshipGroup = PoliceRG;
                            }
                            foreach (var robber in AllRobbers)
                            {
                                var blip = new BlipPlus(robber, HudColor.Enemy.GetColor(), BlipSprite.Enemy);
                                EnemyBlips.Add(blip);
                                robber.RelationshipGroup = RobbersRG;
                            }
                            foreach (var robber in AllSneakRobbers)
                            {
                                if (robber is not null)
                                {
                                    var blip = new BlipPlus(robber, HudColor.Enemy.GetColor(), BlipSprite.Enemy);
                                    EnemyBlips.Add(blip);
                                    robber.RelationshipGroup = SneakRobbersRG;
                                }
                            }
                            foreach (var hostage in AllHostages)
                            {
                                var blip = new BlipPlus(hostage, HudColor.Michael.GetColor(), BlipSprite.Friend);
                                EnemyBlips.Add(blip);
                                hostage.RelationshipGroup = HostageRG;
                            }

                            SetRelationships();

                            CopFightingAIGameFiber = GameFiber.StartNew(CopFightingAI);
                            RobbersFightingAIGameFiber = GameFiber.StartNew(RobbersFightingAI);
                            GameFiber.StartNew(CheckForRobbersOutside);

                            fightingPrepared = true;
                        }
                    }

                    // If player talks to cpt wells during fight
                    if (Status is EPacificBankHeistStatus.FightingWithRobbers)
                    {
                        if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                        {
                            if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                            {
                                if (Commander is not null && Commander.IsValid() && Commander.Exists())
                                {
                                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, Commander.Position) < 3f)
                                    {
                                        KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
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
                    if (Status is not EPacificBankHeistStatus.FightingWithRobbers and not EPacificBankHeistStatus.Surrendering)
                    {
                        foreach (var check in PacificBankInsideChecks)
                        {
                            if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                            {
                                if (Vector3.Distance(check, Game.LocalPlayer.Character.Position) < 2.3f)
                                {
                                    Status = EPacificBankHeistStatus.FightingWithRobbers;
                                }
                            }
                        }
                    }

                    // If all hostages rescued break
                    if (SafeHostagesCount == AliveHostagesCount) break;
                    // If surrendered
                    if (SurrenderComplete) break;

                    if (KeyHelpers.IsKeysDown(Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }

                    if (IsSWATFollowing)
                    {
                        if (Game.LocalPlayer.Character.IsShooting)
                        {
                            IsSWATFollowing = false;
                            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                        }
                    }
                }

                // When surrendered
                if (SurrenderComplete) CopFightingAI();

                SetRelationships();

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    if (SafeHostagesCount == AliveHostagesCount)
                    {
                        GameFiber.Wait(3000);
                        break;
                    }

                    if (KeyHelpers.IsKeysDown(Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey))
                    {
                        SwitchSWATFollowing();
                    }

                    if (IsSWATFollowing)
                    {
                        if (Game.LocalPlayer.Character.IsShooting)
                        {
                            IsSWATFollowing = false;
                            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
                        }
                    }

                    if (Game.LocalPlayer.Character is not null && Game.LocalPlayer.Character.IsValid() && Game.LocalPlayer.Character.IsAlive)
                    {
                        if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                        {
                            if (Commander is not null && Commander.IsValid() && Commander.Exists())
                            {
                                if (Vector3.Distance(Game.LocalPlayer.Character.Position, Commander.Position) < 4f)
                                {
                                    KeyHelpers.DisplayKeyHelp("PressToTalk", [Localization.GetString("Commander")], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                    if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                    {
                                        if (!TalkedToCommander2nd)
                                        {
                                            Conversations.Talk(AfterSurrendered);
                                            TalkedToCommander2nd = true;
                                            Status = EPacificBankHeistStatus.FightingWithRobbers;
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
                                    if (!TalkedToCommander2nd)
                                    {
                                        Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"));
                                    }
                                }
                            }
                        }
                    }
                }

                // The end
                Status = EPacificBankHeistStatus.Last;
                CurrentAlarmState = AlarmState.None;

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();

                    if (!evaluatedWithCommander)
                    {
                        if (!Game.LocalPlayer.Character.IsInAnyVehicle(false))
                        {
                            if (Vector3.Distance(Game.LocalPlayer.Character.Position, Commander.Position) < 4f)
                            {
                                KeyHelpers.DisplayKeyHelp("PressToTalkWith", [Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"], Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                                if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
                                {
                                    evaluatedWithCommander = true;
                                    Conversations.Talk(Final);
                                    GameFiber.Wait(5000);
                                    DetermineResults();
                                    GameFiber.Wait(2500);
                                    break;
                                }
                            }
                            else
                            {
                                Hud.DisplayHelp(Localization.GetString("TalkTo", Localization.GetString("Commander"), $"~{COMMANDER_BLIP.GetIconToken()}~"));
                            }
                        }
                    }
                }

                IsCalloutFinished = true;
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
        if (Status is EPacificBankHeistStatus.FightingWithRobbers || (SurrenderComplete && Status is EPacificBankHeistStatus.TalkingToCommander2nd))
        {
            if (TBPool is not null)
            {
                RescuedHostagesTB.Text = $"{SafeHostagesCount.ToString()}/{TotalHostagesCount.ToString()}";
                DiedHostagesTB.Text = $"{(TotalHostagesCount - AliveHostagesCount).ToString()}";
                DiedHostagesTB.Highlight = TotalHostagesCount - AliveHostagesCount is > 0 ? HudColor.Red.GetColor() : HudColor.Green.GetColor();
                TBPool.Draw();
            }
        }
        if (!IsCalloutRunning || Status is EPacificBankHeistStatus.Last)
        {
            Game.FrameRender -= TimerBarsProcess;
        }
    }

    internal override void Update() { }

    internal override void OnDisplayed() { }
    internal override void NotAccepted() { }
}