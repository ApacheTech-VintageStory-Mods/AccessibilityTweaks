using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.WeatherEffects.Patches;

[SettingsConsumer(EnumAppSide.Client)]
[HarmonySidedPatch(EnumAppSide.Client)]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed partial class WeatherEffectsPatches : WorldSettingsConsumer<WeatherEffectsSettings>
{

}