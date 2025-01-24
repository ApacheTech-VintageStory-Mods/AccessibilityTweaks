using ApacheTech.VintageMods.AccessibilityTweaks.Features.ColourCorrection.DataStructures;
using Gantry.Services.FileSystem.Dialogue;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.ColourCorrection.Dialogue;

/// <summary>
///     User interface for the colour correction settings. Gives presets for colour blindness simulation, and allows custom configuration of colour and saturation balance.
/// </summary>
/// <seealso cref="FeatureSettingsDialogue{TFeatureSettings}" />
[UsedImplicitly]
public sealed class ColourCorrectionDialogue : FeatureSettingsDialogue<ColourCorrectionSettings>
{
    /// <summary>
    /// 	Initialises a new instance of the <see cref="ColourCorrectionDialogue"/> class.
    /// </summary>
    /// <param name="capi">The capi.</param>
    /// <param name="settings">The settings.</param>
    public ColourCorrectionDialogue(ICoreClientAPI capi, ColourCorrectionSettings settings) : base(capi, settings)
    {
        ModalTransparency = 0f;
        Movable = true;
        Alignment = EnumDialogArea.CenterMiddle;
    }

    /// <summary>
    ///     Composes the header for the GUI.
    /// </summary>
    /// <param name="composer">The composer.</param>
    protected override void ComposeBody(GuiComposer composer)
    {
        const int switchSize = 30;
        const int switchPadding = 15;
        const double sliderWidth = 200.0;
        var font = CairoFont.WhiteSmallText();

        var left = ElementBounds.Fixed(0, GuiStyle.TitleBarHeight + 6, 150, switchSize);
        var right = ElementBounds.Fixed(160, GuiStyle.TitleBarHeight, switchSize, switchSize);

        var bounds = right.FlatCopy().WithFixedWidth(sliderWidth).WithFixedHeight(GuiStyle.TitleBarHeight + 2.0);
        composer.AddStaticText(T("lblEnabled"), font, left);
        composer.AddHoverText(T("lblEnabled.HoverText"), font, 260, left);
        composer.AddSwitch(OnEnabledToggle, bounds, "btnEnabled");

        left = left.BelowCopy(fixedDeltaY: switchPadding + 5);
        right = right.BelowCopy(fixedDeltaY: switchPadding + 5);

        var names = Enum.GetNames(typeof(ColourVisionType)).ToArray();
        var values = names.Select(p => T($"Presets.{p}")).ToArray();
        composer.AddStaticText(T("lblPreset"), font, left);
        composer.AddHoverText(T("lblPreset.HoverText"), font, 260, left);
        composer.AddDropDown(names, values, 0, OnSelectionChanged,  right.FlatCopy().WithFixedWidth(sliderWidth), "cbxPresets");

        left = left.BelowCopy(fixedDeltaY: switchPadding + 5);
        right = right.BelowCopy(fixedDeltaY: switchPadding + 5);

        composer.AddStaticText(T("lblRedBalance"), font, left);
        composer.AddHoverText(T("lblRedBalance.HoverText"), font, 260, left);
        composer.AddSlider(OnRedBalanceChanged, right.FlatCopy().WithFixedWidth(sliderWidth), "sliderR");

        left = left.BelowCopy(fixedDeltaY: switchPadding);
        right = right.BelowCopy(fixedDeltaY: switchPadding);

        composer.AddStaticText(T("lblGreenBalance"), font, left);
        composer.AddHoverText(T("lblGreenBalance.HoverText"), font, 260, left);
        composer.AddSlider(OnGreenBalanceChanged, right.FlatCopy().WithFixedWidth(sliderWidth), "sliderG");

        left = left.BelowCopy(fixedDeltaY: switchPadding);
        right = right.BelowCopy(fixedDeltaY: switchPadding);

        composer.AddStaticText(T("lblBlueBalance"), font, left);
        composer.AddHoverText(T("lblBlueBalance.HoverText"), font, 260, left);
        composer.AddSlider(OnBlueBalanceChanged, right.FlatCopy().WithFixedWidth(sliderWidth), "sliderB");

        left = left.BelowCopy(fixedDeltaY: switchPadding);
        right = right.BelowCopy(fixedDeltaY: switchPadding);

        composer.AddStaticText(T("lblSaturation"), font, left);
        composer.AddHoverText(T("lblSaturation.HoverText"), font, 260, left);
        composer.AddSlider(OnSaturationChanged, right.FlatCopy().WithFixedWidth(sliderWidth), "sliderS");

        left = left.BelowCopy(fixedDeltaY: switchPadding);
        composer.AddSmallButton(T("btnReset"), OnResetValues,
            left.FlatCopy().WithFixedWidth(360).WithFixedHeight(GuiStyle.TitleBarHeight + 1.0));
    }

    /// <summary>
    ///     Refreshes the displayed values on the form.
    /// </summary>
    protected override void RefreshValues()
    {
        if (!IsOpened()) return;

        SetSwitchState("btnEnabled", Settings.Enabled);
        SetColourSliderValue("sliderR", Settings.Red);
        SetColourSliderValue("sliderG", Settings.Green);
        SetColourSliderValue("sliderB", Settings.Blue);
        SetColourSliderValue("sliderS", Settings.Saturation);
        SetDropDownIndex("cbxPresets", Settings.Preset.ToString());
    }

    private void OnSelectionChanged(string code, bool selected)
    {
        Settings.Preset = (ColourVisionType)Enum.Parse(typeof(ColourVisionType), code);
        RefreshValues();
    }

    private void SetDropDownIndex(string name, string key)
    {
        SingleComposer
            .GetDropDown(name)
            .SetSelectedValue(key.IfNullOrWhitespace(ColourVisionType.Trichromacy.ToString()));
    }

    private void SetSwitchState(string name, bool state)
    {
        SingleComposer.GetSwitch(name).SetValue(state);
    }

    private void SetColourSliderValue(string name, float rawValue)
    {
        var percentageValue = (int)(rawValue * 100);
        SingleComposer.GetSlider(name).SetValues(percentageValue, 0, 200, 1, "%");
    }

    private void OnEnabledToggle(bool state)
    {
        Settings.Enabled = state;
    }

    private bool OnRedBalanceChanged(int value)
    {
        Settings.Red = value / 100f;
        return true;
    }

    private bool OnGreenBalanceChanged(int value)
    {
        Settings.Green = value / 100f;
        return true;
    }

    private bool OnBlueBalanceChanged(int value)
    {
        Settings.Blue = value / 100f;
        return true;
    }

    private bool OnSaturationChanged(int value)
    {
        Settings.Saturation = value / 100f;
        return true;
    }

    private bool OnResetValues()
    {
        Settings.Enabled = false;
        Settings.Red = 1f;
        Settings.Green = 1f;
        Settings.Blue = 1f;
        Settings.Saturation = 1f;
        Settings.Preset = ColourVisionType.Trichromacy;
        RefreshValues();
        return true;
    }
}