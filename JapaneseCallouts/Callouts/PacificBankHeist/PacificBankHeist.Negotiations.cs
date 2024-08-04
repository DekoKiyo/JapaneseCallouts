namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal partial class PacificBankHeist
{
    internal void DetermineInitialDialogue()
    {
        Conversations.Talk(IntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(IntroSelection);
        // Try to discuss
        if (intro is 0)
        {
            Conversations.Talk(DiscussConversation);
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
            Conversations.Talk(FightConversation);
            SwitchAlarmQuestion();
            GameFiber.Wait(4500);
            PrepareFighting();
        }
    }

    internal void NegotiationIntro()
    {
        ToggleMobilePhone(Game.LocalPlayer.Character, true);
        Conversations.PlayPhoneCallingSound(2);
        GameFiber.Wait(5800);
        IsNegotiationSucceed = false;
        Conversations.Talk(NegotiationIntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(NegotiationIntroSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Negotiation1Conversation);
                var ng1 = Conversations.DisplayAnswersForCallout(Negotiation1Selection);
                switch (ng1)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation11Conversation);
                        var ng11 = Conversations.DisplayAnswersForCallout(Negotiation11Selection);
                        switch (ng11)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation111Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation112Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation113Conversation);
                                IsNegotiationSucceed = true;
                                break;
                        }
                        break;
                    case 1:
                        Request();
                        break;
                    case 2:
                        Conversations.Talk(Negotiation13Conversation);
                        var ng13 = Conversations.DisplayAnswersForCallout(Negotiation13Selection);
                        switch (ng13)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation131Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation111Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                var isSucceed = Main.MT.Next(2) is 0;
                                if (isSucceed)
                                {
                                    Conversations.Talk(Negotiation133Conversation1);
                                    IsNegotiationSucceed = true;
                                }
                                else
                                {
                                    Conversations.Talk(Negotiation133Conversation2);
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
                Conversations.Talk(Negotiation3Conversation);
                var ng3 = Conversations.DisplayAnswersForCallout(Negotiation3Selection);
                switch (ng3)
                {
                    default:
                    case 0:
                        Conversations.Talk(Negotiation31Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 1:
                        Conversations.Talk(Negotiation32Conversation);
                        var ng32 = Conversations.DisplayAnswersForCallout(Negotiation32Selection);
                        switch (ng32)
                        {
                            default:
                            case 0:
                                Conversations.Talk(Negotiation321Conversation);
                                IsNegotiationSucceed = true;
                                break;
                            case 1:
                                Conversations.Talk(Negotiation322Conversation);
                                IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Conversations.Talk(Negotiation323Conversation);
                                IsNegotiationSucceed = false;
                                break;
                        }
                        break;
                    case 2:
                        Conversations.Talk(Negotiation33Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 3:
                        Conversations.Talk(Negotiation34Conversation);
                        IsNegotiationSucceed = true;
                        break;
                }
                break;
        }

        if (!Wife.Exists())
        {
            Conversations.PlayPhoneBusySound(1);
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
        Conversations.Talk(RequestConversation);
        int intro = Conversations.DisplayAnswersForCallout(RequestSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(Request1Conversation);
                IsNegotiationSucceed = false;
                break;
            case 1:
                Conversations.Talk(Request2Conversation);
                int req2 = Conversations.DisplayAnswersForCallout(Request2Selection);
                switch (req2)
                {
                    default:
                    case 0:
                        Conversations.Talk(Request21Conversation);
                        IsNegotiationSucceed = false;
                        break;
                    case 1:
                        var isSucceed = Main.MT.Next(2) is 1;
                        // var isSucceed = true;
                        if (isSucceed)
                        {
                            Conversations.Talk(Request22Conversation2);
                            GetWife();
                            ToggleMobilePhone(Game.LocalPlayer.Character, false);
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
                            IsNegotiationSucceed = true;
                        }
                        else
                        {
                            Conversations.Talk(Request22Conversation1);
                            IsNegotiationSucceed = false;
                        }
                        break;
                    case 2:
                        Conversations.Talk([(Settings.Instance.OfficerName, Localization.GetString("Request231"))]);
                        IsNegotiationSucceed = false;
                        break;
                }
                break;
            case 2:
                Conversations.Talk(Request3Conversation);
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
        int alarm = Conversations.DisplayAnswersForCallout(AlarmSelection);
        if (alarm is 0)
        {
            Conversations.Talk(AlarmOffConversation);
            CurrentAlarmState = AlarmState.None;
            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("Alarm5"))]);
        }
        else if (alarm is 1)
        {
            CurrentAlarmState = AlarmState.Alarm;
            Conversations.Talk(AlarmOnConversation);
        }
        KeyHelpers.DisplayKeyHelp("AlarmSwitchKey", Settings.Instance.ToggleBankHeistAlarmSoundKey, Settings.Instance.ToggleBankHeistAlarmSoundModifierKey);
    }
}