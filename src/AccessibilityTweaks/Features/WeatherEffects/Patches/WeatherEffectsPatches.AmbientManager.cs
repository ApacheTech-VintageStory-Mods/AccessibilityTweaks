

// ReSharper disable InconsistentNaming

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="AmbientManager"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="WeatherEffectsSettings" />
public sealed partial class WeatherEffectsPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPostfix"/> patch to the "UpdateAmbient" method in <see cref="AmbientManager"/>.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="AmbientManager"/> this patch has been applied to.</param>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(AmbientManager), "UpdateAmbient")]
    public static void Patch_AmbientManager_UpdateAmbient_Postfix(AmbientManager __instance)
    {
        if (Settings.FogEnabled) return;
        __instance.BlendedFogDensity = Math.Min(__instance.BlendedFogDensity, 0.001f);
        __instance.BlendedFogMin = Math.Min(__instance.BlendedFogMin, __instance.BlendedFogDensity);
    }
}