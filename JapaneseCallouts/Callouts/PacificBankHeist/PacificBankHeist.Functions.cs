namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal partial class PacificBankHeist
{
    internal void ClearUnrelatedEntities()
    {
        foreach (var ped in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
        {
            GameFiber.Yield();
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped != Game.LocalPlayer.Character && !ped.CreatedByTheCallingPlugin)
                {
                    if (!CalloutEntities.Contains(ped))
                    {
                        if (Vector3.Distance(ped.Position, CalloutPosition) < 50f)
                        {
                            ped.Delete();
                        }
                    }
                }
            }
        }
        foreach (var vehicle in World.GetEntities(CalloutPosition, 50f, GetEntitiesFlags.ConsiderGroundVehicles).Cast<Vehicle>())
        {
            GameFiber.Yield();
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                if (vehicle != Game.LocalPlayer.Character.CurrentVehicle && !vehicle.CreatedByTheCallingPlugin)
                {
                    if (!CalloutEntities.Contains(vehicle))
                    {
                        if (Vector3.Distance(vehicle.Position, CalloutPosition) < 50f)
                        {
                            vehicle.Delete();
                        }
                    }
                }
            }
        }
    }

    internal void SpawnVehicles()
    {
        foreach (var p in Configuration.PoliceCruiserPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. Configuration.PoliceCruisers]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in Configuration.PoliceTransportPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. Configuration.PoliceTransporters]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in Configuration.RiotPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. Configuration.PoliceRiots]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllPoliceVehicles.Add(vehicle);
                AllRiot.Add(vehicle);
                CalloutEntities.Add(vehicle);

                var blip = new Blip(vehicle)
                {
                    Sprite = RIOT_BLIP,
                    Color = HudColor.Michael.GetColor(),
                };
                if (blip is not null && blip.IsValid() && blip.Exists())
                {
                    RiotBlips.Add(blip);
                }
            }
        }
        foreach (var p in Configuration.AmbulancePositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. Configuration.Ambulances]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                AllAmbulance.Add(vehicle);
                CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in Configuration.FiretruckPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. Configuration.Firetrucks]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                CalloutEntities.Add(vehicle);
            }
        }
    }

    internal void PlaceBarriers()
    {
        foreach (var p in Configuration.BarrierPositions)
        {
            GameFiber.Yield();
            var barrier = new RObject(BarrierModel, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPositionFrozen = true,
                IsPersistent = true
            };
            var barrierPed = new Ped(barrier.Position)
            {
                IsVisible = false,
                IsPositionFrozen = true,
                BlockPermanentEvents = true,
                IsPersistent = true
            };
            if (barrier is not null && barrier.IsValid() && barrier.Exists())
            {
                AllBarriersEntities.Add(barrier);
            }
            if (barrierPed is not null && barrierPed.IsValid() && barrierPed.Exists())
            {
                AllBarriersEntities.Add(barrierPed);
            }
        }
    }

    internal void SpawnOfficers(EWeather weather)
    {
        foreach (var p in Configuration.LeftSittingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. Configuration.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);

                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }

        foreach (var p in Configuration.RightLookingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. Configuration.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT_LOOKING, 1f, AnimationFlags.StayInEndFrame);

                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }

        foreach (var p in Configuration.RightSittingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.PoliceSWATModels]);
            var swat = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (swat is not null && swat.IsValid() && swat.Exists())
            {
                Natives.SET_PED_KEEP_TASK(swat, true);
                swat.SetOutfit(data);
                Functions.SetPedAsCop(swat);
                Functions.SetCopAsBusy(swat, true);
                swat.GiveWeapon([.. Configuration.SWATWeapons], true);
                swat.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 1f, AnimationFlags.StayInEndFrame);

                AllSWATUnits.Add(swat);
                CalloutEntities.Add(swat);
            }
        }

        foreach (var p in Configuration.AimingOfficerPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.PoliceOfficerModels]);
            var officer = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (officer is not null && officer.IsValid() && officer.Exists())
            {
                Natives.SET_PED_KEEP_TASK(officer, true);
                officer.SetOutfit(data);
                Functions.SetPedAsCop(officer);
                Functions.SetCopAsBusy(officer, true);
                officer.GiveWeapon([.. Configuration.OfficerWeapons], true);
                var aimPoint = Vector3.Distance(officer.Position, BankDoorPositions[0]) < Vector3.Distance(officer.Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
                Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);

                AllOfficers.Add(officer);
                AllAimingOfficers.Add(officer);
                CalloutEntities.Add(officer);
            }
        }

        foreach (var p in Configuration.StandingOfficerPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.PoliceOfficerModels]);
            var officer = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanBeTargetted = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (officer is not null && officer.IsValid() && officer.Exists())
            {
                Natives.SET_PED_KEEP_TASK(officer, true);
                officer.SetOutfit(data);
                Functions.SetPedAsCop(officer);
                Functions.SetCopAsBusy(officer, true);
                officer.GiveWeapon([.. Configuration.OfficerWeapons], true);

                AllOfficers.Add(officer);
                AllStandingOfficers.Add(officer);
                CalloutEntities.Add(officer);
            }
        }

        var cP = Configuration.CommanderPosition;
        var cData = CalloutHelpers.SelectPed(weather, [.. Configuration.CommanderModels]);
        Commander = new Ped(cData.Model, new(cP.X, cP.Y, cP.Z), cP.Heading)
        {
            BlockPermanentEvents = true,
            IsPersistent = true,
            IsInvincible = true,
            MaxHealth = cData.Health,
            Health = cData.Health,
            Armor = cData.Armor,
        };
        if (Commander is not null && Commander.IsValid() && Commander.Exists())
        {
            Commander.SetOutfit(cData);
            Functions.SetPedCantBeArrestedByPlayer(Commander, true);

            CommanderBlip = Commander.AttachBlip();
            if (CommanderBlip is not null && CommanderBlip.IsValid() && CommanderBlip.Exists())
            {
                CommanderBlip.Sprite = COMMANDER_BLIP;
                CommanderBlip.Color = Color.Green;
            }
            CalloutEntities.Add(Commander);
        }
    }

    internal void SpawnNegotiationRobbers(EWeather weather)
    {
        for (int i = 0; i < Configuration.RobbersNegotiationPositions.Length; i++)
        {
            GameFiber.Yield();
            var rnP = Configuration.RobbersNegotiationPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.RobberModels]);
            var ped = new Ped(data.Model, new(rnP.X, rnP.Y, rnP.Z), rnP.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. Configuration.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                AllRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
    }

    internal void SpawnSneakyRobbers(EWeather weather)
    {
        for (int i = 0; i < Configuration.RobbersSneakPosition.Length; i++)
        {
            GameFiber.Yield();
            var rsP = Configuration.RobbersSneakPosition[i];
            if (Main.MT.Next(5) is >= 2)
            {
                var data = CalloutHelpers.SelectPed(weather, [.. Configuration.RobberModels]);
                var ped = new Ped(data.Model, new(rsP.X, rsP.Y, rsP.Z), rsP.Heading)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                    CanAttackFriendlies = false,
                    MaxHealth = data.Health,
                    Health = data.Health,
                    Armor = data.Armor,
                };
                if (ped is not null && ped.IsValid() && ped.Exists())
                {
                    ped.SetOutfit(data);
                    Functions.SetPedCantBeArrestedByPlayer(ped, true);
                    ped.GiveWeapon([.. Configuration.RobbersWeapons], false);
                    Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                    ped.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, rsP.IsRight ? SWAT_ANIMATION_RIGHT : SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);
                    AllSneakRobbers.Add(ped);
                    CalloutEntities.Add(ped);
                }
            }
            else
            {
                AllSneakRobbers.Add(null);
            }
        }
    }

    internal void SpawnEMS(EWeather weather)
    {
        foreach (var p in Configuration.ParamedicPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.ParamedicModels]);
            var paramedic = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (paramedic is not null && paramedic.IsValid() && paramedic.Exists())
            {
                paramedic.SetOutfit(data);
                CalloutEntities.Add(paramedic);
            }
        }
        foreach (var p in Configuration.FirefighterPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.FirefighterModels]);
            var firefighter = new Ped(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (firefighter is not null && firefighter.IsValid() && firefighter.Exists())
            {
                firefighter.SetOutfit(data);
                CalloutEntities.Add(firefighter);
            }
        }
    }

    internal void SpawnHostages(EWeather weather)
    {
        var positions = Configuration.HostagePositions.Shuffle();
        for (int i = 0; i < HostageCount; i++)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.HostageModels]);
            var pos = new Vector3(positions[i].X, positions[i].Y, positions[i].Z);
            var hostage = new Ped(data.Model, pos, Main.MT.Next(0, 360))
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (hostage is not null && hostage.IsValid() && hostage.Exists())
            {
                Natives.SET_PED_CAN_RAGDOLL(hostage, false);
                hostage.SetOutfit(data);
                AllHostages.Add(hostage);
                SpawnedHostages.Add(hostage);
                CalloutEntities.Add(hostage);
                hostage.Tasks.PlayAnimation(HOSTAGE_ANIMATION_DICTIONARY, HOSTAGE_ANIMATION_KNEELING, 1f, AnimationFlags.Loop);
                GameFiber.Yield();
                AliveHostagesCount++;
                TotalHostagesCount++;
            }
        }
    }

    internal void SpawnAssaultRobbers(EWeather weather)
    {
        var nrP = Configuration.NormalRobbersPositions;
        for (int i = 0; i < nrP.Length; i++)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.RobberModels]);
            var ped = new Ped(data.Model, new(nrP[i].X, nrP[i].Y, nrP[i].Z), nrP[i].Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. Configuration.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. Configuration.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                AllRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
    }

    internal void SpawnVaultRobbers(EWeather weather)
    {
        for (int i = 0; i < Configuration.RobbersInVaultPositions.Length; i++)
        {
            GameFiber.Yield();
            var rvP = Configuration.RobbersInVaultPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. Configuration.RobberModels]);
            var ped = new Ped(data.Model, new(rvP.X, rvP.Y, rvP.Z), rvP.Heading)
            {
                IsPersistent = true,
                BlockPermanentEvents = true,
                CanAttackFriendlies = false,
                MaxHealth = data.Health,
                Health = data.Health,
                Armor = data.Armor,
            };
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                ped.SetOutfit(data);
                Functions.SetPedCantBeArrestedByPlayer(ped, true);
                ped.GiveWeapon([.. Configuration.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. Configuration.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                AllVaultRobbers.Add(ped);
                CalloutEntities.Add(ped);
            }
        }
        GameFiber.StartNew(HandleVaultRobbers);
    }

    internal void MakeNearbyPedsFlee()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();

            foreach (var ped in World.GetEntities(CalloutPosition, 150f, GetEntitiesFlags.ConsiderAllPeds | GetEntitiesFlags.ExcludePlayerPed | GetEntitiesFlags.ExcludePoliceOfficers).Cast<Ped>())
            {
                GameFiber.Yield();
                if (CalloutEntities.Contains(ped)) continue;
                if (ped is not null && ped.IsValid() && ped.Exists())
                {
                    if (!(ped == Game.LocalPlayer.Character || ped.CreatedByTheCallingPlugin || UltimateBackupFunctions.IsUltimateBackupCop(ped)))
                    {
                        if (ped.IsInAnyVehicle(false))
                        {
                            if (ped.CurrentVehicle.Exists())
                            {
                                ped.CurrentVehicle.Delete();
                            }
                        }
                        ped.Delete();
                    }
                }
            }
        }
    }

    internal void SneakyRobbersAI()
    {
        while (IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                foreach (var robber in AllSneakRobbers)
                {
                    GameFiber.Yield();
                    if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsAlive)
                    {
                        if (!FightingSneakRobbers.Contains(robber))
                        {
                            var rsP = Configuration.RobbersSneakPosition;
                            var index = AllSneakRobbers.IndexOf(robber);
                            var pos = new Vector3(rsP[index].X, rsP[index].Y, rsP[index].Z);
                            if (Vector3.Distance(robber.Position, pos) > 0.7f)
                            {
                                robber.Tasks.Clear();
                                robber.Tasks.FollowNavigationMeshToPosition(pos, rsP[index].Heading, 2f);
                            }
                            else
                            {
                                if (rsP[index].IsRight)
                                {
                                    if (!Natives.IS_ENTITY_PLAYING_ANIM(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 3))
                                    {
                                        robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_RIGHT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                    }
                                }
                                else
                                {
                                    if (!Natives.IS_ENTITY_PLAYING_ANIM(robber, SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 3))
                                    {
                                        robber.Tasks.PlayAnimation(SWAT_ANIMATION_DICTIONARY, SWAT_ANIMATION_LEFT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                    }
                                }
                            }

                            var nearPeds = robber.GetNearbyPeds(3);
                            if (nearPeds.Length > 0)
                            {
                                foreach (var ped in nearPeds)
                                {
                                    GameFiber.Yield();
                                    if (ped is not null && ped.IsValid() && ped.Exists() && ped.IsAlive)
                                    {
                                        if (ped.RelationshipGroup == Game.LocalPlayer.Character.RelationshipGroup || ped.RelationshipGroup == RelationshipGroup.Cop || ped.RelationshipGroup == PoliceRG)
                                        {
                                            if (Vector3.Distance(ped.Position, robber.Position) < 3.9f)
                                            {
                                                if (Math.Abs(ped.Position.Z - robber.Position.Z) < 0.9f)
                                                {
                                                    SneakyRobberFight(robber, ped);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    internal void HandleHostages()
    {
        var waitCountForceAttack = 0;
        var enterAmbulanceCount = 0;
        var deleteSafeHostageCount = 0;
        var subtitleCount = 0;

        Ped closeHostage = null;
        while (IsCalloutRunning)
        {
            try
            {
                waitCountForceAttack++;
                enterAmbulanceCount++;

                GameFiber.Yield();
                if (waitCountForceAttack > 250)
                {
                    waitCountForceAttack = 0;
                }
                if (enterAmbulanceCount > 101)
                {
                    enterAmbulanceCount = 101;
                }

                foreach (var hostage in SpawnedHostages)
                {
                    GameFiber.Yield();
                    if (hostage is not null && hostage.IsValid() && hostage.Exists() && hostage.IsAlive)
                    {
                        if (Functions.IsPedGettingArrested(hostage) || Functions.IsPedArrested(hostage))
                        {
                            SpawnedHostages[SpawnedHostages.IndexOf(hostage)] = hostage.ClonePed();
                        }
                        hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                        if (!Game.LocalPlayer.Character.IsShooting)
                        {
                            if (Vector3.Distance(hostage.Position, Game.LocalPlayer.Character.Position) < 1.45f)
                            {
                                if (KeyHelpers.IsKeysDownRightNow(Settings.Instance.HostageRescueKey, Settings.Instance.HostageRescueModifierKey))
                                {
                                    var direction = hostage.Position - Game.LocalPlayer.Character.Position;
                                    direction.Normalize();
                                    IsRescuingHostage = true;
                                    Game.LocalPlayer.Character.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(direction)).WaitForCompletion(1200);
                                    hostage.RelationshipGroup = RelationshipGroup.Cop;
                                    Modules.Conversations.Talk([(Settings.Instance.OfficerName, Localization.GetString("RescueHostage"))], false);
                                    Game.LocalPlayer.Character.Tasks.PlayAnimation("random@rescue_hostage", "bystander_helping_girl_loop", 1.5f, AnimationFlags.None).WaitForCompletion(3000);

                                    if (hostage.IsAlive)
                                    {
                                        hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_get_up", 0.9f, AnimationFlags.None).WaitForCompletion(6000);
                                        Game.LocalPlayer.Character.Tasks.ClearImmediately();
                                        if (hostage.IsAlive)
                                        {
                                            var data = Configuration.HostageSafePosition;
                                            var pos = new Vector3(data.X, data.Y, data.Z);
                                            hostage.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f);
                                            RescuedHostages.Add(hostage);
                                            SpawnedHostages.Remove(hostage);
                                        }
                                        else
                                        {
                                            Game.LocalPlayer.Character.Tasks.ClearImmediately();
                                        }
                                    }
                                    else
                                    {
                                        Game.LocalPlayer.Character.Tasks.ClearImmediately();
                                    }
                                    IsRescuingHostage = false;
                                }
                                else
                                {
                                    subtitleCount++;
                                    closeHostage = hostage;
                                    if (subtitleCount > 5)
                                    {
                                        KeyHelpers.DisplayKeyHelp("BankHeistReleaseHostage", Settings.Instance.HostageRescueKey, Settings.Instance.HostageRescueModifierKey);
                                    }
                                }
                            }
                            else
                            {
                                if (hostage == closeHostage)
                                {
                                    subtitleCount = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        SpawnedHostages.Remove(hostage);
                        AliveHostagesCount--;
                    }
                }

                foreach (var rescued in RescuedHostages)
                {
                    GameFiber.Yield();
                    if (rescued is not null && rescued.IsValid() && rescued.Exists() && rescued.IsAlive)
                    {
                        if (SpawnedHostages.Contains(rescued))
                        {
                            SpawnedHostages.Remove(rescued);
                        }

                        var data = Configuration.HostageSafePosition;
                        var pos = new Vector3(data.X, data.Y, data.Z);

                        if (Vector3.Distance(rescued.Position, pos) < 3f)
                        {
                            SafeHostages.Add(rescued);
                            SafeHostagesCount++;
                        }
                        if (Functions.IsPedGettingArrested(rescued) || Functions.IsPedArrested(rescued))
                        {
                            RescuedHostages[RescuedHostages.IndexOf(rescued)] = rescued.ClonePed();
                        }
                        rescued.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f).WaitForCompletion(200);

                        if (waitCountForceAttack > 150)
                        {
                            var nearest = rescued.GetNearbyPeds(2)[0];
                            if (nearest == Game.LocalPlayer.Character)
                            {
                                nearest = rescued.GetNearbyPeds(2)[1];
                            }
                            if (AllRobbers.Contains(nearest))
                            {
                                nearest.Tasks.FightAgainst(rescued);
                                waitCountForceAttack = 0;
                            }
                        }
                    }
                    else
                    {
                        RescuedHostages.Remove(rescued);
                        AliveHostagesCount--;
                    }
                }

                foreach (var safeH in SafeHostages)
                {
                    if (safeH is not null && safeH.IsValid() && safeH.Exists())
                    {
                        if (RescuedHostages.Contains(safeH))
                        {
                            RescuedHostages.Remove(safeH);
                        }

                        safeH.IsInvincible = true;
                        if (!safeH.IsInAnyVehicle(true))
                        {
                            if (enterAmbulanceCount > 100)
                            {
                                if (AllAmbulance[0].IsSeatFree(2))
                                {
                                    safeH.Tasks.EnterVehicle(AllAmbulance[0], 2);
                                }
                                else if (AllAmbulance[0].IsSeatFree(1))
                                {
                                    safeH.Tasks.EnterVehicle(AllAmbulance[0], 1);
                                }
                                else
                                {
                                    AllAmbulance[0].GetPedOnSeat(2).Delete();
                                    safeH.Tasks.EnterVehicle(AllAmbulance[0], 2);
                                }
                                enterAmbulanceCount = 0;
                            }
                        }
                        else
                        {
                            deleteSafeHostageCount++;
                            if (deleteSafeHostageCount > 50)
                            {
                                if (Vector3.Distance(Game.LocalPlayer.Character.Position, safeH.Position) > 22f)
                                {
                                    if (safeH.IsInAnyVehicle(false))
                                    {
                                        safeH.Delete();
                                        deleteSafeHostageCount = 0;
                                        Natives.SET_VEHICLE_DOORS_SHUT(AllAmbulance[0], true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        SafeHostages.Remove(safeH);
                    }
                }
            }
            catch
            {
                continue;
            }
        }
    }

    internal void HandleOpenBackRiotVan()
    {
        var cooldown = 0;
        while (IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (cooldown > 0) cooldown--;

                foreach (var riot in AllRiot)
                {
                    GameFiber.Yield();
                    if (riot is not null && riot.IsValid() && riot.Exists())
                    {
                        if (Vector3.Distance(riot.GetOffsetPosition(Vector3.RelativeBack * 4f), Game.LocalPlayer.Character.Position) < 2f)
                        {
                            if (KeyHelpers.IsKeysDownRightNow(Settings.Instance.EnterRiotVanKey, Settings.Instance.EnterRiotVanModifierKey))
                            {
                                if (cooldown > 0)
                                {
                                    Hud.DisplayNotification(Localization.GetString("GearRunOut"));
                                }
                                else
                                {
                                    cooldown = 3000;
                                    Game.LocalPlayer.Character.Tasks.EnterVehicle(riot, 1).WaitForCompletion();
                                    Game.LocalPlayer.Character.Armor = 150;
                                    Game.LocalPlayer.Character.Health = Game.LocalPlayer.Character.MaxHealth;
                                    Game.LocalPlayer.Character.GiveWeapon([.. Configuration.WeaponInRiot], false);
                                    Natives.PLAY_SOUND_FRONTEND(-1, "PURCHASE", "HUD_LIQUOR_STORE_SOUNDSET", true);
                                    Game.LocalPlayer.Character.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
                                    FightingPacksUsed++;
                                }
                            }
                            else
                            {
                                if (cooldown is 0)
                                {
                                    KeyHelpers.DisplayKeyHelp("EnterRiot", [$"~{RIOT_BLIP.GetIconToken()}~"], Settings.Instance.EnterRiotVanKey, Settings.Instance.EnterRiotVanModifierKey, 500);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    private static bool AudioStateChanged = false;
    internal void HandleCalloutAudio()
    {
        AudioStateChanged = false;
        while (IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (IsAlarmEnabled)
                {
                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, CalloutPosition) > 70f)
                    {
                        IsAlarmEnabled = false;
                        CurrentAlarmState = AlarmState.None;
                        BankBlip.IsRouteEnabled = true;
                        AudioStateChanged = true;
                    }
                }
                else
                {
                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, CalloutPosition) < 55f)
                    {
                        IsAlarmEnabled = true;
                        CurrentAlarmState = AlarmState.Alarm;
                        BankBlip.IsRouteEnabled = false;
                        AudioStateChanged = true;
                    }
                }

                if (KeyHelpers.IsKeysDown(Settings.Instance.ToggleBankHeistAlarmSoundKey, Settings.Instance.ToggleBankHeistAlarmSoundModifierKey))
                {
                    if (CurrentAlarmState is not AlarmState.None)
                    {
                        CurrentAlarmState++;
                    }
                    else
                    {
                        CurrentAlarmState = AlarmState.Alarm;
                    }
                    AudioStateChanged = true;
                }

                if (AudioStateChanged)
                {
                    switch (CurrentAlarmState)
                    {
                        default:
                        case AlarmState.None:
                            BankAlarm.Stop();
                            break;
                        case AlarmState.Alarm:
                            BankAlarm.PlayLooping();
                            break;
                    }
                    AudioStateChanged = false;
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    internal void HandleVaultRobbers()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            try
            {
                if (Vector3.Distance(Game.LocalPlayer.Character.Position, OutsideBankVault) < 4f)
                {
                    GameFiber.Wait(2000);

                    World.SpawnExplosion(new(252.2609f, 225.3824f, 101.6835f), 2, 0.2f, true, false, 0.6f);
                    CurrentAlarmState = AlarmState.Alarm;
                    GameFiber.Wait(900);
                    foreach (var robber in AllVaultRobbers)
                    {
                        robber.Tasks.FollowNavigationMeshToPosition(OutsideBankVault, robber.Heading, 2f).WaitForCompletion(500);
                    }
                    GameFiber.Wait(700);
                    foreach (var robber in AllVaultRobbers)
                    {
                        GameFiber.Yield();
                        robber.Tasks.FightAgainstClosestHatedTarget(30f);
                        AllRobbers.Add(robber);
                    }
                    break;
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    internal void ToggleMobilePhone(Ped ped, bool toggle)
    {
        if (toggle)
        {
            Natives.SET_PED_CAN_SWITCH_WEAPON(ped, false);
            ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_UNARMED"), -1, true);
            MobilePhone = new(PhoneModel, new(0, 0, 0));
            var boneIndex = Natives.GET_PED_BONE_INDEX(ped, (int)PedBoneId.RightPhHand);
            Natives.ATTACH_ENTITY_TO_ENTITY(MobilePhone, ped, boneIndex, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, true);
            ped.Tasks.PlayAnimation("cellphone@", "cellphone_call_listen_base", 1.3f, AnimationFlags.Loop | AnimationFlags.UpperBodyOnly | AnimationFlags.SecondaryTask);
        }
        else
        {
            Natives.SET_PED_CAN_SWITCH_WEAPON(ped, true);
            ped.Tasks.Clear();
            if (GameFiber.CanSleepNow)
            {
                GameFiber.Wait(800);
            }
            if (MobilePhone is not null && MobilePhone.IsValid() && MobilePhone.Exists()) MobilePhone.Delete();
        }
    }

    internal void GetWife()
    {
        Game.LocalPlayer.Character.IsPositionFrozen = true;
        var vData = CalloutHelpers.Select([.. Configuration.PoliceCruisers]);
        var data = Configuration.WifePosition;
        WifeCar = new(vData.Model, new(data.X, data.Y, data.Z), data.Heading)
        {
            IsPersistent = true,
            IsSirenOn = true,
        };
        if (WifeCar is not null && WifeCar.IsValid() && WifeCar.Exists())
        {
            WifeDriver = WifeCar.CreateRandomDriver();
            if (WifeDriver is not null && WifeDriver.IsValid() && WifeDriver.Exists())
            {
                WifeDriver.IsPersistent = true;
                WifeDriver.BlockPermanentEvents = true;
                var wData = CalloutHelpers.Select([.. Configuration.WifeModels]);
                Wife = new Ped(wData.Model, Vector3.Zero, 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                };
                if (Wife is not null && Wife.IsValid() && Wife.Exists())
                {
                    Wife.WarpIntoVehicle(WifeCar, 1);
                    CalloutEntities.Add(Wife);
                    CalloutEntities.Add(WifeDriver);
                    CalloutEntities.Add(WifeCar);

                    var destination = new Vector3(Configuration.WifeVehicleDestination.X, Configuration.WifeVehicleDestination.Y, Configuration.WifeVehicleDestination.Z);
                    WifeDriver.Tasks.DriveToPosition(destination, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                    GameFiber.WaitWhile(() => Vector3.Distance(WifeCar.Position, destination) >= 6f);
                    Wife.Tasks.LeaveVehicle(LeaveVehicleFlags.None);
                    Wife.Tasks.FollowNavigationMeshToPosition(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeRight * 1.5f), Game.LocalPlayer.Character.Heading, 1.9f).WaitForCompletion(60000);
                    Game.LocalPlayer.Character.IsPositionFrozen = false;
                }
            }
        }
    }

    internal void PrepareFighting()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            KeyHelpers.DisplayKeyHelp("BankHeistMoveIn", Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.Instance.SpeakWithThePersonKey, Settings.Instance.SpeakWithThePersonModifierKey))
            {
                Modules.Conversations.Talk([(Settings.Instance.OfficerName, Localization.GetString("MoveIn"))]);
                KeyHelpers.DisplayKeyHelp("SWATFollowing", Settings.Instance.SWATFollowKey, Settings.Instance.SWATFollowModifierKey);
                Status = EPacificBankHeistStatus.FightingWithRobbers;
                break;
            }
        }
    }

    private static Entity entityPlayerAimingAtSneakyRobber = null;
    internal void SneakyRobberFight(Ped sneak, Ped nearestPed)
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                FightingSneakRobbers.Add(sneak);
                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!nearestPed.Exists() || !nearestPed.IsAlive) break;
                    if (!sneak.Exists() || !sneak.IsAlive) break;

                    if (Vector3.Distance(nearestPed.Position, sneak.Position) is > 5.1f or < 1.7f) break;

                    try
                    {
                        Natives.GET_ENTITY_PLAYER_IS_FREE_AIMING_AT(Game.LocalPlayer, out Entity entityHandle); // Stores the entity the player is aiming at in the uint provided in the second parameter.
                        entityPlayerAimingAtSneakyRobber = entityHandle;
                    }
                    catch // (Exception e)
                    {
                        // Logger.Error(e.ToString());
                    }

                    if (entityPlayerAimingAtSneakyRobber == sneak) break;
                    if (IsRescuingHostage) break;
                }

                if (sneak is not null && sneak.IsValid() && sneak.Exists())
                {
                    sneak.Tasks.FightAgainstClosestHatedTarget(15f);
                    sneak.RelationshipGroup = RobbersRG;
                }

                while (IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!sneak.Exists()) break;
                    if (!nearestPed.Exists()) break;

                    Natives.STOP_CURRENT_PLAYING_AMBIENT_SPEECH(sneak);

                    if (nearestPed.IsDead)
                    {
                        foreach (var hostage in SpawnedHostages)
                        {
                            if (Math.Abs(hostage.Position.Z - sneak.Position.Z) < 0.6f)
                            {
                                if (Vector3.Distance(hostage.Position, sneak.Position) < 14f)
                                {
                                    int waitCount = 0;
                                    while (hostage.IsAlive)
                                    {
                                        GameFiber.Yield();
                                        waitCount++;
                                        if (waitCount > 450)
                                        {
                                            hostage.Kill();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    if (sneak.IsDead) break;
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
            finally
            {
                FightingSneakRobbers.Remove(sneak);
            }
        });
    }

    internal void DetermineResults()
    {
        foreach (var robber in AllRobbers)
        {
            if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsDead)
            {
                DiedRobbersCount++;
            }
        }
        Hud.DisplayNotification(Localization.GetString("BankHeistReportText", $"{SafeHostagesCount.ToString()}", $"{(TotalHostagesCount - AliveHostagesCount).ToString()}", $"{DiedRobbersCount.ToString()}"), Localization.GetString("BankHeistReportTitle"), TotalHostagesCount - AliveHostagesCount is < 3 ? Localization.GetString("BankHeistReportSubtitle") : "", "mphud", "mp_player_ready");
        if (TotalHostagesCount == AliveHostagesCount)
        {
            var bigMessage = new BigMessageThread(true);
            bigMessage.MessageInstance.ShowColoredShard(Localization.GetString("Congratulations"), Localization.GetString("NoDiedHostage"), HudColor.Yellow, HudColor.MenuGrey);
            Common.PlaySound("Mission_Pass_Notify", "DLC_HEISTS_GENERAL_FRONTEND_SOUNDS");
        }
    }

    private static Entity entityPlayerAimingAt = null;
    internal void RobbersFightingAI()
    {
        while (IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (Status is EPacificBankHeistStatus.FightingWithRobbers)
                {
                    foreach (var robber in AllRobbers)
                    {
                        GameFiber.Yield();
                        if (robber.Exists())
                        {
                            var distance = Vector3.Distance(robber.Position, PacificBankInsideChecks[0]) < Vector3.Distance(robber.Position, PacificBankInsideChecks[1])
                                ? Vector3.Distance(robber.Position, PacificBankInsideChecks[0])
                                : Vector3.Distance(robber.Position, PacificBankInsideChecks[1]);
                            if (distance < 16.5f) distance = 16.5f;
                            else if (distance > 21f) distance = 21f;
                            Natives.REGISTER_HATED_TARGETS_AROUND_PED(robber, distance);
                            robber.Tasks.FightAgainstClosestHatedTarget(distance);
                        }
                    }

                    try
                    {
                        // Stores the entity the player is aiming at in the uint provided in the second parameter.
                        entityPlayerAimingAt = Natives.GET_ENTITY_PLAYER_IS_FREE_AIMING_AT(Game.LocalPlayer, out Entity entityHandle) ? entityHandle : null;
                    }
                    catch { }

                    if (entityPlayerAimingAt is not null && AllRobbers.Contains(entityPlayerAimingAt))
                    {
                        var pedAimingAt = (Ped)entityPlayerAimingAt;
                        pedAimingAt.Tasks.FightAgainst(Game.LocalPlayer.Character);
                    }
                    GameFiber.Sleep(3000);
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    internal void CopFightingAI()
    {
        while (IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (Status is EPacificBankHeistStatus.FightingWithRobbers)
                {
                    if (OfficersTargetsToShoot.Count > 0)
                    {
                        if (OfficersTargetsToShoot[0].Exists())
                        {
                            if (OfficersTargetsToShoot[0].IsAlive)
                            {
                                foreach (var cop in AllOfficers)
                                {
                                    cop.Tasks.FightAgainst(OfficersTargetsToShoot[0]);
                                }
                            }
                            else
                            {
                                OfficersTargetsToShoot.RemoveAt(0);
                            }
                        }
                        else
                        {
                            OfficersTargetsToShoot.RemoveAt(0);
                        }
                    }
                    else
                    {
                        CopsReturnToLocation();
                    }
                }
                if (Status is EPacificBankHeistStatus.FightingWithRobbers || IsSWATFollowing)
                {
                    foreach (var swat in AllSWATUnits)
                    {
                        GameFiber.Yield();
                        if (swat is not null && swat.IsValid() && swat.Exists())
                        {
                            if (IsSWATFollowing)
                            {
                                if (Math.Abs(Game.LocalPlayer.Character.Position.Z - swat.Position.Z) > 1f)
                                {
                                    swat.Tasks.FollowNavigationMeshToPosition(Game.LocalPlayer.Character.Position, Game.LocalPlayer.Character.Heading, 1.6f, 1f);
                                }
                                else
                                {
                                    swat.Tasks.FollowNavigationMeshToPosition(Game.LocalPlayer.Character.Position, Game.LocalPlayer.Character.Heading, 1.6f, 4f);
                                }
                            }
                            else
                            {
                                Natives.REGISTER_HATED_TARGETS_AROUND_PED(swat, 60f);
                                swat.Tasks.FightAgainstClosestHatedTarget(60f);
                            }
                        }
                    }
                    GameFiber.Sleep(4000);
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error(e.ToString());
            }
        }
    }

    internal void CheckForRobbersOutside()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            if (Status is EPacificBankHeistStatus.FightingWithRobbers)
            {
                foreach (var location in BankDoorPositions)
                {
                    foreach (var robber in World.GetEntities(location, 1.6f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
                    {
                        if (robber.Exists())
                        {
                            if (Vector3.Distance(robber.Position, location) < 1.5f)
                            {
                                if (robber.IsAlive)
                                {
                                    if (AllRobbers.Contains(robber))
                                    {
                                        if (!OfficersTargetsToShoot.Contains(robber))
                                        {
                                            OfficersTargetsToShoot.Add(robber);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    internal void CopsReturnToLocation()
    {
        for (int i = 0; i < AllStandingOfficers.Count; i++)
        {
            var officer = AllStandingOfficers[i];
            var data = Configuration.StandingOfficerPositions[i];
            var pos = new Vector3(data.X, data.Y, data.Z);
            if (officer is not null && officer.IsValid() && officer.Exists() && officer.IsAlive)
            {
                if (Vector3.Distance(officer.Position, officer) > 0.5f)
                {
                    officer.BlockPermanentEvents = true;
                    officer.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 2f);
                }
            }
        }

        for (int i = 0; i < AllAimingOfficers.Count; i++)
        {
            var officer = AllAimingOfficers[i];
            var data = Configuration.AimingOfficerPositions[i];
            var pos = new Vector3(data.X, data.Y, data.Z);
            if (officer is not null && officer.IsValid() && officer.Exists() && officer.IsAlive)
            {
                if (Vector3.Distance(officer.Position, pos) > 0.5f)
                {
                    officer.BlockPermanentEvents = true;
                    officer.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 2f);
                }
                else
                {
                    var aimPoint = Vector3.Distance(officer.Position, BankDoorPositions[0]) < Vector3.Distance(officer.Position, BankDoorPositions[1]) ? BankDoorPositions[0] : BankDoorPositions[1];
                    Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);
                }
            }
        }
    }

    internal void SwitchSWATFollowing()
    {
        IsSWATFollowing = !IsSWATFollowing;
        if (IsSWATFollowing)
        {
            Hud.DisplayHelp(Localization.GetString("SWATIsFollowing"));
        }
        else
        {
            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
        }
    }

    internal void SetRelationships()
    {
        RobbersRG.SetRelationshipWith(SneakRobbersRG, Relationship.Respect);
        RobbersRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Hate);
        RobbersRG.SetRelationshipWith(PoliceRG, Relationship.Hate);
        RobbersRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);

        SneakRobbersRG.SetRelationshipWith(RobbersRG, Relationship.Respect);
        SneakRobbersRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Hate);
        SneakRobbersRG.SetRelationshipWith(PoliceRG, Relationship.Hate);
        SneakRobbersRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);

        RelationshipGroup.Cop.SetRelationshipWith(RobbersRG, Relationship.Hate);
        RelationshipGroup.Cop.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        RelationshipGroup.Cop.SetRelationshipWith(PoliceRG, Relationship.Respect);
        RelationshipGroup.Cop.SetRelationshipWith(HostageRG, Relationship.Respect);

        PoliceRG.SetRelationshipWith(RobbersRG, Relationship.Hate);
        PoliceRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        PoliceRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Respect);
        PoliceRG.SetRelationshipWith(HostageRG, Relationship.Respect);

        HostageRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        HostageRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Respect);
        HostageRG.SetRelationshipWith(PoliceRG, Relationship.Respect);
    }

    internal void UnlockDoorsAlways()
    {
        while (IsCalloutRunning)
        {
            GameFiber.Yield();
            foreach (var (pos, hash) in Doors)
            {
                Natives.SET_LOCKED_UNSTREAMED_IN_DOOR_OF_TYPE(hash, pos.X, pos.Y, pos.Z, false, 0f, 0f, 0f);
            }
        }
    }
}