namespace AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed class FreezingPerceptionEffectPatches : GantrySettingsPatch<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ApplyMotionEffects" method in the <see cref="FreezingPerceptionEffect"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(FreezingPerceptionEffect), "ApplyMotionEffects")]
    public static bool Patch_FreezingPerceptionEffect_ApplyMotionEffects_Prefix()
        => Settings.InvoluntaryMouseMovement;
}