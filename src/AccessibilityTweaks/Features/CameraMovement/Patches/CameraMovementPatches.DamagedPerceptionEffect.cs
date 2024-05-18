namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Patches;

// ReSharper disable InconsistentNaming

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed partial class CameraMovementPatches
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ApplyMotionEffects" method in the <see cref="DamagedPerceptionEffect"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyPatch(typeof(DamagedPerceptionEffect), "ApplyMotionEffects")]
    public static bool Patch_DamagedPerceptionEffect_ApplyMotionEffects_Prefix()
        => Settings.InvoluntaryMouseMovement;
}