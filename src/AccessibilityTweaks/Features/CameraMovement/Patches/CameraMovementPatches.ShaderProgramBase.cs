using Vintagestory.Client;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="ShaderProgramBase"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed partial class CameraMovementPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyTranspiler"/> patch to the "Use" method of the <see cref="ShaderProgramBase"/> class.
    /// </summary>
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ShaderProgramBase), "Use")]
    public static void Patch_ShaderProgramBase_Use_Postfix(ShaderProgramBase __instance)
    {
        if (!__instance.includes.Contains("vertexwarp.vsh")) return;
        var shUniforms = ScreenManager.Platform.ShaderUniforms;
        __instance.Uniform("globalWarpIntensity", shUniforms.GlobalWorldWarp * Settings.GlobalWarpMultiplier);
        __instance.Uniform("glitchWaviness", shUniforms.GlitchWaviness * Settings.GlitchStrengthMultiplier);
        __instance.Uniform("perceptionEffectIntensity", shUniforms.PerceptionEffectIntensity * Settings.PerceptionWarpMultiplier);
    }
}