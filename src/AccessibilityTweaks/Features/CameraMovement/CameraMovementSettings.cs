namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement;

/// <summary>
///     Settings for involuntary camera and mouse movements.
/// </summary>
public sealed class CameraMovementSettings : FeatureSettings
{
    /// <summary>
    ///     Limits/Intensifies the strength of Perception Warp Effects within the game.
    /// </summary>
    public float PerceptionWarpMultiplier { get; set; } = 1f;

    /// <summary>
    ///     Enables/Disables the ability for the game to move the mouse without the player's input.
    /// </summary>
    public bool InvoluntaryMouseMovement { get; set; } = true;
}