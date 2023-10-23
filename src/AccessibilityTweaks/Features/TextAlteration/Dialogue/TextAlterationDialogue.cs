using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Dialogue.Components;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.TextAlteration.Dialogue;

/// <summary>
///     Dialogue window to change the Text Alteration settings.
/// </summary>
/// <seealso cref="SingleComponentDialogue{TextAlterationGuiComponent}" />
[UsedImplicitly]
public class TextAlterationDialogue : SingleComponentDialogue<TextAlterationGuiComponent>
{
    /// <summary>
    ///     Initialises a new instance of the <see cref="TextAlterationDialogue"/> class.
    /// </summary>
    /// <param name="capi">The client API.</param>
    public TextAlterationDialogue(ICoreClientAPI capi) : base(capi)
    {
        Title = LangEx.FeatureString("TextAlteration.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}