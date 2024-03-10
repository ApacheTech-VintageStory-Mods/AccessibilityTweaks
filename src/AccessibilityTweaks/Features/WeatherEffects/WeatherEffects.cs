﻿using ApacheTech.Common.DependencyInjection.Abstractions;
using ApacheTech.Common.DependencyInjection.Abstractions.Extensions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Dialogue;
using ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Extensions;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects;

/// <summary>
///     - Feature: Visual Tweaks<br/><br/>
///
///     Various effects within the game can be harmful, and even potentially fatal to some players.<br/>
///     This mod helps to alleviate some of these issues by allowing the player to disable various rendered effects and sound effects.<br/><br/>
/// </summary>
/// <seealso cref="ClientModSystem" />
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed class WeatherEffects : ClientModSystem, IClientServiceRegistrar
{
    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="capi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreClientAPI capi)
    {
        services.AddFeatureWorldSettings<WeatherEffectsSettings>();
        services.AddTransient<WeatherEffectsDialogue>();
    }

    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="api">The client-side API.</param>
    public override void StartClientSide(ICoreClientAPI api)
    {
        ClientSettings.RenderClouds = IOC.Services.Resolve<WeatherEffectsSettings>().CloudsEnabled;
        api.AddAccessibilityHubDialogue<WeatherEffectsDialogue>("WeatherEffects");
    }
}