using ApacheTech.Common.Extensions.Harmony;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Extensions;

/// <summary>
///     Extensions methods that expose internal methods for GUI Elements.
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class GuiElementExtensions
{
    /// <summary>
    ///     Only triggers the callback methods when the user releases the mouse.
    /// </summary>
    /// <param name="slider">The slider.</param>
    /// <param name="value">if set to <c>true</c> the callback action will only be called when the user releases the mouse.</param>
    public static void TriggerOnlyOnMouseUp(this GuiElementSlider slider, bool value)
    {
        slider.CallMethod("TriggerOnlyOnMouseUp", value);
    }

    /// <summary>
    ///     Composes the hover-text element associated with the slider.
    /// </summary>
    /// <param name="slider">The slider.</param>
    public static void ComposeHoverTextElement(this GuiElementSlider slider)
    {
        slider.CallMethod("ComposeHoverTextElement");
    }
}