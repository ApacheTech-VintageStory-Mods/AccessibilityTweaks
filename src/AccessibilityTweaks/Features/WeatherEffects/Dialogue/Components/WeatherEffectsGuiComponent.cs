namespace AccessibilityTweaks.Features.WeatherEffects.Dialogue.Components;

/// <summary>
///     A composable GUI component for displaying Weather Effects settings to the user.
/// </summary>
/// <seealso cref="IGuiComposablePart" />
public sealed class WeatherEffectsGuiComponent : IGuiComposablePart
{
    private GuiComposer? _composer;
    private GenericDialogue? _parent;
    private readonly WeatherEffectsSettings _settings;
    private const string FeatureName = "WeatherEffects";

    /// <summary>
    ///     The bounds of the element.
    /// </summary>
    public ElementBounds Bounds { get; set; } = ElementBounds.Empty;

    /// <summary>
    ///     Initialises a new instance of the <see cref="WeatherEffectsGuiComponent"/> class.
    /// </summary>
    public WeatherEffectsGuiComponent()
    {
        _settings = G.Settings.World.Feature<WeatherEffectsSettings>();
    }

    /// <summary>
    ///     Initialises a new instance of the <see cref="WeatherEffectsGuiComponent"/> class.
    /// </summary>
    /// <param name="bounds">The bounds.</param>
    public WeatherEffectsGuiComponent(ElementBounds bounds)
    {
        Bounds = bounds;
        Bounds.IsDrawingSurface = true;
        _settings = G.Settings.World.Feature<WeatherEffectsSettings>();
    }

    /// <summary>
    ///     Refreshes the displayed values on the form.
    /// </summary>
    public void RefreshValues(GuiComposer composer)
    {
        foreach (var propertyInfo in _settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            composer
                .GetSwitch($"btn{propertyInfo.Name}")?
                .SetValue(propertyInfo.GetValue(_settings)?.To<bool>() ?? default);
        }
        ClientSettings.CloudRenderMode = _settings.CloudsEnabled ? 1 : 0;
        G.Capi.ReloadShadersThreadSafe(_parent!.Gantry);
    }

    /// <summary>
    ///     Uses the injected <see cref="GuiComposer" /> to compose part of a  dialogue window.
    /// </summary>
    public GuiComposer ComposePart(GenericDialogue parent, GuiComposer composer)
    {
        _composer = composer;
        _parent = parent;
        const int switchSize = 20;
        const int gapBetweenRows = 25;
        var font = CairoFont.WhiteSmallText();
        var textWidth = CalculateWidth(font);
        Bounds ??= ElementBounds.FixedSize(0, 20);

        var left = ElementBounds.Fixed(0, 15, textWidth, switchSize).FixedUnder(Bounds, 15);
        var right = ElementBounds.Fixed(textWidth + 10, 10, switchSize, switchSize).FixedUnder(Bounds, 15);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.RaindropsEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.RaindropsEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnRaindropsEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.RaindropsEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.HailstonesEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.HailstonesEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnHailstonesEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.HailstonesEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.SnowflakesEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.SnowflakesEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnSnowflakesEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.SnowflakesEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.DustParticlesEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.DustParticlesEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnDustParticlesEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.DustParticlesEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.LightningEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.LightningEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnLightningEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.LightningEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.CloudsEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.CloudsEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnCloudsEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.CloudsEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.FogEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.FogEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnFogEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.FogEnabled)}");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.MistEnabled)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.MistEnabled)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnMistEnabledChanged, right.FlatCopy().WithFixedWidth(switchSize), $"btn{nameof(_settings.MistEnabled)}");
            
        return composer;
    }

    private static double CalculateWidth(CairoFont font)
    {
        return typeof(WeatherEffectsSettings)
            .GetProperties()
            .Select(x => x.Name)
            .Select(propertyName => G.Lang.Translate(FeatureName, $"Dialogue.lbl{propertyName}"))
            .Select(text => font.GetTextExtents(text).Width / ClientSettings.GUIScale + 30)
            .Prepend(0.0)
            .Max();
    }

    private void OnRaindropsEnabledChanged(bool state)
    {
        _settings.RaindropsEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnHailstonesEnabledChanged(bool state)
    {
        _settings.HailstonesEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnSnowflakesEnabledChanged(bool state)
    {
        _settings.SnowflakesEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnDustParticlesEnabledChanged(bool state)
    {
        _settings.DustParticlesEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnLightningEnabledChanged(bool state)
    {
        _settings.LightningEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnCloudsEnabledChanged(bool state)
    {
        _settings.CloudsEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnFogEnabledChanged(bool state)
    {
        _settings.FogEnabled = state;
        RefreshValues(_composer!);
    }

    private void OnMistEnabledChanged(bool state)
    {
        _settings.MistEnabled = state;
        RefreshValues(_composer!);
    }
}