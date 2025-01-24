using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed partial class WeatherEffectsPatches : WorldSettingsConsumer<WeatherEffectsSettings>;