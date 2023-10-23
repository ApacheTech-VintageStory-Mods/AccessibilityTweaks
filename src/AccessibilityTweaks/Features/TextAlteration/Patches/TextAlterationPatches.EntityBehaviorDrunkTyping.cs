
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

/// <summary>
///     Harmony Patches for the <see cref="TextAlteration"/> feature. This class cannot be inherited.
/// </summary>
public sealed partial class TextAlterationPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPre" method of the <see cref="EntityBehaviorDrunkTyping"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorDrunkTyping), "onChatKeyDownPre")]
    public static bool Patch_EntityBehaviorDrunkTyping_onChatKeyDownPre_Prefix()
    {
        return Settings.WhileDrunk;
    }

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "onChatKeyDownPost" method of the <see cref="EntityBehaviorDrunkTyping"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorDrunkTyping), "onChatKeyDownPost")]
    public static bool Patch_EntityBehaviorDrunkTyping_onChatKeyDownPost_Prefix()
    {
        return Settings.WhileDrunk;
    }
}