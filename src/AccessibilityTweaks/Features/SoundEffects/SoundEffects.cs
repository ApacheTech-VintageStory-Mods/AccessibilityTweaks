using ApacheTech.Common.DependencyInjection.Abstractions;
using ApacheTech.Common.DependencyInjection.Abstractions.Extensions;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.SoundEffects.Dialogue;
using ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Extensions;
using Gantry.Services.FileSystem.Configuration;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.SoundEffects;

/// <summary>
///     - Feature: Sound Effects<br/><br/>
///
///     The sounds within the game have not been normalised, so some sounds will be very loud, compared to others, even when played at the same game volume.<br/>
///     This feature allows the user to selectively choose the volume level for each in-game sound, individually, so that any white-noise can be filtered out.<br/>
///     Very useful for sufferers of Tinnitus, or white-noise affected epilepsy.<br/><br/>
///
///         - Change the volume of every sound in the game, individually.<br/>
///         - Game sounds mute, when the game is paused.<br/>
///         - Game sounds mute, when the game window loses focus.
/// </summary>
/// <seealso cref="ClientModSystem" />
[UsedImplicitly]
public sealed class SoundEffects : ClientModSystem, IClientServiceRegistrar
{
    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="capi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreClientAPI capi)
    {
        services.AddFeatureWorldSettings<SoundEffectsSettings>();
        services.AddTransient<SoundEffectsDialogue>();
    }

    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="capi">The game's core client API.</param>
    public override void StartClientSide(ICoreClientAPI capi)
    {
        UpdateVolumeOverrideSettings();
        capi.AddAccessibilityHubDialogue<SoundEffectsDialogue>("SoundEffects");
    }

    private static void UpdateVolumeOverrideSettings()
    {
        var settings = IOC.Services.Resolve<SoundEffectsSettings>();
        var defaults = ApiEx.Client!.Assets.AllAssets
            .Where(p => p.Key.Category == AssetCategory.sounds || p.Key.Category == AssetCategory.music)
            .Where(p => p.Key.Path.EndsWith(".ogg"));
        foreach (var entry in defaults)
        {
            var path = entry.Key.ToString();
            settings.SoundAssets.AddIfNotPresent(path, new VolumeOverrideModel { Path = path });
        }
        ModSettings.World.Save(settings);
    }
}