using AccessibilityTweaks.Features.CameraMovement.Dialogue.Components;

namespace AccessibilityTweaks.Features.CameraMovement.Dialogue;

/// <summary>
/// 
/// </summary>
/// <seealso cref="SingleComponentDialogue{CameraMovementGuiComponent}" />
public class CameraMovementDialogue : SingleComponentDialogue<CameraMovementGuiComponent>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CameraMovementDialogue"/> class.
    /// </summary>
    /// <param name="gapi">The capi.</param>
    public CameraMovementDialogue(ICoreGantryAPI gapi) : base(gapi)
    {
        Title = G.Lang.Translate("CameraMovement.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}