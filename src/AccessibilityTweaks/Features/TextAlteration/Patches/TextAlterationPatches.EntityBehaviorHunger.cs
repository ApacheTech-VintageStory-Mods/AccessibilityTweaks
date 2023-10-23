
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

public sealed partial class TextAlterationPatches
{

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDown" method of the <see cref="EntityBehaviorHunger"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorHunger), "onChatKeyDown")]
    public static bool Patch_EntityBehaviorHunger_onChatKeyDown_Prefix()
    {
        return Settings.WhileStarving;
    }

    // MAY BE NEEDED IN FUTURE

    ///// <summary>
    /////     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPre" method of the <see cref="EntityBehaviorHunger"/> class.
    ///// </summary>
    //[HarmonyPrefix]
    //[HarmonyPatch(typeof(EntityBehaviorHunger), "onChatKeyDownPre")]
    //public static bool Patch_EntityBehaviorHunger_onChatKeyDownPre_Prefix()
    //{
    //    return Settings.WhileStarving;
    //}

    ///// <summary>
    /////     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPost" method of the <see cref="EntityBehaviorHunger"/> class.
    ///// </summary>
    //[HarmonyPrefix]
    //[HarmonyPatch(typeof(EntityBehaviorHunger), "onChatKeyDownPost")]
    //public static bool Patch_EntityBehaviorHunger_onChatKeyDownPost_Prefix()
    //{
    //    return Settings.WhileStarving;
    //}
}