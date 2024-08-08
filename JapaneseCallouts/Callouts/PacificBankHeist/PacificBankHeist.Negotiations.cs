namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal partial class PacificBankHeist
{
    internal void DetermineInitialDialogue()
    {
        Dialogue.Talk(IntroConversation);
        var intro = new Question().Show(Localization.GetString("SelectAnswerText"), IntroSelection);
        // Try to discuss
        if (intro is 0)
        {
            Dialogue.Talk(DiscussConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4000);
            while (IsCalloutRunning)
            {
                GameFiber.Yield();
                KeyHelpers.DisplayKeyHelp("CallBankRobbers", Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
                if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey)) break;
            }
            Game.HideHelp();
            NegotiationIntro();
        }
        // Fighting
        else if (intro is 1)
        {
            Dialogue.Talk(FightConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4500);
            PrepareFighting();
        }
    }

    internal void NegotiationIntro()
    {
        ToggleMobilePhone(Game.LocalPlayer.Character, true);
        Sounds.PlayPhoneCallingSound(2);
        GameFiber.Wait(5800);
        IsNegotiationSucceed = false;
        Dialogue.Talk(NegotiationIntroConversation);
        var intro = new Question().Show(Localization.GetString("SelectAnswerText"), NegotiationIntroSelection);
        switch (intro)
        {
            default:
            case 0:
                Dialogue.Talk(Negotiation1Conversation);
                var ng1 = new Question().Show(Localization.GetString("SelectAnswerText"), Negotiation1Selection);
                switch (ng1)
                {
                    default:
                    case 0:
                        Dialogue.Talk(Negotiation11Conversation);
                        var ng11 = new Question().Show(Localization.GetString("SelectAnswerText"), Negotiation11Selection);
                        switch (ng11)
                        {
                            default:
                            case 0:
                                Dialogue.Talk(Negotiation111Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Dialogue.Talk(Negotiation112Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Dialogue.Talk(Negotiation113Conversation);
                                IsNegotiationSucceed = true;
                                break;
                        }
                        break;
                    case 1:
                        Request();
                        break;
                    case 2:
                        Dialogue.Talk(Negotiation13Conversation);
                        var ng13 = new Question().Show(Localization.GetString("SelectAnswerText"), Negotiation13Selection);
                        switch (ng13)
                        {
                            default:
                            case 0:
                                Dialogue.Talk(Negotiation131Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Dialogue.Talk(Negotiation111Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                var isSucceed = Main.MT.Next(2) is 0;
                                if (isSucceed)
                                {
                                    Dialogue.Talk(Negotiation133Conversation1);
                                    IsNegotiationSucceed = true;
                                }
                                else
                                {
                                    Dialogue.Talk(Negotiation133Conversation2);
                                    IsNegotiationSucceed = false;
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
                Dialogue.Talk(Negotiation3Conversation);
                var ng3 = new Question().Show(Localization.GetString("SelectAnswerText"), Negotiation3Selection);
                switch (ng3)
                {
                    default:
                    case 0:
                        Dialogue.Talk(Negotiation31Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 1:
                        Dialogue.Talk(Negotiation32Conversation);
                        var ng32 = new Question().Show(Localization.GetString("SelectAnswerText"), Negotiation32Selection);
                        switch (ng32)
                        {
                            default:
                            case 0:
                                Dialogue.Talk(Negotiation321Conversation);
                                IsNegotiationSucceed = true;
                                break;
                            case 1:
                                Dialogue.Talk(Negotiation322Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Dialogue.Talk(Negotiation323Conversation);
                                IsNegotiationSucceed = false;
                                break;
                        }
                        break;
                    case 2:
                        Dialogue.Talk(Negotiation33Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 3:
                        Dialogue.Talk(Negotiation34Conversation);
                        IsNegotiationSucceed = true;
                        break;
                }
                break;
        }

        if (!Wife.Exists())
        {
            Sounds.PlayPhoneBusySound(1);
        }
        ToggleMobilePhone(Game.LocalPlayer.Character, false);

        GameFiber.Wait(2000);
        if (IsNegotiationSucceed)
        {
            NegotiationRobbersSurrender();
            return;
        }
        else
        {
            PrepareFighting();
        }
    }

    internal void Request()
    {
        Dialogue.Talk(RequestConversation);
        int intro = new Question().Show(Localization.GetString("SelectAnswerText"), RequestSelection);
        switch (intro)
        {
            default:
            case 0:
                Dialogue.Talk(Request1Conversation);
                IsNegotiationSucceed = false;
                break;
            case 1:
                Dialogue.Talk(Request2Conversation);
                int req2 = new Question().Show(Localization.GetString("SelectAnswerText"), Request2Selection);
                switch (req2)
                {
                    default:
                    case 0:
                        Dialogue.Talk(Request21Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 1:
                        var isSucceed = Main.MT.Next(2) is 1;
                        // var isSucceed = true;
                        if (isSucceed)
                        {
                            Dialogue.Talk(Request22Conversation2);
                            GetWife();
                            ToggleMobilePhone(Game.LocalPlayer.Character, false);
                            ToggleMobilePhone(Wife, true);
                            Dialogue.Talk(Request22Conversation3);
                            Sounds.PlayPhoneBusySound(1);
                            ToggleMobilePhone(Wife, false);
                            GameFiber.Wait(1500);

                            Dialogue.Talk(Request22Conversation4);
                            Wife.Tasks.FollowNavigationMeshToPosition(WifeCar.GetOffsetPosition(Vector3.RelativeRight * 2f), WifeCar.Heading, 1.9f).WaitForCompletion(15000);
                            Wife.Tasks.EnterVehicle(WifeCar, 5000, 0).WaitForCompletion();
                            GameFiber.StartNew(() =>
                            {
                                WifeDriver.Tasks.CruiseWithVehicle(WifeCar, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds).WaitForCompletion();
                                if (Wife is not null && Wife.IsValid() && Wife.Exists()) Wife.Delete();
                                if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists()) WifeDriver.Delete();
                                if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists()) WifeCar.Delete();
                            });
                            IsNegotiationSucceed = true;
                        }
                        else
                        {
                            Dialogue.Talk(Request22Conversation1);
                            IsNegotiationSucceed = false;
                        }
                        break;
                    case 2:
                        Dialogue.Talk([(Settings.Instance.OfficerName, Localization.GetString("Request231"))]);
                        IsNegotiationSucceed = false;
                        break;
                }
                break;
            case 2:
                Dialogue.Talk(Request3Conversation);
                IsNegotiationSucceed = false;
                break;
        }
    }

    internal void NegotiationRobbersSurrender()
    {
        SurrenderComplete = false;
        Status = EPacificBankHeistStatus.Surrendering;
        GameFiber.StartNew(() =>
        {
            try
            {
                Hud.DisplayNotification($"~b~{Localization.GetString("Commander")}~s~: {Localization.GetString("SeemSurrender")}");
                GameFiber.Wait(5000);
                Hud.DisplayHelp(Localization.GetString("SurrenderHelp"), 50000);
                var allRobbersAtLocation = false;
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();
                    var data = Configuration.RobbersSurrenderingPositions[i];
                    var pos = new Vector3(data.X, data.Y, data.Z);
                    AllRobbers[i].Tasks.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 1f, AnimationFlags.UpperBodyOnly | AnimationFlags.StayInEndFrame | AnimationFlags.SecondaryTask);
                    AllRobbers[i].Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.45f);
                    Natives.SET_PED_CAN_RAGDOLL(AllRobbers[i], false);
                }
                var count = 0;
                while (!allRobbersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < AllRobbers.Count; i++)
                        {
                            var data = Configuration.RobbersSurrenderingPositions[i];
                            var pos = new Vector3(data.X, data.Y, data.Z);
                            AllRobbers[i].Position = pos;
                            AllRobbers[i].Heading = data.Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < AllRobbers.Count; i++)
                    {
                        GameFiber.Yield();
                        var data = Configuration.RobbersSurrenderingPositions[i];
                        var pos = new Vector3(data.X, data.Y, data.Z);
                        if (Vector3.Distance(AllRobbers[i].Position, pos) < 0.8f)
                        {
                            allRobbersAtLocation = true;
                        }
                        else
                        {
                            allRobbersAtLocation = false;
                            break;
                        }
                    }
                    foreach (var swat in AllSWATUnits)
                    {
                        GameFiber.Yield();
                        var robber = AllRobbers[Main.MT.Next(AllRobbers.Count)];
                        Natives.TASK_AIM_GUN_AT_COORD(swat, robber.Position.X, robber.Position.Y, robber.Position.Z, -1, false, false);
                    }
                }
                GameFiber.Wait(1000);
                for (int i = 0; i < AllRobbers.Count; i++)
                {
                    GameFiber.Yield();

                    AllRobbers[i].Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                    Natives.SET_PED_DROPS_WEAPON(AllRobbers[i]);
                    if (AllOfficers.Count > i)
                    {
                        OfficersArresting.Add(AllOfficers[i]);
                        AllOfficers[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[i].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[i].Heading, 1.55f);
                        Natives.SET_PED_CAN_RAGDOLL(AllOfficers[i], false);
                    }
                }
                GameFiber.Wait(1000);

                var allArrestingOfficersAtLocation = false;
                count = 0;
                while (!allArrestingOfficersAtLocation)
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
                            allArrestingOfficersAtLocation = true;
                        }
                        else
                        {
                            OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f), AllRobbers[AllOfficers.IndexOf(OfficersArresting[i])].Heading, 1.55f).WaitForCompletion(500);
                            allArrestingOfficersAtLocation = false;
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
                Main.Logger.Error(e.ToString());
            }
        });
    }

    internal void SwitchAlarmQuestion()
    {
        int alarm = new Question().Show(Localization.GetString("SelectAnswerText"), AlarmSelection);
        if (alarm is 0)
        {
            Dialogue.Talk(AlarmOffConversation);
            CurrentAlarmState = AlarmState.None;
            Dialogue.Talk([(Localization.GetString("Commander"), Localization.GetString("Alarm5"))]);
        }
        else if (alarm is 1)
        {
            CurrentAlarmState = AlarmState.Alarm;
            Dialogue.Talk(AlarmOnConversation);
        }
        KeyHelpers.DisplayKeyHelp("AlarmSwitchKey", Settings.Instance.ToggleBankHeistAlarmSoundKey, Settings.Instance.ToggleBankHeistAlarmSoundModifierKey);
    }
}