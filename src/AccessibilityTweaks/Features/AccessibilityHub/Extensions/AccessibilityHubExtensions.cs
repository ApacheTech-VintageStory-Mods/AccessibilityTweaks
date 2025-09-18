namespace AccessibilityTweaks.Features.AccessibilityHub.Extensions;

/// <summary>
///     Extension methods to aid registration of hub dialogues.
/// </summary>
public static class AccessibilityHubExtensions
{
    /// <summary>
    ///     Adds an accessibility hub dialogue.
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Extension Method")]
    public static void AddAccessibilityHubDialogue<T>(this ICoreClientAPI capi, string title) where T : GenericDialogue
    {
        G.Services.Resolve<AccessibilityHub>().FeatureDialogues
            .AddIfNotPresent(typeof(T), G.T($"{title}.Dialogue", "Title"));
    }
}