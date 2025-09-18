using AccessibilityTweaks.Features.AccessibilityHub.Dialogue;

namespace AccessibilityTweaks.Features.AccessibilityHub;

/// <summary>
///     Provides a main GUI for the mod, as a central location to access each feature; rather than through commands.
/// </summary>
public sealed class AccessibilityHub : ClientModSystem<AccessibilityHub>, IClientServiceRegistrar
{
    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="gapi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreGantryAPI gapi)
    {
        services.AddTransient<AccessibilityHubDialogue>();
        services.AddTransient<SupportDialogue>();
    }

    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="api">The game's core client API.</param>
    public override void StartClientSide(ICoreClientAPI api)
    {
        api.Input.RegisterTransientGuiDialogueHotKey(
            G.Services.Resolve<AccessibilityHubDialogue>, G.Lang.ModTitle(), GlKeys.F8);
    }

    /// <summary>
    ///     The dialogue windows, from features within this mod, that will be displayed within the menu.
    /// </summary>
    public Dictionary<Type, string> FeatureDialogues { get; } = [];
}