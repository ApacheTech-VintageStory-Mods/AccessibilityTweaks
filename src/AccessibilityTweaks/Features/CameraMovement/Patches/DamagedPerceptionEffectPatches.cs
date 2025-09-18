namespace AccessibilityTweaks.Features.CameraMovement.Patches;

/// <summary>
///     Harmony Patches for the <see cref="DrunkPerceptionEffect"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="CameraMovementSettings" />
public sealed class DamagedPerceptionEffectPatches : GantrySettingsPatch<CameraMovementSettings>
{
    /// <summary>
    ///     Applies a <see cref="HarmonyPrefix"/> patch to the "ApplyMotionEffects" method in the <see cref="DamagedPerceptionEffect"/> class.
    /// </summary>
    [HarmonyPrefix]
    [HarmonyClientPatch(typeof(DamagedPerceptionEffect), "ApplyMotionEffects")]
    public static bool Patch_DamagedPerceptionEffect_ApplyMotionEffects_Prefix()
        => Settings.InvoluntaryMouseMovement;
}