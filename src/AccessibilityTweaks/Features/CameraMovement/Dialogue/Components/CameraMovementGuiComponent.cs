using ApacheTech.Common.DependencyInjection.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Extensions;
using Gantry.Core.GameContent.Extensions.Gui;
using Gantry.Services.FileSystem.Configuration;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Dialogue.Components;

/// <summary>
///     Camera Movement GUI Composable Component
/// </summary>
/// <seealso cref="IGuiComposablePart" />
[UsedImplicitly]
public sealed class CameraMovementGuiComponent : IGuiComposablePart
{
    private readonly CameraMovementSettings _settings;

    /// <summary>
    ///     Initialises a new instance of the <see cref="CameraMovementGuiComponent"/> class.
    /// </summary>
    public CameraMovementGuiComponent()
    {
        _settings = ModSettings.World.Feature<CameraMovementSettings>();
    }

    /// <summary>
    ///     Initialises a new instance of the <see cref="CameraMovementGuiComponent"/> class.
    /// </summary>
    /// <param name="bounds">The bounds.</param>
    public CameraMovementGuiComponent(ElementBounds bounds)
    {
        Bounds = bounds;
        Bounds.IsDrawingSurface = true;
        _settings = ModSettings.World.Feature<CameraMovementSettings>();
    }

    /// <summary>
    ///     Gets an entry from the language files, for the feature this instance is representing.
    /// </summary>
    /// <param name="code">The entry to return.</param>
    /// <returns>A localised <see cref="string"/>, for the specified language file code.</returns>
    private static string T(string code)
    {
        return LangEx.FeatureString("CameraMovement.Dialogue", code);
    }

    /// <summary>
    ///     Uses the injected <see cref="GuiComposer" /> to compose part of a GUI.
    /// </summary>
    /// <param name="composer"></param>
    /// <returns></returns>
    public GuiComposer ComposePart(GuiComposer composer)
    {
        const int switchSize = 20;
        const int gapBetweenRows = 30;
        var labelFont = CairoFont.WhiteSmallText();
        var textWidth = CalculateWidth(labelFont);
        Bounds ??= ElementBounds.FixedSize(0, 20);

        //
        // Perception Warp Multiplier
        //

        var left = ElementBounds.Fixed(0, 10, textWidth, switchSize).FixedUnder(Bounds);
        var right = ElementBounds.Fixed(textWidth + 10, 10, 270, switchSize).FixedUnder(Bounds);

        composer
            .AddStaticText(T("lblPerceptionWarpMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblPerceptionWarpMultiplier.HoverText"), labelFont, (int)textWidth, left)
            .AddLazySlider(OnPerceptionWarpMultiplierChanged, right, "sldPerceptionWarpMultiplier");

        //
        // Involuntary Mouse Movement
        //

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(T("lblInvoluntaryMouseMovement"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblInvoluntaryMouseMovement.HoverText"), labelFont, (int)textWidth, left)
            .AddSwitch(OnInvoluntaryMouseMovementToggle, right, "btnInvoluntaryMouseMovement");

        //
        // Toggle Sneak
        //

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(T("lblToggleSneak"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblToggleSneak.HoverText"), labelFont, (int)textWidth, left)
            .AddSwitch(OnToggleSneakToggle, right, "btnToggleSneak");

        return composer;
    }

    private bool OnPerceptionWarpMultiplierChanged(int value)
    {
        _settings.PerceptionWarpMultiplier = value / 100f;
        ApiEx.Client.ReloadShadersThreadSafe();
        return true;
    }

    private void OnInvoluntaryMouseMovementToggle(bool state)
    {
        _settings.InvoluntaryMouseMovement = state;
        ApiEx.Client.ReloadShadersThreadSafe();
    }

    private void OnToggleSneakToggle(bool state)
    {
        _settings.ToggleSneak = state;
    }

    private static void SetSliderProperties(GuiElementSlider slider, float value)
    {
        slider.TriggerOnlyOnMouseUp(true);
        slider.SetValues((int)(value * 100), 0, 1000, 1, "%");
    }

    /// <summary>
    ///     Refreshes the displayed values on the form.
    /// </summary>
    /// <param name="composer"></param>
    public void RefreshValues(GuiComposer composer)
    {
        SetSliderProperties(composer.GetSlider("sldPerceptionWarpMultiplier"), _settings.PerceptionWarpMultiplier);
        composer.GetSwitch("btnInvoluntaryMouseMovement").SetValue(_settings.InvoluntaryMouseMovement);
    }

    /// <summary>
    ///     The bounds of the element.
    /// </summary>
    public ElementBounds Bounds { get; set; }

    private static double CalculateWidth(CairoFont font)
    {
        return typeof(CameraMovementSettings).GetProperties()
            .Select(x => x.Name)
            .Select(propertyName => T($"lbl{propertyName}"))
            .Select(text => font.GetTextExtents(text).Width / ClientSettings.GUIScale + 30)
            .Prepend(0.0)
            .Max();
    }
}