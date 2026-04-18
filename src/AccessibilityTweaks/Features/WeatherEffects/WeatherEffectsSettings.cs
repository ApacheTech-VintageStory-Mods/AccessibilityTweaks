namespace AccessibilityTweaks.Features.WeatherEffects;

/// <summary>
///     Settings for weather effects.
/// </summary>
public sealed class WeatherEffectsSettings : FeatureSettings<WeatherEffectsSettings>
{
    /// <summary>
    ///     Indicates whether rain particle effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if rain particle effects are enabled; otherwise, <c>false</c>.</value>
    public bool RaindropsEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether hail particle effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if hail particle effects are enabled; otherwise, <c>false</c>.</value>
    public bool HailstonesEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether snow particle effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if snow particle effects are enabled; otherwise, <c>false</c>.</value>
    public bool SnowflakesEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether dust particle effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if dust particles effects are enabled; otherwise, <c>false</c>.</value>
    public bool DustParticlesEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether lightning flashing light effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if lightning effects are enabled; otherwise, <c>false</c>.</value>
    public bool LightningEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether fog effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if fog effects are enabled; otherwise, <c>false</c>.</value>
    public bool FogEnabled { get; set; } = true;

    /// <summary>
    ///     Indicates whether mist effects should be rendered, or not.
    /// </summary>
    /// <value><c>true</c> if mist effects are enabled; otherwise, <c>false</c>.</value>
    public bool MistEnabled { get; set; } = true;
}