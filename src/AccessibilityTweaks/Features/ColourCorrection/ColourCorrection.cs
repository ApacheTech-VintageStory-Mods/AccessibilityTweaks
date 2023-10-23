using ApacheTech.VintageMods.AccessibilityTweaks.Features.ColourCorrection.Dialogue;
using ApacheTech.VintageMods.AccessibilityTweaks.Features.ColourCorrection.Shaders;
using ApacheTech.VintageMods.AccessibilityTweaks.Services.AccessibilityHub.Extensions;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.ColourCorrection;

/// <summary>
///  - Feature: Colour Correction
///  
///     Helps people with poor vision, light sensitivity, or colour-blindness.Playing around with the colour settings can give everything a warm, or cool hue.
///  
///     - Choose from different presets to simulate different colour vision deficiencies.
///     - Adjust the colour balance, and saturation of the game scene.
/// </summary>
/// <seealso cref="ClientModSystem" />
[UsedImplicitly]
public sealed class ColourCorrection : ClientModSystem, IClientServiceRegistrar
{
    private ICoreClientAPI _capi;
    private ColourCorrectionRenderer _renderer;

    /// <summary>
    ///     Allows a mod to include Singleton, or Transient services to the IOC Container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="capi">The game's client-side API.</param>
    public void ConfigureClientModServices(IServiceCollection services, ICoreClientAPI capi)
    {
        services.AddFeatureWorldSettings<ColourCorrectionSettings>();
        services.AddSingleton<ColourCorrectionRenderer>();
        services.AddTransient<ColourCorrectionDialogue>();
    }

    /// <summary>
    ///     Minor convenience method to save yourself the check for/cast to ICoreClientAPI in Start()
    /// </summary>
    /// <param name="capi">The core client-side API.</param>
    public override void StartClientSide(ICoreClientAPI capi)
    {
        (_capi = capi).AddAccessibilityHubDialogue<ColourCorrectionDialogue>("ColourCorrection");
        _renderer = IOC.Services.Resolve<ColourCorrectionRenderer>();
        _capi.Event.RegisterRenderer(_renderer, EnumRenderStage.AfterFinalComposition);
        _capi.Event.ReloadShader += LoadShader;
        LoadShader();
    }

    private bool LoadShader()
    {
        var shader = IOC.Services.CreateInstance<ColourCorrectionShaderProgram>();
        _capi.Shader.RegisterFileShaderProgram(shader.PassName, shader);
        shader.Compile();
        _renderer.Shader = shader;
        return true;
    }

    /// <summary>
    ///     If this mod allows runtime reloading, you must implement this method to unregister any listeners / handlers
    /// </summary>
    public override void Dispose()
    {
        if (_capi is null) return;
        _capi.Event.ReloadShader -= LoadShader;
        _capi.Event.UnregisterRenderer(_renderer, EnumRenderStage.AfterFinalComposition);
        _capi.Shader.ReloadShadersThreadSafe();
        _renderer?.Dispose();
        base.Dispose();
    }
}