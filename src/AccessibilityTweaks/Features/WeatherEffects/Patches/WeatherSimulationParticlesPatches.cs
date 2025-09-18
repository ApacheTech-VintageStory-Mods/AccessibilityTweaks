namespace AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="WeatherSimulationParticles"/> class. This class cannot be inherited.
/// </summary>
public sealed class WeatherSimulationParticlesPatches : GantrySettingsPatch<WeatherEffectsSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "SpawnHailParticles" method in <see cref="WeatherSimulationParticles"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationParticles), "SpawnHailParticles")]
    public static bool Patch_WeatherSimulationParticles_SpawnHailParticles_Prefix() 
        => Settings.HailstonesEnabled;

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "SpawnRainParticles" method in <see cref="WeatherSimulationParticles"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationParticles), "SpawnRainParticles")]
    public static bool Patch_WeatherSimulationParticles_SpawnRainParticles_Prefix() 
        => Settings.RaindropsEnabled;

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "SpawnSnowParticles" method in <see cref="WeatherSimulationParticles"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationParticles), "SpawnSnowParticles")]
    public static bool Patch_WeatherSimulationParticles_SpawnSnowParticles_Prefix() 
        => Settings.SnowflakesEnabled;

    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "SpawnDustParticles" method in <see cref="WeatherSimulationParticles"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(WeatherSimulationParticles), "SpawnDustParticles")]
    public static bool Patch_WeatherSimulationParticles_SpawnDustParticles_Prefix() 
        => Settings.DustParticlesEnabled;
}