using AccessibilityTweaks.Features.TextAlteration.Dialogue;
using AccessibilityTweaks.Features.AccessibilityHub.Extensions;

namespace AccessibilityTweaks.Features.TextAlteration;

/// <summary>
///     - Feature: Visual Tweaks<br/><br/>
///
///     Various effects within the game can be harmful, and even potentially fatal to some players.<br/>
///     This mod helps to alleviate some of these issues by allowing the player to disable various rendered effects and sound effects.<br/><br/>
/// </summary>
public sealed class TextAlteration : ClientModSystem<TextAlteration>, IClientServiceRegistrar
{
    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="gapi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreGantryAPI gapi)
    {
        services.AddFeatureWorldSettings<TextAlterationSettings>();
        services.AddTransient<TextAlterationDialogue>();
    }

    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="api">The client-side API.</param>
    public override void StartClientSide(ICoreClientAPI api)
    {
        api.AddAccessibilityHubDialogue<TextAlterationDialogue>("TextAlteration");
    }
}