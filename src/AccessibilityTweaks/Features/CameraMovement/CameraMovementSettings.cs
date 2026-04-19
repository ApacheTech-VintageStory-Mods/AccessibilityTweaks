namespace AccessibilityTweaks.Features.CameraMovement;

/// <summary>
///     Settings for involuntary camera and mouse movements.
/// </summary>
public sealed class CameraMovementSettings : FeatureSettings<CameraMovementSettings>
{
    /// <summary>
    ///     Limits/Intensifies the strength of Perception Warp Effects within the game.
    /// </summary>
    public float PerceptionWarpMultiplier { get; set; } = 1f;

    /// <summary>
    ///     The level at which entities flicker during a Temporal Storm.
    /// </summary>
    public float GlitchEffectStrengthMultiplier { get; set; } = 2f;

    /// <summary>
    ///     The level at which the world distorts when under the influence of psychedelic substances.
    /// </summary>
    public float PsychedelicStrengthMultiplier { get; set; } = 1f;

    /// <summary>
    ///     Enables/Disables the ability for the game to move the mouse without the player's input.
    /// </summary>
    public bool InvoluntaryMouseMovement { get; set; } = true;
}