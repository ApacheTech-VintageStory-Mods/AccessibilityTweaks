using AccessibilityTweaks.Features.TextAlteration.Dialogue.Components;

namespace AccessibilityTweaks.Features.TextAlteration.Dialogue;

/// <summary>
///     Dialogue window to change the Text Alteration settings.
/// </summary>
/// <seealso cref="SingleComponentDialogue{TextAlterationGuiComponent}" />
public class TextAlterationDialogue : SingleComponentDialogue<TextAlterationGuiComponent>
{
    /// <summary>
    ///     Initialises a new instance of the <see cref="TextAlterationDialogue"/> class.
    /// </summary>
    /// <param name="gapi">The client API.</param>
    public TextAlterationDialogue(ICoreGantryAPI gapi) : base(gapi)
    {
        Title = G.Lang.Translate("TextAlteration.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}