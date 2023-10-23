

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="AmbientManager"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="WeatherEffectsSettings" />
public sealed partial class WeatherEffectsPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "Update" method in <see cref="DefaultShaderUniforms"/>.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="AmbientManager"/> this patch has been applied to.</param>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(DefaultShaderUniforms), "Update")]
    public static void Patch_DefaultShaderUniforms_Update_Prefix(DefaultShaderUniforms __instance)
    {
        if (Settings.MistEnabled) return;
        __instance.FlagFogDensity = 0f;
        __instance.FlatFogStartYPos = 1024f;
    }
}