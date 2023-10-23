namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="CloudRenderer"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="WeatherEffectsSettings" />
public sealed partial class WeatherEffectsPatches
{
    /// <summary>
    ///     Applies a Prefix patch to the "CloudTick" method in <see cref="CloudRenderer"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CloudRenderer), "CloudTick")]
    public static bool Patch_CloudRenderer_CloudTick_Prefix()
    {
        return Settings.CloudsEnabled;
    }
}