namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

// ReSharper disable InconsistentNaming

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed partial class CameraMovementPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ApplyMotionEffects" method in the <see cref="FreezingPerceptionEffect"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(FreezingPerceptionEffect), "ApplyMotionEffects")]
    public static bool Patch_FreezingPerceptionEffect_OnBeforeGameRender_Prefix(FreezingPerceptionEffect __instance)
    {
        if (Settings.InvoluntaryMouseMovement) return true;
        ApiEx.Client.Render.ShaderUniforms.PerceptionEffectIntensity = 0;
        __instance.Intensity = 0;
        __instance.NowDisabled();
        return false;
    }
}