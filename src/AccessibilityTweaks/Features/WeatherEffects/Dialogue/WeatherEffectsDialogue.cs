using AccessibilityTweaks.Features.WeatherEffects.Dialogue.Components;

namespace AccessibilityTweaks.Features.WeatherEffects.Dialogue;

/// <summary>
///     A GUI dialogue for displaying Weather Effects settings to the user.
/// </summary>
/// <seealso cref="IGuiComposablePart" />
public class WeatherEffectsDialogue : SingleComponentDialogue<WeatherEffectsGuiComponent>
{
    /// <summary>
    ///     Initialises a new instance of the <see cref="WeatherEffectsDialogue"/> class.
    /// </summary>
    /// <param name="gapi">The client API.</param>
    public WeatherEffectsDialogue(ICoreGantryAPI gapi) : base(gapi)
    {
        Title = G.Lang.Translate("WeatherEffects.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}