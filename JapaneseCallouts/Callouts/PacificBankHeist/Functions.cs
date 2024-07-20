namespace JapaneseCallouts.Callouts.PacificBankHeist;

internal static class PBHFunctions
{
    internal static void ClearUnrelatedEntities(PacificBankHeist instance)
    {
        foreach (var ped in World.GetEntities(instance.CalloutPosition, 50f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
        {
            GameFiber.Yield();
            if (ped is not null && ped.IsValid() && ped.Exists())
            {
                if (ped != Game.LocalPlayer.Character && !ped.CreatedByTheCallingPlugin)
                {
                    if (!instance.CalloutEntities.Contains(ped))
                    {
                        if (Vector3.Distance(ped.Position, instance.CalloutPosition) < 50f)
                        {
                            ped.Delete();
                        }
                    }
                }
            }
        }
        foreach (var vehicle in World.GetEntities(instance.CalloutPosition, 50f, GetEntitiesFlags.ConsiderGroundVehicles).Cast<Vehicle>())
        {
            GameFiber.Yield();
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                if (vehicle != Game.LocalPlayer.Character.CurrentVehicle && !vehicle.CreatedByTheCallingPlugin)
                {
                    if (!instance.CalloutEntities.Contains(vehicle))
                    {
                        if (Vector3.Distance(vehicle.Position, instance.CalloutPosition) < 50f)
                        {
                            vehicle.Delete();
                        }
                    }
                }
            }
        }
    }

    internal static void SpawnVehicles(PacificBankHeist instance)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.PoliceCruiserPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceCruisers]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                instance.variables.AllPoliceVehicles.Add(vehicle);
                instance.CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.PoliceTransportPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceTransporters]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                instance.variables.AllPoliceVehicles.Add(vehicle);
                instance.CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.RiotPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceRiots]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                instance.variables.AllPoliceVehicles.Add(vehicle);
                instance.variables.AllRiot.Add(vehicle);
                instance.CalloutEntities.Add(vehicle);

                var blip = new Blip(vehicle)
                {
                    Sprite = PBHConstants.RIOT_BLIP,
                    Color = HudColor.Michael.GetColor(),
                };
                if (blip is not null && blip.IsValid() && blip.Exists())
                {
                    instance.variables.RiotBlips.Add(blip);
                }
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.AmbulancePositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.Ambulances]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                instance.variables.AllAmbulance.Add(vehicle);
                instance.CalloutEntities.Add(vehicle);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.FiretruckPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.Firetrucks]);
            var vehicle = new Vehicle(data.Model, new(p.X, p.Y, p.Z), p.Heading)
            {
                IsPersistent = true,
                IsSirenOn = true,
                IsSirenSilent = true,
            };
            if (vehicle is not null && vehicle.IsValid() && vehicle.Exists())
            {
                vehicle.ApplyTexture(data);
                instance.CalloutEntities.Add(vehicle);
            }
        }
    }

    internal static void PlaceBarriers(PacificBankHeist instance)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.BarrierPositions)
        {
            GameFiber.Yield();
            var barrier = new RObject(PBHConstants.BarrierModel, new(p.X, p.Y, p.Z), p.Heading)
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
                instance.variables.AllBarriersEntities.Add(barrier);
            }
            if (barrierPed is not null && barrierPed.IsValid() && barrierPed.Exists())
            {
                instance.variables.AllBarriersEntities.Add(barrierPed);
            }
        }
    }

    internal static void SpawnOfficers(PacificBankHeist instance, EWeather weather)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.LeftSittingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
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
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);

                instance.variables.AllSWATUnits.Add(swat);
                instance.CalloutEntities.Add(swat);
            }
        }

        foreach (var p in XmlManager.PacificBankHeistConfig.RightLookingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
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
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_RIGHT_LOOKING, 1f, AnimationFlags.StayInEndFrame);

                instance.variables.AllSWATUnits.Add(swat);
                instance.CalloutEntities.Add(swat);
            }
        }

        foreach (var p in XmlManager.PacificBankHeistConfig.RightSittingSWATPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceSWATModels]);
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
                swat.GiveWeapon([.. XmlManager.PacificBankHeistConfig.SWATWeapons], true);
                swat.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_RIGHT, 1f, AnimationFlags.StayInEndFrame);

                instance.variables.AllSWATUnits.Add(swat);
                instance.CalloutEntities.Add(swat);
            }
        }

        foreach (var p in XmlManager.PacificBankHeistConfig.AimingOfficerPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceOfficerModels]);
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
                officer.GiveWeapon([.. XmlManager.PacificBankHeistConfig.OfficerWeapons], true);
                var aimPoint = Vector3.Distance(officer.Position, PBHConstants.BankDoorPositions[0]) < Vector3.Distance(officer.Position, PBHConstants.BankDoorPositions[1]) ? PBHConstants.BankDoorPositions[0] : PBHConstants.BankDoorPositions[1];
                Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);

                instance.variables.AllOfficers.Add(officer);
                instance.variables.AllAimingOfficers.Add(officer);
                instance.CalloutEntities.Add(officer);
            }
        }

        foreach (var p in XmlManager.PacificBankHeistConfig.StandingOfficerPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.PoliceOfficerModels]);
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
                officer.GiveWeapon([.. XmlManager.PacificBankHeistConfig.OfficerWeapons], true);

                instance.variables.AllOfficers.Add(officer);
                instance.variables.AllStandingOfficers.Add(officer);
                instance.CalloutEntities.Add(officer);
            }
        }

        var cP = XmlManager.PacificBankHeistConfig.CommanderPosition;
        var cData = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.CommanderModels]);
        instance.variables.Commander = new Ped(cData.Model, new(cP.X, cP.Y, cP.Z), cP.Heading)
        {
            BlockPermanentEvents = true,
            IsPersistent = true,
            IsInvincible = true,
            MaxHealth = cData.Health,
            Health = cData.Health,
            Armor = cData.Armor,
        };
        if (instance.variables.Commander is not null && instance.variables.Commander.IsValid() && instance.variables.Commander.Exists())
        {
            instance.variables.Commander.SetOutfit(cData);
            Functions.SetPedCantBeArrestedByPlayer(instance.variables.Commander, true);

            instance.variables.CommanderBlip = instance.variables.Commander.AttachBlip();
            if (instance.variables.CommanderBlip is not null && instance.variables.CommanderBlip.IsValid() && instance.variables.CommanderBlip.Exists())
            {
                instance.variables.CommanderBlip.Sprite = PBHConstants.COMMANDER_BLIP;
                instance.variables.CommanderBlip.Color = Color.Green;
            }
            instance.CalloutEntities.Add(instance.variables.Commander);
        }
    }

    internal static void SpawnNegotiationRobbers(PacificBankHeist instance, EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersNegotiationPositions.Count; i++)
        {
            GameFiber.Yield();
            var rnP = XmlManager.PacificBankHeistConfig.RobbersNegotiationPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
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
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                instance.variables.AllRobbers.Add(ped);
                instance.CalloutEntities.Add(ped);
            }
        }
    }

    internal static void SpawnSneakyRobbers(PacificBankHeist instance, EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersSneakPosition.Count; i++)
        {
            GameFiber.Yield();
            var rsP = XmlManager.PacificBankHeistConfig.RobbersSneakPosition[i];
            if (Main.MT.Next(5) is >= 2)
            {
                var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
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
                    ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                    Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                    ped.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, rsP.IsRight ? PBHConstants.SWAT_ANIMATION_RIGHT : PBHConstants.SWAT_ANIMATION_LEFT, 1f, AnimationFlags.StayInEndFrame);
                    instance.variables.AllSneakRobbers.Add(ped);
                    instance.CalloutEntities.Add(ped);
                }
            }
            else
            {
                instance.variables.AllSneakRobbers.Add(null);
            }
        }
    }

    internal static void SpawnEMS(PacificBankHeist instance, EWeather weather)
    {
        foreach (var p in XmlManager.PacificBankHeistConfig.ParamedicPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.ParamedicModels]);
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
                instance.CalloutEntities.Add(paramedic);
            }
        }
        foreach (var p in XmlManager.PacificBankHeistConfig.FirefighterPositions)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.FirefighterModels]);
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
                instance.CalloutEntities.Add(firefighter);
            }
        }
    }

    internal static void SpawnHostages(PacificBankHeist instance, EWeather weather)
    {
        var positions = XmlManager.PacificBankHeistConfig.HostagePositions.Shuffle();
        for (int i = 0; i < instance.variables.HostageCount; i++)
        {
            GameFiber.Yield();
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.HostageModels]);
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
                instance.variables.AllHostages.Add(hostage);
                instance.variables.SpawnedHostages.Add(hostage);
                instance.CalloutEntities.Add(hostage);
                hostage.Tasks.PlayAnimation(PBHConstants.HOSTAGE_ANIMATION_DICTIONARY, PBHConstants.HOSTAGE_ANIMATION_KNEELING, 1f, AnimationFlags.Loop);
                GameFiber.Yield();
                instance.variables.AliveHostagesCount++;
                instance.variables.TotalHostagesCount++;
            }
        }
    }

    internal static void SpawnAssaultRobbers(PacificBankHeist instance, EWeather weather)
    {
        var nrP = XmlManager.PacificBankHeistConfig.NormalRobbersPositions;
        for (int i = 0; i < nrP.Count; i++)
        {
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
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
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                instance.variables.AllRobbers.Add(ped);
                instance.CalloutEntities.Add(ped);
            }
        }
    }

    internal static void SpawnVaultRobbers(PacificBankHeist instance, EWeather weather)
    {
        for (int i = 0; i < XmlManager.PacificBankHeistConfig.RobbersInVaultPositions.Count; i++)
        {
            GameFiber.Yield();
            var rvP = XmlManager.PacificBankHeistConfig.RobbersInVaultPositions[i];
            var data = CalloutHelpers.SelectPed(weather, [.. XmlManager.PacificBankHeistConfig.RobberModels]);
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
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersThrowableWeapons], false);
                ped.GiveWeapon([.. XmlManager.PacificBankHeistConfig.RobbersWeapons], false);
                Natives.SET_PED_COMBAT_ABILITY(ped, 3);
                instance.variables.AllVaultRobbers.Add(ped);
                instance.CalloutEntities.Add(ped);
            }
        }
        GameFiber.StartNew(() => HandleVaultRobbers(instance));
    }

    internal static void MakeNearbyPedsFlee(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            GameFiber.Yield();

            foreach (var ped in World.GetEntities(instance.CalloutPosition, 150f, GetEntitiesFlags.ConsiderAllPeds | GetEntitiesFlags.ExcludePlayerPed | GetEntitiesFlags.ExcludePoliceOfficers).Cast<Ped>())
            {
                GameFiber.Yield();
                if (instance.CalloutEntities.Contains(ped)) continue;
                if (ped is not null && ped.IsValid() && ped.Exists())
                {
                    if (ped != Game.LocalPlayer.Character && !ped.CreatedByTheCallingPlugin)
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

    internal static void SneakyRobbersAI(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                foreach (var robber in instance.variables.AllSneakRobbers)
                {
                    GameFiber.Yield();
                    if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsAlive)
                    {
                        if (!instance.variables.FightingSneakRobbers.Contains(robber))
                        {
                            var rsP = XmlManager.PacificBankHeistConfig.RobbersSneakPosition;
                            var index = instance.variables.AllSneakRobbers.IndexOf(robber);
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
                                    if (!Natives.IS_ENTITY_PLAYING_ANIM(robber, PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_RIGHT, 3))
                                    {
                                        robber.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_RIGHT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
                                    }
                                }
                                else
                                {
                                    if (!Natives.IS_ENTITY_PLAYING_ANIM(robber, PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_LEFT, 3))
                                    {
                                        robber.Tasks.PlayAnimation(PBHConstants.SWAT_ANIMATION_DICTIONARY, PBHConstants.SWAT_ANIMATION_LEFT, 2f, AnimationFlags.StayInEndFrame).WaitForCompletion(20);
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
                                        if (ped.RelationshipGroup == Game.LocalPlayer.Character.RelationshipGroup || ped.RelationshipGroup == RelationshipGroup.Cop || ped.RelationshipGroup == PBHConstants.PoliceRG)
                                        {
                                            if (Vector3.Distance(ped.Position, robber.Position) < 3.9f)
                                            {
                                                if (Math.Abs(ped.Position.Z - robber.Position.Z) < 0.9f)
                                                {
                                                    SneakyRobberFight(instance, robber, ped);
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

    internal static void HandleHostages(PacificBankHeist instance)
    {
        var waitCountForceAttack = 0;
        var enterAmbulanceCount = 0;
        var deleteSafeHostageCount = 0;
        var subtitleCount = 0;

        Ped closeHostage = null;
        while (instance.IsCalloutRunning)
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

                foreach (var hostage in instance.variables.SpawnedHostages)
                {
                    GameFiber.Yield();
                    if (hostage is not null && hostage.IsValid() && hostage.Exists() && hostage.IsAlive)
                    {
                        if (Functions.IsPedGettingArrested(hostage) || Functions.IsPedArrested(hostage))
                        {
                            instance.variables.SpawnedHostages[instance.variables.SpawnedHostages.IndexOf(hostage)] = hostage.ClonePed();
                        }
                        hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_idle", 1f, AnimationFlags.Loop);
                        if (!Game.LocalPlayer.Character.IsShooting)
                        {
                            if (Vector3.Distance(hostage.Position, Game.LocalPlayer.Character.Position) < 1.45f)
                            {
                                if (KeyHelpers.IsKeysDownRightNow(Settings.HostageRescueKey, Settings.HostageRescueModifierKey))
                                {
                                    var direction = hostage.Position - Game.LocalPlayer.Character.Position;
                                    direction.Normalize();
                                    instance.variables.IsRescuingHostage = true;
                                    Game.LocalPlayer.Character.Tasks.AchieveHeading(MathHelper.ConvertDirectionToHeading(direction)).WaitForCompletion(1200);
                                    hostage.RelationshipGroup = RelationshipGroup.Cop;
                                    Conversations.Talk([(Settings.OfficerName, Localization.GetString("RescueHostage"))], false);
                                    Game.LocalPlayer.Character.Tasks.PlayAnimation("random@rescue_hostage", "bystander_helping_girl_loop", 1.5f, AnimationFlags.None).WaitForCompletion(3000);

                                    if (hostage.IsAlive)
                                    {
                                        hostage.Tasks.PlayAnimation("random@arrests", "kneeling_arrest_get_up", 0.9f, AnimationFlags.None).WaitForCompletion(6000);
                                        Game.LocalPlayer.Character.Tasks.ClearImmediately();
                                        if (hostage.IsAlive)
                                        {
                                            var data = XmlManager.PacificBankHeistConfig.HostageSafePosition;
                                            var pos = new Vector3(data.X, data.Y, data.Z);
                                            hostage.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f);
                                            instance.variables.RescuedHostages.Add(hostage);
                                            instance.variables.SpawnedHostages.Remove(hostage);
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
                                    instance.variables.IsRescuingHostage = false;
                                }
                                else
                                {
                                    subtitleCount++;
                                    closeHostage = hostage;
                                    if (subtitleCount > 5)
                                    {
                                        KeyHelpers.DisplayKeyHelp("BankHeistReleaseHostage", Settings.HostageRescueKey, Settings.HostageRescueModifierKey);
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
                        instance.variables.SpawnedHostages.Remove(hostage);
                        instance.variables.AliveHostagesCount--;
                    }
                }

                foreach (var rescued in instance.variables.RescuedHostages)
                {
                    GameFiber.Yield();
                    if (rescued is not null && rescued.IsValid() && rescued.Exists() && rescued.IsAlive)
                    {
                        if (instance.variables.SpawnedHostages.Contains(rescued))
                        {
                            instance.variables.SpawnedHostages.Remove(rescued);
                        }

                        var data = XmlManager.PacificBankHeistConfig.HostageSafePosition;
                        var pos = new Vector3(data.X, data.Y, data.Z);

                        if (Vector3.Distance(rescued.Position, pos) < 3f)
                        {
                            instance.variables.SafeHostages.Add(rescued);
                            instance.variables.SafeHostagesCount++;
                        }
                        if (Functions.IsPedGettingArrested(rescued) || Functions.IsPedArrested(rescued))
                        {
                            instance.variables.RescuedHostages[instance.variables.RescuedHostages.IndexOf(rescued)] = rescued.ClonePed();
                        }
                        rescued.Tasks.FollowNavigationMeshToPosition(pos, data.Heading, 1.55f).WaitForCompletion(200);

                        if (waitCountForceAttack > 150)
                        {
                            var nearest = rescued.GetNearbyPeds(2)[0];
                            if (nearest == Game.LocalPlayer.Character)
                            {
                                nearest = rescued.GetNearbyPeds(2)[1];
                            }
                            if (instance.variables.AllRobbers.Contains(nearest))
                            {
                                nearest.Tasks.FightAgainst(rescued);
                                waitCountForceAttack = 0;
                            }
                        }
                    }
                    else
                    {
                        instance.variables.RescuedHostages.Remove(rescued);
                        instance.variables.AliveHostagesCount--;
                    }
                }

                foreach (var safeH in instance.variables.SafeHostages)
                {
                    if (safeH is not null && safeH.IsValid() && safeH.Exists())
                    {
                        if (instance.variables.RescuedHostages.Contains(safeH))
                        {
                            instance.variables.RescuedHostages.Remove(safeH);
                        }

                        safeH.IsInvincible = true;
                        if (!safeH.IsInAnyVehicle(true))
                        {
                            if (enterAmbulanceCount > 100)
                            {
                                if (instance.variables.AllAmbulance[0].IsSeatFree(2))
                                {
                                    safeH.Tasks.EnterVehicle(instance.variables.AllAmbulance[0], 2);
                                }
                                else if (instance.variables.AllAmbulance[0].IsSeatFree(1))
                                {
                                    safeH.Tasks.EnterVehicle(instance.variables.AllAmbulance[0], 1);
                                }
                                else
                                {
                                    instance.variables.AllAmbulance[0].GetPedOnSeat(2).Delete();
                                    safeH.Tasks.EnterVehicle(instance.variables.AllAmbulance[0], 2);
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
                                        Natives.SET_VEHICLE_DOORS_SHUT(instance.variables.AllAmbulance[0], true);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        instance.variables.SafeHostages.Remove(safeH);
                    }
                }
            }
            catch
            {
                continue;
            }
        }
    }

    internal static void HandleOpenBackRiotVan(PacificBankHeist instance)
    {
        var cooldown = 0;
        while (instance.IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (cooldown > 0) cooldown--;

                foreach (var riot in instance.variables.AllRiot)
                {
                    GameFiber.Yield();
                    if (riot is not null && riot.IsValid() && riot.Exists())
                    {
                        if (Vector3.Distance(riot.GetOffsetPosition(Vector3.RelativeBack * 4f), Game.LocalPlayer.Character.Position) < 2f)
                        {
                            if (KeyHelpers.IsKeysDownRightNow(Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey))
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
                                    Game.LocalPlayer.Character.GiveWeapon([.. XmlManager.PacificBankHeistConfig.WeaponInRiot], false);
                                    Natives.PLAY_SOUND_FRONTEND(-1, "PURCHASE", "HUD_LIQUOR_STORE_SOUNDSET", true);
                                    Game.LocalPlayer.Character.Tasks.LeaveVehicle(LeaveVehicleFlags.None).WaitForCompletion();
                                    instance.variables.FightingPacksUsed++;
                                }
                            }
                            else
                            {
                                if (cooldown is 0)
                                {
                                    KeyHelpers.DisplayKeyHelp("EnterRiot", [$"~{PBHConstants.RIOT_BLIP.GetIconToken()}~"], Settings.EnterRiotVanKey, Settings.EnterRiotVanModifierKey, 500);
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
    internal static void HandleCalloutAudio(PacificBankHeist instance)
    {
        AudioStateChanged = false;
        while (instance.IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (instance.variables.IsAlarmEnabled)
                {
                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, instance.CalloutPosition) > 70f)
                    {
                        instance.variables.IsAlarmEnabled = false;
                        instance.variables.CurrentAlarmState = AlarmState.None;
                        instance.variables.BankBlip.IsRouteEnabled = true;
                        AudioStateChanged = true;
                    }
                }
                else
                {
                    if (Vector3.Distance(Game.LocalPlayer.Character.Position, instance.CalloutPosition) < 55f)
                    {
                        instance.variables.IsAlarmEnabled = true;
                        instance.variables.CurrentAlarmState = AlarmState.Alarm;
                        instance.variables.BankBlip.IsRouteEnabled = false;
                        AudioStateChanged = true;
                    }
                }

                if (KeyHelpers.IsKeysDown(Settings.ToggleBankHeistAlarmSoundKey, Settings.ToggleBankHeistAlarmSoundModifierKey))
                {
                    if (instance.variables.CurrentAlarmState is not AlarmState.None)
                    {
                        instance.variables.CurrentAlarmState++;
                    }
                    else
                    {
                        instance.variables.CurrentAlarmState = AlarmState.Alarm;
                    }
                    AudioStateChanged = true;
                }

                if (AudioStateChanged)
                {
                    switch (instance.variables.CurrentAlarmState)
                    {
                        default:
                        case AlarmState.None:
                            instance.variables.BankAlarm.Stop();
                            break;
                        case AlarmState.Alarm:
                            instance.variables.BankAlarm.PlayLooping();
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

    internal static void HandleVaultRobbers(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            GameFiber.Yield();
            try
            {
                if (Vector3.Distance(Game.LocalPlayer.Character.Position, PBHConstants.OutsideBankVault) < 4f)
                {
                    GameFiber.Wait(2000);

                    World.SpawnExplosion(new(252.2609f, 225.3824f, 101.6835f), 2, 0.2f, true, false, 0.6f);
                    instance.variables.CurrentAlarmState = AlarmState.Alarm;
                    GameFiber.Wait(900);
                    foreach (var robber in instance.variables.AllVaultRobbers)
                    {
                        robber.Tasks.FollowNavigationMeshToPosition(PBHConstants.OutsideBankVault, robber.Heading, 2f).WaitForCompletion(500);
                    }
                    GameFiber.Wait(700);
                    foreach (var robber in instance.variables.AllVaultRobbers)
                    {
                        GameFiber.Yield();
                        robber.Tasks.FightAgainstClosestHatedTarget(30f);
                        instance.variables.AllRobbers.Add(robber);
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

    internal static void ToggleMobilePhone(PacificBankHeist instance, Ped ped, bool toggle)
    {
        if (toggle)
        {
            Natives.SET_PED_CAN_SWITCH_WEAPON(ped, false);
            ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_UNARMED"), -1, true);
            instance.variables.MobilePhone = new(PBHConstants.PhoneModel, new(0, 0, 0));
            var boneIndex = Natives.GET_PED_BONE_INDEX(ped, (int)PedBoneId.RightPhHand);
            Natives.ATTACH_ENTITY_TO_ENTITY(instance.variables.MobilePhone, ped, boneIndex, 0f, 0f, 0f, 0f, 0f, 0f, true, true, false, false, 2, true);
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
            if (instance.variables.MobilePhone is not null && instance.variables.MobilePhone.IsValid() && instance.variables.MobilePhone.Exists()) instance.variables.MobilePhone.Delete();
        }
    }

    internal static void GetWife(PacificBankHeist instance)
    {
        Game.LocalPlayer.Character.IsPositionFrozen = true;
        var vData = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.PoliceCruisers]);
        var data = XmlManager.PacificBankHeistConfig.WifePosition;
        instance.variables.WifeCar = new(vData.Model, new(data.X, data.Y, data.Z), data.Heading)
        {
            IsPersistent = true,
            IsSirenOn = true,
        };
        if (instance.variables.WifeCar is not null && instance.variables.WifeCar.IsValid() && instance.variables.WifeCar.Exists())
        {
            instance.variables.WifeDriver = instance.variables.WifeCar.CreateRandomDriver();
            if (instance.variables.WifeDriver is not null && instance.variables.WifeDriver.IsValid() && instance.variables.WifeDriver.Exists())
            {
                instance.variables.WifeDriver.IsPersistent = true;
                instance.variables.WifeDriver.BlockPermanentEvents = true;
                var wData = CalloutHelpers.Select([.. XmlManager.PacificBankHeistConfig.WifeModels]);
                instance.variables.Wife = new Ped(wData.Model, Vector3.Zero, 0f)
                {
                    IsPersistent = true,
                    BlockPermanentEvents = true,
                };
                if (instance.variables.Wife is not null && instance.variables.Wife.IsValid() && instance.variables.Wife.Exists())
                {
                    instance.variables.Wife.WarpIntoVehicle(instance.variables.WifeCar, 1);
                    instance.CalloutEntities.Add(instance.variables.Wife);
                    instance.CalloutEntities.Add(instance.variables.WifeDriver);
                    instance.CalloutEntities.Add(instance.variables.WifeCar);

                    var destination = new Vector3(XmlManager.PacificBankHeistConfig.WifeVehicleDestination.X, XmlManager.PacificBankHeistConfig.WifeVehicleDestination.Y, XmlManager.PacificBankHeistConfig.WifeVehicleDestination.Z);
                    instance.variables.WifeDriver.Tasks.DriveToPosition(destination, 20f, VehicleDrivingFlags.DriveAroundVehicles | VehicleDrivingFlags.DriveAroundObjects | VehicleDrivingFlags.DriveAroundPeds);
                    GameFiber.WaitWhile(() => Vector3.Distance(instance.variables.WifeCar.Position, destination) >= 6f);
                    instance.variables.Wife.Tasks.LeaveVehicle(LeaveVehicleFlags.None);
                    instance.variables.Wife.Tasks.FollowNavigationMeshToPosition(Game.LocalPlayer.Character.GetOffsetPosition(Vector3.RelativeRight * 1.5f), Game.LocalPlayer.Character.Heading, 1.9f).WaitForCompletion(60000);
                    Game.LocalPlayer.Character.IsPositionFrozen = false;
                }
            }
        }
    }

    internal static void PrepareFighting(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            GameFiber.Yield();
            KeyHelpers.DisplayKeyHelp("BankHeistMoveIn", Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey);
            if (KeyHelpers.IsKeysDown(Settings.SpeakWithThePersonKey, Settings.SpeakWithThePersonModifierKey))
            {
                Conversations.Talk([(Settings.OfficerName, Localization.GetString("MoveIn"))]);
                KeyHelpers.DisplayKeyHelp("SWATFollowing", Settings.SWATFollowKey, Settings.SWATFollowModifierKey);
                instance.variables.Status = EPacificBankHeistStatus.FightingWithRobbers;
                break;
            }
        }
    }

    private static Entity entityPlayerAimingAtSneakyRobber = null;
    internal static void SneakyRobberFight(PacificBankHeist instance, Ped sneak, Ped nearestPed)
    {
        GameFiber.StartNew(() =>
        {
            try
            {
                instance.variables.FightingSneakRobbers.Add(sneak);
                while (instance.IsCalloutRunning)
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
                    if (instance.variables.IsRescuingHostage) break;
                }

                if (sneak is not null && sneak.IsValid() && sneak.Exists())
                {
                    sneak.Tasks.FightAgainstClosestHatedTarget(15f);
                    sneak.RelationshipGroup = PBHConstants.RobbersRG;
                }

                while (instance.IsCalloutRunning)
                {
                    GameFiber.Yield();
                    if (!sneak.Exists()) break;
                    if (!nearestPed.Exists()) break;

                    Natives.STOP_CURRENT_PLAYING_AMBIENT_SPEECH(sneak);

                    if (nearestPed.IsDead)
                    {
                        foreach (var hostage in instance.variables.SpawnedHostages)
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
                instance.variables.FightingSneakRobbers.Remove(sneak);
            }
        });
    }

    internal static void DetermineResults(PacificBankHeist instance)
    {
        foreach (var robber in instance.variables.AllRobbers)
        {
            if (robber is not null && robber.IsValid() && robber.Exists() && robber.IsDead)
            {
                instance.variables.DiedRobbersCount++;
            }
        }
        Hud.DisplayNotification(Localization.GetString("BankHeistReportText", $"{instance.variables.SafeHostagesCount.ToString()}", $"{(instance.variables.TotalHostagesCount - instance.variables.AliveHostagesCount).ToString()}", $"{instance.variables.DiedRobbersCount.ToString()}"), Localization.GetString("BankHeistReportTitle"), instance.variables.TotalHostagesCount - instance.variables.AliveHostagesCount is < 3 ? Localization.GetString("BankHeistReportSubtitle") : "", "mphud", "mp_player_ready");
        if (instance.variables.TotalHostagesCount == instance.variables.AliveHostagesCount)
        {
            var bigMessage = new BigMessageThread(true);
            bigMessage.MessageInstance.ShowColoredShard(Localization.GetString("Congratulations"), Localization.GetString("NoDiedHostage"), HudColor.Yellow, HudColor.MenuGrey);
            Common.PlaySound("Mission_Pass_Notify", "DLC_HEISTS_GENERAL_FRONTEND_SOUNDS");
        }
    }

    private static Entity entityPlayerAimingAt = null;
    internal static void RobbersFightingAI(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (instance.variables.Status is EPacificBankHeistStatus.FightingWithRobbers)
                {
                    foreach (var robber in instance.variables.AllRobbers)
                    {
                        GameFiber.Yield();
                        if (robber.Exists())
                        {
                            var distance = Vector3.Distance(robber.Position, PBHConstants.PacificBankInsideChecks[0]) < Vector3.Distance(robber.Position, PBHConstants.PacificBankInsideChecks[1])
                                ? Vector3.Distance(robber.Position, PBHConstants.PacificBankInsideChecks[0])
                                : Vector3.Distance(robber.Position, PBHConstants.PacificBankInsideChecks[1]);
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

                    if (entityPlayerAimingAt is not null && instance.variables.AllRobbers.Contains(entityPlayerAimingAt))
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

    internal static void CopFightingAI(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            try
            {
                GameFiber.Yield();
                if (instance.variables.Status is EPacificBankHeistStatus.FightingWithRobbers)
                {
                    if (instance.variables.OfficersTargetsToShoot.Count > 0)
                    {
                        if (instance.variables.OfficersTargetsToShoot[0].Exists())
                        {
                            if (instance.variables.OfficersTargetsToShoot[0].IsAlive)
                            {
                                foreach (var cop in instance.variables.AllOfficers)
                                {
                                    cop.Tasks.FightAgainst(instance.variables.OfficersTargetsToShoot[0]);
                                }
                            }
                            else
                            {
                                instance.variables.OfficersTargetsToShoot.RemoveAt(0);
                            }
                        }
                        else
                        {
                            instance.variables.OfficersTargetsToShoot.RemoveAt(0);
                        }
                    }
                    else
                    {
                        CopsReturnToLocation(instance);
                    }
                }
                if (instance.variables.Status is EPacificBankHeistStatus.FightingWithRobbers || instance.variables.IsSWATFollowing)
                {
                    foreach (var swat in instance.variables.AllSWATUnits)
                    {
                        GameFiber.Yield();
                        if (swat is not null && swat.IsValid() && swat.Exists())
                        {
                            if (instance.variables.IsSWATFollowing)
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

    internal static void CheckForRobbersOutside(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            GameFiber.Yield();
            if (instance.variables.Status is EPacificBankHeistStatus.FightingWithRobbers)
            {
                foreach (var location in PBHConstants.BankDoorPositions)
                {
                    foreach (var robber in World.GetEntities(location, 1.6f, GetEntitiesFlags.ConsiderAllPeds).Cast<Ped>())
                    {
                        if (robber.Exists())
                        {
                            if (Vector3.Distance(robber.Position, location) < 1.5f)
                            {
                                if (robber.IsAlive)
                                {
                                    if (instance.variables.AllRobbers.Contains(robber))
                                    {
                                        if (!instance.variables.OfficersTargetsToShoot.Contains(robber))
                                        {
                                            instance.variables.OfficersTargetsToShoot.Add(robber);
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

    internal static void CopsReturnToLocation(PacificBankHeist instance)
    {
        for (int i = 0; i < instance.variables.AllStandingOfficers.Count; i++)
        {
            var officer = instance.variables.AllStandingOfficers[i];
            var data = XmlManager.PacificBankHeistConfig.StandingOfficerPositions[i];
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

        for (int i = 0; i < instance.variables.AllAimingOfficers.Count; i++)
        {
            var officer = instance.variables.AllAimingOfficers[i];
            var data = XmlManager.PacificBankHeistConfig.AimingOfficerPositions[i];
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
                    var aimPoint = Vector3.Distance(officer.Position, PBHConstants.BankDoorPositions[0]) < Vector3.Distance(officer.Position, PBHConstants.BankDoorPositions[1]) ? PBHConstants.BankDoorPositions[0] : PBHConstants.BankDoorPositions[1];
                    Natives.TASK_AIM_GUN_AT_COORD(officer, aimPoint.X, aimPoint.Y, aimPoint.Z, -1, false, false);
                }
            }
        }
    }

    internal static void SwitchSWATFollowing(PacificBankHeist instance)
    {
        instance.variables.IsSWATFollowing = !instance.variables.IsSWATFollowing;
        if (instance.variables.IsSWATFollowing)
        {
            Hud.DisplayHelp(Localization.GetString("SWATIsFollowing"));
        }
        else
        {
            Hud.DisplayHelp(Localization.GetString("SWATIsNotFollowing"));
        }
    }

    internal static void SetRelationships()
    {
        PBHConstants.RobbersRG.SetRelationshipWith(PBHConstants.SneakRobbersRG, Relationship.Respect);
        PBHConstants.RobbersRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Hate);
        PBHConstants.RobbersRG.SetRelationshipWith(PBHConstants.PoliceRG, Relationship.Hate);
        PBHConstants.RobbersRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);

        PBHConstants.SneakRobbersRG.SetRelationshipWith(PBHConstants.RobbersRG, Relationship.Respect);
        PBHConstants.SneakRobbersRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Hate);
        PBHConstants.SneakRobbersRG.SetRelationshipWith(PBHConstants.PoliceRG, Relationship.Hate);
        PBHConstants.SneakRobbersRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Hate);

        RelationshipGroup.Cop.SetRelationshipWith(PBHConstants.RobbersRG, Relationship.Hate);
        RelationshipGroup.Cop.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        RelationshipGroup.Cop.SetRelationshipWith(PBHConstants.PoliceRG, Relationship.Respect);
        RelationshipGroup.Cop.SetRelationshipWith(PBHConstants.HostageRG, Relationship.Respect);

        PBHConstants.PoliceRG.SetRelationshipWith(PBHConstants.RobbersRG, Relationship.Hate);
        PBHConstants.PoliceRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        PBHConstants.PoliceRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Respect);
        PBHConstants.PoliceRG.SetRelationshipWith(PBHConstants.HostageRG, Relationship.Respect);

        PBHConstants.HostageRG.SetRelationshipWith(Game.LocalPlayer.Character.RelationshipGroup, Relationship.Respect);
        PBHConstants.HostageRG.SetRelationshipWith(RelationshipGroup.Cop, Relationship.Respect);
        PBHConstants.HostageRG.SetRelationshipWith(PBHConstants.PoliceRG, Relationship.Respect);
    }

    internal static void UnlockDoorsAlways(PacificBankHeist instance)
    {
        while (instance.IsCalloutRunning)
        {
            GameFiber.Yield();
            foreach (var (pos, hash) in PBHConstants.Doors)
            {
                Natives.SET_LOCKED_UNSTREAMED_IN_DOOR_OF_TYPE(hash, pos.X, pos.Y, pos.Z, false, 0f, 0f, 0f);
            }
        }
    }
}