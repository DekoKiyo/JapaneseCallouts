namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal class PBHNegotiations
{
    internal PBHNegotiations() { }

    internal void DetermineInitialDialogue(PacificBankHeist instance)
    {
        Conversations.Talk(instance.conversations.IntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(instance.conversations.IntroSelection);
        // Try to discuss
        if (intro is 0)
        {
            Conversations.Talk(instance.conversations.DiscussConversation);
            SwitchAlarmQuestion(instance);
            GameFiber.Wait(4000);
            while (instance.IsCalloutRunning)
            {
                GameFiber.Yield();
                KeyHelpers.DisplayKeyHelp("CallBankRobbers", Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
                if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey)) break;
            }
            Game.HideHelp();
            NegotiationIntro(instance);
        }
        // Fighting
        else if (intro is 1)
        {
            Conversations.Talk(instance.conversations.FightConversation);
            SwitchAlarmQuestion(instance);
            GameFiber.Wait(4500);
            PBHFunctions.PrepareFighting(instance);
        }
    }

    internal void NegotiationIntro(PacificBankHeist instance)
    {
        PBHFunctions.ToggleMobilePhone(instance, Game.LocalPlayer.Character, true);
        Conversations.PlayPhoneCallingSound(2);
        GameFiber.Wait(5800);
        instance.variables.IsNegotiationSucceed = false;
        Conversations.Talk(instance.conversations.NegotiationIntroConversation);
        var intro = Conversations.DisplayAnswersForCallout(instance.conversations.NegotiationIntroSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(instance.conversations.Negotiation1Conversation);
                var ng1 = Conversations.DisplayAnswersForCallout(instance.conversations.Negotiation1Selection);
                switch (ng1)
                {
                    default:
                    case 0:
                        Conversations.Talk(instance.conversations.Negotiation11Conversation);
                        var ng11 = Conversations.DisplayAnswersForCallout(instance.conversations.Negotiation11Selection);
                        switch (ng11)
                        {
                            default:
                            case 0:
                                Conversations.Talk(instance.conversations.Negotiation111Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Conversations.Talk(instance.conversations.Negotiation112Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Conversations.Talk(instance.conversations.Negotiation113Conversation);
                                instance.variables.IsNegotiationSucceed = true;
                                break;
                        }
                        break;
                    case 1:
                        Request(instance);
                        break;
                    case 2:
                        Conversations.Talk(instance.conversations.Negotiation13Conversation);
                        var ng13 = Conversations.DisplayAnswersForCallout(instance.conversations.Negotiation13Selection);
                        switch (ng13)
                        {
                            default:
                            case 0:
                                Conversations.Talk(instance.conversations.Negotiation131Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                            case 1:
                                Conversations.Talk(instance.conversations.Negotiation111Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                            case 2:
                                var isSucceed = Main.MT.Next(2) is 0;
                                if (isSucceed)
                                {
                                    Conversations.Talk(instance.conversations.Negotiation133Conversation1);
                                    instance.variables.IsNegotiationSucceed = true;
                                }
                                else
                                {
                                    Conversations.Talk(instance.conversations.Negotiation133Conversation2);
                                    instance.variables.IsNegotiationSucceed = false;
                                }
                                break;
                        }
                        break;
                }
                break;
            case 1:
                Request(instance);
                break;
            case 2:
                Conversations.Talk(instance.conversations.Negotiation3Conversation);
                var ng3 = Conversations.DisplayAnswersForCallout(instance.conversations.Negotiation3Selection);
                switch (ng3)
                {
                    default:
                    case 0:
                        Conversations.Talk(instance.conversations.Negotiation31Conversation);
                        instance.variables.IsNegotiationSucceed = false;
                        break;
                    case 1:
                        Conversations.Talk(instance.conversations.Negotiation32Conversation);
                        var ng32 = Conversations.DisplayAnswersForCallout(instance.conversations.Negotiation32Selection);
                        switch (ng32)
                        {
                            default:
                            case 0:
                                Conversations.Talk(instance.conversations.Negotiation321Conversation);
                                instance.variables.IsNegotiationSucceed = true;
                                break;
                            case 1:
                                Conversations.Talk(instance.conversations.Negotiation322Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                            case 2:
                                Conversations.Talk(instance.conversations.Negotiation323Conversation);
                                instance.variables.IsNegotiationSucceed = false;
                                break;
                        }
                        break;
                    case 2:
                        Conversations.Talk(instance.conversations.Negotiation33Conversation);
                        instance.variables.IsNegotiationSucceed = false;
                        break;
                    case 3:
                        Conversations.Talk(instance.conversations.Negotiation34Conversation);
                        instance.variables.IsNegotiationSucceed = true;
                        break;
                }
                break;
        }

        if (!instance.variables.Wife.Exists())
        {
            Conversations.PlayPhoneBusySound(1);
        }
        PBHFunctions.ToggleMobilePhone(instance, Game.LocalPlayer.Character, false);

        GameFiber.Wait(2000);
        if (instance.variables.IsNegotiationSucceed)
        {
            NegotiationRobbersSurrender(instance);
            return;
        }
        else
        {
            PBHFunctions.PrepareFighting(instance);
        }
    }

    internal void Request(PacificBankHeist instance)
    {
        Conversations.Talk(instance.conversations.RequestConversation);
        int intro = Conversations.DisplayAnswersForCallout(instance.conversations.RequestSelection);
        switch (intro)
        {
            default:
            case 0:
                Conversations.Talk(instance.conversations.Request1Conversation);
                instance.variables.IsNegotiationSucceed = false;
                break;
            case 1:
                Conversations.Talk(instance.conversations.Request2Conversation);
                int req2 = Conversations.DisplayAnswersForCallout(instance.conversations.Request2Selection);
                switch (req2)
                {
                    default:
                    case 0:
                        Conversations.Talk(instance.conversations.Request21Conversation);
                        instance.variables.IsNegotiationSucceed = false;
                        break;
                    case 1:
                        var isSucceed = Main.MT.Next(2) is 1;
                        // var isSucceed = true;
                        if (isSucceed)
                        {
                            Conversations.Talk(instance.conversations.Request22Conversation2);
                            PBHFunctions.GetWife(instance);
                            PBHFunctions.ToggleMobilePhone(instance, Game.LocalPlayer.Character, false);
                            PBHFunctions.ToggleMobilePhone(instance, instance.variables.Wife, true);
                            Conversations.Talk(instance.conversations.Request22Conversation3);
                            Conversations.PlayPhoneBusySound(1);
                            PBHFunctions.ToggleMobilePhone(instance, instance.variables.Wife, false);
                            GameFiber.Wait(1500);

                            Conversations.Talk(instance.conversations.Request22Conversation4);
                            instance.variables.Wife.Tasks.FollowNavigationMeshToPosition(instance.variables.WifeCar.GetOffsetPosition(Vector3.RelativeRight * 2f), instance.variables.WifeCar.Heading, 1.9f).WaitForCompletion(15000);
                            instance.variables.Wife.Tasks.EnterVehicle(instance.variables.WifeCar, 5000, 0).WaitForCompletion();
                            GameFiber.StartNew(() =>
                            {
                                instance.variables.WifeDriver.Tasks.CruiseWithVehicle(instance.variables.WifeCar, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds).WaitForCompletion();
                                if (instance.variables.Wife is not null && instance.variables.Wife.IsValid() && instance.variables.Wife.Exists()) instance.variables.Wife.Delete();
                                if (instance.variables.WifeDriver is not null && instance.variables.WifeDriver.IsValid() && instance.variables.WifeDriver.Exists()) instance.variables.WifeDriver.Delete();
                                if (instance.variables.WifeCar is not null && instance.variables.WifeCar.IsValid() && instance.variables.WifeCar.Exists()) instance.variables.WifeCar.Delete();
                            });
                            instance.variables.IsNegotiationSucceed = true;
                        }
                        else
                        {
                            Conversations.Talk(instance.conversations.Request22Conversation1);
                            instance.variables.IsNegotiationSucceed = false;
                        }
                        break;
                    case 2:
                        Conversations.Talk([(Settings.OfficerName, Localization.GetString("Request231"))]);
                        instance.variables.IsNegotiationSucceed = false;
                        break;
                }
                break;
            case 2:
                Conversations.Talk(instance.conversations.Request3Conversation);
                instance.variables.IsNegotiationSucceed = false;
                break;
        }
    }

    internal void NegotiationRobbersSurrender(PacificBankHeist instance)
    {
        instance.variables.SurrenderComplete = false;
        instance.variables.Status = EPacificBankHeistStatus.Surrendering;
        GameFiber.StartNew(() =>
        {
            try
            {
                Hud.DisplayNotification($"~b~{Localization.GetString("Commander")}~s~: {Localization.GetString("SeemSurrender")}");
                GameFiber.Wait(5000);
                Hud.DisplayHelp(Localization.GetString("SurrenderHelp"), 50000);
                var allRobbersAtLocation = false;
                for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                {
                    GameFiber.Yield();
                    var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                    var pos = new Vector3(data.X, data.Y, data.Z);
                    instance.variables.AllRobbers[i].Tasks.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 1f, AnimationFlags.UpperBodyOnly | AnimationFlags.StayInEndFrame | AnimationFlags.SecondaryTask);
                    instance.variables.AllRobbers[i].Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.45f);
                    Natives.SET_PED_CAN_RAGDOLL(instance.variables.AllRobbers[i], false);
                }
                var count = 0;
                while (!allRobbersAtLocation)
                {
                    GameFiber.Yield();
                    count++;
                    if (count >= 10000)
                    {
                        for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                        {
                            var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                            var pos = new Vector3(data.X, data.Y, data.Z);
                            instance.variables.AllRobbers[i].Position = pos;
                            instance.variables.AllRobbers[i].Heading = data.Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                    {
                        GameFiber.Yield();
                        var data = XmlManager.PacificBankHeistConfig.RobbersSurrenderingPositions[i];
                        var pos = new Vector3(data.X, data.Y, data.Z);
                        if (Vector3.Distance(instance.variables.AllRobbers[i].Position, pos) < 0.8f)
                        {
                            allRobbersAtLocation = true;
                        }
                        else
                        {
                            allRobbersAtLocation = false;
                            break;
                        }
                    }
                    foreach (var swat in instance.variables.AllSWATUnits)
                    {
                        GameFiber.Yield();
                        var robber = instance.variables.AllRobbers[Main.MT.Next(instance.variables.AllRobbers.Count)];
                        Natives.TASK_AIM_GUN_AT_COORD(swat, robber.Position.X, robber.Position.Y, robber.Position.Z, -1, false, false);
                    }
                }
                GameFiber.Wait(1000);
                for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                {
                    GameFiber.Yield();

                    instance.variables.AllRobbers[i].Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                    Natives.SET_PED_DROPS_WEAPON(instance.variables.AllRobbers[i]);
                    if (instance.variables.AllOfficers.Count > i)
                    {
                        instance.variables.OfficersArresting.Add(instance.variables.AllOfficers[i]);
                        instance.variables.AllOfficers[i].Tasks.FollowNavigationMeshToPosition(instance.variables.AllRobbers[i].GetOffsetPosition(Vector3.RelativeBack * 0.7f), instance.variables.AllRobbers[i].Heading, 1.55f);
                        Natives.SET_PED_CAN_RAGDOLL(instance.variables.AllOfficers[i], false);
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
                        for (int i = 0; i < instance.variables.OfficersArresting.Count; i++)
                        {
                            instance.variables.OfficersArresting[i].Position = instance.variables.AllRobbers[instance.variables.AllOfficers.IndexOf(instance.variables.OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f);
                            instance.variables.OfficersArresting[i].Heading = instance.variables.AllRobbers[instance.variables.AllOfficers.IndexOf(instance.variables.OfficersArresting[i])].Heading;
                        }
                        break;
                    }
                    for (int i = 0; i < instance.variables.OfficersArresting.Count; i++)
                    {
                        if (Vector3.Distance(instance.variables.OfficersArresting[i].Position, instance.variables.AllRobbers[instance.variables.AllOfficers.IndexOf(instance.variables.OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f)) < 0.8f)
                        {
                            allArrestingOfficersAtLocation = true;
                        }
                        else
                        {
                            instance.variables.OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(instance.variables.AllRobbers[instance.variables.AllOfficers.IndexOf(instance.variables.OfficersArresting[i])].GetOffsetPosition(Vector3.RelativeBack * 0.7f), instance.variables.AllRobbers[instance.variables.AllOfficers.IndexOf(instance.variables.OfficersArresting[i])].Heading, 1.55f).WaitForCompletion(500);
                            allArrestingOfficersAtLocation = false;
                            break;
                        }
                    }
                }
                foreach (var swat in instance.variables.AllSWATUnits)
                {
                    swat.Tasks.Clear();
                }
                for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                {
                    instance.variables.AllRobbers[i].Tasks.PlayAnimation("mp_arresting", "idle", 8f, AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask | AnimationFlags.Loop);
                    instance.variables.AllRobbers[i].Tasks.FollowNavigationMeshToPosition(instance.variables.AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), instance.variables.AllPoliceVehicles[i].Heading, 1.58f);
                    instance.variables.OfficersArresting[i].Tasks.FollowNavigationMeshToPosition(instance.variables.AllPoliceVehicles[i].GetOffsetPosition(Vector3.RelativeLeft * 2f), instance.variables.AllPoliceVehicles[i].Heading, 1.55f);
                }
                GameFiber.Wait(5000);
                instance.variables.SurrenderComplete = true;
                GameFiber.Wait(12000);
                for (int i = 0; i < instance.variables.AllRobbers.Count; i++)
                {
                    instance.variables.AllRobbers[i].BlockPermanentEvents = true;
                    instance.variables.AllRobbers[i].Tasks.EnterVehicle(instance.variables.AllPoliceVehicles[i], 11000, 1);
                    instance.variables.OfficersArresting[i].BlockPermanentEvents = true;
                    instance.variables.OfficersArresting[i].Tasks.EnterVehicle(instance.variables.AllPoliceVehicles[i], 11000, -1);
                }
                GameFiber.Wait(11100);
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        });
    }

    internal void SwitchAlarmQuestion(PacificBankHeist instance)
    {
        int alarm = Conversations.DisplayAnswersForCallout(instance.conversations.AlarmSelection);
        if (alarm is 0)
        {
            Conversations.Talk(instance.conversations.AlarmOffConversation);
            instance.variables.CurrentAlarmState = AlarmState.None;
            Conversations.Talk([(Localization.GetString("Commander"), Localization.GetString("Alarm5"))]);
        }
        else if (alarm is 1)
        {
            instance.variables.CurrentAlarmState = AlarmState.Alarm;
            Conversations.Talk(instance.conversations.AlarmOnConversation);
        }
        KeyHelpers.DisplayKeyHelp("AlarmSwitchKey", Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey);
    }
}