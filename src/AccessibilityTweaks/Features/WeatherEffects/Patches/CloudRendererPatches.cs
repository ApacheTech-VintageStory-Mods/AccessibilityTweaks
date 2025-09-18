namespace AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="CloudRenderer"/> class. This class cannot be inherited.
/// </summary>
public sealed class CloudRendererPatches : GantrySettingsPatch<WeatherEffectsSettings>
{
    /// <summary>
    ///     Applies a Prefix patch to the "CloudTick" method in <see cref="CloudRenderer"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(CloudRenderer), "CloudTick")]
    public static bool Patch_CloudRenderer_CloudTick_Prefix()
        => Settings.CloudsEnabled;
}