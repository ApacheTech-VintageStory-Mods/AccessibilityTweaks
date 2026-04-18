namespace AccessibilityTweaks.Features.AutoHotkeys.Dialogue;

/// <summary>
///     User interface for changing the settings for the Scene Brightness feature.
/// </summary>
/// <seealso cref="FeatureSettingsDialogue{TFeatureSettings}" />
public sealed class AutoHotkeysDialogue : AutomaticFeatureSettingsDialogue<AutoHotkeySettings>
{
    /// <summary>
    /// 	Initialises a new instance of the <see cref="AutoHotkeysDialogue"/> class.
    /// </summary>
    /// <param name="capi">The capi.</param>
    /// <param name="settings">The settings.</param>
    public AutoHotkeysDialogue(ICoreGantryAPI capi, AutoHotkeySettings settings) : base(capi, settings)
    {
        Alignment = EnumDialogArea.CenterMiddle;
        ModalTransparency = 0.4f;
        Movable = false;
    }
}