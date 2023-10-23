using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Dialogue.Components;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Dialogue;

/// <summary>
///     A GUI dialogue for displaying Weather Effects settings to the user.
/// </summary>
/// <seealso cref="IGuiComposablePart" />
[UsedImplicitly]
public class WeatherEffectsDialogue : SingleComponentDialogue<WeatherEffectsGuiComponent>
{
    /// <summary>
    ///     Initialises a new instance of the <see cref="WeatherEffectsDialogue"/> class.
    /// </summary>
    /// <param name="capi">The client API.</param>
    public WeatherEffectsDialogue(ICoreClientAPI capi) : base(capi)
    {
        Title = LangEx.FeatureString("WeatherEffects.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}