namespace AccessibilityTweaks.Features.WeatherEffects.Patches;

/// <summary>
///     Harmony Patches for the <see cref="DefaultShaderUniforms"/> class. This class cannot be inherited.
/// </summary>
public sealed class DefaultShaderUniformsPatches : GantrySettingsPatch<WeatherEffectsSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "Update" method in <see cref="DefaultShaderUniforms"/>.
    /// </summary>
    /// <param name="__instance">The instance of <see cref="AmbientManager"/> this patch has been applied to.</param>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(DefaultShaderUniforms), "Update")]
    public static void Patch_DefaultShaderUniforms_Update_Prefix(DefaultShaderUniforms __instance)
    {
        if (Settings.MistEnabled) return;
        __instance.FlagFogDensity = 0f;
        __instance.FlatFogStartYPos = 1024f;
    }
}