using AccessibilityTweaks.Features.CameraMovement.Dialogue;

namespace AccessibilityTweaks.Features.CameraMovement;

/// <summary>
///     - Feature: Visual Tweaks<br/><br/>
///
///     Various effects within the game can be harmful, and even potentially fatal to some players.<br/>
///     This mod helps to alleviate some of these issues by allowing the player to disable various rendered effects and sound effects.<br/><br/>
/// </summary>
public sealed class CameraMovement : ClientModSystem<CameraMovement>, IClientServiceRegistrar
{
    /// <inheritdoc />
    public void ConfigureClientModServices(IServiceCollection services, ICoreGantryAPI gapi)
    {
        services.AddFeatureWorldSettings<CameraMovementSettings>();
        services.AddTransient<CameraMovementDialogue>();
    }

    /// <inheritdoc />
    public override void StartClientSide(ICoreClientAPI api)
    {
        api.AddAccessibilityHubDialogue<CameraMovementDialogue>("CameraMovement");
    }
}