// namespace JapaneseCallouts.Callouts;

// [CalloutInfo("[JPC] Yakuza Activity", CalloutProbability.Low)]
// internal class YakuzaActivity : CalloutBase
// {
//     private Ped shooter, victim, caller;
//     private int sentenceNum = 0;
//     private bool arrived = false;
//     private Vector3[] Positions =
//     [
//         new(),
//     ];

//     private string[] sentence =
//     [
//         Localization.GetString("Yakuza1-Caller1"),
//         Localization.GetString("Yakuza1-Officer1"),
//         Localization.GetString("Yakuza1-Caller2"),
//         Localization.GetString("Yakuza1-Officer2"),
//         Localization.GetString("Yakuza1-Caller3"),
//     ];

//     internal override void Setup()
//     {
//         CalloutPosition = Positions[Main.MersenneTwister.Next(Positions.Length)];

//         try
//         {
//             shooter = new(x => x.IsPed, CalloutPosition.Around(50f))
//             {
//                 BlockPermanentEvents = true,
//                 IsPersistent = true,
//             };
//             if (shooter is not null && shooter.IsValid() && shooter.Exists())
//             {
//                 shooter.Inventory.GiveNewWeapon(WeaponHash.Pistol, 5000, true);
//                 shooter.Tasks.Wander();
//             }

//             caller = new(x => x.IsPed, CalloutPosition)
//             {
//                 BlockPermanentEvents = true,
//                 IsPersistent = true,
//             };

//             victim = new(x => x.IsPed, CalloutPosition.Around2D(10f))
//             {
//                 BlockPermanentEvents = true,
//                 IsPersistent = true,
//                 Health = 0,
//             };

//             DeleteEntities().AddRange([shooter, caller, victim]);
//         }
//         catch (Exception e)
//         {
//             Logger.Error(e.ToString());
//         }

//         CalloutMessage = Localization.GetString("YakuzaActivity");

//         ShowCalloutAreaBlipBeforeAccepting(CalloutPosition, 12f);
//         Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT JP_CRIME_GANG_ACTIVITY_INCIDENT IN_OR_ON_POSITION UNITS_RESPOND_CODE_99", CalloutPosition);

//         if (Main.IsCalloutInterfaceAPIExist)
//         {
//             CalloutInterfaceAPIFunctions.SendMessage(this, Localization.GetString("YakuzaActivityDesc"));
//         }
//     }

//     internal override void OnDisplayed()
//     {
//     }

//     internal override void Accepted()
//     {
//         HudExtensions.DisplayNotification(Localization.GetString("YakuzaActivityDesc"));

//         try
//         {
//             HudExtensions.DisplayNotification(Localization.GetString("YakuzaTalkCaller"));
//         }
//         catch (Exception e)
//         {
//             Logger.Error(e.ToString());
//         }
//     }

//     internal override void Update()
//     {
//         if (Game.LocalPlayer.Character.DistanceTo(caller) < 25f && !arrived)
//         {
//             if (Game.LocalPlayer.Character.IsOnFoot)
//             {
//                 if (Settings.SpeakWithThePersonModifierKey is Keys.None)
//                 {
//                     HudExtensions.DisplayNotification(Localization.GetString("PressToTalk", $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~"));
//                 }
//                 else
//                 {
//                     HudExtensions.DisplayNotification(Localization.GetString("PressToTalk", $"~{Settings.SpeakWithThePersonKey.GetInstructionalId()}~ ~+~ ~{Settings.SpeakWithThePersonModifierKey.GetInstructionalId()}~"));
//                 }

//                 Functions.PlayScannerAudio("JP_ATTENTION_GENERIC JP_OFFICERS_ARRIVED_ON_SCENE");
//                 HudExtensions.DisplaySubtitle(Localization.GetString("YakuzaHere"));
//                 caller.Face(Game.LocalPlayer.Character);
//                 arrived = true;
//             }
//             else
//             {
//                 HudExtensions.DisplayHelp(Localization.GetString("LeaveTheVehicle"));
//             }
//         }

//         if (KeyExtensions.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
//         {
//             if (arrived && Game.LocalPlayer.Character.DistanceTo(caller) < 2f && sentenceNum < sentence.Length)
//             {
//                 caller.Face(Game.LocalPlayer.Character);
//                 HudExtensions.DisplaySubtitle($"{sentence[sentenceNum]} ({sentenceNum}/{sentence.Length})");
//                 sentenceNum++;
//             }
//         }
//     }

//     internal override void EndCallout(bool notAccepted = false, bool isPlayerDead = false)
//     {
//     }
// }