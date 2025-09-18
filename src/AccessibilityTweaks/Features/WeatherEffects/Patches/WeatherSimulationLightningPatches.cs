namespace AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="WeatherSimulationLightning"/> class. This class cannot be inherited.
/// </summary>
public sealed class WeatherSimulationLightningPatches : GantrySettingsPatch<WeatherEffectsSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ClientTick" method in <see cref="WeatherSimulationLightning"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationLightning), "ClientTick")]
    public static bool Patch_WeatherSimulationLightning_ClientTick_Prefix() 
        => Settings.LightningEnabled;

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "OnRenderFrame" method in <see cref="WeatherSimulationLightning"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationLightning), "OnRenderFrame")]
    public static bool Patch_WeatherSimulationLightning_OnRenderFrame_Prefix() 
        => Settings.LightningEnabled;
}