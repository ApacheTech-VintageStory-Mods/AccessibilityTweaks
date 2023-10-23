using System.Diagnostics.CodeAnalysis;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Extensions;

/// <summary>
///     Extension methods to aid registration of hub dialogues.
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class AccessibilityHubExtensions
{
    /// <summary>
    ///     Adds an accessibility hub dialogue.
    /// </summary>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Extension Method")]
    public static void AddAccessibilityHubDialogue<T>(this ICoreClientAPI capi, string title) where T : GenericDialogue
    {
        IOC.Services.Resolve<AccessibilityHub>().FeatureDialogues
            .AddIfNotPresent(typeof(T), LangEx.FeatureString($"{title}.Dialogue", "Title"));
    }
}