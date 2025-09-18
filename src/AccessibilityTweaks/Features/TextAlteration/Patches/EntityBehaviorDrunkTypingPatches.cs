namespace AccessibilityTweaks.Features.TextAlteration.Patches;

/// <summary>
///     Harmony Patches for the <see cref="TextAlteration"/> feature. This class cannot be inherited.
/// </summary>
public sealed class EntityBehaviorDrunkTypingPatches : GantrySettingsPatch<TextAlterationSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "slurText" method of the <see cref="EntityBehaviorDrunkTyping"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(EntityBehaviorDrunkTyping), "slurText")]
    public static bool Patch_EntityBehaviorDrunkTyping_slurText_Prefix(string text, ref string __result)
    {
        __result = text;
        return Settings.WhileDrunk;
    }
}