﻿using ApacheTech.Common.DependencyInjection.Abstractions;
using Gantry.Services.HarmonyPatches.Hosting;

namespace ApacheTech.VintageMods.AccessibilityTweaks;

/// <summary>
///     Entry-point for the mod. This class will configure and build the IOC Container, and Service list for the rest of the mod.
///     
///     Registrations performed within this class should be global scope; by convention, features should aim to be as stand-alone as they can be.
/// </summary>
/// <remarks>
///     Only one derived instance of this class should be added to any single mod within
///     the VintageMods domain. This class will enable Dependency Injection, and add all
///     the domain services. Derived instances should only have minimal functionality, 
///     instantiating, and adding Application specific services to the IOC Container.
/// </remarks>
/// <seealso cref="ModHost" />
[UsedImplicitly]
public sealed class Program : ModHost
{
    /// <summary>
    ///     Configures any services that need to be added to the IO Container, on the client side.
    /// </summary>
    /// <param name="services">The as-of-yet un-built services container.</param>
    /// <param name="capi">The game's client-side API.</param>
    protected override void ConfigureClientModServices(IServiceCollection services, ICoreClientAPI capi)
    {
        services.AddFileSystemService(capi, o => o.RegisterSettingsFiles = true);
        services.AddHarmonyPatchingService(capi, o => o.AutoPatchModAssembly = true);
    }
}