using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Dialogue.Components;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.CameraMovement.Dialogue;

/// <summary>
/// 
/// </summary>
/// <seealso cref="SingleComponentDialogue{CameraMovementGuiComponent}" />
[UsedImplicitly]
public class CameraMovementDialogue : SingleComponentDialogue<CameraMovementGuiComponent>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="CameraMovementDialogue"/> class.
    /// </summary>
    /// <param name="capi">The capi.</param>
    public CameraMovementDialogue(ICoreClientAPI capi) : base(capi)
    {
        Title = LangEx.FeatureString("CameraMovement.Dialogue", "Title");
        Alignment = EnumDialogArea.CenterMiddle;
        Modal = true;
        ModalTransparency = 0.3f;
    }
}