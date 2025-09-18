namespace AccessibilityTweaks.Features.TextAlteration.Patches;

public sealed class EntityBehaviorTemporalStabilityAffectedPatches : GantrySettingsPatch<TextAlterationSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "destabilizeText" method of the <see cref="EntityBehaviorHunger"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(EntityBehaviorTemporalStabilityAffected), "destabilizeText")]
    public static bool Patch_EntityBehaviorTemporalStabilityAffected_destabilizeText_Prefix(string text, ref string __result)
    {
        __result = text;
        return Settings.WhileDrunk;
    }
}