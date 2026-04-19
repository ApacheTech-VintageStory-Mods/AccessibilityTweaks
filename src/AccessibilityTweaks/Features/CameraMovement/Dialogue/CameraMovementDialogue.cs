namespace AccessibilityTweaks.Features.CameraMovement.Dialogue;

/// <summary>
/// 
/// </summary>
/// <seealso cref="SingleComponentDialogue{CameraMovementGuiComponent}" />
public class CameraMovementDialogue : FeatureSettingsDialogue<CameraMovementSettings>
{
    /// <summary>
    ///     Initialises a new instance of the <see cref="CameraMovementDialogue"/> class.
    /// </summary>
    /// <param name="gapi">The capi.</param>
    public CameraMovementDialogue(ICoreGantryAPI gapi, CameraMovementSettings settings) : base(gapi, settings)
    {
        Title = G.Lang.Translate("CameraMovement.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }

    /// <summary>
    ///     Uses the injected <see cref="GuiComposer" /> to compose part of a GUI.
    /// </summary>
    /// <param name="composer"></param>
    /// <returns></returns>
    protected sealed override void ComposeBody(GuiComposer composer)
    {
        const int gapBetweenRows = 20;
        var labelFont = CairoFont.WhiteSmallText();
        var leftWidth = Math.Max(250, GetMaxWidth(labelFont, FeatureName,
        [
            nameof(Settings.PerceptionWarpMultiplier),
            nameof(Settings.GlitchEffectStrengthMultiplier),
            nameof(Settings.PsychedelicStrengthMultiplier),
            nameof(Settings.InvoluntaryMouseMovement)
        ]));

        //
        // Perception Warp Multiplier
        //

        var left = ElementBounds.Fixed(0, GuiStyle.TitleBarHeight, leftWidth, 20);
        var right = ElementBounds.Fixed(leftWidth + 10, GuiStyle.TitleBarHeight, 270, 20);

        composer
            .AddStaticText(T("lblPerceptionWarpMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblPerceptionWarpMultiplier.HoverText"), labelFont, (int)leftWidth, left)
            .AddSlider<float>(OnPerceptionWarpMultiplierChanged, right, "sldPerceptionWarpMultiplier");

        //
        // Glitch Effect Strength Multiplier
        //

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(T("lblGlitchEffectStrengthMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblGlitchEffectStrengthMultiplier.HoverText"), labelFont, (int)leftWidth, left)
            .AddSlider<float>(OnGlitchEffectStrengthMultiplierChanged, right, "sldGlitchEffectStrengthMultiplier");

        //
        // Psychedelic Strength Multiplier
        //

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(T("lblPsychedelicStrengthMultiplier"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblPsychedelicStrengthMultiplier.HoverText"), labelFont, (int)leftWidth, left)
            .AddSlider<float>(OnPsychedelicStrengthMultiplierChanged, right, "sldPsychedelicStrengthMultiplier");

        //
        // Involuntary Mouse Movement
        //

        left = left.BelowCopy(fixedDeltaY: gapBetweenRows + 5);
        right = right.BelowCopy(fixedDeltaY: gapBetweenRows);

        composer
            .AddStaticText(T("lblInvoluntaryMouseMovement"), labelFont, EnumTextOrientation.Right, left)
            .AddHoverText(T("lblInvoluntaryMouseMovement.HoverText"), labelFont, (int)leftWidth, left)
            .AddSwitch(OnInvoluntaryMouseMovementToggle, right, "btnInvoluntaryMouseMovement");
    }

    private bool OnPerceptionWarpMultiplierChanged(float value)
    {
        Settings.PerceptionWarpMultiplier = value;
        return true;
    }

    private bool OnGlitchEffectStrengthMultiplierChanged(float value)
    {
        Settings.GlitchEffectStrengthMultiplier = value;
        return true;
    }

    private bool OnPsychedelicStrengthMultiplierChanged(float value)
    {
        Settings.PsychedelicStrengthMultiplier = value;
        return true;
    }

    private void OnInvoluntaryMouseMovementToggle(bool state)
    {
        Settings.InvoluntaryMouseMovement = state;
    }

    protected override void RefreshValues()
    {
        var sldPerceptionWarpMultiplier = SingleComposer.GetSlider<float>("sldPerceptionWarpMultiplier");
        sldPerceptionWarpMultiplier.SetValues(Settings.PerceptionWarpMultiplier, 0.0f, 10f, 0.05f, 2, "x");

        var sldGlitchEffectStrengthMultiplier = SingleComposer.GetSlider<float>("sldGlitchEffectStrengthMultiplier");
        sldGlitchEffectStrengthMultiplier.SetValues(Settings.GlitchEffectStrengthMultiplier, 0.0f, 10f, 0.05f, 2, "x");

        var sldPsychedelicStrengthMultiplier = SingleComposer.GetSlider<float>("sldPsychedelicStrengthMultiplier");
        sldPsychedelicStrengthMultiplier.SetValues(Settings.PsychedelicStrengthMultiplier, 0.0f, 10f, 0.05f, 2, "x");

        SingleComposer.GetSwitch("btnInvoluntaryMouseMovement").SetValue(Settings.InvoluntaryMouseMovement);
    }
}