using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Extensions;
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
    private static string LangEntry(string code)
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

        var left = ElementBounds.Fixed(0, 10, textWidth, switchSize).FixedUnder(Bounds);
        var right = ElementBounds.Fixed(textWidth + 10, 10, 270, switchSize).FixedUnder(Bounds);

        composer
            .AddStaticText(LangEntry("lblGlobalWarpMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblGlobalWarpMultiplier.HoverText"), labelFont, (int)textWidth, left)
            .AddSlider(OnGlobalWarpMultiplierChanged, right, "sldGlobalWarpMultiplier");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(LangEntry("lblPerceptionWarpMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblPerceptionWarpMultiplier.HoverText"), labelFont, (int)textWidth, left)
            .AddSlider(OnPerceptionWarpMultiplierChanged, right, "sldPerceptionWarpMultiplier");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(LangEntry("lblGlitchStrengthMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblGlitchStrengthMultiplier.HoverText"), labelFont, (int)textWidth, left)
            .AddSlider(OnGlitchStrengthMultiplierChanged, right, "sldGlitchStrengthMultiplier");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(LangEntry("lblCameraShakeMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblCameraShakeMultiplier.HoverText"), labelFont, (int)textWidth, left)
            .AddSlider(OnCameraShakeMultiplierChanged, right, "sldCameraShakeMultiplier");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows + 5);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(LangEntry("lblInvoluntaryMouseMovement"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblInvoluntaryMouseMovement.HoverText"), labelFont, (int)textWidth, left)
            .AddSwitch(OnInvoluntaryMouseMovementToggle, right, "btnInvoluntaryMouseMovement");

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows + 5);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(LangEntry("lblHeldItemBobbing"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(LangEntry("lblHeldItemBobbing.HoverText"), labelFont, (int)textWidth, left)
            .AddSwitch(OnHeldItemBobbingToggle, right, "btnHeldItemBobbing");
        return composer;
    }

    private bool OnGlobalWarpMultiplierChanged(int value)
    {
        _settings.GlobalWarpMultiplier = value / 100f;
        ApiEx.Client.Shader.ReloadShadersThreadSafe();
        return true;
    }

    private bool OnPerceptionWarpMultiplierChanged(int value)
    {
        _settings.PerceptionWarpMultiplier = value / 100f;
        ApiEx.Client.Shader.ReloadShadersThreadSafe();
        return true;
    }

    private bool OnGlitchStrengthMultiplierChanged(int value)
    {
        _settings.GlitchStrengthMultiplier = value / 100f;
        return true;
    }

    private bool OnCameraShakeMultiplierChanged(int value)
    {
        _settings.CameraShakeMultiplier = value / 100f;
        ApiEx.Client.Shader.ReloadShadersThreadSafe();
        return true;
    }

    private void OnInvoluntaryMouseMovementToggle(bool state)
    {
        _settings.InvoluntaryMouseMovement = state;
        ApiEx.Client.Shader.ReloadShadersThreadSafe();
    }

    private void OnHeldItemBobbingToggle(bool state)
    {
        _settings.HeldItemBobbing = state;
        ApiEx.Client.Shader.ReloadShadersThreadSafe();
    }

    private static void SetSliderProperties(GuiElementSlider slider, float value)
    {
        slider.TriggerOnlyOnMouseUp(true);
        slider.SetValues((int)(value * 100), 0, 200, 1, "%");
    }

    /// <summary>
    ///     Refreshes the displayed values on the form.
    /// </summary>
    /// <param name="composer"></param>
    public void RefreshValues(GuiComposer composer)
    {
        SetSliderProperties(composer.GetSlider("sldGlobalWarpMultiplier"), _settings.GlobalWarpMultiplier);
        SetSliderProperties(composer.GetSlider("sldPerceptionWarpMultiplier"), _settings.PerceptionWarpMultiplier);
        SetSliderProperties(composer.GetSlider("sldGlitchStrengthMultiplier"),_settings.GlitchStrengthMultiplier);
        SetSliderProperties(composer.GetSlider("sldCameraShakeMultiplier"), _settings.CameraShakeMultiplier);
        composer.GetSwitch("btnInvoluntaryMouseMovement").SetValue(_settings.InvoluntaryMouseMovement);
        composer.GetSwitch("btnHeldItemBobbing").SetValue(_settings.HeldItemBobbing);
    }

    /// <summary>
    ///     The bounds of the element.
    /// </summary>
    public ElementBounds Bounds { get; set; }

    private static double CalculateWidth(CairoFont font)
    {
        return typeof(CameraMovementSettings).GetProperties()
            .Select(x => x.Name)
            .Select(propertyName => LangEntry($"lbl{propertyName}"))
            .Select(text => font.GetTextExtents(text).Width / ClientSettings.GUIScale + 30)
            .Prepend(0.0)
            .Max();
    }
}