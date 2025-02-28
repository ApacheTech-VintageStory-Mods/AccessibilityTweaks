using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

// ReSharper disable InconsistentNaming

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed class FreezingPerceptionEffectPatches : WorldSettingsConsumer<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ApplyMotionEffects" method in the <see cref="FreezingPerceptionEffect"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(FreezingPerceptionEffect), "ApplyMotionEffects")]
    public static bool Patch_FreezingPerceptionEffect_ApplyMotionEffects_Prefix()
        => Settings.InvoluntaryMouseMovement;
}