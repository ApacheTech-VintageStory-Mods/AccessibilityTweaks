
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

public sealed partial class TextAlterationPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPre" method of the <see cref="EntityBehaviorHunger"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "onChatKeyDownPre")]
    public static bool Patch_EntityBehaviorTemporalStabilityAffected_onChatKeyDownPre_Prefix()
    {
        return Settings.DuringTemporalStorm;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPost" method of the <see cref="EntityBehaviorHunger"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "onChatKeyDownPost")]
    public static bool Patch_EntityBehaviorTemporalStabilityAffected_onChatKeyDownPost_Prefix()
    {
        return Settings.DuringTemporalStorm;
    }
}