namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration;

/// <summary>
///     Settings for text alteration effects.
/// </summary>
public sealed class TextAlterationSettings : FeatureSettings
{
    /// <summary>
    ///     Enables/Disables the alteration of chat messages during a temporal storm.
    /// </summary>
    public bool DuringTemporalStorm { get; set; } = true;

    /// <summary>
    ///     Enables/Disables the alteration of chat messages while the character is drunk.
    /// </summary>
    public bool WhileDrunk { get; set; } = true;

    /// <summary>
    ///     Enables/Disables the alteration of chat messages while the character is starving.
    /// </summary>
    public bool WhileStarving { get; set; } = true;
}