# Bank Heist Callouts

## Overview
The robber has taken hostages and barricaded themselves inside the pacific bank.<br/>
Which do you choose, the lives of hostages or solve the incident quickly?

::: warning
This callout **contains some violent words**. Please be careful **it is not for you**.
:::

## Callout Settings
**Config File**: `plugins/LSPDFR/JapaneseCallouts/Xml/BankHeistConfig.xml`

### The config used in this callout
<!-- <Cards>
    <Card title="Vehicle" href="/config/vehicle" />
    <Card title="Ped" href="/config/ped" />
    <Card title="Weapon" href="/config/weapon" />
    <Card title="Position" href="/config/position" />
</Cards> -->

### Settings List
|           Setting Name           |                                                          Description                                                          |                      Note                       |
| :------------------------------: | :---------------------------------------------------------------------------------------------------------------------------: | :---------------------------------------------: |
|   <span>**HostageCount**<span>   |                                                    The count of hostages.                                                     | Its maximum will be `HostagePositions`'s count. |
|           **WifeName**           |                                   The wife name of the robber who you can talk to by phone.                                   |                                                 |
|        **PoliceCruisers**        |                                                   Police Cruisers' settings                                                   |                                                 |
|      **PoliceTransporters**      |                                                 Police Transporters' settings                                                 |                                                 |
|         **PoliceRiots**          |                                                    Police Riots' settings                                                     |                                                 |
|          **Ambulances**          |                                                     Ambulances' settings                                                      |                                                 |
|          **Firetrucks**          |                                                     Firetrucks' settings                                                      |                                                 |
|     **PoliceOfficerModels**      |                                                   Police Officers' settings                                                   |                                                 |
|       **PoliceSWATModels**       |                                                        SWATs' settings                                                        |                                                 |
|       **ParamedicModels**        |                                                     Paramedics' settings                                                      |                                                 |
|      **FirefighterModels**       |                                                    Firefighters' settings                                                     |                                                 |
|       **CommanderModels**        |                                                     Commander's settings                                                      |                                                 |
|          **WifeModels**          |                                                        Wife's settings                                                        |                                                 |
|         **RobberModels**         |                                                       Robber's settings                                                       |                                                 |
|        **HostageModels**         |                                                      Hostages' settings                                                       |                                                 |
|        **OfficerWeapons**        |                                                   Officers' weapon settings                                                   |                                                 |
|         **SWATWeapons**          |                                                    SWATs' weapon settings                                                     |                                                 |
|        **RobbersWeapons**        |                                                   Robbers' weapon settings                                                    |                                                 |
|   **RobbersThrowableWeapons**    |                                                 Robber's sub weapon settings                                                  |  You can set a weapon which not throwable too   |
|         **WeaponInRiot**         |                                      The weapons which you can get in the riot settings                                       |                                                 |
|    **PoliceCruiserPositions**    |                                             Police Cruisers' position and heading                                             |                                                 |
|   **PoliceTransportPositions**   |                                           Police Transporters' position and heading                                           |                                                 |
|        **RiotPositions**         |                                              Police Riots' position and heading                                               |                                                 |
|      **AmbulancePositions**      |                                               Ambulances' position and heading                                                |                                                 |
|      **FiretruckPositions**      |                                               Firetrucks' position and heading                                                |                                                 |
|    **AimingOfficerPositions**    |                                             Aiming officers' position and heading                                             |                                                 |
|   **StandingOfficerPositions**   |                                            Standing officer's position and heading                                            |                                                 |
|    **NormalRobbersPositions**    |                                                 Robbers' position and heading                                                 |                                                 |
| **RobbersNegotiationPositions**  |                                         Robbers' position and heading in negotiation                                          |                                                 |
|     **RobbersSneakPosition**     |                                           Robbers who sneaking position and heading                                           |       Set \"is_right\" true to turn right       |
|   **RobbersInVaultPositions**    |                                         Robbers who is in vault position and heading                                          |                                                 |
| **RobbersSurrenderingPositions** |                                                Surrender position and heading                                                 |                                                 |
|     **FirefighterPositions**     |                                              Firefighters' position and heading                                               |                                                 |
|      **ParamedicPositions**      |                                               Paramedics' position and heading                                                |                                                 |
|   **LeftSittingSWATPositions**   |             The position and heading settings of SWAT officers who are on the left side of the door of the bank.              |                                                 |
|  **RightSittingSWATPositions**   |             The position and heading settings of SWAT officers who are on the right side of the door of the bank.             |                                                 |
|  **RightLookingSWATPositions**   | The position and heading settings of SWAT officers who are on the right side of the door of the bank and looking at the door. |                                                 |
|       **HostagePositions**       |                                                      Hostages' position                                                       |               Cannot set heading                |
|     **HostageSafePosition**      |                                          Rescued hotages' safe position and heading                                           |                                                 |
|      **CommanderPosition**       |                                               Commander's position and heading                                                |                                                 |
|         **WifePosition**         |                                         The robber's wife spawn position and heading                                          |                                                 |
|    **WifeVehicleDestination**    |                                               The destination of wife position                                                |               Cannot set heading                |

Original config file
```xml:line-numbers
<?xml version="1.0" encoding="UTF-8"?>
<BankHeistConfig>
  <HostageCount>8</HostageCount>
  <WifeName>Maria</WifeName>
  <PoliceCruisers>
    <Vehicle chance="50">POLICE</Vehicle>
    <Vehicle chance="30">POLICE2</Vehicle>
    <Vehicle chance="80">POLICE3</Vehicle>
    <Vehicle chance="15">POLICE4</Vehicle>
    <Vehicle chance="40">POLICE5</Vehicle>
  </PoliceCruisers>
  <PoliceTransporters>
    <Vehicle chance="100">POLICET</Vehicle>
  </PoliceTransporters>
  <PoliceRiots>
    <Vehicle chance="100">RIOT</Vehicle>
  </PoliceRiots>
  <Ambulances>
    <Vehicle chance="100">AMBULANCE</Vehicle>
  </Ambulances>
  <Firetrucks>
    <Vehicle chance="100">FIRETRUK</Vehicle>
  </Firetrucks>
  <PoliceOfficerModels>
    <!-- Male -->
    <!-- LSPD Class A -->
    <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="201" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="1" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- LSPD Class B -->
    <Ped chance="40" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="194" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="15" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- LSPD Class C -->
    <Ped chance="40" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_watch_model="4" comp_watch_texture="1" comp_mask_model="102" comp_mask_texture="1" comp_top_model="191" comp_top_texture="1" comp_upperskin_model="12" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="14" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- LSPD Suit -->
    <Ped chance="20" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="1" comp_mask_texture="1" comp_top_model="5" comp_top_texture="5" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="179" comp_undercoat_texture="2" comp_pants_model="11" comp_pants_texture="5" comp_shoes_model="11" comp_shoes_texture="1" comp_accessories_model="39" comp_accessories_texture="14" comp_armor_model="25" comp_armor_texture="1" comp_parachute_model="69" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- LSPD Jacket -->
    <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="true" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="30" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="29" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- LSPD Raincoat -->
    <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="188" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="57" comp_undercoat_texture="1" comp_pants_model="36" comp_pants_texture="1" comp_shoes_model="52" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="29" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>

    <!-- Female -->
    <!-- LSPD Class A -->
    <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="203" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="1" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
    <!-- LSPD Class B -->
    <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="196" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="17" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
    <!-- LSPD Class C -->
    <Ped chance="30" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="193" comp_top_texture="1" comp_upperskin_model="10" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="15" comp_armor_texture="1" comp_parachute_model="53" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
    <!-- LSPD Suit -->
    <Ped chance="15" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" comp_mask_model="1" comp_mask_texture="1" comp_top_model="25" comp_top_texture="2" comp_upperskin_model="8" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="65" comp_undercoat_texture="2" comp_pants_model="4" comp_pants_texture="4" comp_shoes_model="30" comp_shoes_texture="1" comp_accessories_model="1" comp_accessories_texture="1" comp_armor_model="27" comp_armor_texture="1" comp_parachute_model="69" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
    <!-- LSPD Jacket -->
    <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="true" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="201" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="31" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>
    <!-- LSPD Raincoat -->
    <Ped chance="50" is_sunny="false" is_rainy="true" is_snowy="false" health="200" armor="250" comp_mask_model="102" comp_mask_texture="1" comp_top_model="190" comp_top_texture="1" comp_upperskin_model="4" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="34" comp_undercoat_texture="1" comp_pants_model="35" comp_pants_texture="1" comp_shoes_model="53" comp_shoes_texture="1" comp_accessories_model="9" comp_accessories_texture="1" comp_armor_model="31" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>

    <Ped chance="20" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" random_props="true">S_M_Y_COP_01</Ped>
    <Ped chance="20" is_sunny="true" is_rainy="false" is_snowy="false" health="200" armor="250" random_props="true">S_F_Y_COP_01</Ped>
  </PoliceOfficerModels>
  <PoliceSWATModels>
    <!-- Male -->
    <Ped chance="100" is_sunny="true" is_rainy="true" is_snowy="true" health="200" armor="400" comp_hat_model="151" comp_hat_texture="1" comp_glasses_model="22" comp_glasses_texture="1" comp_mask_model="186" comp_mask_texture="1" comp_top_model="221" comp_top_texture="1" comp_upperskin_model="180" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="16" comp_undercoat_texture="1" comp_pants_model="32" comp_pants_texture="1" comp_shoes_model="26" comp_shoes_texture="1" comp_accessories_model="111" comp_accessories_texture="1" comp_armor_model="17" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>
    <!-- Female -->
    <Ped chance="100" is_sunny="true" is_rainy="true" is_snowy="true" health="200" armor="400" comp_hat_model="150" comp_hat_texture="1" comp_glasses_model="23" comp_glasses_texture="1" comp_mask_model="186" comp_mask_texture="1" comp_top_model="231" comp_top_texture="1" comp_upperskin_model="216" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="49" comp_undercoat_texture="1" comp_pants_model="31" comp_pants_texture="1" comp_shoes_model="26" comp_shoes_texture="1" comp_accessories_model="82" comp_accessories_texture="1" comp_armor_model="19" comp_armor_texture="1" comp_parachute_model="49" comp_parachute_texture="1">MP_F_FREEMODE_01</Ped>

    <Ped chance="20" is_sunny="true" is_rainy="true" is_snowy="true" health="200" armor="400" random_props="true">S_M_Y_SWAT_01</Ped>
  </PoliceSWATModels>
  <ParamedicModels>
    <Ped chance="100" is_sunny="true" is_rainy="true" is_snowy="true" health="200" armor="200" random_props="true">S_M_M_PARAMEDIC_01</Ped>
  </ParamedicModels>
  <FirefighterModels>
    <Ped chance="100" is_sunny="true" is_rainy="true" is_snowy="true" health="200" armor="200" random_props="true">S_M_Y_FIREMAN_01</Ped>
  </FirefighterModels>
  <CommanderModels>
    <!-- Male FBI Suit -->
    <Ped chance="100" is_sunny="true" is_rainy="false" is_snowy="false" comp_mask_model="1" comp_mask_texture="1" comp_top_model="11" comp_top_texture="1" comp_upperskin_model="5" comp_upperskin_texture="1" comp_decal_model="1" comp_decal_texture="1" comp_undercoat_model="179" comp_undercoat_texture="1" comp_pants_model="11" comp_pants_texture="1" comp_shoes_model="11" comp_shoes_texture="1" comp_accessories_model="39" comp_accessories_texture="9" comp_armor_model="23" comp_armor_texture="1" comp_parachute_model="29" comp_parachute_texture="1">MP_M_FREEMODE_01</Ped>

    <Ped chance="20" is_sunny="true" is_rainy="true" is_snowy="true" random_props="true">IG_FBISUIT_01</Ped>
  </CommanderModels>
  <WifeModels>
    <Ped chance="100" random_props="true">A_F_Y_EASTSA_01</Ped>
  </WifeModels>
  <RobberModels>
    <Ped chance="100" health="200" armor="750" random_props="true">MP_G_M_PROS_01</Ped>
  </RobberModels>
  <HostageModels>
    <Ped chance="20" random_props="true">A_M_M_BUSINESS_01</Ped>
    <Ped chance="20" random_props="true">A_M_Y_BUSINESS_01</Ped>
    <Ped chance="20" random_props="true">A_M_Y_BUSINESS_02</Ped>
    <Ped chance="20" random_props="true">A_M_Y_BUSINESS_03</Ped>
    <Ped chance="20" random_props="true">A_F_Y_BUSINESS_01</Ped>
    <Ped chance="20" random_props="true">A_F_Y_FEMALEAGENT</Ped>
  </HostageModels>
  <OfficerWeapons>
    <Weapon chance="100">
      <Model>WEAPON_PISTOL</Model>
      <Components>
        <Component>COMPONENT_AT_PI_FLSH</Component>
      </Components>
    </Weapon>
    <Weapon chance="50">
      <Model>WEAPON_COMBATPISTOL</Model>
      <Components>
        <Component>COMPONENT_AT_PI_FLSH</Component>
      </Components>
    </Weapon>
  </OfficerWeapons>
  <SWATWeapons>
    <Weapon chance="100">
      <Model>WEAPON_CARBINERIFLE</Model>
      <Components>
        <Component>COMPONENT_AT_AR_FLSH</Component>
      </Components>
    </Weapon>
    <Weapon chance="50">
      <Model>WEAPON_ASSAULTRIFLE</Model>
      <Components>
        <Component>COMPONENT_AT_AR_FLSH</Component>
      </Components>
    </Weapon>
  </SWATWeapons>
  <RobbersWeapons>
    <Weapon chance="100">
      <Model>WEAPON_SAWNOFFSHOTGUN</Model>
    </Weapon>
    <Weapon chance="80">
      <Model>WEAPON_ASSAULTRIFLE</Model>
    </Weapon>
    <Weapon chance="80">
      <Model>WEAPON_PUMPSHOTGUN</Model>
    </Weapon>
    <Weapon chance="80">
      <Model>WEAPON_ADVANCEDRIFLE</Model>
    </Weapon>
    <Weapon chance="30">
      <Model>WEAPON_PISTOL</Model>
    </Weapon>
    <Weapon chance="50">
      <Model>WEAPON_ASSAULTSMG</Model>
    </Weapon>
  </RobbersWeapons>
  <RobbersThrowableWeapons>
    <Weapon chance="50">
      <Model>WEAPON_GRENADE</Model>
    </Weapon>
    <Weapon chance="50">
      <Model>WEAPON_SMOKEGRENADE</Model>
    </Weapon>
  </RobbersThrowableWeapons>
  <WeaponInRiot>
    <Weapon chance="100">
      <Model>WEAPON_CARBINERIFLE</Model>
      <Components>
        <Component>COMPONENT_AT_AR_FLSH</Component>
      </Components>
    </Weapon>
    <Weapon chance="100">
      <Model>WEAPON_ASSAULTRIFLE</Model>
      <Components>
        <Component>COMPONENT_AT_AR_FLSH</Component>
      </Components>
    </Weapon>
  </WeaponInRiot>
  <PoliceCruiserPositions>
    <Position x="271.3" y="180.6" z="104.3" heading="69.4" />
    <Position x="258.9" y="185.2" z="104.4" heading="69.8" />
    <Position x="246.8" y="189.8" z="104.8" heading="68.1" />
    <Position x="236.9" y="193.9" z="104.9" heading="-110.7" />
    <Position x="227.7" y="197.9" z="105.0" heading="64.5" />
    <Position x="220.6" y="217.9" z="105.1" heading="-18.9" />
    <Position x="223.9" y="226.8" z="105.1" heading="-20.0" />
    <Position x="231.3" y="173.6" z="104.9" heading="-110.2" />
  </PoliceCruiserPositions>
  <PoliceTransportPositions>
    <Position x="273.9" y="191.7" z="104.6" heading="54.3" />
    <Position x="219.0" y="175.1" z="105.2" heading="-111.1" />
  </PoliceTransportPositions>
  <RiotPositions>
    <Position x="220.2" y="209.2" z="105.1" heading="23.2" />
    <Position x="265.4" y="192.2" z="104.4" heading="-139.9" />
  </RiotPositions>
  <AmbulancePositions>
    <Position x="263.2" y="158.9" z="104.2" heading="-110.3" />
    <Position x="254.0" y="162.2" z="104.4" heading="-109.2" />
  </AmbulancePositions>
  <FiretruckPositions>
    <Position x="239.6" y="170.7" z="105.1" heading="-109.9" />
  </FiretruckPositions>
  <BarrierPositions>
    <Position x="266.5" y="182.4" z="103.6" heading="-20.1" />
    <Position x="263.4" y="183.6" z="103.7" heading="-20.1" />
    <Position x="254.3" y="186.8" z="103.9" heading="-20.1" />
    <Position x="251.2" y="187.9" z="103.9" heading="-20.1" />
    <Position x="241.6" y="192.0" z="104.1" heading="-21.5" />
    <Position x="232.2" y="195.8" z="104.2" heading="-22.2" />
    <Position x="223.8" y="200.1" z="104.4" heading="-33.5" />
    <Position x="220.9" y="203.0" z="104.4" heading="-53.1" />
    <Position x="222.2" y="222.2" z="104.4" heading="-110.2" />
    <Position x="227.7" y="228.2" z="104.5" heading="159.8" />
    <Position x="230.5" y="227.2" z="104.5" heading="159.8" />
  </BarrierPositions>
  <AimingOfficerPositions>
    <Position x="219.8" y="220.3" z="105.5" heading="-120.1" />
    <Position x="218.5" y="216.5" z="105.5" heading="-105.7" />
    <Position x="226.1" y="197.0" z="105.4" heading="-19.9" />
    <Position x="234.4" y="193.3" z="105.2" heading="-21.4" />
    <Position x="247.8" y="187.9" z="105.0" heading="-29.9" />
    <Position x="257.3" y="184.2" z="104.9" heading="2.6" />
    <Position x="260.5" y="183.1" z="104.8" heading="-12.2" />
  </AimingOfficerPositions>
  <StandingOfficerPositions>
    <Position x="215.2" y="177.1" z="105.3" heading="70.4" />
    <Position x="218.6" y="207.0" z="105.4" heading="113.0" />
  </StandingOfficerPositions>
  <NormalRobbersPositions>
    <Position x="262.5" y="211.9" z="106.2" heading="161.0" />
    <Position x="257.3" y="214.9" z="106.2" heading="160.5" />
    <Position x="235.4" y="217.3" z="106.2" heading="116.3" />
    <Position x="237.9" y="213.6" z="106.2" heading="69.5" />
    <Position x="243.7" y="212.4" z="110.2" heading="-20.6" />
    <Position x="266.6" y="220.1" z="110.2" heading="69.9" />
    <Position x="254.7" y="228.1" z="106.2" heading="161.5" />
    <Position x="268.2" y="223.0" z="103.4" heading="159.4" />
    <Position x="246.7" y="218.7" z="106.2" heading="169.6" />
    <Position x="251.0" y="208.6" z="106.2" heading="-21.0" />
  </NormalRobbersPositions>
  <RobbersNegotiationPositions>
    <Position x="235.3" y="216.8" z="106.2" heading="115.5" />
    <Position x="243.0" y="222.6" z="106.2" heading="70.8" />
    <Position x="256.1" y="216.8" z="106.2" heading="-109.0" />
    <Position x="254.0" y="213.1" z="106.2" heading="-109.1" />
    <Position x="257.6" y="222.5" z="106.2" heading="159.9" />
    <Position x="264.0" y="224.6" z="101.6" heading="-110.5" />
    <Position x="237.0" y="218.3" z="110.2" heading="-66.1" />
  </RobbersNegotiationPositions>
  <RobbersSneakPosition>
    <Position x="255.1" y="222.0" z="106.2" heading="-18.7" is_right="false" />
    <Position x="263.1" y="215.4" z="110.2" heading="-175.0" is_right="false" />
    <Position x="265.2" y="222.3" z="101.6" heading="125.6" is_right="false" />
    <Position x="257.0" y="205.4" z="110.2" heading="-24.9" is_right="true" />
    <Position x="235.8" y="228.0" z="110.2" heading="-122.6" is_right="true" />
    <Position x="238.9" y="228.3" z="106.2" heading="34.7" is_right="true" />
    <Position x="245.7" y="214.3" z="106.2" heading="69.8" is_right="true" />
  </RobbersSneakPosition>
  <RobbersInVaultPositions>
    <Position x="255.9" y="217.0" z="101.6" heading="69.9" />
    <Position x="258.5" y="216.0" z="101.6" heading="70.8" />
    <Position x="251.3" y="218.9" z="101.6" heading="-19.1" />
  </RobbersInVaultPositions>
  <RobbersSurrenderingPositions>
    <Position x="229.2" y="207.9" z="105.4" heading="160.1" />
    <Position x="233.8" y="206.2" z="105.3" heading="160.1" />
    <Position x="238.2" y="204.6" z="105.3" heading="160.1" />
    <Position x="242.8" y="202.9" z="105.2" heading="160.1" />
    <Position x="246.8" y="201.4" z="105.1" heading="160.1" />
    <Position x="251.5" y="199.7" z="105.0" heading="160.1" />
    <Position x="257.1" y="197.7" z="104.9" heading="160.1" />
  </RobbersSurrenderingPositions>
  <FirefighterPositions>
    <Position x="244.9" y="168.3" z="104.9" heading="-24.0" />
  </FirefighterPositions>
  <ParamedicPositions>
    <Position x="250.8" y="165.5" z="104.7" heading="-21.2" />
    <Position x="259.8" y="162.4" z="104.6" heading="-20.5" />
  </ParamedicPositions>
  <LeftSittingSWATPositions>
    <Position x="231.6" y="211.8" z="105.4" heading="84.4" />
    <Position x="232.0" y="211.0" z="105.4" heading="93.8" />
    <Position x="232.9" y="210.4" z="105.4" heading="129.7" />
    <Position x="260.7" y="200.2" z="104.9" heading="133.2" />
    <Position x="261.5" y="199.9" z="104.9" heading="118.9" />
    <Position x="262.3" y="199.6" z="104.9" heading="127.6" />
  </LeftSittingSWATPositions>
  <RightSittingSWATPositions>
    <Position x="228.7" y="218.0" z="105.5" heading="150.2" />
    <Position x="228.9" y="219.2" z="105.5" heading="125.3" />
    <Position x="255.7" y="202.1" z="105.0" heading="-128.7" />
    <Position x="254.8" y="202.5" z="105.0" heading="-145.8" />
  </RightSittingSWATPositions>
  <RightLookingSWATPositions>
    <Position x="229.1" y="217.1" z="105.5" heading="171.3" />
    <Position x="256.6" y="201.8" z="105.0" heading="-138.0" />
  </RightLookingSWATPositions>
  <HostagePositions>
    <Position x="242.8" y="228.3" z="106.2" />
    <Position x="248.3" y="229.7" z="106.2" />
    <Position x="241.4" y="221.5" z="106.2" />
    <Position x="245.6" y="216.6" z="106.2" />
    <Position x="253.6" y="218.9" z="106.2" />
    <Position x="267.2" y="223.0" z="110.2" />
    <Position x="262.3" y="224.8" z="101.6" />
    <Position x="262.6" y="208.2" z="110.2" />
    <Position x="250.6" y="209.1" z="110.2" />
    <Position x="243.3" y="211.7" z="110.2" />
    <Position x="236.1" y="216.3" z="110.2" />
    <Position x="255.0" y="224.7" z="106.2" />
  </HostagePositions>
  <HostageSafePosition x="248.6" y="169.1" z="104.9" heading="158.3" />
  <CommanderPosition x="271.9" y="190.8" z="104.7" heading="138.2" />
  <WifePosition x="178.6" y="120.6" z="95.6" heading="-17.2" />
  <WifeVehicleDestination x="241.7" y="178.9" z="104.7" />
</BankHeistConfig>
```