namespace JapaneseCallouts.Helpers;

// https://gtaforums.com/topic/858970-all-gtao-face-ids-pedset_ped_head_blend_data-explained/
// 男性の顔: 0~20
// 女性の顔: 21~40
// 41以降は不明。第三者枠?

// This source code is came from here. Thanks Mr.Yobbin
// https://github.com/YobB1n/YobbinCallouts/blob/master/EUPHelper.cs
internal static class FreemodeHelpers
{
    private const string MP_M_FREEMODE_01 = "MP_M_FREEMODE_01";
    private const string MP_F_FREEMODE_01 = "MP_F_FREEMODE_01";

    internal static void GenerateRandomCharacter(this Ped ped)
    {
        SetRandomHeadBlend(ped);
        SetRandomFaceFeatures(ped);
        SetRandomAppearance(ped);
    }

    private static void SetRandomHeadBlend(this Ped ped)
    {
        var shapeMix = Main.MersenneTwister.NextSingle();
        var skinMix = Main.MersenneTwister.NextSingle();
        if (ped.Model == new Model(MP_M_FREEMODE_01))
        {
            var mother = Main.MersenneTwister.Next(0);
            var father = Main.MersenneTwister.Next(20);
            NativeFunction.Natives.SET_PED_HEAD_BLEND_DATA(ped, mother, father, 0, mother, father, 0, shapeMix, skinMix, 0f, false);
        }
        else if (ped.Model == new Model(MP_F_FREEMODE_01))
        {
            var mother = Main.MersenneTwister.Next(21, 41);
            var father = Main.MersenneTwister.Next(21, 41);
            NativeFunction.Natives.SET_PED_HEAD_BLEND_DATA(ped, mother, father, 0, mother, father, 0, shapeMix, skinMix, 0f, false);
        }
    }

    private static void SetRandomFaceFeatures(Ped ped)
    {
        for (int i = 0; i < 20; i++)
        {
            var value = 0f;
            if (Main.MersenneTwister.Next(2) is 1)
            {
                value = Main.MersenneTwister.NextSingle();
                if (Main.MersenneTwister.Next(2) is 0) value *= -1f;
            }

            SetPedFacialFeature(ped, i, value);
        }
    }

    private static void SetPedFacialFeature(Ped ped, int facialFeature, float value)
    {
        NativeFunction.Natives.x71A5C1DBA060049E(ped, facialFeature, value);
    }

    private static void SetRandomAppearance(Ped ped)
    {
        SetRandomHair(ped);

        // 1: ヒゲ
        // 2: まゆげ
        // 3: 老化
        NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 1, Main.MersenneTwister.Next(2) is 0 ? Main.MersenneTwister.Next(29) : 255, 0.8f);
        NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 2, Main.MersenneTwister.Next(2) is 0 ? Main.MersenneTwister.Next(34) : 255, 0.8f);
        NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 3, Main.MersenneTwister.Next(2) is 0 ? Main.MersenneTwister.Next(15) : 255, 0.8f);
        // 目の色を設定
        NativeFunction.Natives.x50B56988B170AFDF(ped, Main.MersenneTwister.Next(7));
    }

    private static void SetRandomHair(Ped ped)
    {
        // Banned Male Hairstyles
        var excludedMaleHairItems = new int[] { 8, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        // Banned Female Hairstyles
        var excludedFemaleHairItems = new int[] { };

        var drawableCount = NativeFunction.Natives.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS<int>(ped, 2);
        int randomHair = Main.MersenneTwister.Next(drawableCount - 1);
        var randomBrow = Main.MersenneTwister.Next(22);

        if (ped.Model == new Model(MP_M_FREEMODE_01))
        {
            var beardChance = Main.MersenneTwister.Next(10);
            var randomBeard = beardChance <= 3 ? Main.MersenneTwister.Next(18) : 255;

            if (excludedMaleHairItems.Contains(randomHair))
            {
                SetRandomHair(ped);
            }
            else
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 2, randomHair, 0, 2);
                NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 1, randomBeard, 1f);
                NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 2, randomBrow, 1f);
                SetRandomHairColor(ped);
                return;
            }
        }
        else if (ped.Model == new Model(MP_F_FREEMODE_01))
        {
            if (excludedFemaleHairItems.Contains(randomHair))
            {
                SetRandomHair(ped);
            }
            else
            {
                NativeFunction.Natives.SET_PED_COMPONENT_VARIATION(ped, 2, randomHair, 0, 2);
                NativeFunction.Natives.SET_PED_HEAD_OVERLAY(ped, 2, randomBrow, 1f);
                SetRandomHairColor(ped);
                return;
            }
        }
    }

    private static void SetRandomHairColor(Ped ped)
    {
        // Banned Hair Colors:
        // List only goes until 27 to filter out 'unnatural'
        // hair colors like pink or green.
        var excludedHairColors = new int[] { 20, 21, 22, 23, 24, 25, 26 };
        var randomColor = Main.MersenneTwister.Next(27);

        // Checks if the random hair color is on the banned hair color list.
        // Checks if bannedColor was set to true, if so, repeat the randomization.
        if (excludedHairColors.Contains(randomColor))
        {
            SetRandomHairColor(ped);
        }
        else
        {
            NativeFunction.Natives.SET_PED_HAIR_TINT(ped, randomColor, randomColor);
            SetPedHeadOverlayColor(ped, 1, randomColor);
            SetPedHeadOverlayColor(ped, 2, randomColor);
        }
    }

    private static void SetPedHeadOverlayColor(Ped ped, int id, int color)
    {
        NativeFunction.Natives.x497BF74A7B9CB952(ped, id, 1, color, color);
    }
}