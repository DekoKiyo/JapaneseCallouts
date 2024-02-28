namespace JapaneseCallouts;

internal static class NT
{
    internal static class VEHICLE
    {
        internal static void SET_VEHICLE_FORWARD_SPEED(Vehicle veh, float speed)
        => NativeFunction.Natives.SET_VEHICLE_FORWARD_SPEED(veh, speed);
        internal static void SET_VEHICLE_LIVERY(Vehicle vehicle, int livery)
        => NativeFunction.Natives.SET_VEHICLE_LIVERY(vehicle, livery);
        internal static int GET_VEHICLE_LIVERY_COUNT(Vehicle vehicle)
        => NativeFunction.Natives.GET_VEHICLE_LIVERY_COUNT<int>(vehicle);
        internal static void SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(Vehicle vehicle, int r, int g, int b)
        => NativeFunction.Natives.SET_VEHICLE_CUSTOM_PRIMARY_COLOUR(vehicle, r, g, b);
        internal static void SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(Vehicle vehicle, int r, int g, int b)
        => NativeFunction.Natives.SET_VEHICLE_CUSTOM_SECONDARY_COLOUR(vehicle, r, g, b);
        internal static int GET_VEHICLE_NUMBER_OF_PASSENGERS(Vehicle vehicle)
        => NativeFunction.Natives.GET_VEHICLE_NUMBER_OF_PASSENGERS<int>(vehicle);
        internal static Vehicle CREATE_VEHICLE(uint hash, float x, float y, float z, float heading, bool isNetwork = false, bool netMissionEntity = false)
        => NativeFunction.Natives.CREATE_VEHICLE<Vehicle>(hash, x, y, z, heading, isNetwork, netMissionEntity);
        internal static Vehicle CREATE_VEHICLE(uint hash, Vector3 position, float heading, bool isNetwork = false, bool netMissionEntity = false)
        => NativeFunction.Natives.CREATE_VEHICLE<Vehicle>(hash, position.X, position.Y, position.Z, heading, isNetwork, netMissionEntity);
    }

    internal static class PED
    {
        internal static void SET_PED_SUFFERS_CRITICAL_HITS(Ped p, bool status)
        => NativeFunction.Natives.SET_PED_SUFFERS_CRITICAL_HITS(p, status);
        internal static Model GET_PED_CAUSE_OF_DEATH(Ped body)
        => NativeFunction.Natives.GET_PED_CAUSE_OF_DEATH<Model>(body);
        internal static void SET_DRIVER_AGGRESSIVENESS(Ped driver, float aggressiveness)
        => NativeFunction.Natives.SET_DRIVER_AGGRESSIVENESS(driver, aggressiveness);
        internal static void SET_DRIVER_ABILITY(Ped driver, float ability)
        => NativeFunction.Natives.SET_DRIVER_ABILITY(driver, ability);
        internal static void SET_PED_RANDOM_PROPS(Ped ped)
        => NativeFunction.Natives.SET_PED_RANDOM_PROPS(ped);
        internal static int REGISTER_PEDHEADSHOT(Ped ped)
        => NativeFunction.Natives.REGISTER_PEDHEADSHOT<int>(ped);
        internal static string GET_PEDHEADSHOT_TXD_STRING(int id)
        => NativeFunction.Natives.GET_PEDHEADSHOT_TXD_STRING<string>(id);
        internal static void UNREGISTER_PEDHEADSHOT(int id)
        => NativeFunction.Natives.UNREGISTER_PEDHEADSHOT(id);
        internal static bool IS_PEDHEADSHOT_READY(int id)
        => NativeFunction.Natives.IS_PEDHEADSHOT_READY<bool>(id);
        internal static bool IS_PEDHEADSHOT_VALID(int id)
        => NativeFunction.Natives.IS_PEDHEADSHOT_VALID<bool>(id);
        internal static void CLEAR_ALL_PED_PROPS(Ped ped)
        => NativeFunction.Natives.CLEAR_ALL_PED_PROPS(ped);
        internal static int GET_PED_PROP_INDEX(Ped ped, int componentId)
        => NativeFunction.Natives.GET_PED_PROP_INDEX<int>(ped, componentId);
        internal static int GET_PED_PROP_TEXTURE_INDEX(Ped ped, int componentId)
        => NativeFunction.Natives.GET_PED_PROP_TEXTURE_INDEX<int>(ped, componentId);
        internal static void SET_PED_PROP_INDEX(Ped ped, int componentId, int drawableId, int textureId, bool attach)
        => NativeFunction.Natives.SET_PED_PROP_INDEX(ped, componentId, drawableId, textureId, attach);
        internal static Ped CREATE_PED(Model model, float x, float y, float z, float heading, bool isNetwork = false, bool bScriptHostPed = false, int pedType = 0)
        => NativeFunction.Natives.CREATE_PED<Ped>(pedType, model, x, y, z, heading, isNetwork, bScriptHostPed);
        internal static Ped CREATE_PED(Model model, Vector3 position, float heading, bool isNetwork = false, bool bScriptHostPed = false, int pedType = 0)
        => NativeFunction.Natives.CREATE_PED<Ped>(pedType, model, position.X, position.Y, position.Z, heading, isNetwork, bScriptHostPed);
        internal static void SET_PED_AS_COP(Ped ped, bool toggle)
        => NativeFunction.Natives.SET_PED_AS_COP(ped, toggle);
        internal static void REVIVE_INJURED_PED(Ped ped)
        => NativeFunction.Natives.REVIVE_INJURED_PED(ped);
        internal static int CREATE_SYNCHRONIZED_SCENE(float x, float y, float z, float roll, float pitch, float yaw, int p6)
        => NativeFunction.Natives.CREATE_SYNCHRONIZED_SCENE<int>(x, y, z, roll, pitch, yaw, p6);
        internal static void SET_SYNCHRONIZED_SCENE_LOOPED(int sceneID, bool toggle)
        => NativeFunction.Natives.SET_SYNCHRONIZED_SCENE_LOOPED(sceneID, toggle);
        internal static void SET_SYNCHRONIZED_SCENE_PHASE(int sceneID, float phase)
        => NativeFunction.Natives.SET_SYNCHRONIZED_SCENE_PHASE(sceneID, phase);
        internal static bool IS_SYNCHRONIZED_SCENE_RUNNING(int sceneId)
        => NativeFunction.Natives.IS_SYNCHRONIZED_SCENE_RUNNING<bool>(sceneId);
    }

    internal static class STREAMING
    {
        internal static void REQUEST_MODEL(Model hash)
        => NativeFunction.Natives.REQUEST_MODEL(hash);
        internal static void REQUEST_ANIM_DICT(string animDict)
        => NativeFunction.Natives.REQUEST_ANIM_DICT(animDict);
    }

    internal static class TASK
    {
        internal static void SET_DRIVE_TASK_DRIVING_STYLE(Ped ped, int drivingStyle)
        => NativeFunction.Natives.SET_DRIVE_TASK_DRIVING_STYLE(ped, drivingStyle);
        internal static void TASK_VEHICLE_PARK(Ped ped, Vehicle vehicle, float x, float y, float z, float heading, int mode, float radius, bool keepEngineOn)
        => NativeFunction.Natives.TASK_VEHICLE_PARK(ped, vehicle, x, y, z, heading, mode, radius, keepEngineOn);
        internal static void TASK_GO_TO_ENTITY(Entity entity, Entity target, int duration, float distance, float speed, float p5 = 1073741824f, int p6 = 0)
        => NativeFunction.Natives.TASK_GO_TO_ENTITY(entity, target, duration, distance, speed, p5, p6);
        internal static void SET_TASK_VEHICLE_CHASE_IDEAL_PURSUIT_DISTANCE(Ped ped, float distance)
        => NativeFunction.Natives.SET_TASK_VEHICLE_CHASE_IDEAL_PURSUIT_DISTANCE(ped, distance);
        internal static void SET_TASK_VEHICLE_CHASE_BEHAVIOR_FLAG(Ped ped, int flag, bool set = true)
        => NativeFunction.Natives.SET_TASK_VEHICLE_CHASE_BEHAVIOR_FLAG(ped, flag, set);
        internal static void TASK_SYNCHRONIZED_SCENE(Ped ped, int scene, string animDictionary, string animationName, float speed, float speedMultiplier, int duration, int flag, float playbackRate = 0x447a0000)
        => NativeFunction.Natives.TASK_SYNCHRONIZED_SCENE(ped, scene, animDictionary, animationName, speed, speedMultiplier, duration, flag, playbackRate, 0);
    }

    internal static class MISC
    {
        internal static int UPDATE_ONSCREEN_KEYBOARD()
        => NativeFunction.Natives.UPDATE_ONSCREEN_KEYBOARD<int>();
        internal static Model GET_HASH_KEY(string name)
        => NativeFunction.Natives.GET_HASH_KEY<Model>(name);
    }

    internal static class HUD
    {
        internal static void BEGIN_TEXT_COMMAND_PRINT(string GxtEntry)
        => NativeFunction.Natives.BEGIN_TEXT_COMMAND_PRINT(GxtEntry);
        internal static void END_TEXT_COMMAND_PRINT(int duration, bool drawImmediately)
        => NativeFunction.Natives.END_TEXT_COMMAND_PRINT(duration, drawImmediately);
        internal static void BEGIN_TEXT_COMMAND_DISPLAY_HELP(string inputType)
        => NativeFunction.Natives.BEGIN_TEXT_COMMAND_DISPLAY_HELP(inputType);
        internal static void END_TEXT_COMMAND_DISPLAY_HELP(int shape, bool loop, bool beep, int duration)
        => NativeFunction.Natives.END_TEXT_COMMAND_DISPLAY_HELP(shape, loop, beep, duration);
        internal static void BEGIN_TEXT_COMMAND_THEFEED_POST(string text)
        => NativeFunction.Natives.BEGIN_TEXT_COMMAND_THEFEED_POST(text);
        internal static void ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(string text)
        => NativeFunction.Natives.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME(text);
        internal static void END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(string textureDict, string textureName, bool flash, int iconType, string sender, string subject)
        => NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT(textureDict, textureName, flash, iconType, sender, subject);
        internal static void END_TEXT_COMMAND_THEFEED_POST_TICKER(bool isImportant, bool bHasTokens)
        => NativeFunction.Natives.END_TEXT_COMMAND_THEFEED_POST_TICKER(isImportant, bHasTokens);
    }

    internal static class WEAPON
    {
        internal static void GIVE_WEAPON_TO_PED(Ped ped, Model weaponHash, int ammoCount, bool isHidden = false, bool bForceInHand = false)
        => NativeFunction.Natives.GIVE_WEAPON_TO_PED(ped, weaponHash, ammoCount, isHidden, bForceInHand);
        internal static void GIVE_WEAPON_COMPONENT_TO_PED(Ped ped, Model weaponHash, Model componentHash)
        => NativeFunction.Natives.GIVE_WEAPON_COMPONENT_TO_PED(ped, weaponHash, componentHash);
    }
}