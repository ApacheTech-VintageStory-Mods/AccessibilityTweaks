using ApacheTech.Common.DependencyInjection.Abstractions;
using ApacheTech.Common.DependencyInjection.Abstractions.Extensions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.SceneBrightness.Dialogue;
using ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Extensions;
using Vintagestory.API.MathTools;

// ReSharper disable StringLiteralTypo

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.SceneBrightness;

/// <summary>
///  - Feature: Scene Brightness
///  
///     Helps people with light sensitivity, and for content creators, compensating for compression rates on YouTube / Twitch.
///  
///    - Adjust the brightness level of the game scene.
/// </summary>
/// <seealso cref="ClientModSystem" />
[UsedImplicitly]
public sealed class SceneBrightness : ClientModSystem, IClientServiceRegistrar
{
    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="capi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreClientAPI capi)
    {
        services.AddFeatureWorldSettings<SceneBrightnessSettings>();
        services.AddSingleton<SceneBrightnessDialogue>();
    }
        
    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="api">The API.</param>
    public override void StartClientSide(ICoreClientAPI api)
    {
        api.AddAccessibilityHubDialogue<SceneBrightnessDialogue>("SceneBrightness");

        api.ChatCommands.Create("nv")
            .WithArgs(api.ChatCommands.Parsers.OptionalFloat("brightness"))
            .WithDescription(LangEx.FeatureString("SceneBrightness", "Description"))
            .HandleWith(ToggleSuperBright);
    }

    private static TextCommandResult ToggleSuperBright(TextCommandCallingArgs args)
    {
        var settings = IOC.Services.Resolve<SceneBrightnessSettings>();
        if (args.ArgCount == 0)
        {
            settings.Enabled = !settings.Enabled;
            var enabledMessage = LangEx.FeatureString("SceneBrightness", "Dialogue.Enabled", LangEx.BooleanString(settings.Enabled));
            return TextCommandResult.Success(enabledMessage);
        }

        var brightness = args.Parsers[0].GetValue().To<float?>() ?? 0.1f;
        settings.Brightness = GameMath.Clamp(brightness, 0f, 1f);
        var brightnessLevelMessage = LangEx.FeatureString("SceneBrightness", "Dialogue.BrightnessLevel", settings.Brightness);
        return TextCommandResult.Success(brightnessLevelMessage);
    }
}