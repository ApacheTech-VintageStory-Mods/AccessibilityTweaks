using ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Abstractions;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Core.GameContent.Gui.Extensions;

/// <summary>
///     Extension methods to aid the composition of partial GUI components.
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class GuiComposerExtensions
{
    /// <summary>
    ///     Adds a composable part to a GUI composer.
    /// </summary>
    /// <param name="composer">The composer.</param>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public static GuiComposer AddComposablePart(this GuiComposer composer, IGuiComposablePart content) 
        => content.ComposePart(composer);
}