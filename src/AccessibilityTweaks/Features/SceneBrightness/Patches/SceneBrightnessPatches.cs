using Gantry.Services.FileSystem.Configuration.Consumers;

namespace ApacheTech.VintageMods.AccessibilityTweaks.Features.SceneBrightness.Patches;

/// <summary>
///     Harmony Patches for the <see cref="ClientGameCalendar"/> class. This class cannot be inherited.
/// </summary>
/// <seealso cref="SceneBrightnessSettings" />
[HarmonyClientSidePatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public sealed partial class SceneBrightnessPatches : WorldSettingsConsumer<SceneBrightnessSettings>;