using System.Reflection;

namespace AccessibilityTweaks.Features.TextAlteration.Dialogue.Components;

/// <summary>
///     Composable GUI Component for the Text Alteration Dialogue Window.
/// </summary>
/// <seealso cref="IGuiComposablePart" />
public sealed class TextAlterationGuiComponent : IGuiComposablePart
{
    private GuiComposer? _composer;
    private GenericDialogue? _parent;

    private readonly TextAlterationSettings _settings;
    private const string FeatureName = "TextAlteration";

    private const int SwitchSize = 20;
    private const int GapBetweenRows = 25;

    /// <summary>
    ///     Initialises a new instance of the <see cref="TextAlterationGuiComponent"/> class.
    /// </summary>
    public TextAlterationGuiComponent()
    {
        _settings = G.Settings.World.Feature<TextAlterationSettings>();
    }

    /// <summary>
    ///     Initialises a new instance of the <see cref="TextAlterationGuiComponent"/> class.
    /// </summary>
    /// <param name="bounds">The bounds.</param>
    public TextAlterationGuiComponent(ElementBounds bounds)
    {
        Bounds = bounds;
        Bounds.IsDrawingSurface = true;
        _settings = G.Settings.World.Feature<TextAlterationSettings>();
    }

    /// <summary>
    ///     Refreshes the displayed values on the form.
    /// </summary>
    /// <param name="composer"></param>
    public void RefreshValues(GuiComposer composer)
    {
        foreach (var propertyInfo in _settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            composer
                .GetSwitch($"btn{propertyInfo.Name}")?
                .SetValue(propertyInfo.GetValue(_settings)?.To<bool>() ?? default);
        }
    }

    /// <summary>
    ///     Uses the injected <see cref="GuiComposer" /> to compose part of a dialogue window.
    /// </summary>
    /// <param name="composer"></param>
    /// <returns></returns>
    public GuiComposer ComposePart(GenericDialogue parent, GuiComposer composer)
    {
        _composer = composer;
        _parent = parent;
        const int switchSize = 20;
        var font = CairoFont.WhiteSmallText();
        var textWidth = CalculateWidth(font);
        Bounds ??= ElementBounds.FixedSize(0, 20);

        var left = ElementBounds.Fixed(0, 15, textWidth, switchSize).FixedUnder(Bounds, 15);
        var right = ElementBounds.Fixed(textWidth + 10, 10, switchSize, switchSize).FixedUnder(Bounds, 15);

        _composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.DuringTemporalStorm)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        _composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.DuringTemporalStorm)}.HoverText"), font, 260, left);
        _composer.AddSwitch(OnDuringTemporalStormChanged, right.FlatCopy().WithFixedWidth(SwitchSize), $"btn{nameof(_settings.DuringTemporalStorm)}");

        left = left.BelowCopy(fixedDeltaY: GapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: GapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.WhileDrunk)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.WhileDrunk)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnWhileDrunkChanged, right.FlatCopy().WithFixedWidth(SwitchSize), $"btn{nameof(_settings.WhileDrunk)}");

        left = left.BelowCopy(fixedDeltaY: GapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: GapBetweenRows);

        composer.AddStaticText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.WhileStarving)}"), font.Clone().WithOrientation(EnumTextOrientation.Right), left);
        composer.AddHoverText(G.Lang.Translate(FeatureName, $"Dialogue.lbl{nameof(_settings.WhileStarving)}.HoverText"), font, 260, left);
        composer.AddSwitch(OnWhileStarvingChanged, right.FlatCopy().WithFixedWidth(SwitchSize), $"btn{nameof(_settings.WhileStarving)}");

        return composer;
    }

    private void OnDuringTemporalStormChanged(bool state)
    {
        _settings.DuringTemporalStorm = state;
        RefreshValues(_composer!);
    }

    private void OnWhileDrunkChanged(bool state)
    {
        _settings.WhileDrunk = state;
        RefreshValues(_composer!);
    }

    private void OnWhileStarvingChanged(bool state)
    {
        _settings.WhileStarving = state;
        RefreshValues(_composer!);
    }

    /// <summary>
    ///     The bounds of the component.
    /// </summary>
    public ElementBounds Bounds { get; set; } = ElementBounds.Empty;

    private static double CalculateWidth(CairoFont font)
    {
        return typeof(TextAlterationSettings)
            .GetProperties()
            .Select(x => x.Name)
            .Select(propertyName => G.Lang.Translate(FeatureName, $"Dialogue.lbl{propertyName}"))
            .Select(text => font.GetTextExtents(text).Width / ClientSettings.GUIScale + 30)
            .Prepend(0.0)
            .Max();
    }
}