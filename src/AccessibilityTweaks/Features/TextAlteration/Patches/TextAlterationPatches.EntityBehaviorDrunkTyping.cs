
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

/// <summary>
///     Harmony Patches for the <see cref="TextAlteration"/> feature. This class cannot be inherited.
/// </summary>
public sealed partial class TextAlterationPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "slurText" method of the <see cref="EntityBehaviorDrunkTyping"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorDrunkTyping), "slurText")]
    public static bool Patch_EntityBehaviorDrunkTyping_slurText_Prefix(string text, ref string __result)
    {
        __result = text;
        return Settings.WhileDrunk;
    }
}