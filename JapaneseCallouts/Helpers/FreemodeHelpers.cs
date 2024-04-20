namespace JapaneseCallouts.Helpers;

// https://gtaforums.com/topic/858970-all-gtao-face-ids-pedset_ped_head_blend_data-explained/
// 男性の顔: 0~20
// 女性の顔: 21~40
// 41以降は不明。第三者枠?
internal static class FreemodeHelpers
{
    internal static void GenerateRandomFace(this Ped ped)
    {
        var shapeMix = Main.MersenneTwister.NextSingle();
        var skinMix = Main.MersenneTwister.NextSingle();
        if (ped.Model == new Model("MP_M_FREEMODE_01"))
        {
            var mother = Main.MersenneTwister.Next(20);
            var father = Main.MersenneTwister.Next(20);
            NativeFunction.Natives.SET_PED_HEAD_BLEND_DATA(ped, mother, father, shapeMix, skinMix);
        }
        else if (ped.Model == new Model("MP_F_FREEMODE_01"))
        {
            var mother = Main.MersenneTwister.Next(21, 41);
            var father = Main.MersenneTwister.Next(21, 41);
            NativeFunction.Natives.SET_PED_HEAD_BLEND_DATA(ped, mother, father, shapeMix, skinMix);
        }
    }
}