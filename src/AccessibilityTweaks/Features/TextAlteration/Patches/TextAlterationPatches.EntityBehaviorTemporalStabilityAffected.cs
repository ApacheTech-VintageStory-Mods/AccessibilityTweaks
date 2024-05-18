
// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Patches;

public sealed partial class TextAlterationPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "destabilizeText" method of the <see cref="EntityBehaviorHunger"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorTemporalStabilityAffected), "destabilizeText")]
    public static bool Patch_EntityBehaviorTemporalStabilityAffected_destabilizeText_Prefix(string text, ref string __result)
    {
        __result = text;
        return Settings.WhileDrunk;
    }
}